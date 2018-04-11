using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NoOfSubjects { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)

        {
             CallGetOnAPi(5).Wait();
          //  CallPostOnApi().Wait();
         //   CallDeleteOnApi(13).Wait();
         //   CallPutOnApi(12).Wait();
        }

        static async Task CallGetOnAPi(int id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52967/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "female", "female"))));
                HttpResponseMessage response = await client.GetAsync("api/students/"+id);
                if (response.IsSuccessStatusCode)
                {
                    Student student1 = await response.Content.ReadAsAsync<Student>();
                    Console.WriteLine("Id :{0}\tFirstName: {1}\tlasName: {2} \tgender: {3}\tNo of sunjects: {4}",
                        student1.ID, student1.FirstName, student1.LastName, student1.Gender, student1.NoOfSubjects);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("internal erver error");
                }
           
            }
        }
        static async Task CallPostOnApi()
        {
            client.BaseAddress = new Uri("http://localhost:52967/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          var student = new Student()
            {
                ID = 13,
                FirstName = "rishab",
                LastName = "modi",
                Gender = "male",
                NoOfSubjects = "5"
            };
            HttpResponseMessage res = await client.PostAsJsonAsync("api/students/", student);
            if (res.IsSuccessStatusCode)
            {
                Uri returnUrl = res.Headers.Location;
                Console.WriteLine(returnUrl);
                Console.ReadLine();
            }
        }
        static async Task CallPutOnApi(int id)
        {
            client.BaseAddress = new Uri("http://localhost:52967/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var student = new Student()
            {
                ID = 12,
                FirstName = "shalini",
                LastName = "modi",
                Gender = "female",
                NoOfSubjects = "5"
            };
            HttpResponseMessage res = await client.PutAsJsonAsync("api/students/"+id, student);
            if (res.IsSuccessStatusCode)
            {
                Uri returnUrl = res.Headers.Location;
                Console.WriteLine(returnUrl);
                Console.ReadLine();
            }
        }

        static async Task CallDeleteOnApi(int id)
        {
            client.BaseAddress = new Uri("http://localhost:52967/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          
            HttpResponseMessage res = await client.DeleteAsync("api/students/"+id);
            if (res.IsSuccessStatusCode)
            {
                Uri returnUrl = res.Headers.Location;
                Console.WriteLine(returnUrl);
                Console.ReadLine();
            }
        }
    }
}
