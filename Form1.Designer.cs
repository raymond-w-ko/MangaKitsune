namespace MangaKitsune {
  partial class MangaKitsuneForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.textBoxManga_ = new System.Windows.Forms.TextBox();
      this.imageContainerPanel_ = new System.Windows.Forms.Panel();
      this.pictureBox1_ = new System.Windows.Forms.PictureBox();
      this.navigationContainerPanel_ = new System.Windows.Forms.Panel();
      this.chaptersComboBox_ = new System.Windows.Forms.ComboBox();
      this.imageContainerPanel_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_)).BeginInit();
      this.navigationContainerPanel_.SuspendLayout();
      this.SuspendLayout();
      // 
      // textBoxManga_
      // 
      this.textBoxManga_.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.textBoxManga_.Dock = System.Windows.Forms.DockStyle.Top;
      this.textBoxManga_.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxManga_.Location = new System.Drawing.Point(0, 0);
      this.textBoxManga_.Name = "textBoxManga_";
      this.textBoxManga_.Size = new System.Drawing.Size(256, 29);
      this.textBoxManga_.TabIndex = 1;
      this.textBoxManga_.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxManga__KeyDown);
      // 
      // imageContainerPanel_
      // 
      this.imageContainerPanel_.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.imageContainerPanel_.BackColor = System.Drawing.Color.Black;
      this.imageContainerPanel_.Controls.Add(this.pictureBox1_);
      this.imageContainerPanel_.Location = new System.Drawing.Point(262, 0);
      this.imageContainerPanel_.Name = "imageContainerPanel_";
      this.imageContainerPanel_.Size = new System.Drawing.Size(754, 741);
      this.imageContainerPanel_.TabIndex = 2;
      // 
      // pictureBox1_
      // 
      this.pictureBox1_.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1_.Name = "pictureBox1_";
      this.pictureBox1_.Size = new System.Drawing.Size(1, 1);
      this.pictureBox1_.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1_.TabIndex = 0;
      this.pictureBox1_.TabStop = false;
      this.pictureBox1_.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1__MouseMove);
      this.pictureBox1_.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1__MouseDown);
      // 
      // navigationContainerPanel_
      // 
      this.navigationContainerPanel_.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.navigationContainerPanel_.Controls.Add(this.chaptersComboBox_);
      this.navigationContainerPanel_.Controls.Add(this.textBoxManga_);
      this.navigationContainerPanel_.Location = new System.Drawing.Point(0, 0);
      this.navigationContainerPanel_.Name = "navigationContainerPanel_";
      this.navigationContainerPanel_.Size = new System.Drawing.Size(256, 741);
      this.navigationContainerPanel_.TabIndex = 3;
      // 
      // chaptersComboBox_
      // 
      this.chaptersComboBox_.Dock = System.Windows.Forms.DockStyle.Top;
      this.chaptersComboBox_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.chaptersComboBox_.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chaptersComboBox_.FormattingEnabled = true;
      this.chaptersComboBox_.Location = new System.Drawing.Point(0, 29);
      this.chaptersComboBox_.Name = "chaptersComboBox_";
      this.chaptersComboBox_.Size = new System.Drawing.Size(256, 29);
      this.chaptersComboBox_.TabIndex = 2;
      this.chaptersComboBox_.TabStop = false;
      this.chaptersComboBox_.SelectedIndexChanged += new System.EventHandler(this.chaptersComboBox__SelectedIndexChanged);
      // 
      // MangaKitsuneForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1016, 741);
      this.Controls.Add(this.navigationContainerPanel_);
      this.Controls.Add(this.imageContainerPanel_);
      this.Name = "MangaKitsuneForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Manga Kitsune";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.MangaKitsune_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MangaKitsuneForm_FormClosing);
      this.imageContainerPanel_.ResumeLayout(false);
      this.imageContainerPanel_.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_)).EndInit();
      this.navigationContainerPanel_.ResumeLayout(false);
      this.navigationContainerPanel_.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox textBoxManga_;
    private System.Windows.Forms.Panel imageContainerPanel_;
    private System.Windows.Forms.PictureBox pictureBox1_;
    private System.Windows.Forms.Panel navigationContainerPanel_;
    private System.Windows.Forms.ComboBox chaptersComboBox_;
  }
}

