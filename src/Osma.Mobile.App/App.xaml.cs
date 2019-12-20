using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Hyperledger.Aries.Agents;
using Hyperledger.Aries.Configuration;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Osma.Mobile.App.Services.Interfaces;
using Osma.Mobile.App.Utilities;
using Osma.Mobile.App.ViewModels;
using Osma.Mobile.App.ViewModels.Account;
using Osma.Mobile.App.ViewModels.Connections;
using Osma.Mobile.App.ViewModels.CreateInvitation;
using Osma.Mobile.App.ViewModels.Credentials;
using Osma.Mobile.App.Views;
using Osma.Mobile.App.Views.Account;
using Osma.Mobile.App.Views.Connections;
using Osma.Mobile.App.Views.CreateInvitation;
using Osma.Mobile.App.Views.Credentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using MainPage = Osma.Mobile.App.Views.MainPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Osma.Mobile.App
{
    public partial class App : Application
    {
        public new static App Current => Application.Current as App;
        public Palette Colors;

        private readonly INavigationService _navigationService;
        private readonly IAgentProvider _contextProvider;
        private readonly IProvisioningService _provisioningService;
        public App(IContainer container)
        {
            InitializeComponent();

            Colors.Init();
            container.Resolve<IAgent>();
            _navigationService = container.Resolve<INavigationService>();
            _contextProvider = container.Resolve<IAgentProvider>();
            _provisioningService = container.Resolve<IProvisioningService>();

            InitializeTask = Initialize();
        }

        Task InitializeTask;
        private async Task Initialize()
        {
            //Pages
            _navigationService.AddPageViewModelBinding<MainViewModel, MainPage>();
            _navigationService.AddPageViewModelBinding<ConnectionsViewModel, ConnectionsPage>();
            _navigationService.AddPageViewModelBinding<ConnectionViewModel, ConnectionPage>();
            _navigationService.AddPageViewModelBinding<RegisterViewModel, RegisterPage>();
            _navigationService.AddPageViewModelBinding<AcceptInviteViewModel, AcceptInvitePage>();
            _navigationService.AddPageViewModelBinding<CredentialsViewModel, CredentialsPage>();
            _navigationService.AddPageViewModelBinding<CredentialViewModel, CredentialPage>();
            _navigationService.AddPageViewModelBinding<AccountViewModel, AccountPage>();
            _navigationService.AddPageViewModelBinding<CreateInvitationViewModel, CreateInvitationPage>();

            //await _provisioningService.ProvisionAgentAsync();
            //if (_contextProvider.Agent != null)
            //{
            Debug.WriteLine("Switching to MainViewModel");
            //await _navigationService.NavigateToAsync<MainViewModel>();
            await _navigationService.NavigateToAsync<MainViewModel>();
            Debug.WriteLine("Switched to MainViewModel");
            //}
            //else
            //{
            //    await _navigationService.NavigateToAsync<RegisterViewModel>();
            //}
        }

        protected override void OnStart()
        {
            #if !DEBUG 
                AppCenter.Start("ios=" + AppConstant.IosAnalyticsKey + ";" +
                                "android=" + AppConstant.AndroidAnalyticsKey + ";",
                        typeof(Analytics), typeof(Crashes));
            #endif
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
