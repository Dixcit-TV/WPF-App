using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MTG_Library.Model;
using MTG_Library.Repository;

namespace MTG_App.ViewModel
{
    class CardDetailViewModel : ViewModelBase
    {
        private Card _selectedCard;

        public Card SelectedCard
        {
            get => _selectedCard;
            set
            {
                if (_selectedCard == value) return;
                _selectedCard = value;
                RaisePropertyChanged();
            }
        }

        public CardDetailViewModel()
        { }
    }
}
