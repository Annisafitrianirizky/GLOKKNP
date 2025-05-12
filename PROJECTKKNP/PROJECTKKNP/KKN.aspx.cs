using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class KKN : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
            {
                string sql = "";
                sql = "INSERT INTO [INEXFOLER].[dbo].[kkn_h]([nama_kelompok],[tempat],[detail_lokasi],[judul],[durasi]) " +
                    "VALUES(@kelompok, @tempat, @lokasi_d, @judul, @lama)";

                SqlCommand cmdsv = new SqlCommand(sql, koneksi);
                cmdsv.Parameters.Add("@kelompok", SqlDbType.NVarChar, 50).Value = tNamaKel.Text;
                cmdsv.Parameters.Add("@tempat", SqlDbType.NVarChar, 50).Value = tTemPen.Text;
                cmdsv.Parameters.Add("@lokasi_d", SqlDbType.NVarChar, 50).Value = tAlamat.Text;
                cmdsv.Parameters.Add("@judul", SqlDbType.NVarChar, 50).Value = tJudul.Text;
                cmdsv.Parameters.Add("@lama", SqlDbType.NVarChar, 50).Value = tDurasi.Text;

                koneksi.Open();
                cmdsv.ExecuteNonQuery();

                // Retrieve the id_kkn value from the kkn_h table
                string sqlGetId = "SELECT id_kkn FROM kkn_h WHERE nama_kelompok = @nama_kelompok AND tempat = @tempat AND detail_lokasi = @lokasi_detail AND judul = @judul AND durasi = @lama";
                SqlCommand cmdGetId = new SqlCommand(sqlGetId, koneksi);
                cmdGetId.Parameters.AddWithValue("@nama_kelompok", tNamaKel.Text);
                cmdGetId.Parameters.AddWithValue("@tempat", tTemPen.Text);
                cmdGetId.Parameters.AddWithValue("@lokasi_detail", tAlamat.Text);
                cmdGetId.Parameters.AddWithValue("@judul", tJudul.Text);
                cmdGetId.Parameters.AddWithValue("@lama", tDurasi.Text);

                object result = cmdGetId.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    int idKkn = Convert.ToInt32(result);

                    // Insert records into the kkn_d table
                    string sql2 = "";
                    sql2 = "INSERT INTO [INEXFOLER].[dbo].[kkn_d] ([id_kkn],[nim],[nama_mahasiswa],[kelas],[prodi],[fakultas],[angkatan], " +
                        "[sks],[ipk]) " +
                        "VALUES (@id_kkn, @Nim, @Nama, @Kelas, @Prodi, @Fakultas, @Angkatan, @SKS, @IPK)";

                    SqlCommand cmdsv2 = new SqlCommand(sql2, koneksi);// Set the id_kkn parameter value
                    cmdsv2.Parameters.Add("@id_kkn", SqlDbType.Int).Value = idKkn;
                    cmdsv2.Parameters.Add("@Nim", SqlDbType.NVarChar, 50).Value = tNim.Text;
                    cmdsv2.Parameters.Add("@Nama", SqlDbType.NVarChar, 50).Value = tNama.Text;
                    cmdsv2.Parameters.Add("@Kelas", SqlDbType.NVarChar, 50).Value = tKelas.Text;
                    cmdsv2.Parameters.Add("@Prodi", SqlDbType.NVarChar, 50).Value = tProdi.Text;
                    cmdsv2.Parameters.Add("@Fakultas", SqlDbType.NVarChar, 50).Value = tFakultas.Text;
                    cmdsv2.Parameters.Add("@Angkatan", SqlDbType.NVarChar, 50).Value = tAngkatan.Text;
                    cmdsv2.Parameters.Add("@SKS", SqlDbType.NVarChar, 50).Value = tSks.Text;
                    cmdsv2.Parameters.Add("@IPK", SqlDbType.NVarChar, 50).Value = tIpk.Text;

                    cmdsv2.ExecuteNonQuery();

                    string sql3 = "";
                    sql3 = "INSERT INTO [INEXFOLER].[dbo].[kkn_d] ([id_kkn],[nim],[nama_mahasiswa],[kelas],[prodi],[fakultas],[angkatan], " +
                        "[sks],[ipk]) " +
                        "VALUES (@id_kkn, @Nim, @Nama, @Kelas, @Prodi, @Fakultas, @Angkatan, @SKS, @IPK)";

                    SqlCommand cmdsv3 = new SqlCommand(sql2, koneksi);
                    cmdsv3.Parameters.Add("@id_kkn", SqlDbType.Int).Value = idKkn;
                    cmdsv3.Parameters.Add("@Nim", SqlDbType.NVarChar, 50).Value = tNim2.Text;
                    cmdsv3.Parameters.Add("@Nama", SqlDbType.NVarChar, 50).Value = tNama2.Text;
                    cmdsv3.Parameters.Add("@Kelas", SqlDbType.NVarChar, 50).Value = tKelas2.Text;
                    cmdsv3.Parameters.Add("@Prodi", SqlDbType.NVarChar, 50).Value = tProdi2.Text;
                    cmdsv3.Parameters.Add("@Fakultas", SqlDbType.NVarChar, 50).Value = tFakultas2.Text;
                    cmdsv3.Parameters.Add("@Angkatan", SqlDbType.NVarChar, 50).Value = tAngkatan2.Text;
                    cmdsv3.Parameters.Add("@SKS", SqlDbType.NVarChar, 50).Value = tSks2.Text;
                    cmdsv3.Parameters.Add("@IPK", SqlDbType.NVarChar, 50).Value = tIpk2.Text;

                    cmdsv3.ExecuteNonQuery();

                    string sql4 = "";
                    sql4 = "INSERT INTO [INEXFOLER].[dbo].[kkn_d] ([id_kkn],[nim],[nama_mahasiswa],[kelas],[prodi],[fakultas],[angkatan], " +
                        "[sks],[ipk]) " +
                        "VALUES (@id_kkn, @Nim, @Nama, @Kelas, @Prodi, @Fakultas, @Angkatan, @SKS, @IPK)";

                    SqlCommand cmdsv4 = new SqlCommand(sql2, koneksi);
                    cmdsv4.Parameters.Add("@id_kkn", SqlDbType.Int).Value = idKkn;
                    cmdsv4.Parameters.Add("@Nim", SqlDbType.NVarChar, 50).Value = tNim3.Text;
                    cmdsv4.Parameters.Add("@Nama", SqlDbType.NVarChar, 50).Value = tNama3.Text;
                    cmdsv4.Parameters.Add("@Kelas", SqlDbType.NVarChar, 50).Value = tKelas3.Text;
                    cmdsv4.Parameters.Add("@Prodi", SqlDbType.NVarChar, 50).Value = tProdi3.Text;
                    cmdsv4.Parameters.Add("@Fakultas", SqlDbType.NVarChar, 50).Value = tFakultas3.Text;
                    cmdsv4.Parameters.Add("@Angkatan", SqlDbType.NVarChar, 50).Value = tAngkatan3.Text;
                    cmdsv4.Parameters.Add("@SKS", SqlDbType.NVarChar, 50).Value = tSks3.Text;
                    cmdsv4.Parameters.Add("@IPK", SqlDbType.NVarChar, 50).Value = tIpk3.Text;

                    cmdsv4.ExecuteNonQuery();

                    string sql5 = "";
                    sql5 = "INSERT INTO [INEXFOLER].[dbo].[kkn_d] ([id_kkn],[nim],[nama_mahasiswa],[kelas],[prodi],[fakultas],[angkatan], " +
                        "[sks],[ipk]) " +
                        "VALUES (@id_kkn, @Nim, @Nama, @Kelas, @Prodi, @Fakultas, @Angkatan, @SKS, @IPK)";

                    SqlCommand cmdsv5 = new SqlCommand(sql2, koneksi);
                    cmdsv5.Parameters.Add("@id_kkn", SqlDbType.Int).Value = idKkn;
                    cmdsv5.Parameters.Add("@Nim", SqlDbType.NVarChar, 50).Value = tNim4.Text;
                    cmdsv5.Parameters.Add("@Nama", SqlDbType.NVarChar, 50).Value = tNama4.Text;
                    cmdsv5.Parameters.Add("@Kelas", SqlDbType.NVarChar, 50).Value = tKelas4.Text;
                    cmdsv5.Parameters.Add("@Prodi", SqlDbType.NVarChar, 50).Value = tProdi4.Text;
                    cmdsv5.Parameters.Add("@Fakultas", SqlDbType.NVarChar, 50).Value = tFakultas4.Text;
                    cmdsv5.Parameters.Add("@Angkatan", SqlDbType.NVarChar, 50).Value = tAngkatan4.Text;
                    cmdsv5.Parameters.Add("@SKS", SqlDbType.NVarChar, 50).Value = tSks4.Text;
                    cmdsv5.Parameters.Add("@IPK", SqlDbType.NVarChar, 50).Value = tIpk4.Text;

                    cmdsv5.ExecuteNonQuery();

                    string sql6 = "";
                    sql6 = "INSERT INTO [INEXFOLER].[dbo].[kkn_d] ([id_kkn],[nim],[nama_mahasiswa],[kelas],[prodi],[fakultas],[angkatan], " +
                        "[sks],[ipk]) " +
                        "VALUES (@id_kkn, @Nim, @Nama, @Kelas, @Prodi, @Fakultas, @Angkatan, @SKS, @IPK)";

                    SqlCommand cmdsv6 = new SqlCommand(sql2, koneksi);
                    cmdsv6.Parameters.Add("@id_kkn", SqlDbType.Int).Value = idKkn;
                    cmdsv6.Parameters.Add("@Nim", SqlDbType.NVarChar, 50).Value = tNim5.Text;
                    cmdsv6.Parameters.Add("@Nama", SqlDbType.NVarChar, 50).Value = tNama5.Text;
                    cmdsv6.Parameters.Add("@Kelas", SqlDbType.NVarChar, 50).Value = tKelas5.Text;
                    cmdsv6.Parameters.Add("@Prodi", SqlDbType.NVarChar, 50).Value = tProdi5.Text;
                    cmdsv6.Parameters.Add("@Fakultas", SqlDbType.NVarChar, 50).Value = tFakultas5.Text;
                    cmdsv6.Parameters.Add("@Angkatan", SqlDbType.NVarChar, 50).Value = tAngkatan5.Text;
                    cmdsv6.Parameters.Add("@SKS", SqlDbType.NVarChar, 50).Value = tSks5.Text;
                    cmdsv6.Parameters.Add("@IPK", SqlDbType.NVarChar, 50).Value = tIpk5.Text;

                    cmdsv6.ExecuteNonQuery();

                    koneksi.Close();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('Save Data Success');window.location.href='KKN.aspx';", true);
                }
                else
                {
                    int idKkn = 0; // or -1, or any other default value
                                   // proceed with the rest of the code
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }

            }
        }
    }