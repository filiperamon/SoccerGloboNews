using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml;
using FutebolNews.Entity;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Android.Graphics;

namespace FutebolNews.Server
{
    public class ServiceGetRss
    {

        public Channel getRssNews(string url)
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(url);

            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel");
            StringBuilder rssContent = new StringBuilder();

            Channel canal = new Channel();

            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                canal.title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                canal.link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                canal.description = rssSubNode != null ? rssSubNode.InnerText : "";
            }

            canal.item = getItem(url);

            return canal;
        }

        private static List<News> getItem(string url)
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(url);

            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            StringBuilder rssContent = new StringBuilder();

            List<News> ListaItens = new List<News>(); ;
            int maximoNoticia = 0;

            foreach (XmlNode rssNode in rssNodes)
            {
                News item = new News();
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                item.title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                item.link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                item.description = rssSubNode != null ? rssSubNode.InnerText : "";

                try
                {
                    item.urlImg = GetImageBitmapFromUrl(getUrlImg(item.description));
                }
                catch (Exception e)
                {
                    item.urlImg = GetImageBitmapFromUrl("http://s.glbimg.com/es/ge/media/common/img/Icon_platform_bigger.jpg");
                }

                //Remove tag img da descricao
                item.description = Regex.Replace(item.description, @"<img(.*?)>", String.Empty);

                ListaItens.Add(item);

                if (maximoNoticia == 20)
                    break;                

                maximoNoticia++;                
            }

            return ListaItens;
        }

        private static string getUrlImg(string texto)
        {
            Regex r = new Regex("src='(.*?)'");
            MatchCollection mc = r.Matches(texto);

            foreach (Match m in mc)
            {
                return m.Groups[0].Value.Trim().Replace("src=", "").Replace("'", "");
            }

            return string.Empty;
        }

        private static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            
            return imageBitmap;
        }
        
    }
}
