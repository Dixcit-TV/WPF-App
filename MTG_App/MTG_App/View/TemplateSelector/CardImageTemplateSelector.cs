using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MTG_App.ViewModel;
using MTG_Library.Model;

namespace MTG_App.View.TemplateSelector
{
    class CardImageTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Card card = item as Card;
            if (card == null)
                return null;

            FrameworkElement element = container as FrameworkElement;

            return (card.ImageUrl == null ? element?.FindResource("Card_NoImage") : element?.FindResource("Card_ImageAvailable")) as DataTemplate;
        }
    }
}
