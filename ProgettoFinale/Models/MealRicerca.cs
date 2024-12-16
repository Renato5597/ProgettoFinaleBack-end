namespace ProgettoFinale.Models
{
    public class MealRicerca
    {
        public int Id { get; set; }
        public string? NomeRicerca { get; set; }
        public int? CountSearch{  get; set; }


        public MealRicerca(){
            
        }

        public MealRicerca(string nomeRicerca)
        {
            NomeRicerca = nomeRicerca;
            CountSearch = 1;
        }

    }
}
