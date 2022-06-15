using DevExpress.Mvvm;
using ICAdmin.Commands;
using ICAdmin.Models;
using ICAdmin.Services;
using ICAdmin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICAdmin.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly MessageBus _messageBus;

        public User SelectedUser { get => GetValue<User>(); set => SetValue(value); }
        public ObservableCollection<User> Users;
        public ObservableCollection<Message> Messages;
        public ChatViewModel(PageService pageService, MessageBus meessageBus)
        {
            _pageService = pageService;
            _messageBus = meessageBus;

            Users = new ObservableCollection<User>();
            Messages = SelectedUser.Messages;
        }

        private ICommand checkOverlayCommand;
        public ICommand CheckOverlayCommand
        {
            get
            {
                return checkOverlayCommand ??
                    (checkOverlayCommand = new RelayCommand(param => OverlayService.GetInstance().Show("Функционал находится в разработке, следите за обновлениями!")));
            }
        }
        private ICommand openMainMenuCommand;
        public ICommand OpenMainMenuCommand
        {
            get
            {
                return openMainMenuCommand ??
                    (openMainMenuCommand = new RelayCommand(param => _pageService.ChangePage(new MainMenuPage())));
            }
        }
    }
}
