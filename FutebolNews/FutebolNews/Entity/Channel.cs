using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FutebolNews.Entity
{    
    public class Channel
    {        
        public string title { get; set; }        
        public string link { get; set; }        
        public string description { get; set; }                        
        public List<News> item { get; set; }
    }
    
}
