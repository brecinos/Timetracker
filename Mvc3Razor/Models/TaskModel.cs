using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Mvc3Razor.Models {

    public class TaskModel 
    {
        public ObjectId _id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        [Display(Name = "Item")]
        public string Item { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Priorized")]
        public string Priorized { get; set; }

         [Required]
        [DataType(DataType.Date)]
        public DateTime Due_Date { get; set; }

       
    }

    public class Tasks 
    {
       
        MongoCollection TaskDetailCollection = null;        
        private IUserRepositary _configData = new UserRepositary();        

        private List<TaskModel> _TaskList = new List<TaskModel>();
        
        public Tasks() 
        {
            TaskDetailCollection = _configData.getCollectionTaskModel;
            if (TaskDetailCollection == null)
            {
                throw new ArgumentNullException("config Data Service");
            }
        }

        public IEnumerable<TaskModel> GetAllTask()
        {
            if (Convert.ToInt32(TaskDetailCollection.Count()) > 0)
            {
                _TaskList.Clear();

                var AllUsers = TaskDetailCollection.FindAs(typeof(TaskModel), Query.NE("Item", "null"));

                if (AllUsers.Count() > 0)
                {
                    foreach (TaskModel task in AllUsers)
                    {
                        _TaskList.Add(task);
                    }
                }
            }

            var result = _TaskList.AsQueryable();
            return result;
        }

        public TaskModel Add(TaskModel UM)
        {
            TaskDetailCollection.Save(UM);
            return UM;
        }

        public TaskModel GetTaskByID(string id)
        {
            TaskModel SearchTask = null;

            if (!string.IsNullOrEmpty(id))
            {
                SearchTask = (TaskModel)TaskDetailCollection.FindOneAs(typeof(TaskModel), Query.EQ("Item", id));
            }

            return SearchTask;
        }


        public bool Update(string objectid, TaskModel UM)
        {
            UpdateBuilder upBuilder = MongoDB.Driver.Builders.Update
                .Set("Item", UM.Item)
                .Set("Status", UM.Status)
                .Set("Priorized", UM.Priorized)
                .Set("Due_Date", UM.Due_Date);

            TaskDetailCollection.Update(Query.EQ("Item", objectid), upBuilder);

            return true;
        }


        public bool Remove(string objectid)
        {

            TaskDetailCollection.Remove(Query.EQ("Item", objectid));
            return true;
        }
      
    }         
}