using DevExpress.Mvvm;
using ICAdmin.Services;
using ICAdmin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICAdmin.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly CheckConnectionService _connectionService;

        public Page PageSource { get; set; }

        public MainWindowViewModel(PageService pageService, CheckConnectionService checkConnectionService)
        {
            _pageService = pageService;
            _connectionService = checkConnectionService;

            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new LoginPage());
        }

        public CheckConnectionService CheckConnection
        {
            get
            {
                return _connectionService;
            }
        }

    }
}
