
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoFinale.Models;
using System.Text.Json;

namespace ProgettoFinale
{
    public class AppDBContext : DbContext
    {
        public DbSet<MealRicerca> mealsDB { get; set; }


        public AppDBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=meals.db");
        }


        public List<MealRicerca> GetMealsDB()
        {
            // Retrieve all meals from the database
            return mealsDB.ToList();
        }


        public List<MealRicerca> GetMealSearch(int choice)
        {
            if (choice <= 0)
            {
                throw new ArgumentException("Choice must be greater than zero.");
            }

            List<MealRicerca> mealRicercas = new List<MealRicerca>(mealsDB.ToList());

            List<MealRicerca> sortedMealRicerca = mealRicercas.OrderByDescending(o => o.CountSearch).Take(choice).ToList();

            return sortedMealRicerca;

        }

    }
}
