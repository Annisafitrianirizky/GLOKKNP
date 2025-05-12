using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Logout : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear authentication cookies
        FormsAuthentication.SignOut();

        // Clear session state
        Session.Abandon();

        // Redirect to login page
        Response.Redirect("Login.aspx");
    }
}