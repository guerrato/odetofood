using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {
                    Id = 1,
                    Name = "John Doe Pizza",
                    Location = "Napoli",
                    Cuisine = CuisineType.Italian
                },
                new Restaurant {
                    Id = 2,
                    Name = "Hadji's",
                    Location = "NY",
                    Cuisine = CuisineType.Indian
                },
                new Restaurant {
                    Id = 3,
                    Name = "Miguelito",
                    Location = "SP",
                    Cuisine = CuisineType.Mexican
                },
                new Restaurant {
                    Id = 4,
                    Name = "Mexicalian",
                    Location = "Texas",
                    Cuisine = CuisineType.Mexican
                }
            };
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name, StringComparison.Ordinal)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            Restaurant r = this.GetById(updatedRestaurant.Id);

            if (r != null)
            {
                r.Name = updatedRestaurant.Name;
                r.Location = updatedRestaurant.Location;
                r.Cuisine = updatedRestaurant.Cuisine;
            }
            return r;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;

            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);

            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
