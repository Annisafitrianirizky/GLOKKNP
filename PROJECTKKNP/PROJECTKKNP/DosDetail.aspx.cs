using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DosDetail : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string idKKN = Request.QueryString["id_kkn"];
            if (!string.IsNullOrEmpty(idKKN))
            {
                BindData(idKKN);
            }
        }
    }

    private void BindData(string idKKN)
    {
        string queryKKNDetails = @"
                SELECT * 
                FROM kkn_h 
                WHERE id_kkn = @id_kkn";

        string queryKetua = @"
                SELECT TOP 1 * 
                FROM kkn_d 
                WHERE id_kkn = @id_kkn
                ORDER BY [id]";

        string queryAnggota = @"
                SELECT * 
                FROM kkn_d 
                WHERE id_kkn = @id_kkn
                AND [id] != (SELECT MIN([id]) FROM kkn_d WHERE id_kkn = @id_kkn)";

        // Bind KKN Details
        using (SqlCommand cmd = new SqlCommand(queryKKNDetails, koneksi))
        {
            cmd.Parameters.AddWithValue("@id_kkn", idKKN);
            koneksi.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblNamaKelompok.Text = reader["nama_kelompok"].ToString();
                lblTempat.Text = reader["tempat"].ToString();
                lblDetailLokasi.Text = reader["detail_lokasi"].ToString();
                lblJudul.Text = reader["judul"].ToString();
                lblDurasi.Text = reader["durasi"].ToString();
                lblNamaDosen.Text = reader["nama_dosen"].ToString();
            }
            koneksi.Close();
        }

        // Bind Ketua Kelompok
        using (SqlCommand cmd = new SqlCommand(queryKetua, koneksi))
        {
            cmd.Parameters.AddWithValue("@id_kkn", idKKN);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtKetua = new DataTable();
            da.Fill(dtKetua);

            rptKetua.DataSource = dtKetua;
            rptKetua.DataBind();
        }

        // Bind Anggota Kelompok
        using (SqlCommand cmd = new SqlCommand(queryAnggota, koneksi))
        {
            cmd.Parameters.AddWithValue("@id_kkn", idKKN);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtAnggota = new DataTable();
            da.Fill(dtAnggota);

            rptAnggota.DataSource = dtAnggota;
            rptAnggota.DataBind();
        }
    }
}