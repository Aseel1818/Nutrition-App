
using oauth.Models;
using oauth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views.QuestionnairePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Fruits : ContentPage
    {
        ObservableCollection<QuestionnaireModel> FruitsCollection;
 




        public Fruits()
        {
            InitializeComponent();





            FruitsCollection = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel { QuestionnairImage = "Asset7.png" , QuestionnairName = "توت بأنواعه " },
                                                                    new QuestionnaireModel { QuestionnairImage = "citrus.png" , QuestionnairName = "حمضيات" } ,
                                                                    new QuestionnaireModel { QuestionnairImage = "apple.png" , QuestionnairName = "تفاح" } ,
                                                                    new QuestionnaireModel { QuestionnairImage = "banana.png" , QuestionnairName = "موز" }  ,
                                                                    new QuestionnaireModel { QuestionnairImage = "watermelon.png" , QuestionnairName = "بطيخ" } ,
                                                                    new QuestionnaireModel { QuestionnairImage = "pineapple.png" , QuestionnairName = "أناناس" }

        };

           


            myCollectionView.ItemsSource = FruitsCollection;


            myCollectionView.SelectionChanged += myCollectionView_SelectionChanged;



        }



        public static List<string> myListOfFruits = new List<string> ();

        public QuestionnaireModel MyFruits { get; set; }


        private void myCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection;



            for (int i = 0; i < current.Count; i++)
            {
                MyFruits = current[i] as QuestionnaireModel;

            }

                

                if(myListOfFruits.Contains(MyFruits.QuestionnairName)==false)
                   {            
                
                   myListOfFruits.Add($"{MyFruits.QuestionnairName}");


                   }
           
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {


            var mongodb = new QuestionnaireMongoServices();
            await mongodb.UpdateFruitsList(myListOfFruits);
            App.Current.MainPage = new vegetablesView();

            myListOfFruits.Clear();


        }

        private async void nextBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new vegetablesView();
            myListOfFruits.Clear();

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new CarbohydratesQ();
            myListOfFruits.Clear();

        }

        
    }

        
    }


    


