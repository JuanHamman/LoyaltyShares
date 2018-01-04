using LoyaltyShares.Services.Implementation;
using LoyaltyShares.Services.Interfaces;
using LoyaltyShares.ViewModels;
using LoyaltyShares.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LoyaltyShares
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var XamarinAuthHelper = Container.Resolve<IXamarinAuthHelper>();

            if (string.IsNullOrWhiteSpace(XamarinAuthHelper.GetUserDetail("FBToken")))
            {
                await NavigationService.NavigateAsync("NavigationPage/LoginPage");
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/MainPage");
            }

           
        }

        protected override void RegisterTypes()
        {
            //Views
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<LoginPage>();

            //Services
            Container.RegisterType<Services.Interfaces.IXamarinAuthHelper, XamarinAuthHelper>(new ContainerControlledLifetimeManager());
        }
    }
}
