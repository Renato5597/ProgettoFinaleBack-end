using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoFinale.Models;
using ProgettoFinale.Services;
using ProgettoFinale;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Azure.Core;
using Microsoft.AspNetCore.Cors;

namespace ProgettoFinale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("myPolicy")]
    public class MealsController : ControllerBase
    {

        [HttpPost("mealInfo")]
        public ActionResult<List<MealInfo>> GetAllMealInfo(string stringMeal)
        {
            try
            {
                using var context = new AppDBContext();
                MealRetriver mealRetriver = new MealRetriver();
                List<MealInfo> mealInfoList = mealRetriver.GetMealInfo(stringMeal);
                MealRicerca mealRicerca = new MealRicerca(stringMeal);


                MealRicerca? actualMeal = context.mealsDB.FirstOrDefault(p => p.NomeRicerca == stringMeal);

                if (actualMeal == null) // if no corrispodence return null else return object
                {
                    MealRicerca newMeal = new MealRicerca
                    {
                        NomeRicerca = stringMeal,
                        CountSearch = 1
                    };

                    context.mealsDB.Add(newMeal);
                    context.SaveChanges();
                    return Ok(new { message = "Meal added successfully", mealInfoList });
                }
                else
                {

                    actualMeal.CountSearch++;
                    context.mealsDB.Update(actualMeal);
                    context.SaveChanges();

                    return(mealInfoList == null ? Ok(new { message = "Meal already exists but no in DB", mealInfoList }): Ok(new { message = "Meal already exists", mealInfoList }));
                    
                }
                
            }
            catch (Exception ex)
            {
               
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("mostSearched")]
        public ActionResult<IEnumerable<MealRicerca>> GetMostSearched(int choice)
        {
            if (choice <= 0)
            {
                return BadRequest("Invalid choice parameter.");
            }

            try
            {
                using var context = new AppDBContext();
                return Ok(context.GetMealSearch(choice));

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("mealRicerca")]
        public ActionResult<IEnumerable<MealRicerca>> GetAll()
        {
            using var context = new AppDBContext();
            return Ok(context.GetMealsDB());
        }
    }
}
