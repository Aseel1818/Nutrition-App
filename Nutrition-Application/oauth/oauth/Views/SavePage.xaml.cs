using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using oauth.ViewModels;
using oauth.Models;

namespace oauth.Views
{
    public partial class SavePage : ContentPage
    {
        bool _isNew;
        RegisterViewModel _viewModel ;

        public SavePage(UsersModel task, bool isNew)
        {
            InitializeComponent();

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
                await DisplayAlert("save" , "was complete ","ok"); 
            else
                await Navigation.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;





                if (string.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Warning", "Please enter your name ", "Cancel");
                    return;
                }

                if (string.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Please enter your email ", "Cancel");
                    return;
                }

                

                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Please enter your password ", "Cancel");
                    return;
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Warning", "Please enter confirm password ", "Cancel");
                    return;
                }

                if (password != confirmPassword)
                {
                    await DisplayAlert("Warning", "password not match .. ", "Cancel");
                    return;
                }

                
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("Warning", "EMAIL_EXISTS", "ok");
                }
                else
                {
                    await DisplayAlert("Errore", exception.Message, "ok");
                }
            }
        }

        protected async void create_Account_Button(object sender, EventArgs e)
        {
            var myTask = new UsersModel();
            var detailPage = new SavePage(myTask, true);

            await Navigation.PushModalAsync(new NavigationPage(detailPage));

        }

        protected async void login_Button(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}