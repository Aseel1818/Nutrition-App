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
    public partial class TargetView : ContentPage
    {
        ObservableCollection<QuestionnaireModel> TargetCollection;
   

       
        public TargetView()
        {
            InitializeComponent();
            TargetCollection = new ObservableCollection<QuestionnaireModel> { new QuestionnaireModel {  QuestionnairName = "انقاص الوزن" },
                new QuestionnaireModel {  QuestionnairName = "زيادة الوزن" } ,
                new QuestionnaireModel {  QuestionnairName = "بناء عضلات" } ,
                new QuestionnaireModel { QuestionnairName = "البدء في اسلوب حياة صحي" },
                new QuestionnaireModel { QuestionnairName = "أريد أن أستفيد من البرنامج الخاص بتغذية المرأة الحامل" }

        };

          



          
            myCollectionView.ItemsSource = TargetCollection;

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
        private async void nextBtn_Tapped(object sender, EventArgs e)
        {
            myListOfFruits.Clear();
            App.Current.MainPage = new HomePage();

        }



        private void BackBtn_Tapped(object sender, EventArgs e)
        {           
           

            App.Current.MainPage = new GenderView();
            myListOfFruits.Clear();
        }







        private async void Button_Clicked(object sender, EventArgs e)
        {

            var mongodb = new QuestionnaireMongoServices();

            await mongodb.UpdateTargetList(myListOfFruits);

            if (myListOfFruits.Contains("أريد أن أستفيد من البرنامج الخاص بتغذية المرأة الحامل") == true)
            {
                App.Current.MainPage = new Pregnancy();
                myListOfFruits.Clear();


            }
            else
            {

                App.Current.MainPage = new HomePage();

               myListOfFruits.Clear();

            }


        }
    }
}