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

namespace oauth.Views.GoogleQuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Healthy_LifestyleView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public Healthy_LifestyleView()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel {  QuestionnairName = "اعتمد على الوجبات السريعة" },
                new QuestionnaireModel {  QuestionnairName = "اتناول المشروبات الغازية والعصائر المصنعة" } ,
                                new QuestionnaireModel {  QuestionnairName = "انام بشكل كافي  ( 6-8 ساعات ) يوميا " } ,
                                new QuestionnaireModel { QuestionnairName = "افرط في تناول الموالح والمخللات" } ,
                                new QuestionnaireModel { QuestionnairName = "اتناول الحلويات المصنعة بشكل يومي" } ,



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

        private   async     void Button_Clicked(object sender, EventArgs e)
        {
            var mongodb = new QuestionnaireMongoServicesGoogle();
            await mongodb.UpdateHealthy_LifestyleViewListGoogle(myListOfFruits);
          
            App.Current.MainPage = new PerfectWeight();
            myListOfFruits.Clear();
            



        }

        private  async void nextBtn_Tapped(object sender, EventArgs e)
        {
  
            App.Current.MainPage = new PerfectWeight();
           myListOfFruits.Clear();
        }         

        private void BackBtn_Tapped(object sender, EventArgs e)
        {
   
            App.Current.MainPage = new ActivityLevel();
         myListOfFruits.Clear();

        }




    }
}