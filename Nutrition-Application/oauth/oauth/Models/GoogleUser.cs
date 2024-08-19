using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace oauth.Models
{
	[JsonObject]
	public class GoogleUser
	{
		[JsonProperty("id")]
		public string Id { get; set; }


		[JsonProperty("email")]
		public string Email { get; set; }


		[JsonProperty("verified_email")]
		public bool VerifiedEmail { get; set; }


		[JsonProperty("name")]
		public string name { get; set; }


		[JsonProperty("given_name")]
		public string GivenName { get; set; }


		[JsonProperty("family_name")]
		public string FamilyName { get; set; }


		[JsonProperty("link")]
		public string Link { get; set; }


		[JsonProperty("picture")]
		public string Picture { get; set; }

		

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





        List<string> _nuts;
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
