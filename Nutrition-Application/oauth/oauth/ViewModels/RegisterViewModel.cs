using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using oauth.ViewModels;
using oauth.Models;
using oauth.Services;
using Xamarin.Essentials;
using oauth.Views;

namespace oauth.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public UsersModel TheTask { get; set; }
        public ICommand SaveCommand { get; }

        bool _isNew;

        public event EventHandler SaveComplete;

        public RegisterViewModel(UsersModel theTask, bool isNew)
        {
            _isNew = isNew;

            TheTask = theTask;

            Title = isNew ? "New Task" : TheTask.Name;

            SaveCommand = new Command(async () => await ExecuteSaveCommand());
        }
        public string Email { set; get; }
        async Task ExecuteSaveCommand()
        {
            var email = await SecureStorage.GetAsync("Email");
            Email = email;
            var mongoService = new AuthenticationMongoService();
            if (mongoService.IsExist(Email) == false)
            {
                await mongoService.CreateUser(TheTask);
                App.Current.MainPage = new HomePage();

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", Email, "تم");
                App.Current.MainPage = new LoginPage();

            }

  SaveComplete?.Invoke(this, new EventArgs());


        }
    }
}
