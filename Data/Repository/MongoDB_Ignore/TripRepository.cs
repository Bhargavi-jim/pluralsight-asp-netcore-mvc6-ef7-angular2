/*
    Mongo C# 2.0 driver doesn't support Core CLR yet? Can't get this to work.
    Will revisit next time.
    http://mongodb.github.io/mongo-csharp-driver/2.0/getting_started/installation/

using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MyWorld.Data.Models;
using MyWorld.Data.Repository.Interfaces;

namespace MyWorld.Data.Repository
{
    public class TripRepository : ITripRepository, IDisposable
    {
        //private MongoServer mongoServer = null;
        //MongoClient client = new MongoClient(ConfigurationManager.AppSettings["connectionString"]);
        //MongoServer server = client.GetServer();

        private bool disposed = false;
        private string connectionString = "";
        private string dbName = "";
        private string collectionName = "";

        public TripRepository()
        {
                
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                var collection = GetTasksCollection();
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (MongoConnectionException)
            {
                return Enumerable.Empty<Trip>();
            }
        }

        // // Creates a Task and inserts it into the collection in MongoDB.
        // public void CreateTask(MyTask task)
        // {
        //     var collection = GetTasksCollectionForEdit();
        //     try
        //     {
        //         collection.InsertOne(task);
        //     }
        //     catch (MongoCommandException ex)
        //     {
        //         string msg = ex.Message;
        //     }
        // }

        private IMongoCollection<Trip> GetTasksCollection()
        {
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Trip>(collectionName);
            return todoTaskCollection;
        }

        private IMongoCollection<Trip> GetTasksCollectionForEdit()
        {
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Trip>(collectionName);
            return todoTaskCollection;
        }
        
        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (mongoServer != null)
                    {
                         mongoServer.Disconnect();
                    }
                }
            }

            disposed = true;
        }

        # endregion
    }
}
*/
