using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Mvc3Razor.Models
{

    public class UserRepositary : IUserRepositary
    {        

        private MongoClient _client = null;        
        private MongoServer _server = null;        
        private MongoDatabase _database = null;
        private MongoCollection _UserDetailscollection = null;
        const string _ConnectionString = "mongodb://localhost/?safe=true";

        public UserRepositary()
        {
            try
            {                                
                _server = MongoServer.Create(_ConnectionString);
                _database = _server.GetDatabase("blog");                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        MongoCollection IUserRepositary.getCollection
        {
            get { 
                
                if (_UserDetailscollection == null)
                {
                    //load configured plan from DB
                    _UserDetailscollection = _database.GetCollection<UserModel>("UserModel");
                }

                return _UserDetailscollection;                        
               }                    
         }
    }
}     
