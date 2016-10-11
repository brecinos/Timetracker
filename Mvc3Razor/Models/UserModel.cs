﻿using System.ComponentModel.DataAnnotations;
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

        public Users() {

            _usrList.Add(new UserModel
            {
                UserName = "BenM",
                FirstName = "Ben",
                LastName = "Miller",
                City = "Seattle",
                EmployeeCode = "E101"
            });
            _usrList.Add(new UserModel
            {
                UserName = "AnnB",
                FirstName = "Ann",
                LastName = "Beebe",
                City = "Boston",
                EmployeeCode = "E102"
            });

            UserRepositary T= new UserRepositary();
            
        }

        public List<UserModel> _usrList = new List<UserModel>();

        public void Update(UserModel umToUpdate) {

            foreach (UserModel um in _usrList) {
                if (um.UserName == umToUpdate.UserName) {
                    _usrList.Remove(um);
                    _usrList.Add(umToUpdate);
                    break;
                }
            }
        }

        public void Create(UserModel umToUpdate) {
            foreach (UserModel um in _usrList) {
                if (um.UserName == umToUpdate.UserName) {
                    throw new System.InvalidOperationException("Duplicate username: " + um.UserName);
                }
            }
            _usrList.Add(umToUpdate);
        }

        public void Remove(string usrName) {

            foreach (UserModel um in _usrList) {
                if (um.UserName == usrName) {
                    _usrList.Remove(um);
                    break;
                }
            }
        }

        public UserModel GetUser(string uid) {
            UserModel usrMdl = null;

            foreach (UserModel um in _usrList)
                if (um.UserName == uid)
                    usrMdl = um;

            return usrMdl;
        }

    }
}