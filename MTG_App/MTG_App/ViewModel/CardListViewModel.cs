using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MTG_Library.Model;
using MTG_Library.Repository;

namespace MTG_App.ViewModel
{
    class CardListViewModel : ViewModelBase
    {
        public MainVM MainViewModel { get; set; }

        private List<Card> _cards;
        public List<Card> Cards
        {
            get => _cards;
            private set
            {
                _cards = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<Card> CardSelectedCommand { get; }

        public CardListViewModel()
        {
            InitCards();
            CardSelectedCommand = new RelayCommand<Card>(OnCardSelected);
        }

        private async void InitCards()
        {
            Cards = await RepositoryManager.Instance.CurrentRepository.GetCardsAsync();
        }

        public async void FilterCards(string searchString, string setCode)
        {
            Cards = await RepositoryManager.Instance.CurrentRepository.SearchCardAsync(searchString, setCode);
        }

        private void OnCardSelected(Card card)
        {
            MainViewModel.LoadDetails(card);
        }

        public async void SelectCardFromSet(CardSet cardSet)
        {
            Cards = await RepositoryManager.Instance.CurrentRepository.GetCardsAsync(cardSet.Code);
        }
    }
}
