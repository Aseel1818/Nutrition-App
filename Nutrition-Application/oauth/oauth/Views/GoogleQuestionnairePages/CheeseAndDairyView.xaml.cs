using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.ObjectModel;
using oauth.Models;
using oauth.Services;

namespace oauth.Views.GoogleQuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheeseAndDairyView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public CheeseAndDairyView()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "milk.png" , QuestionnairName = "حليب " },
                new QuestionnaireModel { QuestionnairImage = "Yogurt.png" , QuestionnairName = "زبادي" } ,
                                new QuestionnaireModel { QuestionnairImage = "cottageCheese.png" , QuestionnairName = "جبن قريش" } ,
                                new QuestionnaireModel { QuestionnairImage = "whiteCheese.png" , QuestionnairName = "جبن أبيض بلدي" }  ,

                                new QuestionnaireModel { QuestionnairImage = "mozzarella.png" , QuestionnairName = "موزوريلا" } ,
                                new QuestionnaireModel { QuestionnairImage = "egg.png" , QuestionnairName = "بيض" }

        };
            myCollectionView.ItemsSource = Fruits;


            myCollectionView.SelectionChanged += myCollectionView_SelectionChanged;

        }

        public static List<string> myListOfFruits = new List<string>();
        public static List<string> myListOfFruits2 = new List<string>();
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
            var newList = myListOfFruits.Concat(myListOfFruits2);

            var mongodb = new QuestionnaireMongoServicesGoogle();

            if (newList.Contains("أعاني من حساسية لاكتوز"))
            {

                await mongodb.UpdateCheeseAndDairyViewListGoogle(myListOfFruits2);

                App.Current.MainPage = new NutsView();
                myListOfFruits2.Clear();

            }

            else
            {
                await mongodb.UpdateCheeseAndDairyViewListGoogle(myListOfFruits);
                App.Current.MainPage = new NutsView();
                myListOfFruits.Clear();

            }



        }


        private async void nextBtn_Tapped(object sender, EventArgs e)
        {
      
            App.Current.MainPage = new NutsView();
            myListOfFruits.Clear();
            myListOfFruits2.Clear();
        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {
         
            App.Current.MainPage = new meatView();
            myListOfFruits.Clear();
            myListOfFruits2.Clear();

        }

        int count = 0;

        private async void ButtonLactoseIntolerance(object sender, EventArgs e)
        {
            count++;
            int size;


            //Input size of the array
            size = 5;

            var content = (Button)sender;

            for (int i = 0; i < size; i++)
            {
                //If the current element of array is even then increment even count 
                if (count % 2 == 0)
                {
                    content.BackgroundColor = Color.Azure;
                    myListOfFruits2.Clear();
                }
                else
                {


                    content.BackgroundColor = Color.Orange;


                    // to avoid the duplicate when  save the selection , so we check if the item is not exist we won't 


                    if (myListOfFruits2.Contains("أعاني من حساسية لاكتوز") == false)
                    {
                        myListOfFruits2.Add("أعاني من حساسية لاكتوز");

                    }

                }
            }
        }
    }
}