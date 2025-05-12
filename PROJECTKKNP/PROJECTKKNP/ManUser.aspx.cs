using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ManUser : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "SELECT * FROM [INEXFOLER].[dbo].[user]";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    grc1.DataSource = dataTable;
                    grc1.DataBind();
                }
            }
        }
    }

    protected void txtsave_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO [INEXFOLER].[dbo].[user] ([id_user],[username],[nama_user],[password],[status]) VALUES (@Userid, @Username, @Nama, @password, @Status)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Userid", txtuserid.Text);
                    command.Parameters.AddWithValue("@Username", txtusername.Text);
                    command.Parameters.AddWithValue("@Nama", txtnama.Text);
                    command.Parameters.AddWithValue("@password", txtpassword.Text);
                    command.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('" + "Save Data Success " + "');window.location.href='ManUser.aspx';", true);
        }
        catch (Exception ex)
        {
            // Log the error or display it in a way that you can see it
            Response.Write(ex.Message.ToString());
        }
    }

    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = (sender as LinkButton).CommandArgument;

            string sql = "";
            sql = " DELETE FROM [INEXFOLER].[dbo].[user] where id_user=@userid ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmddel = new SqlCommand(sql, connection))
                {
                    cmddel.Parameters.AddWithValue("@userid", userid);
                    connection.Open();
                    cmddel.ExecuteNonQuery();
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('" + "Delete Data Success " + "');window.location.href='ManUser.aspx';", true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            txtsave.Visible = false;

            string userid = (sender as LinkButton).CommandArgument;

            LinkButton lnk = (LinkButton)sender;
            GridViewRow grc1 = (GridViewRow)lnk.NamingContainer;

            txtuserid.Text = grc1.Cells[0].Text;
            txtusername.Text = grc1.Cells[1].Text;
            txtnama.Text = grc1.Cells[2].Text;
            txtpassword.Text = grc1.Cells[3].Text;
            ddlstatus.SelectedValue = grc1.Cells[4].Text;

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            string sql = "";
            sql = " UPDATE [INEXFOLER].[dbo].[user] SET "
                + "[username] = @Username, [nama_user] = @Nama, [password] = @password, [status] = @Status" +
                " WHERE [id_user] = @Userid ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdupdt = new SqlCommand(sql, connection))
                {
                    cmdupdt.Parameters.AddWithValue("@Userid", txtuserid.Text);
                    cmdupdt.Parameters.AddWithValue("@Username", txtusername.Text);
                    cmdupdt.Parameters.AddWithValue("@Nama", txtnama.Text);
                    cmdupdt.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmdupdt.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
                    connection.Open();
                    cmdupdt.ExecuteNonQuery();
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('" + "Save Data Success " + "');window.location.href='ManUser.aspx';", true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
}