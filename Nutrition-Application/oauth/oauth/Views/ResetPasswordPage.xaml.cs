using oauth.Models;
using oauth.Services;
using oauth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ResetPasswordPage : ContentPage
    {
       
        bool _isNew;
        
        public  ResetPasswordPage(PasswordPropertyModel task, bool isNew)
        {
            InitializeComponent();

            save_button.IsVisible = true;

             _isNew = isNew;

           
            
        }
        async void Handle_SaveComplete(object sender, EventArgs eventArgs)
        {
            await Navigation.PushModalAsync(new HomePage());

        }
       





        private async void ButtonSaveNewPassword_Clicked(object sender, EventArgs e)
        {

            var mongodb = new ForgetPasswordMongoService();

            string newPassword = TxtNewPassword.Text;

            string confirmNewPassword = TxtConfirmNewPassword.Text;

           
            if (newPassword.Length < 6)
            {
                await DisplayAlert("تحذير", "الرجاء إدخال كلمة سر تحتوي على 6 خانات", "موافق");
            }
            else
            {


                if (string.IsNullOrEmpty(newPassword))

                {
                    await DisplayAlert("تحذير", " الرجاء إدخال كلمة السر الجديدة ", "موافق");

                }

                if (string.IsNullOrEmpty(confirmNewPassword))

                {

                    await DisplayAlert("تحذير", "الرجاء إدخال تأكيد كلمة السر الجديدة", "موافق");

                }


                if (confirmNewPassword != newPassword)
                {
                    await DisplayAlert("تحذير", "الكلمتان غير متطابقتان", "موافق");
                }
                else
                {
                   await mongodb.UpdatePassword(confirmNewPassword);

                    App.Current.MainPage=new HomePage();
                }

              


            }


        }
        private  void registerTap_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new EnterCodePage();

        }

        private void ButtonShow_Clicked(object sender, EventArgs e)
        {

             TxtNewPassword.IsPassword = !TxtNewPassword.IsPassword;
         


        }
    }
}