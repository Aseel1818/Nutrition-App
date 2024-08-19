using oauth.AuthHelpers;
using oauth.Models;
using oauth.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using oauth.Views.SaveGoogleUserData;
using oauth.Services;
using oauth.Views.QuestionnairePages;

namespace oauth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
         bool _isNew;
        LoginViewModel viewModel;
        Account account;
        AccountStore store;
        public LoginPage()
        {

            InitializeComponent();

            store = AccountStore.Create();

            viewModel = new LoginViewModel();
            BindingContext = viewModel;

            viewModel.Title = "My Tasks";
        }




        void Googlelogin_Button(System.Object sender, System.EventArgs e)
        {

            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = AppConstant.Constants.iOSClientId;
                    redirectUri = AppConstant.Constants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = AppConstant.Constants.AndroidClientId;
                    redirectUri = AppConstant.Constants.AndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(AppConstant.Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                AppConstant.Constants.Scope,
                new Uri(AppConstant.Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(AppConstant.Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

        }
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            GoogleUser user = null;

            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(AppConstant.Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();

                    user = JsonConvert.DeserializeObject<GoogleUser>(userJson);

                   await  SecureStorage.SetAsync("userJsonData", userJson);
                }

                if (user != null)
                {
                    Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                    App.Current.MainPage = new HomePage();

                }
                //// save user data from google
                Application.Current.Properties.Add("Id", user.Id);
                Application.Current.Properties.Add("FirstName", user.GivenName);
                Application.Current.Properties.Add("LastName", user.FamilyName);
                Application.Current.Properties.Add("DisplayName", user.name);
                Application.Current.Properties.Add("EmailAddress", user.Email);
                Application.Current.Properties.Add("ProfilePicture", user.Picture);
                

                var userData = await SecureStorage.GetAsync("userJsonData");

                await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
               
            }
        }






        protected  void create_Account_Button(object sender, EventArgs e)
        { 
            var myTask = new UsersModel();
            App.Current.MainPage = new RegisterPage(myTask,false);

           


        }

        protected  void login_Button(object sender, EventArgs e)
        {
            App.Current.MainPage=new LoginPage();
        }


      

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsRefreshing = true;
        }





        private async void BtnSignIn_Clicked_1Async(object sender, EventArgs e)
        {
            var mongoService = new AuthenticationMongoService();

            if (mongoService.IsExist(TxtEmail.Text) == true)
            {


                var userByEmail = mongoService.GetUserByEmail(TxtEmail.Text);

                if (userByEmail.Password == TxtPassword.Text)
                {
                    Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                    await SecureStorage.SetAsync("loggedEmail", TxtEmail.Text);


                    App.Current.MainPage = new HomePage();
                }
                else
                {
                    await DisplayAlert("تحذير", " كلمة المرور غير صحيحة ", "موافق");
                }
            }
            else
            {
                await DisplayAlert("تحذير", " البريد الإلكتروني غير مسجل في التطبيق  ", "موافق");
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new EnterEmailPage();
        }

        private void forgetPasswordTapped_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new EnterEmailPage();

        }
    }
}