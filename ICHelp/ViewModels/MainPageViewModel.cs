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
        public ObservableCollection<Message> Messages { get; set; }

        public MainPageViewModel()
        {
            Messages = new ObservableCollection<Message>();

        }

        private CheckConnectionService checkConnection = CheckConnectionService.GetInstance();
        public CheckConnectionService CheckConnection
        {
            get => checkConnection;
        }

        private AssignmentService assignmentService = AssignmentService.GetInstance();
        public AssignmentService AssignmentService
        {
            get => assignmentService;
        }
    }
}
