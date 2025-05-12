using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AdmKKN : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string sql = "select * from [INEXFOLER].[dbo].[kkn_h]";
        SqlCommand cmdget = new SqlCommand(sql, koneksi);
        SqlDataAdapter daget = new SqlDataAdapter(cmdget);
        DataTable dtget = new DataTable();
        daget.Fill(dtget);

        if (dtget.Rows.Count > 0)
        {
            grc1.DataSource = dtget;
            grc1.DataBind();
        }

        string sqlDosen = "SELECT nama_dosen FROM [INEXFOLER].[dbo].[dosen]";
        SqlCommand cmdDosen = new SqlCommand(sqlDosen, koneksi);
        SqlDataAdapter daDosen = new SqlDataAdapter(cmdDosen);
        DataTable dtDosen = new DataTable();
        daDosen.Fill(dtDosen);

        foreach (GridViewRow row in grc1.Rows)
        {
            DropDownList ddlDosen = (DropDownList)row.FindControl("ddldosen");
            ddlDosen.DataSource = dtDosen;
            ddlDosen.DataTextField = "nama_dosen";
            ddlDosen.DataBind();
        }
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string idKKN = btn.CommandArgument;
        Response.Redirect("AdmDetail.aspx?id_kkn=" + idKKN);
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DropDownList ddlDosen = (DropDownList)row.FindControl("ddldosen");

            if (ddlDosen != null)
            {
                if (ddlDosen.SelectedItem != null)
                {
                    string namaDosenValue = ddlDosen.SelectedItem.Text;

                    // Ensure DataKeys and RowIndex are valid
                    if (grc1.DataKeys != null && row.RowIndex >= 0 && row.RowIndex < grc1.DataKeys.Count)
                    {
                        int idkkn = Convert.ToInt32(grc1.DataKeys[row.RowIndex].Value);

                        string sql = "UPDATE [INEXFOLER].[dbo].[kkn_h] SET [nama_dosen] = @nama_dosen WHERE [id_kkn] = @idkkn";

                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                        {
                            connection.Open();

                            using (SqlCommand cmdupdt = new SqlCommand(sql, connection))
                            {
                                cmdupdt.Parameters.AddWithValue("@nama_dosen", namaDosenValue);
                                cmdupdt.Parameters.AddWithValue("@idkkn", idkkn);
                                int rowsAffected = cmdupdt.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('Save Data Success');window.location.href='AdmKKN.aspx';", true);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('No rows affected');", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('Invalid row index');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('Please select a dosen');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('DropDownList not found');", true);
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // This method is required for GridView to work correctly
    }
}