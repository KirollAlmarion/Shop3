﻿using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;//as sert à faire un cast
            if (products==null)
            {
                products = new List<Product>();
            }
        }

        public void SaveChanges()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product prodToUpdate = products.Find(prod => prod.Id == p.Id);
            if (prodToUpdate != null)
            {
                prodToUpdate = p;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public Product FindById(int id)
        {
            Product p = products.Find(prod => prod.Id == id);
            if (p !=null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        //le type IQueryable accepte les requêtes LINQ contrairement à une liste classique
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(int id)
        {
            Product prodToDelete = products.Find(p => p.Id == id);
            if (prodToDelete != null)
            {
                products.Remove(prodToDelete);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }
    }
}
