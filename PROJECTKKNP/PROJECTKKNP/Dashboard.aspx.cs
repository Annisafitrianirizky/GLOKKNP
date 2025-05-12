using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Dash : System.Web.UI.Page
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
        try
        {
            koneksi.Open();

            // Query untuk jumlah pendaftar KKP
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM kkp", koneksi))
            {
                jumlahKKP = (int)command.ExecuteScalar();
            }

            // Query untuk jumlah pendaftar KKN
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM kkn_d", koneksi))
            {
                jumlahKKN = (int)command.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            // Handle exception (log, show error message, etc.)
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            koneksi.Close();
        }
    }

    private void BindGridViews()
    {
        try
        {
            // Bind GridView untuk KKP
            string sqlKKP = "SELECT * FROM kkp";
            using (SqlCommand cmdgetKKP = new SqlCommand(sqlKKP, koneksi))
            {
                koneksi.Open();
                SqlDataAdapter dagetKKP = new SqlDataAdapter(cmdgetKKP);
                DataTable dtgetKKP = new DataTable();
                dagetKKP.Fill(dtgetKKP);
                koneksi.Close();
                if (dtgetKKP.Rows.Count > 0)
                {
                    grc1.DataSource = dtgetKKP;
                    grc1.DataBind();
                }
            }

            // Bind GridView untuk KKN
            string sqlKKN = "SELECT * FROM kkn_d"; // Pastikan tabel sesuai
            using (SqlCommand cmdgetKKN = new SqlCommand(sqlKKN, koneksi))
            {
                koneksi.Open();
                SqlDataAdapter dagetKKN = new SqlDataAdapter(cmdgetKKN);
                DataTable dtgetKKN = new DataTable();
                dagetKKN.Fill(dtgetKKN);
                koneksi.Close();
                if (dtgetKKN.Rows.Count > 0)
                {
                    grc2.DataSource = dtgetKKN;
                    grc2.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Tangani kesalahan (misalnya, log atau tampilkan pesan kesalahan)
            Response.Write("Error: " + ex.Message);
        }
    }
}