using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace oauth.Models
{
    public class UsersModel : INotifyPropertyChanged
    {

        [BsonId(IdGenerator =typeof(CombGuidGenerator))]

        public Guid Id { get; set; }


        string _name;
        [BsonElement("Name")]
        public string Name
        {
            get => _name; set
            {
                if (_name == value)
                    return;

                _name = value;

                HandlePropertyChanged();
            }
        }




        string _password;
        [BsonElement("Password")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;

                _password = value;

                HandlePropertyChanged();
            }
        }


        string _email;
        [BsonElement("Email")]
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;

                _email = value;

                HandlePropertyChanged();
            }
        }


       



        List<string> _fruitsList;
        [BsonElement("FruitsList")]
        public List<string> FruitsList
        {
            get => _fruitsList;
            set
            {
                if (_fruitsList == value)
                    return;

                _fruitsList = value;

                HandlePropertyChanged();
            }
        }





        List<string>  _nuts;
        [BsonElement("Nuts")]
        public List<string> Nuts
        {
            get => _nuts;
            set
            {
                if (_nuts == value) 
                    return;

                _nuts = value;

                HandlePropertyChanged();
            }
        }



        List<string> _meat;
        [BsonElement("Meat")]
        public List<string> Meat
        {
            get => _meat;
            set
            {
                if (_meat == value)
                    return;

                _meat = value;

                HandlePropertyChanged();
            }
        }




        List<string> _vegetables;
        [BsonElement("Vegetables")]
        public List<string> Vegetables
        {
            get => _vegetables;
            set
            {
                if (_vegetables == value)
                    return;

                _vegetables = value;

                HandlePropertyChanged();
            }
        }








        List<string> _foodSystem;
        [BsonElement("FoodSystem")]
        public List<string> FoodSystem
        {
            get => _foodSystem;
            set
            {
                if (_foodSystem == value)
                    return;

                _foodSystem = value;

                HandlePropertyChanged();
            }
        }


        List<string> _carbohydrates;
        [BsonElement("Carbohydrates")]
        public List<string> Carbohydrates
        {
            get => _carbohydrates;
            set
            {
                if (_carbohydrates == value)
                    return;

                _carbohydrates = value;

                HandlePropertyChanged();
            }
        }

        List<string> _cheeseAndDairy;
        [BsonElement("CheeseAndDairy")]
        public List<string> CheeseAndDairy
        {
            get => _cheeseAndDairy;
            set
            {
                if (_cheeseAndDairy == value)
                    return;

                _cheeseAndDairy = value;

                HandlePropertyChanged();
            }
        }

        List<string> _water;
        [BsonElement("Water")]
        public List<string> Water
        {
            get => _water;
            set
            {
                if (_water == value)
                    return;

                _water = value;

                HandlePropertyChanged();
            }
        }

        List<string> _pregnancy;
        [BsonElement("Pregnancy")]
        public List<string> Pregnancy
        {
            get => _pregnancy;
            set
            {
                if (_pregnancy == value)
                    return;

                _pregnancy = value;

                HandlePropertyChanged();
            }
        }



        List<string> _gender;
        [BsonElement("Gender")]
        public List<string> Gender
        {
            get => _gender;
            set
            {
                if (_gender == value)
                    return;

                _gender = value;

                HandlePropertyChanged();
            }
        }




        List<string> _healthyLifeStyle;
        [BsonElement("HealthyLifeStyle")]
        public List<string> HealthyLifeStyle
        {
            get => _healthyLifeStyle;
            set
            {
                if (_healthyLifeStyle == value)
                    return;

                _healthyLifeStyle = value;

                HandlePropertyChanged();
            }
        }



        List<string> _perfectWeight;
        [BsonElement("PerfectWeight ")]
        public List<string> PerfectWeight
        {
            get => _perfectWeight;
            set
            {
                if (_perfectWeight == value)
                    return;

                _perfectWeight = value;

                HandlePropertyChanged();
            }
        }


        List<string> _activityLevel;
        [BsonElement("ActivityLevel")]
        public List<string> ActivityLevel
        {
            get => _activityLevel;
            set
            {
                if (_activityLevel == value)
                    return;

                _activityLevel = value;

                HandlePropertyChanged();
            }
        }




        List<MeasurementsModel> _measurements;
        [BsonElement("Measurements")]
        public List<MeasurementsModel> Measurements
        {
            get => _measurements;
            set
            {
                if (_measurements == value)
                    return;

                _measurements = value;

                HandlePropertyChanged();
            }
        }



        List<string> _target;
        [BsonElement("Target")]
        public List<string> Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                _target = value;

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