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
    public partial class foodSystem : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;


        public foodSystem()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "motawazen.png" , QuestionnairName = "متوازن" } ,
                new QuestionnaireModel { QuestionnairImage = "keto.png" , QuestionnairName = "كيتو" }
               

        };
            myCollectionView.ItemsSource = Fruits;


            myCollectionView.SelectionChanged += myCollectionView_SelectionChanged;

        }

        public static List<string> myListOfFruits = new List<string>();
        public QuestionnaireModel MyFruits { get; set; }

        public string FoodSystem;


        private void myCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection;


            for (int i = 0; i < current.Count; i++)
            {
                MyFruits = current[i] as QuestionnaireModel;

            }

            if (myListOfFruits.Contains($"{MyFruits.QuestionnairName}") == false)
            {
                FoodSystem = $"{MyFruits.QuestionnairName}";

                myListOfFruits.Add($"{MyFruits.QuestionnairName}");

            }

            SecureStorage.SetAsync("googleFoodsystem", FoodSystem);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            var mongodb = new QuestionnaireMongoServicesGoogle();
            await mongodb.UpdateFoodSystemListGoogle(myListOfFruits);


            if (myListOfFruits.Contains("متوازن") == true)
            {


                App.Current.MainPage = new CarbohydratesQ();

            }



            if (myListOfFruits.Contains("كيتو") == true)
            {
                App.Current.MainPage = new vegetablesView();


            }
            myListOfFruits.Clear();





        }


        private  async void nextBtn_Tapped(object sender, EventArgs e)
        {
            if (myListOfFruits.Contains("متوازن") == true)
            {

                App.Current.MainPage = new CarbohydratesQ();
            }

            if (myListOfFruits.Contains("كيتو") == true)
            {
                App.Current.MainPage = new vegetablesView();


            }
            myListOfFruits.Clear();

        }

        

    }
}