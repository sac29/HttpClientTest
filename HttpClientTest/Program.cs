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
        static void Main(string[] args)

        {
            CallWebApiAsync().Wait();
            
        }

        static async Task CallWebApiAsync()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54240/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/students/1");
                if (response.IsSuccessStatusCode)
                {
                    Student student1 = await response.Content.ReadAsAsync<Student>();
                    Console.WriteLine("Id :{0}\tFirstName: {1}\tlasName: {2} \tgender: {3}\tFirstName: {4}",
                        student1.ID, student1.FirstName, student1.LastName, student1.Gender, student1.NoOfSubjects);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("inernal erver error");
                }
                var student = new Student()
                {
                    ID = 12,
                    FirstName ="rishab",
                    LastName ="modi",
                    Gender = "male",
                    NoOfSubjects = "5"
                };
                HttpResponseMessage res = await client.PostAsJsonAsync("api/students", student);
                if (res.IsSuccessStatusCode)
                {
                    Uri returnUrl = res.Headers.Location;
                    Console.WriteLine(returnUrl);
                    Console.ReadLine();
                }
            }
        }
    }
}
