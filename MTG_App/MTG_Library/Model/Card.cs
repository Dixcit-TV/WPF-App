using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MTG_Library.Model
{
    public class Card
    {
        public string Name { get; set; }
        public string ManaCost { get; set; }
        public float Cmc { get; set; }
        public string[] Colors { get; set; }
        public string[] ColorIdentity { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }
        public string SetName { get; set; }
        public string Text { get; set; }
        public string Artist { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public string Multiverseid { get; set; }
        public string ImageUrl { get; set; }
        public string Id { get; set; }
        [JsonIgnore] 
        public string Stats => $"{Power}/{Toughness}";
    }

}
