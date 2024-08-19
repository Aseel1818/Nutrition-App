using oauth.Models;
using oauth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views.QuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenderView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> GenderCollection;


        public GenderView()
        {
            InitializeComponent();
            GenderCollection = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "male.png" , QuestionnairName = "ذكر" } ,
                new QuestionnaireModel { QuestionnairImage = "femal.png" , QuestionnairName = "أنثى" }


        };
            myCollectionView.ItemsSource = GenderCollection;


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


            var mongodb = new QuestionnaireMongoServices();

            await mongodb.UpdateGenderList(myListOfFruits);
            if (myListOfFruits[0] == "أنثى")
            {
              App.Current.MainPage = new TargetView();
            }
            else
            {
                App.Current.MainPage = new TargetViewMale();

            }

            myListOfFruits.Clear();

        }


        private async void nextBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new TargetView();   
            myListOfFruits.Clear();


        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new PerfectWeight();   
            myListOfFruits.Clear();


        }



    }
}


 