using oauth.Models;
using oauth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views.GoogleQuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Measurements : ContentPage
    {
        public Measurements()
        {
            InitializeComponent();
        }

        public static List<MeasurementsModel> myListOfData = new List<MeasurementsModel>();

        private  async  void Button_Clicked(object sender, EventArgs e)
        {

            var obj = new MeasurementsModel { Age = TxtAge.Text, Height = TxtHeight.Text, Weight = TxtWeight.Text };
            myListOfData.Add(obj);
            var mongodb = new QuestionnaireMongoServicesGoogle();
            await mongodb.UpdateMeasurementsListGoogle(myListOfData);
            App.Current.MainPage = new ActivityLevel();
            myListOfData.Clear();




        }


        private async void nextBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new ActivityLevel();
            myListOfData.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new WaterView();
            myListOfData.Clear();

        }


    }
}