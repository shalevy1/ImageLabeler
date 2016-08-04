using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageLabeller.Models
{
    public class Url
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string image { get; set; }
    }
}