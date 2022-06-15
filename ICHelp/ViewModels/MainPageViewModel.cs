using DevExpress.Mvvm;
using ICHelp.Models;
using ICHelp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICHelp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private CheckConnectionService _checkConnectionService;
        private PageService _pageService;
        private AssignmentService _assignmentService;
        private AuthorizationService _authorizationService;
        private RegistrationService _registrationService;
        private AnyDeskService _anyDeskService;
        public ObservableCollection<Message> Messages { get; set; }
        public Message SelectedMessage { get => GetValue<Message>(); set => SetValue(value); }


        public MainPageViewModel(PageService pageService, AnyDeskService anyDeskService, AssignmentService assignmentService, AuthorizationService authorizationService, RegistrationService registrationService, CheckConnectionService checkConnectionService)
        {
            Messages = new ObservableCollection<Message>();
            _checkConnectionService = checkConnectionService;
            _pageService = pageService;
            _assignmentService = assignmentService;
            _authorizationService = authorizationService;
            _registrationService = registrationService;
            _anyDeskService = anyDeskService;
            _registrationService.Start();
            Messages.Add(new Message() { Body = "Николай, добрый день, у меня возникла проблема!", Sender = SenderType.Employee, SendTime = DateTime.Now });
            Messages.Add(new Message() { Body = "Добрый день, хорошо, сейчас подключусь и посмотрю", Sender = SenderType.SysAdmin, SendTime = DateTime.Now });
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
                            var message = new Message() { Body = MessageBody, SendTime = DateTime.Now, Sender = SenderType.Employee };
                            SendAsync(message);
                            SelectedMessage = message;
                            MessageBody = String.Empty;
                        }
                    }, () => CanSend()));
            }
        }

        private async void SendAsync(Message message)
        {
            Messages.Add(message);
        }

        private bool CanSend()
        {
            if (!string.IsNullOrEmpty(MessageBody))
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
