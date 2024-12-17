using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using ProgettoFinale.Models;
using RestSharp;
using System.Xml.Serialization;

namespace ProgettoFinale.Services
{
    public class MealRetriver
    {
        public string? URL { get; set; }

        public MealRetriver()
        {
            this.URL = "https://www.themealdb.com/api/json/v1/1/";
        }

        public List<MealInfo> GetMealInfo(string choice)
        {
            try
            {
                RestClient client = new RestClient(this.URL);

                RestRequest request = new RestRequest($"search.php?s={choice}", Method.Get);

                RestResponse response = client.Execute(request);


                if (response.IsSuccessful)
                {
                    var myResponse = JsonConvert.DeserializeObject<Meals>(response.Content);     
                    

                    if(myResponse?.meals == null) {
                        throw new Exception("No Meal in database");
                    }
                    List<MealInfo> mealinfoList = myResponse.meals;
                    return mealinfoList;
                    

                }
                else
                {

                    Console.WriteLine("Errore nella richiesta API: " + response.ErrorMessage);
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return null;
            }

        }
    }
}

