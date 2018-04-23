using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisplayForSlow.Models;

namespace DisplayForSlow.Controllers
{
    public class CustomersController : Controller
    {
        private static readonly List<Customer> _customers;

        static CustomersController()
        {
            var _TestNullable = Enumerable.Range(1,2).Select(x => new TestNullable() { Id = x, Name = "Name " + x }).ToList();

            _customers = Enumerable
                .Range(1, 500)
                .Select(i =>
                {
                    var customer = new Customer
                    {
                        Id = i,
                        FirstName = "FirstName" + i,
                        LastName = "LastName" + i,
                        Address = new Address
                        {
                            Id = i,
                            Street = "Street" + i,
                            City = "City" + i,
                            State = "State" + i
                        }
                    };

                    if (i % 2 == 0)
                    {
                        customer.Address.Zipcode = i;
                    }

                    if (i % 10 <= 1) {
                        customer.TestNullable = _TestNullable[i % 10];
                    }

                    return customer;
                }).ToList();
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(_customers);
        }

    }
}
