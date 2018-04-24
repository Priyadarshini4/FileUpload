<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .style1 {
            width: 303px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center" style="width: 600px; height: 300px; background-color: #CCCCFF" border="1px">
                <tr>
                    <td colspan="2" style="color: #8506A9; font-weight: bold; font-size: large; background-color: #CCCCFF">
                        <marquee direction="left" scrollamount="2" loop="true" width="100%" bgcolor="#ffffff">
                                <asp:Label ID="lblHeader" runat="server" Width ="400"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCompany" runat="server" Text="Company Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqComapny" runat="server" ControlToValidate="txtCompany" ValidationGroup="vldsubmit" Text="*" Enabled="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblContact" runat="server" Text="Contact Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqContact" runat="server" ControlToValidate="txtContact" ValidationGroup="vldsubmit" Text="*" Enabled="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblContactNo" runat="server" Text="Contact No. :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqContactNo" runat="server" ControlToValidate="txtContactNo" ValidationGroup="vldsubmit" Text="*" Enabled="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgtxtContactNo" runat="server" ValidationGroup="vldsubmit" ForeColor="Red"
                            ControlToValidate="txtContactNo" ErrorMessage="Please Enter Valid Contact No."
                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="vldsubmit" Text="*" Enabled="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regTxtEmail"
                            runat="server" ErrorMessage="Please Enter Valid Email ID"
                            ValidationGroup="vldsubmit" ControlToValidate="txtEmail"
                            ForeColor="Red"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIPAddress" runat="server" Text="IP Addess :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIPAddress" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqTextIpAddress" runat="server" ControlToValidate="txtIPAddress" ValidationGroup="vldsubmit" Text="*" Enabled="true" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgIpAddess" ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                            runat="server" ErrorMessage="Invalid IP !" ControlToValidate="txtIPAddress" ForeColor="Red"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Select Files: 
                            <asp:FileUpload ID="fldUpload" runat="server" Width="250px" AllowMultiple="true" />

                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <asp:ListBox ID="lstFile" runat="server" Rows="5" SelectionMode="Single" Width="300px" BackColor="#CCCCFF"></asp:ListBox>
                        <br />
                        <asp:Label ID="lblMess" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="75px" OnClick="btnAdd_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDel" runat="server" Text="Remove" Width="98px" OnClick="btnDel_Click" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vldsubmit" OnClick="btnSubmit_Click" />

                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
