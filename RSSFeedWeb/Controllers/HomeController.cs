using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

namespace RSSFeedWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult RSSFeed()
        {
            var blogs = new List<BlogVM>() {
                new BlogVM() {
                    Title = "How to Find book?", Content = "abcdefghijklmnopqrstuvwxyz1234567890", PublishDate = DateTime.Now, URLTitle = "How-to-find-book"
                },
                new BlogVM() {
                    Title = "Why Read this book?", Content = "0987654321abcdefghijklmnopqrstuvwxyz", PublishDate = DateTime.Now, URLTitle = "Why-read-this-book"
                }
            };

            SyndicationFeed feed = null;
            string siteTitle, description, siteUrl;
            siteTitle = "Vision Infotech | The Ultimate It Solution";
            siteUrl = "https://www.visioninfotech.net/";
            description = "We aim at translating clients IT vision into reality and craft powerful. Vision Infotech is an organization that provides strategic business solutions and develop.";

            List<SyndicationItem> items = new List<SyndicationItem>();
            foreach (var blog in blogs)
            {
                SyndicationItem item = new SyndicationItem
                {
                    Title = new TextSyndicationContent(blog.Title),
                    Content = new TextSyndicationContent(blog.Content),
                    PublishDate = DateTimeOffset.Parse(blog.PublishDate.ToString())
                };
                item.Links.Add(new SyndicationLink(new Uri(Request.Url.Scheme + "://" + Request.Url.Host + "/" + blog.URLTitle)));
                items.Add(item);
            }

            feed = new SyndicationFeed(siteTitle, description, new Uri(siteUrl));
            feed.Items = items;

            return new RSSResult { feedData = feed };
        }
    }

    public class BlogVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string URLTitle { get; set; }
    }
}