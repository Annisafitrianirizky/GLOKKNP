<%@ Page Title="" Language="C#" MasterPageFile="~/Dash.Master" AutoEventWireup="true" CodeFile="AdmKKP.aspx.cs" Inherits="AdmKKP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>KKP Data</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Begin Page Content -->
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Kuliah Kerja Praktek</h1>
        </div>
        <div class="row">
            <form id="form1" runat="server">
                <asp:GridView ID="grc1" runat="server" CssClass="gridview-container" BackColor="White" BorderColor="#cccccc" BorderStyle="None"  AutoGenerateColumns="false" DataKeyNames="id_kkp">
                <Columns>
                <asp:BoundField HeaderText="ID" DataField="id_kkp" />
                <asp:BoundField HeaderText="Nama Mahasiswa" DataField="nama_mahasiswa" HeaderStyle-Width="150px" />
                <asp:BoundField HeaderText="Kelas" DataField="kelas" HeaderStyle-Width="100px" />
                <asp:BoundField HeaderText="Perusahaan" DataField="penempatan" HeaderStyle-Width="150px" />
                <asp:BoundField HeaderText="Alamat" DataField="alamat" HeaderStyle-Width="200px" />
                <asp:BoundField HeaderText="Judul" DataField="judul" />
                <asp:BoundField HeaderText="Durasi" DataField="durasi" />
                <asp:TemplateField HeaderText="Pembimbing">
                    <ItemTemplate>
                        <asp:DropDownList runat="server" ID="ddldosen" DataField="nama_dosen" >
                        </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                    <asp:Button ID="txtUpdate" runat="server" Text="Confirm" CssClass="button" OnClick="txtUpdate_Click" />  
                                    </ItemTemplate>
                                </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="true" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#f1f1f1" />
                <SortedAscendingHeaderStyle BackColor="#007088" />
                <SortedDescendingCellStyle BackColor="#cAc9c9" />
                <SortedDescendingHeaderStyle BackColor="#00547e" />
                </asp:GridView>
            </form>
        </div>
    </div>
</asp:Content>