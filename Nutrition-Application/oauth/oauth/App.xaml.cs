using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using oauth.AuthHelpers;
using oauth.Views;
using oauth.Models;
using oauth.Views.QuestionnairePages;

namespace oauth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
            MainPage = new LoginPage();

               bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
                if (!isLoggedIn)
                {
                    //Load if Not Logged In
                }
                else
                {
                    //Load if Logged In
                    MainPage = new  HomePage();
                }
             

            


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
