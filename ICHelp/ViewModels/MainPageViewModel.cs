using DevExpress.Mvvm;
using ICHelp.Models;
using ICHelp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ICHelp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private CheckConnectionService _checkConnectionService;
        private PageService _pageService;
        private AuthorizationService _authorizationService;
        private RegistrationService _registrationService;
        private AnyDeskService _anyDeskService;
        private MessageService _messageService;
        public ObservableCollection<Message> Messages { get => GetValue<ObservableCollection<Message>>(); set => SetValue(value); }
        public Message SelectedMessage { get => GetValue<Message>(); set => SetValue(value); }


        public MainPageViewModel(PageService pageService, AnyDeskService anyDeskService, AuthorizationService authorizationService, RegistrationService registrationService, CheckConnectionService checkConnectionService, MessageService messageService)
        {
            Messages = new ObservableCollection<Message>();
            _checkConnectionService = checkConnectionService;
            _pageService = pageService;
            _authorizationService = authorizationService;
            _registrationService = registrationService;
            _anyDeskService = anyDeskService;
            _messageService = messageService;
            _registrationService.Start();
            Task.Run(async () => GetMessages());
        }

        public string MessageBody { get => GetValue<string>(); set => SetValue(value); }
        private ICommand sendMessage;
        public ICommand SendMessage
        {
            get
            {
                return sendMessage ??
                    (sendMessage = new AsyncCommand(async () =>
                    {
                        {
                            var message = new Message() { Body = MessageBody, SendTime = DateTime.Now, Sender = SenderType.Employee, User = _registrationService.CurrentUserMachine.CurrentUser };
                            if (await _messageService.SendMessageAsync(message))
                            {
                                Messages.Add(message);
                            }
                            else
                            {
                                MessageBox.Show("Сообщение не отправлено!", "Ошибка!");
                            }
                            SelectedMessage = message;
                            MessageBody = String.Empty;
                        }
                    }, () => CanSend()));
            }
        }
        private Message newMessage;
        public Message NewMessage { get { return newMessage; } set { newMessage = value; RaisePropertiesChanged("NewMessage"); } }
        public async void GetMessages()
        {
            if (_registrationService.CurrentUserMachine != null && _checkConnectionService.IsServerConnected)
            {
                var currentUser = _registrationService.CurrentUserMachine.CurrentUser;
                string MotherBoarderialNumber = _registrationService.CurrentUserMachine.InventorizationData.MotherBoardSerialNumber;
                var messages = await _messageService.GetMessages(currentUser, MotherBoarderialNumber);
                var tempCollection = new ObservableCollection<Message>();
                if (messages != null)
                    foreach (var item in messages)
                    {
                        if (item != null)
                            tempCollection.Add(item);
                    }
                Messages = tempCollection;
            }
            await Task.Delay(2000);
            GetMessages();
        }
        private bool CanSend()
        {
            if (!string.IsNullOrEmpty(MessageBody) && _registrationService.CurrentUserMachine != null)
                return true;
            return false;
        }
        public CheckConnectionService CheckConnection
        {
            get => _checkConnectionService;
        }

        public AnyDeskService AnyDesk
        {
            get => _anyDeskService;
        }

        public RegistrationService Registration
        {
            get => _registrationService;
        }

    }
}
