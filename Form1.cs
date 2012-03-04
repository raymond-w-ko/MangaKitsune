using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MangaKitsune.Internet;
using HtmlAgilityPack;
using System.Threading;
using MangaKitsune.Model;
using System.Diagnostics;
using System.Net;

namespace MangaKitsune {
public partial class MangaKitsuneForm : Form {
  public static MangaKitsuneForm Inst = null;
  private Manga manga_;
  
  public MangaKitsuneForm() {
    InitializeComponent();
    
    if (MangaKitsuneForm.Inst != null) {
      throw new ApplicationException("Only one instance of MangaKistuneForm is allowed");
    }
    MangaKitsuneForm.Inst = this;

    manga_ = null;
  }
  
  #region Public Methods
  
  public void ReplaceChapterList(List<string> chapters) {
    chaptersComboBox_.Items.Clear();
    chaptersComboBox_.BeginUpdate();
    chaptersComboBox_.Items.AddRange(chapters.ToArray());
    chaptersComboBox_.EndUpdate();
    chaptersComboBox_.SelectedIndex = 0;
  }
  
  public void DrawImage(Image img) {
    pictureBox1_.Image = img;
    centerImage();
  }
  
  #endregion

  private void MangaKitsune_Load(object sender, EventArgs e) {
    this.textBoxManga_.MouseWheel += new MouseEventHandler(mouseWheelHandler);
    this.chaptersComboBox_.MouseWheel += new MouseEventHandler(mouseWheelHandler);
    
    textBoxManga_.AutoCompleteSource = AutoCompleteSource.CustomSource;
    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
    foreach (object o in Properties.Settings.Default.MangaList) {
      string name = (string)o;
      data.Add(name);
    }
    textBoxManga_.AutoCompleteCustomSource = data;
  }
  
  private void mouseWheelHandler(object sender, MouseEventArgs e) {
    imageContainerPanel_.Select();
  }
  
  private void textBoxManga__KeyDown(object sender, KeyEventArgs e) {
    switch (e.KeyCode) {
    case Keys.Enter: {
      if (manga_ != null) manga_.StopPreloading();
      manga_ = null;
      try {
        manga_ = new Manga(textBoxManga_.Text);
      }
      catch (WebException ex) {
        if (ex.ToString().Contains("(404)")) {
          MessageBox.Show("Specified manga does not exist: " + textBoxManga_.Text);
        }
        else {
          MessageBox.Show(ex.ToString());
        }
      }
      
      if (!Properties.Settings.Default.MangaList.Contains(textBoxManga_.Text)) {
        Properties.Settings.Default.MangaList.Add(textBoxManga_.Text);
      }
      if (!textBoxManga_.AutoCompleteCustomSource.Contains(textBoxManga_.Text)) {
        textBoxManga_.AutoCompleteCustomSource.Add(textBoxManga_.Text);
      }
      }
      
      break;
      
    // Try autocomplete stuff
    default: {
      ;
      }
      
      break;
    }
  }
  
  #region Picture Box
  
  private void pictureBox1__MouseDown(object sender, MouseEventArgs e) {
    if (e.Button == MouseButtons.Left) {
      Image img = manga_.GetNextPage();
      if (img == null) return;
      pictureBox1_.Image = img;
      centerImage();
    }
    else if (e.Button == MouseButtons.Middle) {
      Image img = manga_.GetPrevPage();
      if (img == null) return;
      pictureBox1_.Image = img;
      centerImage();
    }
    else if (e.Button == MouseButtons.Right) {
      ;
    }
  }
  private void centerImage() {
    int panel_width = imageContainerPanel_.Width;
    
    Point new_loc = new Point();
    
    new_loc.X = panel_width / 2 - pictureBox1_.Image.Width / 2;
    new_loc.Y = 0;
    
    pictureBox1_.Location = new_loc;
  }
  
  Point mouse_location_ = new Point(-1, -1);
  int lastY_ = 0;
  
  private void pictureBox1__MouseMove(object sender, MouseEventArgs e) {
    if (manga_ == null) return;
    if (pictureBox1_.Image == null) return;
    if (mouse_location_.X == -1 && mouse_location_.Y == -1) {
      mouse_location_ = Control.MousePosition;
      return;
    }
    
    int newY = lastY_;
    if (Control.MousePosition.Y > mouse_location_.Y) newY += 1;
    if (Control.MousePosition.Y < mouse_location_.Y) newY -= 1;
    
    if (newY > 5) newY = 5;
    int max = pictureBox1_.Image.Height - imageContainerPanel_.Height  + 5;
    if (max > 0 && newY < -max) newY = -max;
    if (max < 0 && newY < 5) newY = 5;
    
    lastY_ = newY;
    
    pictureBox1_.Location = new Point(
        pictureBox1_.Location.X,
        newY);
        
    mouse_location_ = Control.MousePosition;
  }
  
  #endregion
  
  private void MangaKitsuneForm_FormClosing(object sender, FormClosingEventArgs e) {
    Properties.Settings.Default.Save();
    
    if (manga_ == null) return;
    manga_.StopPreloading();
  } 

  private void chaptersComboBox__SelectedIndexChanged(object sender, EventArgs e) {
    if (manga_ == null) return;
    string chapter = chaptersComboBox_.Items[chaptersComboBox_.SelectedIndex].ToString();
    pictureBox1_.Image = Properties.Resources.Loading;
    centerImage();
    pictureBox1_.Refresh();
    manga_.ChangeToChapter(chapter);
    pictureBox1_.Select();
  }

}
}
