using ImageLabeller.Models;
using ImageLabeller.Properties;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageLabeller
{
    public partial class _default : System.Web.UI.Page
    {
        public MongoDatabase mongoDatabase;
        public MongoDatabase gridFSDatabase;

        public MongoCollection mongoCollection;
        public MongoCollection gridFsCollection;

        public static string ImgID { get; set; }
        public static string ImgUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ConnectDB();

            image.ImageUrl = GetImageUrl();
        }

        protected void btn_Click(object sender, EventArgs e)
        {

            string label = otherTextBox.Text != "" ? otherTextBox.Text : radioButtonList.SelectedValue;

            SaveImageToGridFs(label);

            var result = mongoCollection.Remove(Query.EQ("_id", BsonObjectId.Parse(ImgID)));

            Response.Redirect(Request.RawUrl);

        }

        private void ConnectDB()
        {
            try
            {
                var mongoClient = new MongoClient("mongodb://localhost"); // Connection String
                var server = mongoClient.GetServer();
                mongoDatabase = server.GetDatabase("justurl"); // Database Name 
                mongoCollection = mongoDatabase.GetCollection("Url"); // Collection name
            }
            catch (Exception)
            {
                Response.Write("Db Connection Error");
            }

        }

        public string GetImageUrl()
        {
            try
            {
                var url = (Url)mongoCollection.FindOneAs(typeof(Url), Query.NE("id", "null")); // This code gives first record on db.
                ImgUrl = url.image;
                ImgID = url._id;
                return ImgUrl;
            }
            catch (Exception)
            {
                radioButtonList.Visible = false;
                btn.Visible = false;
                Response.Write("Db Query Error");
                return "data:image/png;base64,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";
            }


        }

        private void SaveImageToGridFs(string label)
        {
            try
            {
                var mongoClient = new MongoClient("mongodb://localhost"); // Connection String
                var server = mongoClient.GetServer();
                gridFSDatabase = server.GetDatabase("gridfsimage"); // Database Name 

                // Get image from URL or API    
                WebRequest webRequest = System.Net.WebRequest.Create(ImgUrl);
                WebResponse webResponse = webRequest.GetResponse();
                Response.Write("Response length is " + webResponse.ContentLength + " bytes");

                // Copy from WebResponse to MemoryStream
                MemoryStream memoryStream;
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    memoryStream = new MemoryStream();

                    byte[] buffer = new byte[1024];
                    int byteCount;
                    do
                    {
                        byteCount = responseStream.Read(buffer, 0, buffer.Length);
                        memoryStream.Write(buffer, 0, byteCount);
                    } while (byteCount > 0);
                    responseStream.Close();
                }

                // Reset to beginning of stream
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Save to GridFS    
                var gridFsInfo = gridFSDatabase.GridFS.Upload(memoryStream, ImgUrl);
                string gridFsImageID = gridFsInfo.Id.ToString();

                gridFsCollection = gridFSDatabase.GetCollection("fs.files");

                UpdateBuilder updateBuilder = MongoDB.Driver.Builders.Update
                    .Set("category", label);
                gridFsCollection.Update(Query.EQ("_id", BsonObjectId.Parse(gridFsImageID)), updateBuilder);

            }
            catch (Exception err)
            {
                Response.Write("Something went wrong: " + err.Message);
            }
        }

        protected void skipBtn_Click(object sender, EventArgs e)
        {
            var queryResult = mongoCollection.Remove(Query.EQ("_id", BsonObjectId.Parse(ImgID)));
            Response.Redirect(Request.RawUrl);
        }

        protected void radioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            otherTextBox.Visible = radioButtonList.SelectedIndex ==3 ? true:false;
        }
    }
}
