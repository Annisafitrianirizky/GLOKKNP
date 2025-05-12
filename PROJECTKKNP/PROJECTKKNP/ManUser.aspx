<%@ Page Title="" Language="C#" MasterPageFile="~/Dash.Master" AutoEventWireup="true" CodeFile="ManUser.aspx.cs" Inherits="ManUser" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Manage User</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Begin Page Content -->
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Manage User</h1>
            <!--<a
            href="#"
            class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"
            ><i class="fas fa-download fa-sm text-white-50"></i> Generate
            Report</a
            >-->
        </div>
        <div class="row">
        <div class="container">
            <form id="form1" runat="server">
                <div class="row">
                    <div class="column">
                        <div class="input-box">
                            <span>User ID</span>
                            <asp:TextBox runat="server" Text="" ID="txtuserid"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredUserID" runat="server" ErrorMessage="User Id tidak boleh kosong" ControlToValidate="txtuserid" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input-box">
                            <span>Username</span>
                            <asp:TextBox runat="server" Text="" ID="txtusername"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredUserName" runat="server" ErrorMessage="Username tidak boleh kosong" ControlToValidate="txtusername" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input-box">
                          <span>Password</span>
                          <asp:TextBox runat="server" Text="" ID="txtpassword"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ErrorMessage="Password tidak boleh kosong" ControlToValidate="txtpassword" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="column">
                        <div class="input-box">
                          <span>Nama User</span>
                          <asp:TextBox runat="server" Text="" ID="txtnama"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredNama" runat="server" ErrorMessage="Nama Mahasiswa tidak boleh kosong" ControlToValidate="txtnama" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input-box">
                          <span>Status</span>
                          <asp:DropDownList runat="server" ID="ddlstatus">
                            <asp:ListItem Text="mahasiswa" Value="mahasiswa"></asp:ListItem>
                            <asp:ListItem Text="admin" Value="admin"></asp:ListItem>
                            <asp:ListItem Text="dosen" Value="dosen"></asp:ListItem>
                          </asp:DropDownList>
                        </div>
                    <br />
                    <br />
                    <div class="column">
                        <div class="flex">
                            <div class="input-box">
                              <asp:Button ID="txtsave" Text="Save" runat="server" CssClass="btn" OnClick="txtsave_Click" />
                            </div>
                            <div class="input-box">
                              <asp:Button ID="txtUpdate" runat="server" Text="Update" CssClass="btn" OnClick="txtUpdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                <asp:GridView ID="grc1" runat="server" CssClass="gridview-container" BackColor="White" BorderColor="#cccccc" BorderStyle="None" BorderWidth="1px" CellPadding="5" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="id_user" HeaderStyle-Width="50px" />
                        <asp:BoundField HeaderText="User Name" DataField="username" HeaderStyle-Width="150px" />
                        <asp:BoundField HeaderText="Nama User" DataField="nama_user" HeaderStyle-Width="200px" />
                        <asp:BoundField HeaderText="Password" DataField="password" HeaderStyle-Width="150px" />
                        <asp:BoundField HeaderText="Status" DataField="status" HeaderStyle-Width="150px" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkdelete" runat="server" CssClass="button" Text="Delete" CommandArgument='<%# Eval("id_user") %>' CausesValidation="false" OnClick="lnkdelete_Click"></asp:LinkButton> 
                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="button" Text="Edit" CommandArgument='<%# Eval("id_user") %>' CausesValidation="false" OnClick="lnkEdit_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="true" ForeColor="White" HorizontalAlign="Center" />
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
    </div>
</asp:Content>