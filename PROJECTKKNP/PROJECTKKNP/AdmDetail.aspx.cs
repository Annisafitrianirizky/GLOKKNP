using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AdmDetail : System.Web.UI.Page
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

        using (SqlCommand cmd = new SqlCommand(queryKetua, koneksi))
        {
            cmd.Parameters.AddWithValue("@id_kkn", idKKN);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtKetua = new DataTable();
            da.Fill(dtKetua);

            rptKetua.DataSource = dtKetua;
            rptKetua.DataBind();
        }

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