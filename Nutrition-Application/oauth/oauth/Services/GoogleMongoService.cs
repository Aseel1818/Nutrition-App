using System;
using System.Security.Authentication;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Bson;
using oauth.AuthHelpers;
using Xamarin.Essentials;
using Newtonsoft.Json;
using oauth.Models;
using oauth.Views;
using oauth.Views.GoogleQuestionnairePages;

namespace oauth.Services
{
    public class GoogleMongoService
    {
        string dbName = "GoogleUsers";
        string collectionName = "GoogleUsers";

        IMongoCollection<GoogleUser> usersCollection;
        IMongoCollection<GoogleUser> UsersCollection
        {
            get
            {
                if (usersCollection == null)
                {
                    var mongoUrl = new MongoUrl("mongodb://cherrytech:XPIvswyjT5RvKCRhA4wdP9QAKDfk0iFNmJQjbHdabWDAvnRdVPUyj0iuclT7VZHFybupLT3eMXt4JSoUl8Kt4g==@cherrytech.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cherrytech@");

                    // APIKeys.Connection string is found in the portal under the "Connection String" blade
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      mongoUrl
                    );

                    settings.SslSettings =
                        new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

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

        public bool isEmpty()
        {
            bool x = UsersCollection.Find(user => user.FruitsList == null).Any();
            
                return true;
            
        }


        #region Save Functions

        public async Task CreateUser(GoogleUser googleUser)
        {
            var userEmail = await SecureStorage.GetAsync("userJsonData");

            GoogleUser user = JsonConvert.DeserializeObject<GoogleUser>(userEmail);

            if (IsExist(user.Email) == false)
            {
            await UsersCollection.InsertOneAsync(googleUser);
                App.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                await SecureStorage.SetAsync("loggedEmail", user.Email);

                var myUser = new QuestionnaireModel();

                App.Current.MainPage = new foodSystem();
             //   App.Current.MainPage = new HomePage();
                return;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("تحذير", "الايميل مسجل مسبقاً", "حسناً");
            }
        }
        #endregion

    }
}
