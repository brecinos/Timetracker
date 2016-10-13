using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Mvc3Razor.Models
{
    public interface IUserRepositary
    {        
        MongoCollection getCollection { get; }        
    }
}