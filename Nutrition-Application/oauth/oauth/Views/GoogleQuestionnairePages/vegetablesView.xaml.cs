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


namespace oauth.Views.GoogleQuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vegetablesView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public vegetablesView()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "bellPepper.png" , QuestionnairName = "فلفل رومي" },
                new QuestionnaireModel { QuestionnairImage = "broccoli.png" , QuestionnairName = "بروكلي" } ,
                                new QuestionnaireModel { QuestionnairImage = "carrots.png" , QuestionnairName = "جزر" } ,
                                new QuestionnaireModel { QuestionnairImage = "mushroom.png" , QuestionnairName = "مشروم" }  ,

                                new QuestionnaireModel { QuestionnairImage = "leafyVegetables.png" , QuestionnairName = "خضار ورقية" } ,
                                new QuestionnaireModel { QuestionnairImage = "pumpkin.png" , QuestionnairName = "يقطين" }

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

        private   async   void Button_Clicked(object sender, EventArgs e)
        {
            var mongodb = new QuestionnaireMongoServicesGoogle();
            await mongodb.UpdateVegetablesListGoogle(myListOfFruits);
            App.Current.MainPage = new meatView();
            myListOfFruits.Clear();


        }



        private  async void nextBtn_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new meatView();
            myListOfFruits.Clear();

        }

        private  async void BackBtn_Tapped(object sender, EventArgs e)
        {
            string foodSystem = await SecureStorage.GetAsync("googleFoodsystem");

            if (foodSystem == "متوازن")
            {

                App.Current.MainPage = new Fruits();

            }

            if (foodSystem == "كيتو")
            {

                App.Current.MainPage = new foodSystem();

            }
            myListOfFruits.Clear();

        }


    }
}