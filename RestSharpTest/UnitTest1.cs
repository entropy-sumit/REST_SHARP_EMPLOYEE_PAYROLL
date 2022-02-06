using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpTest

{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
    }
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }
        private IRestResponse GetEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);

            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(4, employeeList.Count);
            foreach (Employee emp in employeeList)
            {
                Console.WriteLine("id: " + emp.id + "\t" + "name: " + emp.name + "\t" + "salary: " + emp.salary);
            }
        }
    }
}
