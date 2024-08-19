using oauth.Models;
using oauth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views.QuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NutsView : ContentPage
    {
      
        ObservableCollection<QuestionnaireModel> Nuts;

        public NutsView()
        {
            InitializeComponent();
            Nuts = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "almonds.png" , QuestionnairName = "لوز" },
                new QuestionnaireModel { QuestionnairImage = "walnut.png" , QuestionnairName = "جوز" } ,
                                new QuestionnaireModel { QuestionnairImage = "hazelnut.png" , QuestionnairName = "بندق" } ,
                                new QuestionnaireModel { QuestionnairImage = "cashew.png" , QuestionnairName = "كاجو" }  ,

                                new QuestionnaireModel { QuestionnairImage = "pecan.png" , QuestionnairName = "بيكان" } ,
                                new QuestionnaireModel { QuestionnairImage = "seedIntention.png" , QuestionnairName = "بذرة نية" }

        };
            myCollectionView.ItemsSource = Nuts;


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

        private  async void Button_Clicked(object sender, EventArgs e)
        {

            var mongodb = new QuestionnaireMongoServices();

            await mongodb.UpdateNutsList(myListOfFruits);
           
            App.Current.MainPage = new WaterView();
            myListOfFruits.Clear();
        }


        private async void nextBtn_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new WaterView();
            myListOfFruits.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new CheeseAndDairyView();
            myListOfFruits.Clear();

        }




    }
}