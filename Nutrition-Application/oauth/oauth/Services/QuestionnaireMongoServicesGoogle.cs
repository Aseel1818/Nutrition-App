using System;
using System.Security.Authentication;
using MongoDB.Driver;
using System.Threading.Tasks;
using oauth.Models;
using Xamarin.Essentials;
using System.Collections.Generic;
using MongoDB.Bson;

/// <summary>
/// This file contains the connection with azure Cosmos mongodb 
/// and Update opeartin wich we used to update user password
/// </summary>


namespace oauth.Services
{

    public class QuestionnaireMongoServicesGoogle
    {
        string dbName = "GoogleUsers";           // Database Name

        string collectionName = "GoogleUsers"; //  Collection Name

        IMongoCollection<GoogleUser> usersCollection;
        IMongoCollection<GoogleUser> UsersCollection
        {
            get
            {
                if (usersCollection == null)
                {
                    var mongoUrl = new MongoUrl("mongodb://cherrytech:XPIvswyjT5RvKCRhA4wdP9QAKDfk0iFNmJQjbHdabWDAvnRdVPUyj0iuclT7VZHFybupLT3eMXt4JSoUl8Kt4g==@cherrytech.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cherrytech@");

                    // APIKeys.Connection string is found in the portal under the "Connection String" blade
                    MongoClientSettings settings = MongoClientSettings.FromUrl(mongoUrl);



                    settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };


                    settings.RetryWrites = false;

                    // Initialize the client
                    var mongoClient = new MongoClient(settings);

                    // This will create or get the database
                    var db = mongoClient.GetDatabase(dbName);

                    // This will create or get the collection
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    usersCollection = db.GetCollection<GoogleUser>(collectionName, collectionSettings);
                }
                return usersCollection;
            }
        }







        public bool IsExist(string email)
        {
            bool exists = UsersCollection.Find(user => user.Email == email).Any();
            return exists;
        }




        #region Update Functions

        public string LoginEmail { set; get; }

        public async Task UpdateFruitsListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;


            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.FruitsList, choiceName);

            var result = await UsersCollection.UpdateOneAsync(filter, update);



        }

        public async Task UpdateMeatListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Meat, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);



        }


        public async Task UpdateVegetablesListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Vegetables, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateNutsListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Nuts, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateFoodSystemListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.FoodSystem, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateCarbohydratesListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Carbohydrates, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateCheeseAndDairyViewListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.CheeseAndDairy, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateHealthy_LifestyleViewListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.HealthyLifeStyle, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdatePerfectWeightListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.PerfectWeight, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateActivityLevelListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.ActivityLevel, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateWaterListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Water, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateGenderListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Gender, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateMeasurementsListGoogle(List<MeasurementsModel> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Measurements, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdatePregnancyListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Pregnancy, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateTargetListGoogle(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<GoogleUser>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<GoogleUser>.Update.Set(item => item.Target, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        #endregion







    }
}
