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
    public partial class Pregnancy : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public Pregnancy()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel {  QuestionnairName = "6" },
                new QuestionnaireModel {  QuestionnairName = "5" } ,
                                new QuestionnaireModel { QuestionnairName = "4" } ,
                                new QuestionnaireModel {  QuestionnairName = "3" }  ,

                                new QuestionnaireModel { QuestionnairName = "2" } ,
                                new QuestionnaireModel { QuestionnairName = "1" },
                                new QuestionnaireModel {  QuestionnairName = "12" },
                new QuestionnaireModel {  QuestionnairName = "11" } ,
                                new QuestionnaireModel { QuestionnairName = "10" } ,
                                new QuestionnaireModel {  QuestionnairName = "9" }  ,

                                new QuestionnaireModel { QuestionnairName = "8" } ,
                                new QuestionnaireModel { QuestionnairName = "7" },
                                new QuestionnaireModel {  QuestionnairName = "18" },
                new QuestionnaireModel {  QuestionnairName = "17" } ,
                                new QuestionnaireModel { QuestionnairName = "16" } ,
                                new QuestionnaireModel {  QuestionnairName = "15" }  ,
                                new QuestionnaireModel { QuestionnairName = "14" } ,
                                new QuestionnaireModel { QuestionnairName = "13" },


                                new QuestionnaireModel {  QuestionnairName = "24" },
                new QuestionnaireModel {  QuestionnairName = "23" } ,
                                new QuestionnaireModel { QuestionnairName = "22" } ,
                                new QuestionnaireModel {  QuestionnairName = "21" }  ,

                                new QuestionnaireModel { QuestionnairName = "20" } ,
                                new QuestionnaireModel { QuestionnairName = "19" },

                                new QuestionnaireModel {  QuestionnairName = "30" },
                new QuestionnaireModel {  QuestionnairName = "29" } ,
                                new QuestionnaireModel { QuestionnairName = "28" } ,
                                new QuestionnaireModel {  QuestionnairName = "27" }  ,

                                new QuestionnaireModel { QuestionnairName = "26" } ,
                                new QuestionnaireModel { QuestionnairName = "25" },
                                new QuestionnaireModel {  QuestionnairName = "36" },
                new QuestionnaireModel {  QuestionnairName = "35" } ,
                                new QuestionnaireModel { QuestionnairName = "34" } ,
                                new QuestionnaireModel {  QuestionnairName = "33" }  ,

                                new QuestionnaireModel { QuestionnairName = "32" } ,
                                new QuestionnaireModel { QuestionnairName = "31" }

        };
            myCollectionView.ItemsSource = Fruits;


            myCollectionView.SelectionChanged += myCollectionView_SelectionChanged;



        }
        public static List<string> myListOfFruits = new List<string>();
        public QuestionnaireModel MyFruits { get; set; }
        private  async   void myCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            await mongodb.UpdatePregnancyListGoogle(myListOfFruits);
           
            App.Current.MainPage = new HomePage(); 
            myListOfFruits.Clear();

        }


        private  async void nextBtn_Tapped(object sender, EventArgs e)
        {
          

            App.Current.MainPage = new HomePage(); 
            myListOfFruits.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new TargetView(); 
            myListOfFruits.Clear();

        }

    }
}