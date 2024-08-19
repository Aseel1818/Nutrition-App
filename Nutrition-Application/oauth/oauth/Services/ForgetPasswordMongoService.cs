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

    public class ForgetPasswordMongoService
    {
        string dbName = "NormalUsers";           // Database Name

        string collectionName = "NormalUsers"; //  Collection Name

        IMongoCollection<PasswordPropertyModel> usersCollection;
        IMongoCollection<PasswordPropertyModel> UsersCollection
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
                    usersCollection = db.GetCollection<PasswordPropertyModel>(collectionName, collectionSettings);
                }


                return usersCollection;
            }
        }


        

       
        public bool IsExist( string email)
        {
            bool exists = UsersCollection.Find(user => user.Email == email).Any();
            return exists;
        }

       


        #region Update Functions

        public string UserEmail { get; set; }   // This variable for store the Entry Email from User ( User Email ) to use it in update operation
        public string Password { get; set; }   //  This variable for store the New Password entered from user to use it in update opeation

        public async Task UpdatePassword(string password)
        {



            var userEmail = await SecureStorage.GetAsync("Email"); //// This variable retreive the value of User Email from secure storage

            UserEmail = userEmail;                                 // store the value of the above variable in this public variable to access it easily



            // This part contains the operation of Update for the user password

            var filter = Builders<PasswordPropertyModel>.Filter.Eq(item => item.Email, UserEmail);  ///  Filter to get the Document that have the User Email
            var update = Builders<PasswordPropertyModel>.Update.Set(item => item.Password, password);  ///  This line for put the value we want update

            var result = await UsersCollection.UpdateOneAsync(filter, update);  /// Update the password



        }


        #endregion







    }
}
