using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

namespace MangaKitsune.Internet {
class HTTP {
  private string user_agent_string_;
  private string last_fetched_url_;
  
  public HTTP() {
    last_fetched_url_ = "http://www.mangafox.com";
    user_agent_string_ = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.204 Safari/534.16";
  }
  
  /// <summary>
  /// Returns the content of a given web adress as string.
  /// </summary>
  /// <param name="Url">URL of the webpage</param>
  /// <returns>Website content</returns>
  public string DownloadWebPage(string url) {
    // Open a connection
    HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(url);
 
    // You can also specify additional header values like 
    // the user agent or the referer:
    WebRequestObject.UserAgent	= user_agent_string_;
    WebRequestObject.Referer = last_fetched_url_;
 
    // Request response:
    WebResponse Response = WebRequestObject.GetResponse();
 
    // Open data stream:
    Stream WebStream = Response.GetResponseStream();
 
    // Create reader object:
    StreamReader Reader = new StreamReader(WebStream);
 
    // Read the entire stream content:
    string PageContent = Reader.ReadToEnd();
 
    // Cleanup
    Reader.Close();
    WebStream.Close();
    Response.Close();
    
    // Store url the be used as referer
    last_fetched_url_ = url;
 
    return PageContent;
  }
  
  public Image DownloadImage(string url) {
    // Create the requests.
    WebRequest requestPic = WebRequest.Create(url);
    WebResponse responsePic = requestPic.GetResponse();
    Image webImage = Image.FromStream(responsePic.GetResponseStream());
    
    return webImage;
  }
}
}