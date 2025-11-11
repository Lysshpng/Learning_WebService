using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDemoWebService.Models
{
    /// <summary>
    /// 电影模型
    /// </summary>
    public class Movie
    {
        public string Name { get; set; }

        public string Director { get; set; }    

        public string Actor { get; set; }

        public string Type { get; set; }    

        public int Price { get; set; }    
    }
}