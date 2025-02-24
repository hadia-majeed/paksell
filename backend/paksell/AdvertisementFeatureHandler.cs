using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db
{
    public class AdvertisementFeatureHandler
    {
        public List<AdvertisementFeature> GetAdvertisementFeatures()
        {
            using (paksellContext context = new paksellContext())
            {
                return context.advertisementFeatures.ToList();
            }
        }
        public AdvertisementFeature? GetAdvertisementFeature(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return context.advertisementFeatures.FirstOrDefault(a => a.Id == id);
            }
        }

        public AdvertisementFeature AddAdvertisementFeatures(AdvertisementFeature advertisemetFeature)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Add(advertisemetFeature);
                context.SaveChanges();
                return advertisemetFeature;
            }
        }
        public AdvertisementFeature? UpdateAdFeature(AdvertisementFeature advertisemetFeature)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Update(advertisemetFeature);
                context.SaveChanges();
                return advertisemetFeature;
            }
        }

        public AdvertisementFeature? DeleteAdFeature(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                var found = context.advertisementFeatures.FirstOrDefault(a => a.Id == id);
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
