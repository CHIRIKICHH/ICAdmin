using DevExpress.Mvvm;
using ICHelp.Models;
using ICHelp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public MainPageViewModel(PageService pageService, AnyDeskService anyDeskService, AssignmentService assignmentService, AuthorizationService authorizationService, RegistrationService registrationService, CheckConnectionService checkConnectionService)
        {
            Messages = new ObservableCollection<Message>();
            _checkConnectionService = checkConnectionService;
            _pageService = pageService;
            _assignmentService = assignmentService;
            _authorizationService = authorizationService;
            _registrationService = registrationService;
            _anyDeskService = anyDeskService;
        }

        public CheckConnectionService CheckConnection
        {
            get => _checkConnectionService;
        }

        public AnyDeskService AnyDesk
        {
            get => _anyDeskService;
        }

    }
}
