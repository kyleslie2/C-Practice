<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="Default2" %>
<%@ MasterType  virtualpath="~/ACMasterPage.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="./App_Themes/SiteStyles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h1>Add Students</h1>

     <a>Course </a>
    <asp:DropDownList ID="dropdownCourseList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropdownCourseList_SelectedIndexChanged" >
        <asp:ListItem Enabled="true" Text="Select a course" Value="-1"></asp:ListItem>
    </asp:DropDownList>
    
         <asp:RequiredFieldValidator

            ID="rfvDropdownCourseList" runat="server"            
            ErrorMessage="Please Select a Course" Display="Dynamic" 
            ControlToValidate="dropdownCourseList"
            InitialValue="-1" CssClass="error"/>
        <br /><br/>
    
    
    
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

       


        <p>Following student records have been added: <br /><br />
            
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
                </asp:TableRow>
            </asp:Table>


</asp:Content>

