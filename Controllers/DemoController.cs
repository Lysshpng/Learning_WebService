using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDemoWebApi.Controllers
{
    public class DemoController : ApiController
    {
        [HttpGet]
        public string DemoExample(string param1, int param2)
        {
            string result = $"Received param1: {param1}, param2: {param2}";
            return result;
        }
    }
}
