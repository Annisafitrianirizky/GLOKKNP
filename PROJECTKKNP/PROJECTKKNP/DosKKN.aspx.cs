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

public partial class DosKKN : System.Web.UI.Page
{
    SqlConnection koneksi = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownLists();
            LoadGridView();
            txtupdate.Visible = false;
        }
    }

    private void LoadDropdownLists()
    {
        string loggedInUsername = Session["nama_user"] as string;

        if (string.IsNullOrEmpty(loggedInUsername))
        {
            Response.Redirect("Login.aspx");
            return;
        }

        string sqlQuery = @"SELECT kd.nama_mahasiswa, kh.id_kkn FROM [INEXFOLER].[dbo].[kkn_d] kd 
                        INNER JOIN [INEXFOLER].[dbo].[kkn_h] kh ON kd.id_kkn = kh.id_kkn 
                        LEFT JOIN [INEXFOLER].[dbo].[nilaikkn] nk ON kd.id_kkn = nk.id_kkn AND kd.nama_mahasiswa = nk.nama_mahasiswa 
                        WHERE kh.nama_dosen = @nama_dosen AND nk.id_kkn IS NULL";

        using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                cmd.Parameters.AddWithValue("@nama_dosen", loggedInUsername);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlNama.Items.Clear();

                    if (reader.HasRows)
                    {
                        ddlNama.DataSource = reader;
                        ddlNama.DataTextField = "nama_mahasiswa";
                        ddlNama.DataValueField = "id_kkn";
                        ddlNama.DataBind();

                        ddlNama.Items.Insert(0, new WebListItem("--Pilih Mahasiswa--", "0"));
                    }
                }
            }
        }
    }

    private void LoadGridView()
    {
        string loggedInUsername = Session["nama_user"] as string;

        if (string.IsNullOrEmpty(loggedInUsername))
        {
            Response.Redirect("Login.aspx");
            return;
        }

        string sql = @"
            SELECT DISTINCT nk.*
            FROM [INEXFOLER].[dbo].[nilaikkn] nk
            INNER JOIN [INEXFOLER].[dbo].[kkn_d] kd ON nk.id_kkn = kd.id_kkn
            WHERE nk.nama_dosen = @nama_dosen";

        using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@nama_dosen", loggedInUsername);

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
            string loggedInUsername = Session["nama_user"] as string;

            if (string.IsNullOrEmpty(loggedInUsername))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT kd.id, kd.nama_mahasiswa, kd.kelas, kd.prodi, kh.nama_dosen
                                  FROM kkn_d kd
                                  INNER JOIN kkn_h kh ON kd.id_kkn = kh.id_kkn 
                                  WHERE kd.id_kkn = @id AND kh.nama_dosen = @username", conn);
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
            double laporan = double.Parse(txtlaporan.Text);
            double total = disiplin + kerjasama + inisiatif + kerajinan + tanggungjawab + sikap + laporan;
            double rata_rata = total / 7;
            string grade = CalculateGrade(rata_rata);

            string checkSql = "SELECT COUNT(*) FROM [INEXFOLER].[dbo].[kkn_d] WHERE id_kkn = @id_kkn";
            string insertKknSql = "INSERT INTO [INEXFOLER].[dbo].[kkn] (id_kkn, nama_mahasiswa, kelas, prodi, nama_dosen) VALUES (@id_kkn, @nama, @kelas, @prodi, @dosen)";
            string insertNilaiKknSql = "INSERT INTO [INEXFOLER].[dbo].[nilaikkn] (id_kkn, nama_mahasiswa, kelas, prodi, disiplin, kerjasama, inisiatif, kerajinan, tanggung_jawab, sikap, laporan, total, rata_rata, grade, nama_dosen) VALUES (@id_kkn, @nama_mahasiswa, @kelas, @prodi, @disiplin, @kerjasama, @inisiatif, @kerajinan, @tanggungjawab, @sikap, @laporan, @total, @rata_rata, @grade, @dosen)";

            koneksi.Open();
            SqlTransaction transaction = koneksi.BeginTransaction();

            using (SqlCommand checkCmd = new SqlCommand(checkSql, koneksi, transaction))
            {
                checkCmd.Parameters.AddWithValue("@id_kkn", ddlNama.SelectedValue);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    using (SqlCommand cmdKkn = new SqlCommand(insertKknSql, koneksi, transaction))
                    {
                        cmdKkn.Parameters.AddWithValue("@id_kkn", ddlNama.SelectedValue); 
                        cmdKkn.Parameters.AddWithValue("@nama", ddlNama.SelectedItem.Text);
                        cmdKkn.Parameters.AddWithValue("@kelas", lblKelas.Text);
                        cmdKkn.Parameters.AddWithValue("@prodi", lblProdi.Text);
                        cmdKkn.Parameters.AddWithValue("@dosen", lblDosen.Text);
                        cmdKkn.ExecuteNonQuery();
                    }
                }

                using (SqlCommand cmdNilaiKkn = new SqlCommand(insertNilaiKknSql, koneksi, transaction))
                {
                    cmdNilaiKkn.Parameters.AddWithValue("@id_kkn", ddlNama.SelectedValue);
                    cmdNilaiKkn.Parameters.AddWithValue("@nama_mahasiswa", ddlNama.SelectedItem.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@kelas", lblKelas.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@prodi", lblProdi.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@dosen", lblDosen.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@disiplin", txtdisiplin.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@kerjasama", txtkerjasama.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@inisiatif", txtinisiatif.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@kerajinan", txtkerajinan.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@tanggungjawab", txttanggungjawab.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@sikap", txtsikap.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@laporan", txtlaporan.Text);
                    cmdNilaiKkn.Parameters.AddWithValue("@total", total);
                    cmdNilaiKkn.Parameters.AddWithValue("@rata_rata", rata_rata);
                    cmdNilaiKkn.Parameters.AddWithValue("@grade", grade);
                    cmdNilaiKkn.ExecuteNonQuery();
                }

                transaction.Commit();
            }

            LoadGridView();
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

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnkButton = (LinkButton)sender;
        int id = Convert.ToInt32(lnkButton.CommandArgument);
        ViewState["id"] = id;

        using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [INEXFOLER].[dbo].[nilaikkn] WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
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
                txtlaporan.Text = reader["laporan"].ToString();
            }
        }

        // Sembunyikan tombol simpan dan tampilkan tombol update
        txtsave.Visible = false;
        txtupdate.Visible = true;
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
            double laporan = double.Parse(txtlaporan.Text);
            double total = disiplin + kerjasama + inisiatif + kerajinan + tanggungjawab + sikap + laporan;
            double rata_rata = total / 7;
            string grade = CalculateGrade(rata_rata); // Calculate grade

            using (SqlConnection conn = new SqlConnection(koneksi.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE[INEXFOLER].[dbo].[nilaikkn] SET disiplin=@disiplin, kerjasama=@kerjasama, inisiatif=@inisiatif, kerajinan=@kerajinan, tanggung_jawab=@tanggungjawab, sikap=@sikap, laporan=@laporan, total=@total, rata_rata=@rata_rata, grade=@grade WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@disiplin", txtdisiplin.Text);
                cmd.Parameters.AddWithValue("@kerjasama", txtkerjasama.Text);
                cmd.Parameters.AddWithValue("@inisiatif", txtinisiatif.Text);
                cmd.Parameters.AddWithValue("@kerajinan", txtkerajinan.Text);
                cmd.Parameters.AddWithValue("@tanggungjawab", txttanggungjawab.Text);
                cmd.Parameters.AddWithValue("@sikap", txtsikap.Text);
                cmd.Parameters.AddWithValue("@laporan", txtlaporan.Text);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@rata_rata", rata_rata);
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.Parameters.AddWithValue("@id", ViewState["id"]);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadGridView();
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

    protected void lnkCetak_Click(object sender, EventArgs e)
    {
        try
        {
            string id_kkn = (sender as LinkButton).CommandArgument;
            string sql = "SELECT * FROM [INEXFOLER].[dbo].[nilaikkn] WHERE id_kkn=@id_kkn";

            using (SqlCommand cmd = new SqlCommand(sql, koneksi))
            {
                cmd.Parameters.AddWithValue("@id_kkn", id_kkn);
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

                            document.Add(new Paragraph("Laporan Nilai KKN", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16))
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
                            AddTableRow(scoresTable, "Laporan", reader["laporan"].ToString());

                            document.Add(scoresTable);

                            document.Add(new Paragraph("\n"));

                            var summaryTable = new PdfPTable(2)
                            {
                                WidthPercentage = 50, // Set to 50% of the page width
                                HorizontalAlignment = Element.ALIGN_LEFT
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
                            Response.AddHeader("content-disposition", $"attachment;filename={namaMahasiswa}_{kelas}_NilaiKKN.pdf");
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
        txtlaporan.Text = "";
    }
}