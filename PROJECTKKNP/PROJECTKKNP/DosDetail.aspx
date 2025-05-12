<%@ Page Title="" Language="C#" MasterPageFile="~/DashDos.Master" AutoEventWireup="true" CodeFile="DosDetail.aspx.cs" Inherits="DosDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Detail - Kuliah Kerja Nyata</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Begin Page Content -->
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Detail - Kuliah Kerja Nyata</h1>
        </div>

        <!-- Container for detail display -->
        <div class="detail-container">
            <!-- Display KKN Information -->
            <h3>Informasi KKN:</h3>
            <div class="detail-card">
                <div class="detail-row">
                    <div class="detail-col"><strong>Nama Kelompok :</strong> <asp:Label ID="lblNamaKelompok" runat="server"></asp:Label></div>
                    <div class="detail-col"><strong>Tempat        :</strong> <asp:Label ID="lblTempat" runat="server"></asp:Label></div>
                    <div class="detail-col"><strong>Detail Lokasi :</strong> <asp:Label ID="lblDetailLokasi" runat="server"></asp:Label></div>
                </div>
                <div class="detail-row">
                    <div class="detail-col"><strong>Judul         :</strong> <asp:Label ID="lblJudul" runat="server"></asp:Label></div>
                    <div class="detail-col"><strong>Durasi        :</strong> <asp:Label ID="lblDurasi" runat="server"></asp:Label></div>
                    <div class="detail-col"><strong>Nama Dosen    :</strong> <asp:Label ID="lblNamaDosen" runat="server"></asp:Label></div>
                </div>
            </div>

            <!-- Display Ketua Kelompok -->
            <h3>Ketua Kelompok:</h3>
            <asp:Repeater ID="rptKetua" runat="server">
                <ItemTemplate>
                    <div class="detail-card">
                        <div class="detail-row">
                            <div class="detail-col"><strong>NIM         : </strong> <%# Eval("nim") %></div>
                            <div class="detail-col"><strong>Nama        : </strong> <%# Eval("nama_mahasiswa") %></div>
                            <div class="detail-col"><strong>Kelas       : </strong> <%# Eval("kelas") %></div>
                        </div>
                        <div class="detail-row">
                            <div class="detail-col"><strong>Prodi       : </strong> <%# Eval("prodi") %></div>
                            <div class="detail-col"><strong>Fakultas    : </strong> <%# Eval("fakultas") %></div>
                            <div class="detail-col"><strong>Angkatan    : </strong> <%# Eval("angkatan") %></div>
                        </div>
                        <div class="detail-row">
                            <div class="detail-col"><strong>SKS         : </strong> <%# Eval("sks") %></div>
                            <div class="detail-col"><strong>IPK         : </strong> <%# Eval("ipk") %></div>
                            <div class="detail-col"></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- Display Anggota Kelompok -->
            <h3>Anggota Kelompok:</h3>
            <asp:Repeater ID="rptAnggota" runat="server">
                <ItemTemplate>
                    <div class="detail-card">
                        <div class="detail-row">
                            <div class="detail-col"><strong>NIM         : </strong> <%# Eval("nim") %></div>
                            <div class="detail-col"><strong>Nama        : </strong> <%# Eval("nama_mahasiswa") %></div>
                            <div class="detail-col"><strong>Kelas       : </strong> <%# Eval("kelas") %></div>
                        </div>
                        <div class="detail-row">
                            <div class="detail-col"><strong>Prodi       : </strong> <%# Eval("prodi") %></div>
                            <div class="detail-col"><strong>Fakultas    : </strong> <%# Eval("fakultas") %></div>
                            <div class="detail-col"><strong>Angkatan    : </strong> <%# Eval("angkatan") %></div>
                        </div>
                        <div class="detail-row">
                            <div class="detail-col"><strong>SKS         : </strong> <%# Eval("sks") %></div>
                            <div class="detail-col"><strong>IPK         : </strong> <%# Eval("ipk") %></div>
                            <div class="detail-col"></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>