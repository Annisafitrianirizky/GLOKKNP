using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using WebListItem = System.Web.UI.WebControls.ListItem;
using ITextListItem = iTextSharp.text.ListItem;

public partial class DosKKP : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();
            BindGridView();
            txtupdate.Visible = false;
        }
    }

    private void BindDropDownList()
    {
        string loggedInUsername = Session["nama_user"] as string;

        if (string.IsNullOrEmpty(loggedInUsername))
        {
            Response.Redirect("Login.aspx");
            return;
        }

        using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
        {
            string sqlQuery = @"SELECT kkp.id_kkp, kkp.nama_mahasiswa FROM kkp LEFT JOIN nilaikkp ON kkp.id_kkp = nilaikkp.id_kkp WHERE kkp.nama_dosen = @username AND nilaikkp.id_kkp IS NULL";

            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@username", loggedInUsername);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ddlNama.Items.Clear();

            ddlNama.DataSource = reader;
            ddlNama.DataTextField = "nama_mahasiswa";
            ddlNama.DataValueField = "id_kkp";
            ddlNama.DataBind();

            ddlNama.Items.Insert(0, new WebListItem("--Pilih Mahasiswa--", "0"));
        }
    }

    private void BindGridView()
    {
        string loggedInUsername = Session["nama_user"] as string;

        if (string.IsNullOrEmpty(loggedInUsername))
        {
            Response.Redirect("Login.aspx");
            return;
        }

        string sql = @"SELECT * FROM nilaikkp WHERE nama_dosen = @nama_dosen";

        using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@nama_dosen", loggedInUsername);

            conn.Open();

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                grc1.DataSource = dt;
                grc1.DataBind();
            }
        }
    }

    protected void ddlNama_SelectedIndexChanged(object sender, EventArgs e)
    {
        int mahasiswaId = Convert.ToInt32(ddlNama.SelectedValue);

        if (mahasiswaId > 0)
        {
            // Ambil username dari session
            string loggedInUsername = Session["nama_user"] as string;

            // Pastikan username tidak kosong
            if (string.IsNullOrEmpty(loggedInUsername))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
            {
                // Query untuk menyaring berdasarkan username
                SqlCommand cmd = new SqlCommand("SELECT kelas, prodi, nama_dosen FROM kkp WHERE id_kkp = @id AND nama_dosen = @username", conn);
                cmd.Parameters.AddWithValue("@id", mahasiswaId);
                cmd.Parameters.AddWithValue("@username", loggedInUsername);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblKelas.Text = reader["kelas"].ToString();
                    lblProdi.Text = reader["prodi"].ToString();
                    lblDosen.Text = reader["nama_dosen"].ToString();
                }
                else
                {
                    // Tangani jika tidak ada data yang ditemukan
                    lblKelas.Text = "Data tidak ditemukan.";
                    lblProdi.Text = "Data tidak ditemukan.";
                    lblDosen.Text = "Data tidak ditemukan.";
                }
            }
        }
    }

    protected void txtsave_Click(object sender, EventArgs e)
    {
        try
        {
            double disiplin = double.Parse(txtdisiplin.Text);
            double kerjasama = double.Parse(txtkerjasama.Text);
            double inisiatif = double.Parse(txtinisiatif.Text);
            double kerajinan = double.Parse(txtkerajinan.Text);
            double tanggungjawab = double.Parse(txttanggungjawab.Text);
            double sikap = double.Parse(txtsikap.Text);
            double laporan = double.Parse(txtprestasi.Text);
            double total = disiplin + kerjasama + inisiatif + kerajinan + tanggungjawab + sikap + laporan;
            double rata_rata = total / 7;
            string grade = CalculateGrade(rata_rata);

            string checkSql = "SELECT COUNT(*) FROM [INEXFOLER].[dbo].[kkp] WHERE id_kkp = @id_kkp";
            string insertKkpSql = "INSERT INTO [INEXFOLER].[dbo].[kkp] (id_kkp, nama_mahasiswa, kelas, prodi, nama_dosen) VALUES (@id_kkp, @nama, @kelas, @prodi, @dosen)";
            string insertNilaikkpSql = "INSERT INTO [INEXFOLER].[dbo].[nilaikkp] (id_kkp, nama_mahasiswa, kelas, prodi, disiplin, kerjasama, inisiatif, kerajinan, tanggung_jawab, sikap, prestasi, total, rata_rata, grade, nama_dosen) VALUES (@id_kkp, @nama_mahasiswa, @kelas, @prodi, @disiplin, @kerjasama, @inisiatif, @kerajinan, @tanggungjawab, @sikap, @prestasi, @total, @rata_rata, @grade, @dosen)";

            koneksi.Open();
            SqlTransaction transaction = koneksi.BeginTransaction();

            using (SqlCommand checkCmd = new SqlCommand(checkSql, koneksi, transaction))
            {
                checkCmd.Parameters.AddWithValue("@id_kkp", ddlNama.SelectedValue);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    using (SqlCommand cmdkkp = new SqlCommand(insertKkpSql, koneksi, transaction))
                    {
                        cmdkkp.Parameters.AddWithValue("@id_kkp", ddlNama.SelectedValue);
                        cmdkkp.Parameters.AddWithValue("@nama", ddlNama.SelectedItem.Text);
                        cmdkkp.Parameters.AddWithValue("@kelas", lblKelas.Text);
                        cmdkkp.Parameters.AddWithValue("@prodi", lblProdi.Text);
                        cmdkkp.Parameters.AddWithValue("@dosen", lblDosen.Text);
                        cmdkkp.ExecuteNonQuery();
                    }
                }

                using (SqlCommand cmdNilaikkp = new SqlCommand(insertNilaikkpSql, koneksi, transaction))
                {
                    cmdNilaikkp.Parameters.AddWithValue("@id_kkp", ddlNama.SelectedValue);
                    cmdNilaikkp.Parameters.AddWithValue("@nama_mahasiswa", ddlNama.SelectedItem.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@kelas", lblKelas.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@prodi", lblProdi.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@dosen", lblDosen.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@disiplin", txtdisiplin.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@kerjasama", txtkerjasama.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@inisiatif", txtinisiatif.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@kerajinan", txtkerajinan.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@tanggungjawab", txttanggungjawab.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@sikap", txtsikap.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@prestasi", txtprestasi.Text);
                    cmdNilaikkp.Parameters.AddWithValue("@total", total);
                    cmdNilaikkp.Parameters.AddWithValue("@rata_rata", rata_rata);
                    cmdNilaikkp.Parameters.AddWithValue("@grade", grade);
                    cmdNilaikkp.ExecuteNonQuery();
                }

                transaction.Commit();
            }

            BindGridView();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.alert('Data saved successfully');", true);
            ClearFields();
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
        finally
        {
            if (koneksi.State == ConnectionState.Open)
            {
                koneksi.Close();
            }
        }
    }

    protected void txtupdate_Click(object sender, EventArgs e)
    {
        try
        {
            double disiplin = double.Parse(txtdisiplin.Text);
            double kerjasama = double.Parse(txtkerjasama.Text);
            double inisiatif = double.Parse(txtinisiatif.Text);
            double kerajinan = double.Parse(txtkerajinan.Text);
            double tanggungjawab = double.Parse(txttanggungjawab.Text);
            double sikap = double.Parse(txtsikap.Text);
            double prestasi = double.Parse(txtprestasi.Text);
            double total = disiplin + kerjasama + inisiatif + kerajinan + tanggungjawab + sikap + prestasi;
            double rata_rata = total / 7;
            string grade = CalculateGrade(rata_rata); // Calculate grade

            using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE nilaikkp SET disiplin = @disiplin, kerjasama = @kerjasama, inisiatif = @inisiatif, kerajinan = @kerajinan, tanggung_jawab = @tanggung_jawab, sikap = @sikap, prestasi = @prestasi WHERE id_kkp = @id_kkp", conn);
                cmd.Parameters.AddWithValue("@disiplin", txtdisiplin.Text);
                cmd.Parameters.AddWithValue("@kerjasama", txtkerjasama.Text);
                cmd.Parameters.AddWithValue("@inisiatif", txtinisiatif.Text);
                cmd.Parameters.AddWithValue("@kerajinan", txtkerajinan.Text);
                cmd.Parameters.AddWithValue("@tanggung_jawab", txttanggungjawab.Text);
                cmd.Parameters.AddWithValue("@sikap", txtsikap.Text);
                cmd.Parameters.AddWithValue("@prestasi", txtprestasi.Text);
                cmd.Parameters.AddWithValue("@id_kkp", ViewState["id_kkp"]);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindGridView();
            ClearFields();
            txtsave.Visible = true;
            txtupdate.Visible = false;
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
        finally
        {
            if (koneksi.State == ConnectionState.Open)
            {
                koneksi.Close();
            }
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnkButton = (LinkButton)sender;
        int id_kkp = Convert.ToInt32(lnkButton.CommandArgument);
        ViewState["id_kkp"] = id_kkp;

        using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM nilaikkp WHERE id_kkp = @id_kkp", conn);
            cmd.Parameters.AddWithValue("@id_kkp", id_kkp);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Sembunyikan Dropdown List dan tampilkan Label
                ddlNama.Visible = false;
                lblNama.Visible = true;
                lblNama.Text = reader["nama_mahasiswa"].ToString();

                lblKelas.Text = reader["kelas"].ToString();
                lblProdi.Text = reader["prodi"].ToString();
                lblDosen.Text = reader["nama_dosen"].ToString();
                txtdisiplin.Text = reader["disiplin"].ToString();
                txtkerjasama.Text = reader["kerjasama"].ToString();
                txtinisiatif.Text = reader["inisiatif"].ToString();
                txtkerajinan.Text = reader["kerajinan"].ToString();
                txttanggungjawab.Text = reader["tanggung_jawab"].ToString();
                txtsikap.Text = reader["sikap"].ToString();
                txtprestasi.Text = reader["prestasi"].ToString();
            }
        }

        txtsave.Visible = false;
        txtupdate.Visible = true;
    }

    protected void lnkCetak_Click(object sender, EventArgs e)
    {
        try
        {
            string id_kkp = (sender as LinkButton).CommandArgument;
            string sql = "SELECT * FROM [INEXFOLER].[dbo].[nilaikkp] WHERE id_kkp=@id_kkp";

            using (SqlCommand cmd = new SqlCommand(sql, koneksi))
            {
                cmd.Parameters.AddWithValue("@id_kkp", id_kkp);
                if (koneksi.State == ConnectionState.Closed)
                {
                    koneksi.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string namaMahasiswa = reader["nama_mahasiswa"].ToString();
                    string kelas = reader["kelas"].ToString();
                    string prodi = reader["prodi"].ToString();
                    string dosen = reader["nama_dosen"].ToString();

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var document = new Document(PageSize.A4))
                        {
                            PdfWriter.GetInstance(document, memoryStream);
                            document.Open();

                            document.Add(new Paragraph("Laporan Nilai KKP", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16))
                            {
                                Alignment = Element.ALIGN_CENTER,
                                SpacingAfter = 20
                            });

                            var detailsTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100,
                                HorizontalAlignment = Element.ALIGN_LEFT
                            };

                            detailsTable.SetWidths(new float[] { 2f, 5f });

                            AddDetailRow(detailsTable, "Nama Mahasiswa      :", namaMahasiswa);
                            AddDetailRow(detailsTable, "Kelas                          :", kelas);
                            AddDetailRow(detailsTable, "Prodi                          :", prodi);
                            AddDetailRow(detailsTable, "Pembimbing              :", dosen);

                            document.Add(detailsTable);

                            document.Add(new Paragraph("\n"));

                            var scoresTable = new PdfPTable(2)
                            {
                                WidthPercentage = 100,
                                HorizontalAlignment = Element.ALIGN_LEFT
                            };

                            float[] widths = new float[] { 2f, 2f };
                            scoresTable.SetWidths(widths);

                            scoresTable.AddCell(new PdfPCell(new Phrase("Kriteria", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            });
                            scoresTable.AddCell(new PdfPCell(new Phrase("Nilai", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            });

                            AddTableRow(scoresTable, "Disiplin", reader["disiplin"].ToString());
                            AddTableRow(scoresTable, "Kerja Sama", reader["kerjasama"].ToString());
                            AddTableRow(scoresTable, "Inisiatif", reader["inisiatif"].ToString());
                            AddTableRow(scoresTable, "Kerajinan", reader["kerajinan"].ToString());
                            AddTableRow(scoresTable, "Tanggung Jawab", reader["tanggung_jawab"].ToString());
                            AddTableRow(scoresTable, "Sikap", reader["sikap"].ToString());
                            AddTableRow(scoresTable, "Prestasi", reader["prestasi"].ToString());

                            document.Add(scoresTable);

                            document.Add(new Paragraph("\n"));

                            var summaryTable = new PdfPTable(2)
                            {
                                WidthPercentage = 50, // Set to 50% of the page width
                                HorizontalAlignment = Element.ALIGN_LEFT// Align to the right
                            };

                            float[] summaryWidths = new float[] { 1f, 1f };
                            summaryTable.SetWidths(summaryWidths);

                            summaryTable.AddCell(new PdfPCell(new Phrase("Kriteria", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            });
                            summaryTable.AddCell(new PdfPCell(new Phrase("Nilai", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            });

                            AddTableRow(summaryTable, "Total", reader["total"].ToString());
                            AddTableRow(summaryTable, "Rata-Rata", reader["rata_rata"].ToString());
                            AddTableRow(summaryTable, "Grade", reader["grade"].ToString());

                            document.Add(summaryTable);
                            document.Close();

                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", $"attachment;filename={namaMahasiswa}_{kelas}_NilaiKKP.pdf");
                            Response.BinaryWrite(memoryStream.ToArray());
                            Response.End();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
        finally
        {
            if (koneksi.State == ConnectionState.Open)
            {
                koneksi.Close();
            }
        }
    }

    private void AddDetailRow(PdfPTable table, string label, string value)
    {
        var labelCell = new PdfPCell(new Phrase(label, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
        {
            Border = PdfPCell.NO_BORDER,
            PaddingRight = 10,
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        var valueCell = new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
        {
            Border = PdfPCell.NO_BORDER,
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        table.AddCell(labelCell);
        table.AddCell(valueCell);
    }

    private void AddTableRow(PdfPTable table, string criteria, string value)
    {
        table.AddCell(new PdfPCell(new Phrase(criteria, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
        {
            HorizontalAlignment = Element.ALIGN_CENTER
        });
        table.AddCell(new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 12)))
        {
            HorizontalAlignment = Element.ALIGN_CENTER
        });
    }

    private string CalculateGrade(double rata_rata)
    {
        if (rata_rata >= 80)
            return "A";
        else if (rata_rata >= 60)
            return "B";
        else if (rata_rata >= 40)
            return "C";
        else if (rata_rata >= 20)
            return "D";
        else
            return "E";
    }

    private void ClearFields()
    {
        ddlNama.SelectedIndex = 0;
        lblKelas.Text = "";
        lblProdi.Text = "";
        lblDosen.Text = "";
        txtdisiplin.Text = "";
        txtkerjasama.Text = "";
        txtinisiatif.Text = "";
        txtkerajinan.Text = "";
        txttanggungjawab.Text = "";
        txtsikap.Text = "";
        txtprestasi.Text = "";
    }
}