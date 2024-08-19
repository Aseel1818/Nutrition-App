using oauth.AuthHelpers;
using oauth.Models;
using oauth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Essentials;
using oauth.Views.SaveGoogleUserData;
using oauth.Services;
using oauth.Views.QuestionnairePages;

namespace oauth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        Account account;
        AccountStore store;




        bool _isNew = false;
        RegisterViewModel _viewModel;

        public RegisterPage(UsersModel task, bool isNew)
        {
            InitializeComponent();
            store = AccountStore.Create();

            _isNew = isNew;

            _viewModel = new RegisterViewModel(task, isNew);
            _viewModel.SaveComplete += Handle_SaveComplete;

            BindingContext = _viewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _viewModel.SaveComplete -= Handle_SaveComplete;
        }

        async void Handle_SaveComplete(object sender, EventArgs eventArgs)
        {
            await DismissPage();
        }

        protected async void Handle_CancelClicked(object sender, EventArgs e)
        {
            await DismissPage();
        }

        async Task DismissPage()
        {
            if (_isNew)
                await DisplayAlert("", "لم يتم تخزين البيانات", "تم");

            else
                await DisplayAlert("", "تم تخزين بياناتك", "تم");
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            var mongoServices = new AuthenticationMongoService();
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;


                await SecureStorage.SetAsync("Email", TxtEmail.Text);


                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("تنبيه", "أدخل اسمك  ", "حسناً");
                    return;
                }

                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("تنبيه", " أدخل البريد الإلكتروني", "حسناً");
                    return;
                }



                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("تنبيه", "أدخل كلمة المرور ", "حسناً");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("تنبيه", "أدخل تأكيد كلمة المرور ", "حسناً");
                    return;
                }

                if (password != confirmPassword)
                {
                    await DisplayAlert("تنبيه", "كلمة المرور غير متطابقة ", "حسناً");
                    return;
                }


                if (password.Length < 6)
                {
                    await DisplayAlert("تنبيه", "يجب أن تحتوي كلمة المرور على 6 خانات على الأقل", "حسناً");
                    return;
                }

            }

            catch (Exception exception)
            {
                if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("تحذير", "EMAIL_EXISTS", "حسناً");
                }
                else
                {
                    await DisplayAlert("خطأ", exception.Message, "حسناً");
                }
            }


            var mongodb = new AuthenticationMongoService();

            if (TxtPassword.Text == TxtConfirmPass.Text)
            {
                if (mongodb.IsExist(TxtEmail.Text) == false)
                {
                  await mongodb.CreateUser(new UsersModel { Email = TxtEmail.Text, Name = TxtName.Text, Password = TxtPassword.Text });
                  
                  await  DisplayAlert("", "لقد تم تخزين بياناتك", "موافق");

                  Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                await    SecureStorage.SetAsync("loggedEmail", TxtEmail.Text);


                    App.Current.MainPage = new foodSystem();
                }
                else
                {
                    await DisplayAlert("تحذير", "الايميل مسجل مسبقاً", "حسناً");
                }
            }
        
            
        }










        protected  void create_Account_Button(object sender, EventArgs e)
        {
            var myTask = new UsersModel();
           

            Application.Current.MainPage = new RegisterPage(myTask, true);


        }

        protected void login_Button(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }








        void ButtonGoogle_Clicked(System.Object sender, System.EventArgs e)
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
                 await   SecureStorage.SetAsync("userJsonData", userJson);
                }

                if (user != null)
                {
                    var myUser = new GoogleUser();
                    App.Current.MainPage=new  SaveGoogleDataPage(myUser, true);
                  

                }
                //////// to save the data of user from google 
                Application.Current.Properties.Add("Id", user.Id);
                Application.Current.Properties.Add("FirstName", user.GivenName);
                Application.Current.Properties.Add("LastName", user.FamilyName);
                Application.Current.Properties.Add("DisplayName", user.name);
                Application.Current.Properties.Add("EmailAddress", user.Email);
                Application.Current.Properties.Add("ProfilePicture", user.Picture);

                var userData = await SecureStorage.GetAsync("userJsonData");

                await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
            //    await store.SaveAsync(account=null, AppConstant.Constants.AppName);
              
            }
        }


    }
}