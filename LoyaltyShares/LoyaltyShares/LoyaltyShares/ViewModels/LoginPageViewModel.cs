using LoyaltyShares.Helpers;
using LoyaltyShares.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;

namespace LoyaltyShares.ViewModels
{
	public class LoginPageViewModel : BindableBase
	{
        #region Private Members
        private IXamarinAuthHelper _xamrinAuthHelper;
        private IPageDialogService _pageDialogService;
        private INavigationService _navigationService;
        private string FBToken, UserName, Name, Surname , FBID;
        #endregion

        #region Public Members
        public DelegateCommand FacebookLoginCommand
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public LoginPageViewModel(IXamarinAuthHelper xamarinAuthHelper, IPageDialogService pageDialogService , INavigationService navigationService)
        {
            _pageDialogService = pageDialogService;
            _xamrinAuthHelper = xamarinAuthHelper;
            _navigationService = navigationService;

            FacebookLoginCommand = new DelegateCommand(FacebookLogin);
        }
        #endregion

        #region Methods
        private void FacebookLogin()
        {
            var authenticator = new OAuth2Authenticator
                   (
                       OAuthServerInfo.FacebookClientId,
                       OAuthServerInfo.FacebookScope,
                       OAuthServerInfo.FacebookAuthorizationEndpoint,
                       OAuthServerInfo.FacebookRedirectionEndpoint
                   );

            authenticator.Completed += Authenticator_Completed;
            authenticator.Error += Authenticator_Error;

            var presenter = new OAuthLoginPresenter();
            presenter.Login(authenticator);
        }


        #endregion

        #region OAuth Methods
        private async void Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;

            await _pageDialogService.DisplayAlertAsync("Attention", e.Message, "Ok");

            if (authenticator != null)
            {
                authenticator.Completed -= Authenticator_Completed;
                authenticator.Error -= Authenticator_Error;
            }
        }

        private async void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                FBToken = e.Account.Properties["access_token"];


                if (sender is OAuth2Authenticator authenticator)
                {
                    authenticator.Completed -= Authenticator_Completed;
                    authenticator.Error -= Authenticator_Error;
                }


                var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=email,id,first_name,last_name,gender"), null, e.Account);
                await request.GetResponseAsync().ContinueWith(async t =>
                {
                    if (t.IsFaulted)
                        await _pageDialogService.DisplayAlertAsync("Attention", t.Exception.InnerException.Message, "Ok");
                    else
                    {
                        var obj = JObject.Parse(t.Result.GetResponseText());

                        FBID = obj["id"].ToString().Replace("\"", "");
                        Name = obj["first_name"].ToString().Replace("\"", "");
                        Surname = obj["last_name"].ToString().Replace("\"", "");
                        UserName = Name + Surname;
                    }
                });

                _xamrinAuthHelper.SaveUserDetails(FBToken, Name,Surname, UserName);

                await _navigationService.NavigateAsync("NavigationPage/MainPage");
            }
        }
        #endregion

    }
}
