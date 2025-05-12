using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DashDos : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

    protected int jumlahKKP;
    protected int jumlahKKN;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetJumlahPendaftar();
            BindGridViews();
        }
    }

    private void GetJumlahPendaftar()
    {
        string username = Session["nama_user"] as string;
        if (string.IsNullOrEmpty(username))
        {
            Response.Redirect("Login.aspx");
            return;
        }

        try
        {
            using (SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                koneksi.Open();

                // Query untuk jumlah pendaftar KKP berdasarkan nama_dosen
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM kkp WHERE nama_dosen = @Username", koneksi))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    jumlahKKP = (int)command.ExecuteScalar();
                }

                // Query untuk jumlah pendaftar KKN berdasarkan nama_dosen
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM kkn_h WHERE nama_dosen = @Username", koneksi))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    jumlahKKN = (int)command.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
    }

    private void BindGridViews()
    {
        string username = Session["nama_user"] as string;
        if (string.IsNullOrEmpty(username))
        {
            Response.Redirect("Login.aspx");
            return;
        }

        try
        {
            using (SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                koneksi.Open();

                // Bind GridView untuk KKP
                string sqlKKP = "SELECT * FROM kkp WHERE nama_dosen = @Username";
                using (SqlCommand cmdgetKKP = new SqlCommand(sqlKKP, koneksi))
                {
                    cmdgetKKP.Parameters.AddWithValue("@Username", username);
                    using (SqlDataAdapter dagetKKP = new SqlDataAdapter(cmdgetKKP))
                    {
                        DataTable dtgetKKP = new DataTable();
                        dagetKKP.Fill(dtgetKKP);
                        if (dtgetKKP.Rows.Count > 0)
                        {
                            grc1.DataSource = dtgetKKP;
                            grc1.DataBind();
                        }
                    }
                }

                // Bind GridView untuk KKN
                string sqlKKN = "SELECT * FROM kkn_h WHERE nama_dosen = @Username";
                using (SqlCommand cmdgetKKN = new SqlCommand(sqlKKN, koneksi))
                {
                    cmdgetKKN.Parameters.AddWithValue("@Username", username);
                    using (SqlDataAdapter dagetKKN = new SqlDataAdapter(cmdgetKKN))
                    {
                        DataTable dtgetKKN = new DataTable();
                        dagetKKN.Fill(dtgetKKN);
                        if (dtgetKKN.Rows.Count > 0)
                        {
                            grc2.DataSource = dtgetKKN;
                            grc2.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string idKKN = btn.CommandArgument;
        Response.Redirect("DosDetail.aspx?id_kkn=" + idKKN);
    }
}