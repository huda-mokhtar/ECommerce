using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductService :IGenericService<Product>
    {
        private readonly ApplicationDbContext _db;
      
        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }
        public Product GetById(int id)
        {
            Product product=_db.Products.FirstOrDefault(a=>a.Id==id);
            return product;
        }
        public List<Product> Add(Product product)
        {
            _db.Add(product);
            _db.SaveChanges();
            return _db.Products.ToList();
        }
        public List<Product> Update(Product product)
        {
            Product OldProduct = _db.Products.FirstOrDefault(a => a.Id == product.Id);
            try
            {
                OldProduct.Name = product.Name;
                OldProduct.Description = product.Description;
                OldProduct.Price = product.Price;
                OldProduct.CostPrice = product.CostPrice;
                OldProduct.Discount = product.Discount;
                OldProduct.Quantity = product.Quantity;
                _db.SaveChanges();
            }
            catch
            {
                throw ;
            }
            return _db.Products.ToList();
        }
        public List<Product> Delete(int id)
        {
            Product product = _db.Products.FirstOrDefault(a => a.Id == id);
            _db.Remove(product);
            _db.SaveChanges();
            return _db.Products.ToList();
        }
    }
}
  
       
