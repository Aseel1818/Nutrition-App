using oauth.Models;
using oauth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace oauth.Views.GoogleQuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaterView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;
        public static Fruits SharedInstance { get; } = new Fruits();

        public WaterView()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel {  QuestionnairName = "اقل من نصف لتر يوميا" },
                new QuestionnaireModel {  QuestionnairName = "من نصف الى 2 لتر يوميا" } ,
                new QuestionnaireModel {  QuestionnairName = "اكثر من 2 لتر يوميا" }


        };
            myCollectionView.ItemsSource = Fruits;


            myCollectionView.SelectionChanged += myCollectionView_SelectionChanged;



        }
        public static List<string> myListOfFruits = new List<string>();
        public QuestionnaireModel MyFruits { get; set; }

        private void myCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection;


            for (int i = 0; i < current.Count; i++)
            {
                MyFruits = current[i] as QuestionnaireModel;

            }

            if (myListOfFruits.Contains($"{MyFruits.QuestionnairName}") == false)
            {
                myListOfFruits.Add($"{MyFruits.QuestionnairName}");

            }

        }


        private async void Button_Clicked(object sender, EventArgs e)
        {


            var mongodb = new QuestionnaireMongoServicesGoogle();
            await mongodb.UpdateWaterListGoogle(myListOfFruits);
            App.Current.MainPage = new Measurements();
            myListOfFruits.Clear();

        }

        private async void nextBtn_Tapped(object sender, EventArgs e)
        {



         
            App.Current.MainPage = new Measurements();
            myListOfFruits.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new NutsView();
            myListOfFruits.Clear();

        }

    }
}

