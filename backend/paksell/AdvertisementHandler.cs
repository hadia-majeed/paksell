using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db
{

    public class AdvertisementHandler
    {
        public class test
        {
            public int Count { get; set; }
            public string Name { get; set; }
        }
        public List<Advertisement> GetAdvertisements()
        {
            using (paksellContext context = new paksellContext())
            {
                var countOfeach = context.Advertisements
                    .Include(a => a.Category).GroupBy(a => a.Category).Select(a => new test
                    {
                        Count = a.Count(),
                        Name = a.Key.Name
                    });

                foreach (var item in countOfeach)
                {
                    Console.WriteLine($"{item.Name} {item.Count}");

                }

                return (from a in context.Advertisements

                        .Include(a => a.CityArea)
                        .Include(a => a.PostedBy)
                        .Include(a => a.AdvertisementImages)
                        .Include(a => a.AdvertisementFeatures)
                        .Include(a => a.Category)
                        select a).ToList();
            };
        }
        public Advertisement? GetAdvertisement(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return context.Advertisements
                    .Include(a => a.Category)
                      .Include(a => a.CityArea)
                        .Include(a => a.PostedBy)
                        .Include(a => a.AdvertisementImages)
                        .Include(a => a.AdvertisementFeatures)
                      

                    .FirstOrDefault(a => a.Id == id);
            }
        }
        public Advertisement? AddAdvertisement(Advertisement advertisement)
        {
            using (paksellContext context = new paksellContext())
            {
                try
                {
                    // Set state for related entities to ensure EF only attaches them
                    if (advertisement.Category != null)
                    {
                        context.Entry(advertisement.Category).State = EntityState.Unchanged;
                    }

                    if (advertisement.CityArea != null)
                    {
                        context.Entry(advertisement.CityArea).State = EntityState.Unchanged;
                    }

                    if (advertisement.PostedBy != null)
                    {
                        context.Entry(advertisement.PostedBy).State = EntityState.Unchanged;
                    }

                    // Add the advertisement with its features and images
                    context.Advertisements.Add(advertisement);
                    context.SaveChanges();

                    // Reload the entity with all related data to return a complete object
                    return GetAdvertisement(advertisement.Id);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error adding advertisement: {ex.Message}");
                    return null;
                }
            }
        }
        public Advertisement? UpdateAdvertisement(Advertisement advertisement)
        {
            Advertisement? found = GetAdvertisement(advertisement.Id);
            if (found != null)
            {
                using (paksellContext context = new paksellContext())
                {
                    context.Entry(advertisement.Category).State = EntityState.Unchanged;
                    context.Update(advertisement);
                    context.SaveChanges();
                    return found;

                }
            }
            return null;
        }
        public Advertisement? DeleteAdvertisement(int id)
        {
            Advertisement? found = GetAdvertisement(id);
            if (found != null)
            {
                using (paksellContext context = new paksellContext())
                {
                    context.Remove(found);
                    context.SaveChanges();
                    return found;
                }
            }
            return null;
        }

        public List<Advertisement> GetAdvertisementsByCategory(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return (from a in context.Advertisements

                        .Include(a => a.CityArea)
                        .Include(a => a.PostedBy)
                        .Include(a => a.AdvertisementImages)
                        .Include(a => a.AdvertisementFeatures)
                        .Include(a => a.Category)
                        .Where(a => a.Category.Id == id)
                        select a).ToList();

            }
        }


    }
}
