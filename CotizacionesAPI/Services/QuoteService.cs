using CotizacionesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CotizacionesAPI.Services
{
    public static class QuoteService
    {

        public static void Add(QuoteModel quote)
        {
            using (var dataContext = new DataContext())
            {
                var entity = dataContext.Quotes.Add(quote);
                entity.State = EntityState.Added;

                dataContext.SaveChanges();
            }

        }

        public static void Update(QuoteModel quote)
        {
            using (var dataContext = new DataContext())
            {
                var entity = dataContext.Quotes.Update(quote);
                entity.State = EntityState.Modified;

                dataContext.SaveChanges();
            }
        }

        public static void Remove(QuoteModel quote)
        {
            using (var dataContext = new DataContext())
            {
                var entity = dataContext.Quotes.Remove(quote);
                entity.State = EntityState.Deleted;

                dataContext.SaveChanges();
            }
        }

        public static List<QuoteModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                if (dataContext.Quotes.Any())
                {
                    List<QuoteModel> data = dataContext.Quotes.ToList();
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }


        public static QuoteModel Get(int id)
        {
            using (var dataContext = new DataContext())
            {
                var cuote = dataContext.Quotes.Find(id);

                return cuote;
            }
        }

    }
}
