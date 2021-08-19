using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MTG_Library.Helpers;
using MTG_Library.Model;

namespace MTG_Library.Repository
{
    public partial class RepositoryManager
    {
        private class OnlineApiCardRepositoy : ICardRepository
        {
            private List<Card> _cards;
            private List<CardSet> _cardSets;

            public async Task<List<Card>> GetCardsAsync()
            {
                string endPoint = "https://api.magicthegathering.io/v1/cards";

                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _cards = HelperFunctions.GetCardsFromJson(json);
                }

                return _cards;
            }


            public async Task<List<Card>> GetCardsAsync(string setId)
            {
                string endPoint = $"https://api.magicthegathering.io/v1/cards?set={setId}";
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _cards = HelperFunctions.GetCardsFromJson(json);
                }

                return _cards;
            }

            public async Task<List<CardSet>> GetCardSetsAsync()
            {
                string endPoint = "https://api.magicthegathering.io/v1/sets";

                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _cardSets = HelperFunctions.GetCardSetsFromJson(json);
                }

                return _cardSets;
            }

            public async Task<List<Card>> SearchCardAsync(string searchString, string setCode)
            {
                using HttpClient client = new HttpClient();
                _cards = new List<Card>();
                setCode ??= "";
                string endPoint = $"https://api.magicthegathering.io/v1/cards?name={searchString}&set={setCode}";

                HttpResponseMessage response = await client.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    _cards = HelperFunctions.GetCardsFromJson(json);
                }

                return _cards;
            }

            public async Task<List<CardSet>> SearchCardSetsAsync(string searchString)
            {
                if (_cardSets == null)
                    await GetCardSetsAsync();

                return string.IsNullOrEmpty(searchString) ? _cardSets : _cardSets?.Where(c => c.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
        }
    }
}
