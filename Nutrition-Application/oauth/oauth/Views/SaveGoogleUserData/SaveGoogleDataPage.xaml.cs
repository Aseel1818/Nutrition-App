
using Xamarin.Forms;
using oauth.ViewModels;
using oauth.Models;
using System;
using Xamarin.Essentials;
using oauth.Services;
using oauth.Views.GoogleQuestionnairePages;
using Newtonsoft.Json;


namespace oauth.Views.SaveGoogleUserData
{
    public partial class SaveGoogleDataPage : ContentPage
    {
        GoogleViewModel _viewModel;
       
        bool _isNew;
        public SaveGoogleDataPage(GoogleUser user, bool isNew)
        {
            InitializeComponent();

           
        }
        public string jsonUserData { get; set; }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var userData = await SecureStorage.GetAsync("userJsonData");

            jsonUserData = userData;

            GoogleUser user = null;

            var mongoService = new GoogleMongoService();

            user = JsonConvert.DeserializeObject<GoogleUser>(jsonUserData);

            if (mongoService.IsExist(user.Email) == false)
            {
                var myUser = new QuestionnaireModel();

                await mongoService.CreateUser(user);

                await App.Current.MainPage.DisplayAlert("تحذير", "تم حفظ بياناتك", "موافق");
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                App.Current.MainPage = new foodSystem();

            }

            else
            {
                await App.Current.MainPage.DisplayAlert("تحذير", "هذا الإيميل مسجل مسبقاً", "موافق");


            }

        }
    }
}
