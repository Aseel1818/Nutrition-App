using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace oauth.Models
{
    public class MeasurementsModel : INotifyPropertyChanged
    {

        string _height;
        [BsonElement("Height")]
        public string Height
        {
            get => _height;
            set
            {
                if (_height == value)
                    return;

                _height = value;

                HandlePropertyChanged();
            }
        }




        string _weight;
        [BsonElement("Weight")]
        public string Weight
        {
            get => _weight;
            set
            {
                if (_weight == value)
                    return;

                _weight = value;

                HandlePropertyChanged();
            }
        }




        string _age;
        [BsonElement("Age")]
        public string Age
        {
            get => _age;
            set
            {
                if (_age == value)
                    return;

                _age = value;

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