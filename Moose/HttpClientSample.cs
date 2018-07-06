using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using static System.Console;
using System.Net.Http.Headers;
using System.Windows;

namespace Moose
{
    public class HttpClientSample
    {
        private const string NorthwindUrl = "http://services.odata.org/Northwind/Northwind.svc/Regions";
        private const string IncorrectUrl = "http://services.odata.org/Northwind1/Northwind.svc/Regions";

        private const string TestUrl = "https://ke.youdao.com/course/detail/3624?Pdt=CourseWeb";
        public async Task GetDataSimpleAsync()
        {
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    Client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                    ShowHeaders("Request Headers:", Client.DefaultRequestHeaders);

                    HttpResponseMessage Response = await Client.GetAsync(NorthwindUrl);//该方法默认不产生异常
                    Response.EnsureSuccessStatusCode();//失败时，使用该方法抛出                                                                                          

                    ShowHeaders("Response Headers:", Response.Headers);
                    string ResponseBodyAsTest = await Response.Content.ReadAsStringAsync();
                    WriteLine(ResponseBodyAsTest);
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        public static async Task GetDataWithMessageHandlerAsync()
        {
            var Client = new HttpClient(new HttpClientHandler());

            HttpResponseMessage Response = await Client.GetAsync(NorthwindUrl);

            string ResponseBodyAsTest =await  Response.Content.ReadAsStringAsync();
        }

        private async Task GetDataAdvanceAsync()
        {
            using (var Client = new HttpClient())
            {

                var Request = new HttpRequestMessage(HttpMethod.Get, NorthwindUrl);
                HttpResponseMessage Response = await Client.SendAsync(Request);

                string ResponseBodyAsTest = await Response.Content.ReadAsStringAsync();
            }   
        }

        public static void ShowHeaders(string title,HttpHeaders httpHeaders)
        {
            WriteLine(title);
            foreach (var header in httpHeaders)
            {
                string value = string.Join(" ",header.Value);
                WriteLine($"Header:{header.Key} Value:{value}");
                // WriteLine("Header：{0} value: {1}", header.Key, value);
            }
        }
    }
}
