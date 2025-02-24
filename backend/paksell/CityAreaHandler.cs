using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace paksell.Db
{
    public class CityAreaHandler
    {
        public List<CityArea> GetCityAreas()
        {
            using (paksellContext context = new paksellContext()) 
            {
                return context.CityAreas.ToList();
            }
        }
        public CityArea? GetCityArea(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                return context.CityAreas.FirstOrDefault(a => a.Id == id);
            }
        }
        public CityArea? AddCityArea(CityArea cityArea)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Add(cityArea);
                context.SaveChanges();
                return cityArea;
            }

        }
        public CityArea? UpdatCityArea(CityArea cityArea)
        {
            using (paksellContext context = new paksellContext())
            {
                context.Update(cityArea);
                context.SaveChanges();
                return cityArea;
            }
        }

        public CityArea? DeleteCityArea(int id)
        {
            using (paksellContext context = new paksellContext())
            {
                var found = context.CityAreas.FirstOrDefault(a => a.Id == id);
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
