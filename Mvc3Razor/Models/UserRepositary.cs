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
        ObjectId id = new ObjectId();

        MongoClient client = null;
        
        MongoServer server = null;
        MongoDatabaseSettings ser = null;
        MongoDatabase database = null;
        MongoCollection UserDetailscollection = null;

        string connectionString = "mongodb://localhost";
        private List<UserModel> _UserList = new List<UserModel>();

        public UserRepositary()
        {
            try
            {

                //ObjectId id = new ObjectId();   
                const string ConnectionString = "mongodb://localhost/?safe=true";
                var server = MongoServer.Create(ConnectionString);
                var blog = server.GetDatabase("blog");
              
                var posts = blog.GetCollection<UserModel>("UserModel");

                var firstPost = new UserModel
                    {
                            UserName = "BenM",
                            FirstName = "Ben",
                            LastName = "Miller",
                            City = "Seattle",
                            EmployeeCode = "E101"
                        };   
          
                posts.Save(firstPost);                               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }     


}