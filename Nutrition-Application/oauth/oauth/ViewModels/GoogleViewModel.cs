using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using oauth.Services;
using oauth.AuthHelpers;
using Xamarin.Essentials;
using Newtonsoft.Json;
using oauth.Views;
using oauth.Models;
using oauth.Views.GoogleQuestionnairePages;

namespace oauth.ViewModels
{
    public class GoogleViewModel : BaseViewModel
    {
        public GoogleUser TheUser { get; set; }
        public ICommand SaveCommand { get; }

        bool _isNew;

        public event EventHandler SaveComplete;

        public GoogleViewModel(GoogleUser theUser, bool isNew)
        {
            _isNew = isNew;

            TheUser = theUser;

            Title = isNew ? "New Task" : TheUser.name;

            SaveCommand = new Command(async () => await ExecuteSaveCommand());
        }

        public string jsonUserData { get;  set; }

        
       async Task ExecuteSaveCommand()
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

                App.Current.MainPage = new foodSystem();

            }

            else
            {
              await   App.Current.MainPage. DisplayAlert("تحذير", "هذا الإيميل مسجل مسبقاً", "موافق");


            }




            SaveComplete?.Invoke(this, new EventArgs());
        }
    }
}
