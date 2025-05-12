using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dash : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            // Set the username label text
            usernamelabel.Text = "Hello, " + Session["nama_user"].ToString();
        }
    }

    private bool IsLoggedIn()
    {
        return Session["username"] != null;
    }
}
