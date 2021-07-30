using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class CustomerService : IGenericService<Customer>
    {
        private readonly ApplicationDbContext _db;

        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Customer> GetAll()
        {
            return _db.Customers.ToList();
        }
        public Customer GetById(int id)
        {
            Customer Customer = _db.Customers.FirstOrDefault(a => a.Id == id);
            return Customer;
        }
        public List<Customer> Add(Customer Customer)
        {
            _db.Add(Customer);
            _db.SaveChanges();
            return _db.Customers.ToList();
        }
        public List<Customer> Update(Customer Customer)
        {
            Customer OldCustomer = _db.Customers.FirstOrDefault(a => a.Id == Customer.Id);
            OldCustomer.Name = Customer.Name;
            OldCustomer.EmailAddress = Customer.EmailAddress;
            OldCustomer.Phone = Customer.Phone;
            OldCustomer.StarCustomer = Customer.StarCustomer;
            _db.SaveChanges();
            return _db.Customers.ToList();
        }
        public List<Customer> Delete(int id)
        {
            Customer Customer = _db.Customers.FirstOrDefault(a => a.Id == id);
            _db.Remove(Customer);
            _db.SaveChanges();
            return _db.Customers.ToList();
        }
    }
}

