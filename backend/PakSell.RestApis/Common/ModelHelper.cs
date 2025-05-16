using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using paksell;
namespace PakSell.Models
{
    public static class ModelHelper
    {
        public static AdvertisementFeatureModel ToModel(this AdvertisementFeature entity)
        {
            return new AdvertisementFeatureModel
            {
                Id = entity.Id,
                Name = entity.Name,
                value = entity.value
            };
        }

        public static AdvertisementFeature ToEntity(this AdvertisementFeatureModel model)
        {
            return new AdvertisementFeature
            {
                Id = model.Id,
                Name = model.Name,
                value = model.value

            };
        }

        public static List<AdvertisementFeatureModel> ToModelList(this ICollection<AdvertisementFeature> entitiesCollection)
        {
            List<AdvertisementFeatureModel> modelslist = new List<AdvertisementFeatureModel>();
            foreach (var entity in entitiesCollection)
            {
                modelslist.Add(entity.ToModel());

            }
            modelslist.TrimExcess();
            return modelslist;
        }

        public static List<AdvertisementFeature> ToEntityList(this ICollection<AdvertisementFeatureModel> modelsCollection)
        {
            List<AdvertisementFeature> entitiesList = new List<AdvertisementFeature>();
            foreach (var model in modelsCollection)
            {
                entitiesList.Add(model.ToEntity());
            }
            entitiesList.TrimExcess();
            return entitiesList;
        }

        public static AdvertisementImageModel ToModel(this AdvertisementImage entity)
        {
            return new AdvertisementImageModel
            {
                Id = entity.Id,
                Rank = entity.Rank,
                ImagePath = entity.ImagePath
            };
        }
        public static AdvertisementImage ToEntity(this AdvertisementImageModel model)
        {
            return new AdvertisementImage
            {
                Id = model.Id,
                Rank = model.Rank,
                ImagePath = model.ImagePath
            };
        }

        public static List<AdvertisementImageModel> ToModelList(this ICollection<AdvertisementImage> entitiesCollection)
        {
            List<AdvertisementImageModel> modelsList = new List<AdvertisementImageModel>();
            foreach (var entity in entitiesCollection)
            {
                modelsList.Add(entity.ToModel());
            }
            modelsList.TrimExcess();
            return modelsList;
        }

        public static List<AdvertisementImage> ToEntityList(this ICollection<AdvertisementImageModel> modelsCollection)
        {
            List<AdvertisementImage> entityList = new List<AdvertisementImage>();
            foreach (var model in modelsCollection)
            {
                entityList.Add(model.ToEntity());
            }
            entityList.TrimExcess();
            return entityList;
        }


