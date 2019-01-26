<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="Default2" %>
<%@ MasterType  virtualpath="~/ACMasterPage.master"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <link rel="stylesheet" type="text/css" href="./App_Themes/SiteStyles.css" />   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Add Courses</h1><br /><br />
    <div>
         <a>Course Number: </a>
        <asp:TextBox id="txtCourseNumber" runat="server" CssClass="input" Width="196px"/> <br />
         <asp:RequiredFieldValidator

            ID="rfvCourseNumber" runat="server"            
            ErrorMessage="Course Number is Required" Display="Dynamic" 
            ControlToValidate="txtCourseNumber"
            InitialValue="" CssClass="error"/>
        <br /><br/>
        <a>Course Name: </a>
        <asp:TextBox id="txtCourseName" runat="server" CssClass="input" Width="196px"/> <br />
           <asp:RequiredFieldValidator

            ID="rfvCourseName" runat="server"            
            ErrorMessage="Course Name Required" Display="Dynamic" 
            ControlToValidate="txtCourseName"
            InitialValue="" CssClass="error"/><br />

       
        
        <asp:Button runat="server" ID="btnAddCourseInfo" Text="Add Course Information" cssclass="btn btn-primary"/><br />
        

        <asp:Label ID="txtCourseNumberExist" runat="server" CssClass="error" Text=""></asp:Label>
        <br />

        <asp:Table ID="CourseTable" CssClass="table" runat="server">
            <asp:TableRow>
                <asp:TableHeaderCell><a href="AddCourse.aspx?sort=code">Course Number</a></asp:TableHeaderCell>                
                <asp:TableHeaderCell><a href="AddCourse.aspx?sort=title">Course Name</a></asp:TableHeaderCell>
                <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>

            </asp:TableRow>
        </asp:Table>

 

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">
    <script>
        $(document).ready(function () {
            $(".deleteCourse").on('click', function () {
                if (!confirm("Selected course and its student records will be deleted!")) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>

