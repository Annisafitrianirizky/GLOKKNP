<%@ Page Title="" Language="C#" MasterPageFile="~/Dash.Master" AutoEventWireup="true" CodeFile="AdmKKN.aspx.cs" Inherits="AdmKKN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>KKN Data</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Begin Page Content -->
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Kuliah Kerja Nyata</h1>
        </div>
        <div class="row">
            <form id="form1" runat="server">
                <asp:GridView ID="grc1" runat="server" CssClass="gridview-container" BackColor="White" BorderColor="#cccccc" BorderStyle="None"  AutoGenerateColumns="false" DataKeyNames="id_kkn">
                    <Columns>
                    <asp:BoundField HeaderText="ID" DataField="id_kkn" />
                    <asp:BoundField HeaderText="Nama Kelompok" DataField="nama_kelompok" HeaderStyle-Width="150px" />
                    <asp:BoundField HeaderText="Tempat" DataField="tempat" HeaderStyle-Width="150px" />
                    <asp:BoundField HeaderText="Alamat" DataField="detail_lokasi" HeaderStyle-Width="170px" />
                    <asp:BoundField HeaderText="Judul" DataField="judul" HeaderStyle-Width="150px" />
                    <asp:BoundField HeaderText="Durasi" DataField="durasi" HeaderStyle-Width="100px" />
                    <asp:TemplateField HeaderText="Pembimbing" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddldosen" DataField="nama_dosen" >
                            </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                        <asp:Button ID="btnDetail" runat="server" Text="Detail" CssClass="button" CommandArgument='<%# Eval("id_kkn") %>' OnClick="btnDetail_Click" /> 
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