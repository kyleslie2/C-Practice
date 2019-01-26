<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default</title>
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</head>
<body>
    <h1>Course Information</h1>
    <form id="form1" runat="server">

        <a>Course Name: </a>
        <asp:TextBox id="CourseNumber" runat="server" CssClass="input" Width="196px"/> 
        <br /><br/>
         <asp:RequiredFieldValidator  

            ID="rfvCourseName" runat="server"            
            ErrorMessage="Course Name Required" Display="Dynamic" 
            ControlToValidate="CourseName"
            InitialValue="" CssClass="error"/>
        <br /><br />
        <a>Course Number: </a>
        <asp:TextBox id="CourseName" runat="server" CssClass="input" Width="196px"/> <br /><br />


        <asp:RequiredFieldValidator

            ID="rfvCourseNumber" runat="server"            
            ErrorMessage="Course Number Required" Display="Dynamic" 
            ControlToValidate="CourseNumber"
            InitialValue="" CssClass="error"/><br /><br />


       
        
        <asp:Button runat="server" ID="Button1" Text="Submit Course Information" cssclass="button" OnClick="btnSubmitCourseInfo" Width="181px"/><br /><br />

    </form>  
</body>
</html>


