using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTG_Library.Model;

namespace MTG_Library.Repository
{
    public interface ICardRepository
    {
        Task<List<Card>> GetCardsAsync();
        Task<List<Card>> GetCardsAsync(string setId);
        Task<List<CardSet>> GetCardSetsAsync();
        Task<List<Card>> SearchCardAsync(string searchString, string setCode);
        Task<List<CardSet>> SearchCardSetsAsync(string searchString);
    }
}
