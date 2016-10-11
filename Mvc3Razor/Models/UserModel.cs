using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Mvc3Razor.Models {
    public class UserModel 
    {
        public ObjectId _id { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 3)]
        [Display(Name = "User Name")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed")]
        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required()]
        public string City { get; set; }

        // adding new employee code for time tracking
        [Required]
        [StringLength(4, MinimumLength=2)]
        public string EmployeeCode { get; set; }
        
    }

    public class Users {

        ObjectId id = new ObjectId();
        MongoClient client = null;
        MongoServer server = null;
        MongoDatabaseSettings ser = null;
        MongoDatabase database = null;
        MongoCollection UserDetailscollection = null;

        string connectionString = "mongodb://localhost";
        private List<UserModel> _UserList = new List<UserModel>();

        public Users() 
        {                  

            try
            {         
                server = MongoServer.Create(connectionString);
                var blog = server.GetDatabase("blog");
                UserDetailscollection = blog.GetCollection<UserModel>("UserModel");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public IEnumerable<UserModel> GetAllUsers()
        {
            if (Convert.ToInt32(UserDetailscollection.Count()) > 0)
            {
                _UserList.Clear();

                var AllUsers = UserDetailscollection.FindAs(typeof(UserModel), Query.NE("UserName", "null"));

                if (AllUsers.Count() > 0)
                {
                    foreach (UserModel user in AllUsers)
                    {
                        _UserList.Add(user);
                    }
                }
            }

            var result = _UserList.AsQueryable();
            return result;
        }

        public UserModel Add(UserModel UM)
        {
            UserDetailscollection.Save(UM);
            return UM;
        }     

        public List<UserModel> _usrList = new List<UserModel>();

        public bool Update(string objectid, UserModel UM)
        {
            UpdateBuilder upBuilder = MongoDB.Driver.Builders.Update
                .Set("UserName", UM.UserName)
                .Set("FirstName", UM.FirstName)
                .Set("LastName", UM.LastName)
                .Set("City", UM.City)
                .Set("EmployeeCode", UM.EmployeeCode);

            UserDetailscollection.Update(Query.EQ("UserName", objectid), upBuilder);

            return true;
        }     
             

        public bool Remove(string objectid) 
        {

            UserDetailscollection.Remove(Query.EQ("UserName", objectid));
            return true; 
        }

        public UserModel GetUserByID(string id)
        {
            UserModel SearchUser = null;

            if (!string.IsNullOrEmpty(id))
            {
                SearchUser = (UserModel)UserDetailscollection.FindOneAs(typeof(UserModel), Query.EQ("UserName", id));
            }

            return SearchUser;
        }     
       

    }
}