using System.Text.Json.Serialization;

namespace CinemaAPI.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string genero {get;set;}
        public int ano {get; set;}
        public int CinemaId { get; set; }
        [JsonIgnore]
        public Cinema Cinema {get; set;}
    }
}
