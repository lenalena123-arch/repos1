using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string URL = "https://reqres.in/api/users/2";
            var request = (HttpWebRequest)WebRequest.Create(URL);
            //request.Method = "POST";
            request.Method = "PUT";
            request.ContentType = "application/json";
            var data =
                @"{ ""first_name"" : ""Misha"",
""last_name"" : ""Pigalov""}";
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            using (writer)
            {
                writer.Write(data);
            }
            var response = (HttpWebResponse)request.GetResponse();
            //request.Method = "GET";
            request.Proxy.Credentials = new NetworkCredential("student", "student");
            //var response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string text = reader.ReadToEnd();
            //var JsonText = JsonConvert.DeserializeObject<User>(text);
            //Console.WriteLine($"{JsonText.data.id} {JsonText.data.last_name} {JsonText.data.first_name} {JsonText.data.email}");
            Console.WriteLine($"{text}");
            Console.ReadLine();
        }
    }
}

