<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dosen.aspx.cs" Inherits="Dosen" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/dash-input.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div class="row">
                <div class="column">
                    <div class="input-box">
                        <span>ID Dosen</span>
                        <asp:TextBox runat="server" Text="" ID="txtid_dosen"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredID" runat="server" 
                        ErrorMessage="Id tidak boleh kosong" ControlToValidate="txtid_dosen" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-box">
                        <span>Nama Dosen</span>
                        <asp:TextBox runat="server" Text="" ID="txtNama"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredUserName" runat="server" 
                            ErrorMessage="Nama dosen tidak boleh kosong" ControlToValidate="txtNama" 
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-box">
                        <span>NIDN</span>
                        <asp:TextBox runat="server" Text="" ID="txtNidn"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredNim" runat="server" 
                            ErrorMessage="NIDN tidak boleh kosong" ControlToValidate="txtNidn" 
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                    <div class="column">
                        <div class="input-box">
                            <span>No Telp</span>
                            <asp:TextBox runat="server" Text="" ID="txtTelp"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredNama" runat="server" 
                                ErrorMessage="No Telp tidak boleh kosong" ControlToValidate="txtTelp" 
                            ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input-box">
                            <span>Email</span>
                            <asp:TextBox runat="server" Text="" ID="txtEmail"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="Email tidak boleh kosong" ControlToValidate="txtEmail" 
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <br />
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
            <asp:GridView ID="grc1" runat="server" CssClass="gridview-container" BackColor="White" BorderColor="#cccccc" BorderStyle="None" BorderWidth="1px" CellPadding="5" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="id_dosen" />
                    <asp:BoundField HeaderText="Nama Dosen" DataField="nama_dosen" HeaderStyle-Width="200px" />
                    <asp:BoundField HeaderText="NIDN" DataField="nidn" HeaderStyle-Width="150px" />
                    <asp:BoundField HeaderText="No. Telp" DataField="no_telp" HeaderStyle-Width="150px" />
                    <asp:BoundField HeaderText="Email" DataField="email" HeaderStyle-Width="180px" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CssClass="button" Text="Delete" CommandArgument='<%# Eval("id_dosen") %>' CausesValidation="false" OnClick="lnkdelete_Click"></asp:LinkButton> 
                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="button" Text="Edit" CommandArgument='<%# Eval("id_dosen") %>' CausesValidation="false" OnClick="lnkEdit_Click"></asp:LinkButton>
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
</body>
</html>