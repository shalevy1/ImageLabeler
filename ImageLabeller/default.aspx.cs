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
        protected void Page_Load(object sender, EventArgs e)
        {
            //ImageURL will be get mongoDb
            image.ImageUrl = "https://webassets.mongodb.com/_com_assets/global/mongodb-logo-white.png";
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string label = radioButtonList.SelectedValue;
            //Image and label will be stored in GridFS
        }
    }
}