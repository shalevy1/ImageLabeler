using ImageLabeller.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Builders;
using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Driver.Core;
using System.Net;
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
        public static string ImgUrlNrml { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            ConnectDB();
            image.ImageUrl = GetImageUrl();


        }

        protected void btn_Click(object sender, EventArgs e)
        {

            string label = otherTextBox.Text != "" ? otherTextBox.Text : radioButtonList.SelectedValue;

            SaveImageToGridFs(label);

            //var result = mongoCollection.Remove(Query.EQ("_id", BsonObjectId.Parse(ImgID)));

            //Response.Redirect(Request.RawUrl);
            Page_Load(null, EventArgs.Empty);
        }



        private void ConnectDB()
        {
            try
            {
                var mongoClient = new MongoClient(""); // Connection String
                var server = mongoClient.GetServer();
                mongoDatabase = server.GetDatabase("11102016justurlbackup"); // Database Name 
                mongoCollection = mongoDatabase.GetCollection("Url"); // Collection name
                var countOfAdded = mongoCollection.Count(Query.EQ("category", "added"));
                var countOfAll = mongoCollection.Count(Query.EQ("category","notAdded"));
                lblCount.Text = (countOfAdded+11).ToString() + "/" + countOfAll.ToString();



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
                var url = (Url)mongoCollection.FindOneAs(typeof(Url), Query.EQ("category", "notAdded")); // This gives 

                ImgUrl = url.image;
                ImgID = url._id;
                ImgUrlNrml = url.image;

                HttpWebRequest request = WebRequest.Create(ImgUrl) as HttpWebRequest;

                // instruct the server to return headers only
                request.Method = "HEAD";

                // make the connection

                try
                {
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    HttpStatusCode status = response.StatusCode;
                }
                catch (Exception)
                {
                    UpdateBuilder updateBuilder2 = MongoDB.Driver.Builders.Update
       .Set("category", "notFound");
                    mongoCollection.Update(Query.EQ("_id", BsonObjectId.Parse(ImgID)), updateBuilder2);
                    Page_Load(null, EventArgs.Empty);

                }

                    if (ImgUrl.Contains("_normal")){
                        ImgUrl = ImgUrl.Replace("_normal", "");
                    }
                
                return ImgUrl;
            }
            catch (Exception)
            {
                radioButtonList.Visible = false;
                btn.Visible = false;
                Response.Write("Db Query Error");
                return "http://www.dioceseofmarquette.org/images/images/404.png";
            }
        }

        private void SaveImageToGridFs(string label)
        {
            try
            {
                var mongoClient = new MongoClient(""); // Connection String
                var server = mongoClient.GetServer();
                gridFSDatabase = server.GetDatabase("gridfs1"); // Database Name 

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

                UpdateBuilder updateBuilder2 = MongoDB.Driver.Builders.Update
                   .Set("category", "added");
                mongoCollection.Update(Query.EQ("_id", BsonObjectId.Parse(ImgID)), updateBuilder2);


                UpdateBuilder updateBuilder = MongoDB.Driver.Builders.Update
                    .Set("category", label);
                gridFsCollection.Update(Query.EQ("_id", BsonObjectId.Parse(gridFsImageID)), updateBuilder);




            }
            catch (Exception err)
            {
                Response.Write("Something went wrong: " + err.Message);
            }

            
        }

        private void DownloadDataFromGridFS()
        {
            ObjectId oid = new ObjectId("57ebabc091b6ce16d08cdfb8");
            var file = gridFSDatabase.GridFS.FindOne(Query.EQ("_id", oid));

            var newFileName = "D:\\new_Untitled.png";

            using (var stream = file.OpenRead())
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                using (var newFs = new FileStream(newFileName, FileMode.Create))
                {
                    newFs.Write(bytes, 0, bytes.Length);
                }
            }
        }



        protected void skipBtn_Click(object sender, EventArgs e)
        {
            var queryResult = mongoCollection.Remove(Query.EQ("_id", BsonObjectId.Parse(ImgID)));
            //Response.Redirect(Request.RawUrl);
            Page_Load(null, EventArgs.Empty);
        }

        protected void radioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // otherTextBox.Visible = radioButtonList.SelectedIndex ==3 ? true:false;
        }

        protected void btnPass_Click(object sender, EventArgs e)
        {
            if (txtPass.Text.ToString() == "Password")
            {
                login.Visible = false;
                form1.Visible = true;
            }
            else
            {
                login.Visible = true;
                form1.Visible = false;
            }


        }

        protected void reportImage_Click(object sender, EventArgs e)
        {

            var update = new UpdateDocument
            {
                {"$set",new BsonDocument("category","multipleImage") }
            };

            var query = new QueryDocument
            {
                {"image",ImgUrlNrml },
                {"category","notAdded"}
            };

            mongoCollection.Update(query, update, new MongoUpdateOptions
            {
                Flags = UpdateFlags.Multi
            }
            );

            Page_Load(null, EventArgs.Empty);

        }
    }
}
