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

namespace oauth.Views.QuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityLevel : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public ActivityLevel()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel {  QuestionnairName = "خامل : لا اقوم باي نشاط حيوي" },
                new QuestionnaireModel {  QuestionnairName = "قليل النشاط : احيانا اقوم بالمشي" } ,
                                new QuestionnaireModel {  QuestionnairName = "متوسط النشاط : هتمرن بمعدل 1-2 في الاسبوع" } ,
                                new QuestionnaireModel { QuestionnairName = "نشيط : اتمرن بمعدل 3-5 مرات في الاسبوع" } ,
                                new QuestionnaireModel { QuestionnairName = "نشيط جدا : اتمرن بمعدل 7 مرات في الاسبوع" } ,



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


            var mongodb = new QuestionnaireMongoServices();
            await mongodb.UpdateActivityLevelList(myListOfFruits);
            App.Current.MainPage = new Healthy_LifestyleView();  
            myListOfFruits.Clear();


        }




        private async void nextBtn_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new Healthy_LifestyleView();   
            myListOfFruits.Clear();



        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {


            App.Current.MainPage = new Measurements();    
            myListOfFruits.Clear();


        }


    }
}