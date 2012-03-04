using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MangaKitsune.Internet;
using System.Drawing;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;

namespace MangaKitsune.Model {
public class Manga {
  private string website_ = "http://www.mangafox.com";
  private string url_prefix_ = "http://www.mangafox.com/manga/";
  private string main_page_suffix_ = "?no_warning=1";
  private string name_;
  private List<string> chapter_suffixes_;
  private List<string> simple_chapter_suffixes_;
  private Dictionary<string, string> simple_to_full_chapter_;
  
  private string current_chapter_;
  private int current_page_index_;
  
  private HTTP http_;
  
  private Dictionary<string, List<Image>> chapter_to_images_;
  private Dictionary<string, BackgroundDownloader> chapter_to_background_;
  
  public Manga(string name) {
    name_ = name;
    current_chapter_ = "";
    current_page_index_ = -1;
    
    chapter_suffixes_ = new List<string>();
    simple_chapter_suffixes_ = new List<string>();
    simple_to_full_chapter_ = new Dictionary<string,string>();
    chapter_to_images_ = new Dictionary<string, List<Image>>();
    chapter_to_background_ = new Dictionary<string,BackgroundDownloader>();
    
    http_ = new HTTP();
    
    string main_page_url = url_prefix_ + name_ + "/" + main_page_suffix_;
    string main_page = http_.DownloadWebPage(main_page_url);
    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
    doc.LoadHtml(main_page);
    HtmlNode table = doc.GetElementbyId("listing");
    
    foreach (HtmlNode row in table.SelectNodes("tr")) {
      HtmlNode chapter_row = row.SelectSingleNode("td[1]");
      if (chapter_row == null) continue;
      HtmlNode link_node = chapter_row.SelectSingleNode("a[@class='ch']");
      if (link_node == null) continue;
      string link = link_node.Attributes["href"].Value;
      chapter_suffixes_.Add(link);
    }
    
    // Create list of simplified chapter suffixes
    foreach (string chapter in chapter_suffixes_) {
      Match m = Regex.Match(chapter, "/(c(.*))/");
      string simple = m.Groups[2].Value;
      
      simple_chapter_suffixes_.Add(simple);
      
      simple_to_full_chapter_[simple] = chapter;
    }
    
    MangaKitsuneForm.Inst.ReplaceChapterList(simple_chapter_suffixes_);
  }
  
  public void StopPreloading() {
    foreach (BackgroundDownloader bd in chapter_to_background_.Values) {
      bd.Stop();
    }
  }
  
  public void ChangeToChapter(string chapter) {
    chapter = simple_to_full_chapter_[chapter];
    
    if (chapter == current_chapter_) return;
    current_chapter_ = chapter;
    
    string url = website_ + current_chapter_;
    
    if (!chapter_to_background_.ContainsKey(chapter)) {
      List<Image> new_storage = new List<Image>();
      chapter_to_images_[chapter] = new_storage;
      BackgroundDownloader bd = new BackgroundDownloader(url, new_storage);
      chapter_to_background_[chapter] = bd;
      bd.Start();
    }
    
    current_page_index_ = 0;
    
    List<Image> storage = chapter_to_images_[current_chapter_];
    Image img = null;
    while (img == null) {
      try {
        Monitor.Enter(storage);
        if (storage.Count == 0) continue;
        else img = storage[current_page_index_];
      }
      finally {
        Monitor.Exit(storage);
      }
    }
    
    MangaKitsuneForm.Inst.DrawImage(img);
  }
  
  public Image GetNextPage() {
    if (current_chapter_ == null || current_chapter_.Length == 0) return null;
    List<Image> storage = chapter_to_images_[current_chapter_];
    try {
      Monitor.Enter(storage);
      if (storage.Count <= current_page_index_ + 1) return null;
      return storage[++current_page_index_];
    }
    finally {
      Monitor.Exit(storage);
    }
  }
  
  public Image GetPrevPage() {
    if (current_chapter_ == null || current_chapter_.Length == 0) return null;
    
    List<Image> storage = chapter_to_images_[current_chapter_];
    try {
      Monitor.Enter(storage);
      if (current_page_index_ - 1 < 0) return null;
      if (storage.Count <= current_page_index_ - 1) return null;
      return storage[--current_page_index_];
    }
    finally {
      Monitor.Exit(storage);
    }
  }

}
}
