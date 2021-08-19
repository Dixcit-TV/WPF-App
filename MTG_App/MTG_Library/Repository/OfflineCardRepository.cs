using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MTG_Library.Helpers;
using MTG_Library.Model;
using Newtonsoft.Json;

namespace MTG_Library.Repository
{
    public partial class RepositoryManager //Hides the repository from other classes, enforce the use of the manager
    {
        private class OfflineCardRepository : ICardRepository
        {
            private List<Card> _cards;
            private List<CardSet> _cardSets;

            public async Task<List<Card>> GetCardsAsync()
            {
                if (_cards != null)
                    return _cards;

                var assembly = Assembly.GetEntryAssembly();
                var resource = "MTG_App.Resources.Data.MTG_Cards.json";

                using var stream = assembly?.GetManifestResourceStream(resource);
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                return _cards = HelperFunctions.GetCardsFromJson(json);
            }

            public async Task<List<Card>> GetCardsAsync(string setId)
            {
                if (_cards == null)
                    await GetCardsAsync();

                return string.IsNullOrEmpty(setId) || setId.Equals("All", StringComparison.OrdinalIgnoreCase) ? _cards : _cards?.Where(c => c.Set.Equals(setId)).ToList();
            }

            public async Task<List<CardSet>> GetCardSetsAsync()
            {
                if (_cardSets != null)
                    return _cardSets;

                var assembly = Assembly.GetEntryAssembly();
                var resource = "MTG_App.Resources.Data.MTG_CardSets.json";

                using var stream = assembly?.GetManifestResourceStream(resource);
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                return _cardSets = HelperFunctions.GetCardSetsFromJson(json);
            }

            public async Task<List<Card>> SearchCardAsync(string searchString, string setCode)
            {
                if (_cards == null)
                    await GetCardsAsync();

                return string.IsNullOrEmpty(searchString) ? 
                    _cards : 
                    _cards?.Where(c => c.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 && (string.IsNullOrEmpty(setCode) || c.Set.Equals(setCode))).ToList();
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
