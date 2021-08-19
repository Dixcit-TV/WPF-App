using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MTG_App.View;
using MTG_Library.Helpers;
using MTG_Library.Model;
using MTG_Library.Repository;

namespace MTG_App.ViewModel
{
    class MainVM : ViewModelBase
    {
        private readonly Dictionary<string, Page> _pages;

        public string SearchCardString { get; set; }

        private string _searchSetString;

        public string SearchSetString
        {
            get => _searchSetString;
            set
            {
                _searchSetString = value;
                OnSearchSetUpdate(_searchSetString);
            }
        }

        private bool _isCardSearchEnable;

        public bool IsCardSearchEnable
        {
            get => _isCardSearchEnable;
            set
            {
                _isCardSearchEnable = value;
                RaisePropertyChanged();
            }
        }

        public CardSet _selectedCardSet;

        private Page _currentPage;

        public Page CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage = value;
                RaisePropertyChanged();
            }
        }

        private List<CardSet> _cardSets;
        public List<CardSet> CardSets
        {
            get => _cardSets;
            private set
            {
                _cardSets = value;
                RaisePropertyChanged();
            }
        }

        private RepositoryType _selectRepository;

        public RepositoryType SelectedRepository
        {
            get => _selectRepository;
            set
            {
                _selectRepository = value;
                RepositoryManager.Instance.SwitchRepository(_selectRepository);
            }
        }

        public RelayCommand<CardSet> SetSelectedCommand { get; }
        public RelayCommand SearchCardCommand { get; }

        public MainVM()
        {
            SelectedRepository = RepositoryType.OfflineJson;

            _pages = new Dictionary<string, Page>();
            _pages.Add("CardList", new CardListPage());
            (_pages["CardList"].DataContext as CardListViewModel).MainViewModel = this;
            _pages.Add("CardDetail", new CardDetailPage());

            IsCardSearchEnable = true;
            InitCardSets();
            CurrentPage = _pages["CardList"];
            SetSelectedCommand = new RelayCommand<CardSet>(LoadCardSet);
            SearchCardCommand = new RelayCommand(OnSearchCardUpdate);
        }

        private async void InitCardSets()
        {
            CardSets = await RepositoryManager.Instance.CurrentRepository.GetCardSetsAsync();
        }

        private void OnSearchCardUpdate()
        {
            if (CurrentPage != _pages["CardList"])
                CurrentPage = _pages["CardList"];

            (CurrentPage.DataContext as CardListViewModel)?.FilterCards(SearchCardString, _selectedCardSet?.Code);
        }

        private async void OnSearchSetUpdate(string searchString)
        {
            IsCardSearchEnable = false;
            CardSets = await RepositoryManager.Instance.CurrentRepository.SearchCardSetsAsync(searchString);
            IsCardSearchEnable = true;
        }

        public void LoadDetails(Card card)
        {
            CurrentPage = _pages["CardDetail"];

            (CurrentPage.DataContext as CardDetailViewModel).SelectedCard = card;
        }

        public void LoadCardSet(CardSet cardSet)
        {
            if (CurrentPage != _pages["CardList"])
                CurrentPage = _pages["CardList"];

            _selectedCardSet = cardSet;

            (CurrentPage.DataContext as CardListViewModel)?.SelectCardFromSet(cardSet);
        }
    }
}
