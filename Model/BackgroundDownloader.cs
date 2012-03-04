using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using MangaKitsune.Internet;
using HtmlAgilityPack;

namespace MangaKitsune.Model {
public class BackgroundDownloader {
  private string start_url_;
  private string current_page_url_;
  private string next_page_url_;
  private List<Image> storage_;
  
  private HTTP http_;
  
  private Thread thread_;
  private volatile bool thread_stop_;
  
  public BackgroundDownloader(string start_url, List<Image> storage) {
    start_url_ = start_url;
    next_page_url_ = start_url_;
    current_page_url_ = null;
    storage_ = storage;
    
    http_ = new HTTP();
    
    thread_ = new Thread(new ThreadStart(thread_Downloader));
    thread_stop_ = false;
  }
  
  public void Start() {
    thread_.Start();
  }
  
  public void Stop() {
    thread_stop_ = true;
    while (thread_.IsAlive) ;
  }
  
  private void thread_Downloader() {
    while (!thread_stop_) {
      if (next_page_url_ == null) return;
      Image img = ExtractImage(next_page_url_);
      if (img == null) return;
      try {
        Monitor.Enter(storage_);
        storage_.Add(img);
      }
      catch (Exception) {
        throw;
      }
      finally {
        Monitor.Exit(storage_);
      }
      
      // Be kind to the kitsune
      Thread.Sleep(500);
    }
  }
  
    private Image ExtractImage(string url) {
    // Parse HTML
    string webpage = http_.DownloadWebPage(url);
    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
    doc.LoadHtml(webpage);
    HtmlNodeCollection candidates = doc.DocumentNode.SelectNodes("//img");

    // Find image URL
    string imageURL = "";
    foreach (HtmlNode node in candidates) {
      if (!node.Attributes.Contains("id") || node.Attributes["id"].Value != "image") continue;

      imageURL = node.Attributes["src"].Value;
    }

    // Download image
    Image img = http_.DownloadImage(imageURL);

    // Calculate next page
    candidates = doc.DocumentNode.SelectNodes("//a");
    string nextURL = "";
    foreach (HtmlNode node in candidates) {
      if (!node.Attributes.Contains("onclick") || node.Attributes["onclick"].Value != "return enlarge();") continue;
      nextURL = node.Attributes["href"].Value;
    }

    if (nextURL == "javascript:void(0);") {
      next_page_url_ = null;
    }
    else {
      // Find last '/' for next URL
      int index = -1;
      for (index = url.Length - 1; index >= 0; --index) {
        if (url[index] == '/') break;
      }
      next_page_url_ = url.Substring(0, index + 1) + nextURL;
    }

    // Save current page
    current_page_url_ = url;
    return img;
  }

}
}
