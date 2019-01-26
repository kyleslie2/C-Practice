<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentRecords.aspx.cs" Inherits="StudentRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</head>
<body>
    <h1>Course Information</h1>
    <form id="form1" runat="server">

        <a>Student ID: </a>
        <asp:TextBox id="txtStudentID" runat="server" CssClass="input" Width="196px"/> <br />
         <asp:RequiredFieldValidator

            ID="rfvStudentID" runat="server"            
            ErrorMessage="Student ID required" Display="Dynamic" 
            ControlToValidate="txtStudentID"
            InitialValue="" CssClass="error"/>
        <br /><br/>
        <a>Student Name: </a>
        <asp:TextBox id="txtStudentName" runat="server" CssClass="input" Width="196px"/> <br />
           <asp:RequiredFieldValidator

            ID="rfvStudentName" runat="server"            
            ErrorMessage="Student Name required" Display="Dynamic" 
            ControlToValidate="txtStudentName"
            InitialValue="" CssClass="error"/><br />
        <asp:RegularExpressionValidator 
            
            ID="RegularExpValidator"  
            ValidationExpression="[a-zA-Z]+\s+[a-zA-Z]+" 
            ControlToValidate="txtStudentName" 
            CssClass="error" Display="Dynamic" 
            ErrorMessage="Must be in first_name last_name!" 
            runat="server"/><br />

        <a>Grade (0-100): </a>
        <asp:TextBox id="txtGrade" runat="server" CssClass="input" Width="196px"/> <br />
        <asp:RequiredFieldValidator

            ID="rfvGrade" runat="server"            
            ErrorMessage="Grade required" Display="Dynamic" 
            ControlToValidate="txtGrade"
            InitialValue="" CssClass="error"/><br />
         <asp:RangeValidator

            ID="RangeValidatorGrade"
            runat="server"
            ErrorMessage="Grade Must Be Between 0 and 100"
            Display="Dynamic"
            ControlToValidate="txtGrade"
            CssClass="error"
                MinimumValue="0"
                MaximumValue="100"
                Type="Integer"/><br />


        <asp:Button runat="server" ID="AddToCourseRecord" Text="Add to Course Record" cssclass="button" OnClick="btnAddtoCourseRecord" Width="181px"/><br /><br />

       

     

        

        

       



        <p>Following student records have been added: <br /><br />Order By<br /></p>

        <asp:RadioButtonList ID="sortingOptions" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SortStudentList">
            <asp:ListItem Value="ID">ID</asp:ListItem>
            <asp:ListItem Value="Name">Name</asp:ListItem>
            <asp:ListItem Value="Grade">Grade</asp:ListItem>
        </asp:RadioButtonList>
        
        
        <br />
        </form> 

        <asp:Table runat="server" ID="studentRecordsTable" CssClass="table">
                <asp:TableRow>
                    <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>    
                    <asp:TableHeaderCell>Grade</asp:TableHeaderCell> 
                </asp:TableRow>
            </asp:Table>


     
</body>
</html>
