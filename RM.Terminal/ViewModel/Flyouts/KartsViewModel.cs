using RM.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RM.Terminal
{
    public class KartsViewModel : BaseViewModel
    {
        public ObservableCollection<Button> Buttons { get; set; } = new ObservableCollection<Button>();

        public ICommand KartClickCommand { get; set; }

        public KartsViewModel()
        {
            KartClickCommand = new RelayParameterizedCommand(async (parameter) => await KartClick(parameter));

            GenerateButtons();
        }

        private Task KartClick(object parameter)
        {
            throw new NotImplementedException();
        }

        private void GenerateButtons()
        {
            foreach (Kart kart in KartList.KList)
            {
                Button btn = new Button
                {
                    Style = (Style)Application.Current.FindResource("KartButton"),
                    Content = $"#{kart.ID}",
                    Tag = kart.ID,
                    Command = KartClickCommand
                };

                Buttons.Add(btn);
            }
        }
    }
}
