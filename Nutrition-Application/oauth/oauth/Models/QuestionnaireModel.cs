using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace oauth.Models
{
    public class QuestionnaireModel : INotifyPropertyChanged
    {

        

        string _questionnairImage;
        [BsonElement("QuestionnairImage")]
        public string QuestionnairImage
        {
            get => _questionnairImage;
            set
            {
                if (_questionnairImage == value)
                    return;

                _questionnairImage = value;

                HandlePropertyChanged();
            }
        }




        string _questionnairName;
        [BsonElement("QuestionnairName")]
        public string QuestionnairName
        {
            get => _questionnairName;
            set
            {
                if (_questionnairName == value)
                    return;

                _questionnairName = value;

                HandlePropertyChanged();
            }
        }


        



        void HandlePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}