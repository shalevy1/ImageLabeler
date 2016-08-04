using ImageLabeller.Models;
using ImageLabeller.Properties;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageLabeller
{
    public partial class _default : System.Web.UI.Page
    {
        public MongoDatabase mongoDatabase;
        public MongoCollection ResponseCollection;
        public bool serverIsDown = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            var mongoClient = new MongoClient("mongodb://<dbuser>:<dbpassword>@ds145355.mlab.com:45355/justurl"); // Connection String
            var server = mongoClient.GetServer();
            mongoDatabase = server.GetDatabase("justurl"); // Database Name 

            ResponseCollection = mongoDatabase.GetCollection("Url"); // Collection name

            // Check Server Status
            try
            {
                mongoDatabase.Server.Ping();
            }
            catch (Exception ex)
            {
                serverIsDown = true;
            }

            image.ImageUrl = getImageUrl();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string label = radioButtonList.SelectedValue;

            //Image and label will be stored in GridFS

        }

        public string getImageUrl()
        {
            var urls = (Url)ResponseCollection.FindOneAs(typeof(Url), Query.NE("id", "null")); // This code gives first record on db.
            return urls.image;

        }
    }
}