        public static AdvertisementCategoryModel ToModel(this AdvertisementCategory entity)
        {
            return new AdvertisementCategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Image = entity.Image,
                AdvertisementCount = entity.Advertisements.Count,
                //Advertisements = entity.Advertisements.Select(a=> new AdvertisementModel
                //{
                   
                //})
                
            };
        }
        public static AdvertisementCategory ToEntity(this AdvertisementCategoryModel model)
        {
            return new AdvertisementCategory
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image
            };
        }
        public static List<AdvertisementCategory> ToEntityList(this ICollection<AdvertisementCategoryModel> modelCollection)
        {
            List<AdvertisementCategory> entityList = new List<AdvertisementCategory>();
            {
                foreach (var model in modelCollection)
                {
                    entityList.Add(model.ToEntity());
                }
                entityList.TrimExcess();
                return entityList;
            };
        }

        public static List<AdvertisementCategoryModel> ToModelList(this ICollection<AdvertisementCategory> entitiesCollection)
        {
            List<AdvertisementCategoryModel> modelsList = new List<AdvertisementCategoryModel>();
            foreach (var entity in entitiesCollection)
            {

                modelsList.Add(entity.ToModel());

            }
            modelsList.TrimExcess();
            return modelsList;
        }

        public static CityAreaModel ToModel(this CityArea entity)
        {
            return new CityAreaModel
            {
                Id = entity.Id,
                Name = entity.Name,
                User = entity.User?.ToModel()
            };
        }
        public static CityAreaModel ToCityAreaUserModel(this CityArea entity)
        {
            return new CityAreaModel
            {
                Id = entity.Id,
                Name = entity.Name,
                User = entity.User?.ToModel()
            };
        }
        public static CityArea ToEntity(this CityAreaModel model)
        {
            return new CityArea
            {
                Id = model.Id,
                Name = model.Name,
                User = model.User?.ToEntity()
            };
        }

        // Updated ToEntity method for AdvertisementModel
        public static Advertisement ToEntity(this AdvertisementModel model)
        {
            Advertisement entity = new Advertisement
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                endsOn = model.EndsOn,
                startsOn = model.StartsOn
            };

            // Handle Category (just set ID, no need to load the entire object)
            if (model.Category != null && model.Category.Id > 0)
            {
                entity.Category = new AdvertisementCategory { Id = model.Category.Id };
            }

            // Handle CityArea (just set ID, no need to load the entire object)
            if (model.CityArea != null && model.CityArea.Id > 0)
            {
                entity.CityArea = new CityArea { Id = model.CityArea.Id };
            }

            // Handle PostedBy (just set ID, no need to load the entire object)
            if (model.PostedBy != null && model.PostedBy.Id > 0)
            {
                entity.PostedBy = new User { Id = model.PostedBy.Id.Value };
                entity.PostedById = model.PostedBy.Id.Value;
            }

            // Handle collections (these still need full objects)
            if (model.AdvertisementImages != null && model.AdvertisementImages.Any())
            {
                entity.AdvertisementImages = model.AdvertisementImages.ToEntityList();
            }

            if (model.AdvertisementFeatures != null && model.AdvertisementFeatures.Any())
            {
                entity.AdvertisementFeatures = model.AdvertisementFeatures.ToEntityList();
            }

            return entity;
        }

        public static AdvertisementModel ToModel(this Advertisement entity)
        {
            try
            {
                AdvertisementModel model = new AdvertisementModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Price = entity.Price;
                model.Description = entity.Description;
                model.EndsOn = entity.endsOn;
                model.StartsOn = entity.startsOn;
                model.Category = entity.Category?.ToModel();
                model.CityArea = entity.CityArea?.ToModel();
                model.PostedBy = entity.PostedBy?.ToModel();
                if (model.AdvertisementImages != null)
                {
                    model.AdvertisementImages = entity.AdvertisementImages.ToModelList();
                }
                if (model.AdvertisementFeatures != null)
                {
                    model.AdvertisementFeatures = entity.AdvertisementFeatures.ToModelList();
                }
                return model;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public static AdvertisementModel ToModel(this AdvertisementBindingModel entity)
        {
            AdvertisementModel model = new AdvertisementModel();
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Price = entity.Price;
            model.Description = entity.Description;
            model.EndsOn = DateOnly.FromDateTime(entity.EndsOn);
            model.StartsOn = DateOnly.FromDateTime(entity.StartsOn);
            model.Category = entity.CategoryId > 0 ? new AdvertisementCategoryModel { Id = entity.CategoryId } : null;
            model.PostedBy = entity.PostedBy > 0 ? new UserModel { Id = entity.PostedBy } : null;

            model.AdvertisementFeatures = entity.AdvertisementFeatures?.Select(a => new AdvertisementFeatureModel()
            {
                Name = a
            }).ToList() ?? new List<AdvertisementFeatureModel>();

            model.AdvertisementImages = entity.AdvertisementImages?.Select(a => new AdvertisementImageModel()
            {
                ImagePath = a
            }).ToList() ?? new List<AdvertisementImageModel>();

            return model;
        }


        public static List<AdvertisementModel> ToModelList(this ICollection<Advertisement> entitiesCollection)
        {
            List<AdvertisementModel> modelsList = new List<AdvertisementModel>();
            foreach (var entity in entitiesCollection)
            {
                modelsList.Add(entity.ToModel());
            }
            modelsList.TrimExcess();
            return modelsList;
        }

        public static List<Advertisement> ToEntityList(this ICollection<AdvertisementModel> modelCollection)
        {
            List<Advertisement> entityList = new List<Advertisement>();
            foreach (var entity in modelCollection)
            {
                entityList.Add(entity.ToEntity());
            }
            entityList.TrimExcess();
            return entityList;
        }


        public static UserModel ToModel(this User entity)
        {

            return new UserModel

            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                LoginId = entity.LoginId,
                Password = entity.Password,
                SecurityAnswer = entity.SecurityAnswer,
                SecurityQuestion = entity.SecurityQuestion,
                PhoneNumber = entity.PhoneNumber,
                City = entity.City,
                UserImage = entity.UserImage,
                BirthDate = entity.BirthDate,
              

            };
        }
        public static User ToEntity(this UserModel model)
        {
            return new User
            {
                Id = model.Id.Value,
                Name = model.Name,
                Email = model.Email,
                LoginId = model.LoginId,
                Password = model.Password,
                SecurityAnswer = model.SecurityAnswer,
                SecurityQuestion = model.SecurityQuestion,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                UserImage = model.UserImage,
                BirthDate = model.BirthDate,
            };
        }




        
    }
}
