using oauth.Models;
using oauth.Services;
using oauth.Views.QuestionnairePages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace oauth.Views.GoogleQuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfectWeight : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public PerfectWeight()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel {  QuestionnairName = "منذ سنة" },
                new QuestionnaireModel {  QuestionnairName = "منذ اكثر من ثلاث سنوات" } ,
                                new QuestionnaireModel {  QuestionnairName = "اكثر من خمس سنوات" } ,
                                new QuestionnaireModel { QuestionnairName = "لم احصل عليه ابدا" }

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
            await mongodb.UpdatePerfectWeightListGoogle(myListOfFruits);
            
            App.Current.MainPage = new GenderView();
            myListOfFruits.Clear();
        }


        private  async void nextBtn_Tapped(object sender, EventArgs e)
        {
          
            App.Current.MainPage = new GenderView();
            myListOfFruits.Clear();
        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {
     

            App.Current.MainPage = new Healthy_LifestyleView();  
            myListOfFruits.Clear();

        }


    }
}