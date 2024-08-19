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
    public partial class meatView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> Fruits;

        public meatView()
        {
            InitializeComponent();
            Fruits = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "salmon.png" , QuestionnairName = "السلمون" },
                new QuestionnaireModel { QuestionnairImage = "chicken.png" , QuestionnairName = "الدجاج" } ,
                                new QuestionnaireModel { QuestionnairImage = "tuna.png" , QuestionnairName = "التونا" } ,
                                new QuestionnaireModel { QuestionnairImage = "meat.png" , QuestionnairName = "لحمة حمراء" }  

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
 public int count = 0;

        private async void VegetarrianButton_Clicked(object sender, EventArgs e)
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


                    if (myListOfFruits2.Contains("نباتي لا ارغب الأصناف المذكورة") == false)
                    {
                       myListOfFruits2.Add("نباتي لا ارغب الأصناف المذكورة");

                    }
                   
                }
            }
        }





        private  async    void Button_Clicked(object sender, EventArgs e)
        {
            var newList = myListOfFruits.Concat(myListOfFruits2);

            var mongodb = new QuestionnaireMongoServicesGoogle();

            if (newList.Contains("نباتي لا ارغب الأصناف المذكورة"))
            {

                await mongodb.UpdateMeatListGoogle(myListOfFruits2);

                App.Current.MainPage = new CheeseAndDairyView();
                myListOfFruits2.Clear();

            }

            else
            {
                await mongodb.UpdateMeatListGoogle(myListOfFruits);
                App.Current.MainPage = new CheeseAndDairyView();
                myListOfFruits.Clear();

            }





        }



        private async void nextBtn_Tapped(object sender, EventArgs e)
        {
          
            App.Current.MainPage = new CheeseAndDairyView(); 
            myListOfFruits.Clear();
            myListOfFruits2.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {
 

            App.Current.MainPage = new vegetablesView();  
            myListOfFruits.Clear();
            myListOfFruits2.Clear();

        }
       
    }
}