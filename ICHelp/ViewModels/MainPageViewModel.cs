using ICHelp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Message> Messages = new ObservableCollection<Message>();
    }
}
