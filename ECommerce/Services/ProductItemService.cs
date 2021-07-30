using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductItemService :IGenericService<ProductItem>
    {
        private readonly ApplicationDbContext _db;

        public ProductItemService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<ProductItem> GetAll()
        {
            return _db.ProductItems.ToList();
        }
        public ProductItem GetById(int id)
        {
            ProductItem OrderItem = _db.ProductItems.FirstOrDefault(a => a.Id == id);
            return OrderItem;
        }
        public List<ProductItem> Add(ProductItem OrderItem)
        {
            _db.Add(OrderItem);
            _db.SaveChanges();
            return _db.ProductItems.ToList();
        }
        public List<ProductItem> Update(ProductItem ProductItem)
        {
            ProductItem OldproductItem = _db.ProductItems.FirstOrDefault(a => a.Id == ProductItem.Id);
            OldproductItem.Quantity = ProductItem.Quantity;
            _db.SaveChanges();
            return _db.ProductItems.ToList();
        }
        public List<ProductItem> Delete(int id)
        {
            ProductItem ProductItem = _db.ProductItems.FirstOrDefault(a => a.Id == id);
            _db.Remove(ProductItem);
            _db.SaveChanges();
            return _db.ProductItems.ToList();
        }
    }
}

