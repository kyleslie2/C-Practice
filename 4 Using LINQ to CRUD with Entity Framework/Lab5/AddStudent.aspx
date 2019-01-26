<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="Default3" %>
<%@ MasterType  virtualpath="~/ACMasterPage.master"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <link rel="stylesheet" type="text/css" href="./App_Themes/SiteStyles.css" />   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h1>Add Students</h1>

     <a>Course </a>
    <asp:DropDownList ID="drpCourse" CssClass="dropdown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropdownCourseList_SelectedIndexChanged" > 
        <asp:ListItem Enabled="true" Text="Select a course" Value="-1"></asp:ListItem>
    </asp:DropDownList>
    <br />
    
         &nbsp;<asp:RequiredFieldValidator

            ID="rfvDrpCourse" runat="server"            
            ErrorMessage="Please Select a Course" Display="Dynamic" 
            ControlToValidate="drpCourse"
            InitialValue="-1" CssClass="error"/>
        <br /><br/>
    
    
    
    <a>Student ID: </a>
        <asp:TextBox id="txtStudentID" runat="server" CssClass="input" Width="196px"/> <br />
         <asp:RequiredFieldValidator

            ID="rfvStudentID" runat="server"            
            ErrorMessage="Student ID required" Display="Dynamic" 
            ControlToValidate="txtStudentID"
            InitialValue="" CssClass="error"/>
        <br />
    
    
        <asp:Label ID="txtStudentIdExists" runat="server" CssClass="error" Text=""></asp:Label>
        <br />




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


        <asp:Button runat="server" ID="btnSaveStudent" Text="Add to Course Record" CssClass="btn btn-primary" Width="181px"/><br /><br />

       <asp:Label ID="selectedCourseCodeSession" runat="server" CssClass="error" Text=""></asp:Label>
    <br />   
    <asp:Label ID="selectedCourseSession" runat="server" CssClass="error" Text=""></asp:Label>

        <p>Following student records have been added: </p><br />
            
     <%--   Order By<br /></p>

     <asp:RadioButtonList ID="sortingOptions" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SortStudentList">
            <asp:ListItem Value="ID">ID</asp:ListItem>
            <asp:ListItem Value="Name">Name</asp:ListItem>
            <asp:ListItem Value="Grade">Grade</asp:ListItem>
        </asp:RadioButtonList>
        
        
        <br />--%>
       

        <asp:Table runat="server" ID="studentRecordsTable" CssClass="table">
                <asp:TableRow>
                    <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>    
                    <asp:TableHeaderCell>Grade</asp:TableHeaderCell> 
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
    <script>
        //the following lines of code are from Wei Gong's lecture
        $(document).ready(function () {
            $(".deleteRecord").on('click', function () {
                if (!confirm("Selected academic record will be deleted!")) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>

