using System;
using System.Security.Authentication;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDB.Bson;
using oauth.Models;

namespace oauth.Services
{
    public class AuthenticationMongoService
    {
        string dbName = "NormalUsers";
        string collectionName = "NormalUsers";

        IMongoCollection<UsersModel> userCollection;
        IMongoCollection<UsersModel> UserCollection
        {
            get
            {
                if (userCollection == null)
                {
                    var mongoUrl = new MongoUrl("mongodb://cherrytech:XPIvswyjT5RvKCRhA4wdP9QAKDfk0iFNmJQjbHdabWDAvnRdVPUyj0iuclT7VZHFybupLT3eMXt4JSoUl8Kt4g==@cherrytech.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cherrytech@");

                    // APIKeys.Connection string is found in the portal under the "Connection String" blade
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      mongoUrl
                    );

                    settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                       

                    settings.RetryWrites = false;

                    // Initialize the client
                    var mongoClient = new MongoClient(settings);

                    // This will create or get the database
                    var db = mongoClient.GetDatabase(dbName);

                    // This will create or get the collection
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    userCollection = db.GetCollection<UsersModel>(collectionName, collectionSettings);
                }
                return userCollection;
            }
        }




        public UsersModel GetUserByEmail(string email)
        {

            var userByEmail = UserCollection.Find(user => user.Email.Equals(email)).FirstOrDefault();

            return userByEmail;
        }

        public bool IsExist(string email)
        {
            bool exists = UserCollection.Find(user => user.Email == email).Any();
            return exists;
        }
 public bool isEmpty()
        {
            bool x = UserCollection.Find(user => user.FruitsList == null).Any();

            return true;

        }
        #region Get Functions

        public async Task<List<UsersModel>> GetAllUsers()
        {
            try
            {
                var allTasks = await UserCollection
                    .Find(new BsonDocument())
                    .ToListAsync();

                return allTasks;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return null;
        }

       

        public async Task<UsersModel> GetUserById(Guid userId)
        {
            var singleTask = await UserCollection
                .Find(user => user.Id.Equals(userId))
                .FirstOrDefaultAsync();

            return singleTask;
        }

        #endregion

        

        #region Save/Delete Functions

        public async Task CreateUser(UsersModel user)
        {
            await UserCollection.InsertOneAsync(user);
        }

        public async Task UpdateTask(UsersModel user)
        {
            await UserCollection.ReplaceOneAsync(useritem => useritem.Id.Equals(user.Id), user);
        }

        public async Task DeleteTask(UsersModel user)
        {
            await UserCollection.DeleteOneAsync(useritem => useritem.Id.Equals(user.Id));
        }

        #endregion

    }
}
