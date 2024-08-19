using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            _ = Animate();
        }
        public async Task Animate()
        {
            imgLogo.Opacity = Opacity;
            await imgLogo.FadeTo(1, 4000);
            Application.Current.MainPage = new LoginPage();
        }
    }
}