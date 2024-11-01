using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CinemaAPI.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        [JsonIgnore]
        public ICollection<Filme> Filmes { get; }= new List<Filme>();
    }
}
