using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db
{
    public class AdvertisementImagesHandler
    {
        public List<AdvertisementImage> GetAdvertisementImages()
        {
            using (paksellContext context = new paksellContext())
            {
                return context.AdvertisementImages.ToList();
             }
        }

        public AdvertisementImage? GetAdvertisementImage(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return context.AdvertisementImages.FirstOrDefault(a => a.Id  == id);
            }
        }
        public AdvertisementImage? AddAdvertisementImage(AdvertisementImage advertisementImage)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Add(advertisementImage);
                context.SaveChanges();
                return advertisementImage;
            }
        }

        public AdvertisementImage? UpdateAdvertisementImage(AdvertisementImage advertisementImage)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Update(advertisementImage);
                context.SaveChanges();
                return advertisementImage;
            }
        }

        public AdvertisementImage? DeleteAdvertisementImage(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                var found = context.AdvertisementImages.FirstOrDefault(a => a.Id == id);
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
 