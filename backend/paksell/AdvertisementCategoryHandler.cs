using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db
{
    public class AdvertisementCategoryHandler
    {
        public List<AdvertisementCategory> GetAdvertisementCategories()
        {
            using (paksellContext context = new paksellContext())
            {

                return context.advertisementCategories.Include(a => a.Advertisements).ToList();
            }
        }
        public AdvertisementCategory? GetCategory(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return context.advertisementCategories.FirstOrDefault(a => a.Id == id);
            }
        }

        public AdvertisementCategory AddCategory(AdvertisementCategory category)
        {
            using paksellContext context = new paksellContext();
            context.Add(category);
            context.SaveChanges();
            return category;
        }
        public AdvertisementCategory? UpdateCategory(AdvertisementCategory category)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Update(category);
                context.SaveChanges();
                return category;
            }
        }

        public AdvertisementCategory? DeleteCategory(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                var found = context.advertisementCategories.FirstOrDefault(a => a.Id == id);
                if (found != null)
                {
                    context.Remove(found);
                    context.SaveChanges();
                    return found;
                }
                return null;
            }
        }


    }
}
