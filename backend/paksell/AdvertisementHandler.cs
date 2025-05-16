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
        // Make sure this method correctly sets up entity relationships
        public Advertisement? AddAdvertisement(Advertisement advertisement)
        {
            using (paksellContext context = new paksellContext())
            {
                try
                {
                    // Set state for related entities
                    if (advertisement.Category != null && advertisement.Category.Id > 0)
                    {
                        var category = context.advertisementCategories.Find(advertisement.Category.Id);
                        if (category != null)
                        {
                            advertisement.Category = category;
                        }
                    }

                    if (advertisement.CityArea != null && advertisement.CityArea.Id > 0)
                    {
                        var cityArea = context.CityAreas.Find(advertisement.CityArea.Id);
                        if (cityArea != null)
                        {
                            advertisement.CityArea = cityArea;
                        }
                    }

                    if (advertisement.PostedById > 0)
                    {
                        var user = context.User.Find(advertisement.PostedById);
                        if (user != null)
                        {
                            advertisement.PostedBy = user;
                        }
                    }
                    

                    context.Advertisements.Add(advertisement);
                    context.SaveChanges();
                    return GetAdvertisement(advertisement.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding advertisement: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    }
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
