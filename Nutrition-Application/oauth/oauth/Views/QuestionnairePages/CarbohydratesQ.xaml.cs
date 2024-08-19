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
    public partial class CarbohydratesQ : ContentPage
    {
        
        ObservableCollection<QuestionnaireModel> Fruits;

        public CarbohydratesQ()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "freekeh.png" , QuestionnairName = "الفريكة " },
                new QuestionnaireModel { QuestionnairImage = "rice.png" , QuestionnairName = "االارز" } ,
                                new QuestionnaireModel { QuestionnairImage = "pasta.png" , QuestionnairName = "المعكرونة" } ,
                                new QuestionnaireModel { QuestionnairImage = "bulgur.png" , QuestionnairName = "البرغل" }  ,

                                new QuestionnaireModel { QuestionnairImage = "potato.png" , QuestionnairName = "بطاطا" } ,
                                new QuestionnaireModel { QuestionnairImage = "bread.png" , QuestionnairName = "خبز" }

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
            await mongodb.UpdateCarbohydratesList(myListOfFruits);
            App.Current.MainPage = new Fruits();
            myListOfFruits.Clear();

        }

            
           
        





        private async void nextBtn_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new Fruits();
            myListOfFruits.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new foodSystem();  
            myListOfFruits.Clear();


        }

    }
}