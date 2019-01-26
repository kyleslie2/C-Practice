<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddEmployee.aspx.cs" Inherits="Default4" %>
<%@ MasterType  virtualpath="~/ACMasterPage.master"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <link rel="stylesheet" type="text/css" href="./App_Themes/SiteStyles.css" />   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Add Employees</h1><br /><br />
    <div>
         <a>Full Name: </a>
        <asp:TextBox id="txtFullName" runat="server" CssClass="input" Width="196px"/> <br />
         <asp:RequiredFieldValidator

            ID="rfvFullName" runat="server"            
            ErrorMessage="Full Name is Required" Display="Dynamic" 
            ControlToValidate="txtFullName"
            InitialValue="" CssClass="error"/>
        <br />
        <a>Username: </a>
        <asp:TextBox id="txtUsername" runat="server" CssClass="input" Width="196px"/> <br />
           <asp:RequiredFieldValidator

            ID="rfvUsername" runat="server"            
            ErrorMessage="Username is Required" Display="Dynamic" 
            ControlToValidate="txtUsername"
            InitialValue="" CssClass="error"/>
         <br />
        <a>Password: </a>
        <asp:TextBox id="txtPassword" runat="server" CssClass="input" Width="196px"/> <br />
           <asp:RequiredFieldValidator

            ID="rfvPassword" runat="server"            
            ErrorMessage="Password is Required" Display="Dynamic" 
            ControlToValidate="txtPassword"
            InitialValue="" CssClass="error"/><br />
        <a>Roles: </a>
        <div class="container">
        
            
            <asp:Label ID="lblEmployeeError" CssClass="error" runat="server"></asp:Label>
            
            <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one."
            ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />


            <asp:CheckBoxList ID="checkboxListRoles" runat="server">
                 <asp:ListItem Enabled="true" Text="Department Chair" Value="Department Chair"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="Coordinator" Value="Coordinator"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="Instructor" Value="Instructor"></asp:ListItem> 
            </asp:CheckBoxList>
            </div>



        
        <asp:Button runat="server" ID="btnAddEmployee" Text="Add Employee" cssclass="btn btn-primary" OnClick="btnAddEmployee_Click"/><br />
        

        <br />

        <asp:Table ID="EmployeeTable" CssClass="table" runat="server">
            <asp:TableRow>
                <%--<asp:TableHeaderCell>ID</asp:TableHeaderCell>--%>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>                
                <asp:TableHeaderCell>Username</asp:TableHeaderCell>
                <asp:TableHeaderCell>Role(s)</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>

 

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
    <script>
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=checkboxListRoles.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>
</asp:Content>


