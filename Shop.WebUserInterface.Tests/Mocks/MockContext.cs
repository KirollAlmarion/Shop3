using Shop.Core.Logic;
using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.WebUserInterface.Tests.Mocks
{
    public class MockContext<T>: IRepository<T> where T: BaseEntity
    {
        List<T> items;
        string className;

        public MockContext()
        {
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void SaveChanges()
        {
            return;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T prodToUpdate = items.Find(prod => prod.Id == t.Id);
            if (prodToUpdate != null)
            {
                prodToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found.");
            }
        }

        public T FindById(int id)
        {
            T t = items.Find(prod => prod.Id == id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        //le type IQueryable accepte les requêtes LINQ contrairement à une liste classique
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(int id)
        {
            T prodToDelete = items.Find(p => p.Id == id);
            if (prodToDelete != null)
            {
                items.Remove(prodToDelete);
            }
            else
            {
                throw new Exception(className + " not found.");
            }
        }
    }
}
