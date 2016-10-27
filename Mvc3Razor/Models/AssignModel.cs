using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Mvc3Razor.Models {

    public class AssignModel 
    {
        public ObjectId _id { get; set; }

        [Required]        
        [Display(Name = "User")]
        public string User { get; set; }

        [Required]        
        [Display(Name = "Task")]
        public string Task { get; set; }

        [Required]        
        [Display(Name = "Priorized")]
        public string Priorized { get; set; }

         [Required]
        [DataType(DataType.Date)]
        public DateTime Due_Date { get; set; }

       
    }

    public class Assign 
    {
       
        MongoCollection AssignDetailCollection = null;        
        private IUserRepositary _configData = new UserRepositary();

        private List<AssignModel> _AssignList = new List<AssignModel>();

        public Assign() 
        {
            AssignDetailCollection = _configData.getCollectionAssignModel;
            if (AssignDetailCollection == null)
            {
                throw new ArgumentNullException("config Data Service");
            }
        }

        public IEnumerable<AssignModel> GetAllTask()
        {
            if (Convert.ToInt32(AssignDetailCollection.Count()) > 0)
            {
                _AssignList.Clear();

                var AllUsers = AssignDetailCollection.FindAs(typeof(AssignModel), Query.NE("Item", "null"));

                if (AllUsers.Count() > 0)
                {
                    foreach (AssignModel task in AllUsers)
                    {
                        _AssignList.Add(task);
                    }
                }
            }

            var result = _AssignList.AsQueryable();
            return result;
        }

        public AssignModel Add(AssignModel UM)
        {
            AssignDetailCollection.Save(UM);
            return UM;
        }

        public AssignModel GetTaskByID(string id)
        {
            AssignModel SearchTask = null;

            if (!string.IsNullOrEmpty(id))
            {
                SearchTask = (AssignModel)AssignDetailCollection.FindOneAs(typeof(AssignModel), Query.EQ("", id));
            }

            return SearchTask;
        }

        public bool Update(string objectid, AssignModel UM)
        {
            UpdateBuilder upBuilder = MongoDB.Driver.Builders.Update
                .Set("User", UM.User)
                .Set("Task", UM.Task)
                .Set("Priorized", UM.Priorized)
                .Set("Due_Date", UM.Due_Date);

            AssignDetailCollection.Update(Query.EQ("", objectid), upBuilder);

            return true;
        }

        public bool Remove(string objectid)
        {

            AssignDetailCollection.Remove(Query.EQ("", objectid));
            return true;
        }
      
    }         
}