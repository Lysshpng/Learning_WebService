using MyDemoWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDemoWebService.Controllers
{
    /// <summary>
    /// Movie 控制器
    /// </summary>
    public class MovieController : ApiController
    {
        Movie[] movies = new Movie[]
        {
            new Movie { Name = "肖申克的救赎", Director = "弗兰克·德拉邦特", Actor = "蒂姆·罗宾斯", Type = "剧情", Price = 30 },
            new Movie { Name = "霸王别姬", Director = "陈凯歌", Actor = "张国荣", Type = "剧情", Price = 25 },
            new Movie { Name = "阿甘正传", Director = "罗伯特·泽米吉斯", Actor = "汤姆·汉克斯", Type = "剧情/爱情", Price = 28 },
            new Movie { Name = "这个杀手不太冷", Director = "吕克·贝松", Actor = "让·雷诺", Type = "动作/犯罪", Price = 22 },
            new Movie { Name = "泰坦尼克号", Director = "詹姆斯·卡梅隆", Actor = "莱昂纳多·迪卡普里奥", Type = "爱情/灾难", Price = 35 }
        };

        public IEnumerable<Movie> GetAllMovies()
        {
            return movies;
        }

        public IHttpActionResult GetMovieByName(string name)
        {
            var movie = movies.FirstOrDefault((m) => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

    }
}
