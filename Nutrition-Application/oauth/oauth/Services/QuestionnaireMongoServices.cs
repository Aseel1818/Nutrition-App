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

    public class QuestionnaireMongoServices
    {
        string dbName = "NormalUsers";           // Database Name

        string collectionName = "NormalUsers"; //  Collection Name

        IMongoCollection<UsersModel> usersCollection;
        IMongoCollection<UsersModel> UsersCollection
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
                    usersCollection = db.GetCollection<UsersModel>(collectionName, collectionSettings);
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

        public async Task UpdateFruitsList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;


            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);  
            var update = Builders<UsersModel>.Update.Set(item => item.FruitsList, choiceName);  

            var result = await UsersCollection.UpdateOneAsync(filter, update); 



        }

        public async Task UpdateMeatList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Meat, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);



        }


        public async Task UpdateVegetablesList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Vegetables, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateNutsList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Nuts, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateFoodSystemList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.FoodSystem, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateCarbohydratesList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Carbohydrates, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateCheeseAndDairyViewList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.CheeseAndDairy, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateHealthy_LifestyleViewList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.HealthyLifeStyle, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdatePerfectWeightList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.PerfectWeight, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateActivityLevelList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.ActivityLevel, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateWaterList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Water, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateGenderList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Gender, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        public async Task UpdateMeasurementsList(List<MeasurementsModel> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Measurements, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdatePregnancyList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Pregnancy, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }

        public async Task UpdateTargetList(List<string> choiceName)
        {
            var email = await SecureStorage.GetAsync("loggedEmail");
            LoginEmail = email;
            var filter = Builders<UsersModel>.Filter.Eq(item => item.Email, LoginEmail);
            var update = Builders<UsersModel>.Update.Set(item => item.Target, choiceName);
            var result = await UsersCollection.UpdateOneAsync(filter, update);

        }


        #endregion







    }
}
