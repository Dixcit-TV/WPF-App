using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTG_Library.Model;
using Newtonsoft.Json;

namespace MTG_Library.Helpers
{
    public static class HelperFunctions
    {
        public static List<Card> GetCardsFromJson(string json)
        {
            dynamic jObject = JsonConvert.DeserializeObject(json);
            if (jObject == null || jObject.cards?.Count == 0)
                return null;

            return jObject.cards.ToObject<List<Card>>();
        }

        public static List<CardSet> GetCardSetsFromJson(string json)
        {
            dynamic jObject = JsonConvert.DeserializeObject(json);
            if (jObject == null || jObject.sets?.Count == 0)
                return null;

            List<CardSet> cardSets = jObject.sets.ToObject<List<CardSet>>();
            cardSets.Insert(0, new CardSet { Code = "", Name = "All" });
            return cardSets;
        }
    }
}
