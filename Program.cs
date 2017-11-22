using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Web;
using System.Net;
using System.Net.Http;



namespace CreateHttpClient
{
    public class CustomerView
    {

        public int CustomerID { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string CompanyName { get; set; }
        public string SalesPerson { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime ModifiedDate { get; set; }

    }




    class Program
    {
        static void Main(string[] args)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51317/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Customer");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerView>>();
                    readTask.Wait();
                    //var cu = readTask.Result;
                }


            }

            SelectFrom(20);
            Insert();
        }

        static void SelectFrom(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51317/api/");
                //HTTP GET
               var responseTask = client.GetAsync("Customer?CustomerID="+ Convert.ToString(id));

               

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerView>>();
                    readTask.Wait();
                    var cu = readTask.Result;

                    foreach(var o in cu)
                    {
                        Console.WriteLine(o.CustomerID);
                        Console.WriteLine(o.FirstName);
                        Console.WriteLine(o.LastName);
                        Console.WriteLine(o.EmailAddress);
                    }

                }
            }
        }

        static void Insert()
        {
            CustomerView c = new CustomerView();
            c.FirstName = "Sachin";
            c.LastName = "Tendulkar";
            c.PasswordHash = "srt";
            c.PasswordSalt = "srt";
            c.ModifiedDate = DateTime.Now;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51317/api/");
                var responseTask = client.PostAsJsonAsync("Customer", c);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data Inserted Successfully!!");
                }
            }
        }

        static void Update()
        {



        }

        static void Delete()
        { }
    }
}
