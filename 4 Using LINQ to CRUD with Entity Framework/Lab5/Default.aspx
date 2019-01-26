<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType  virtualpath="~/ACMasterPage.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" type="text/css" href="./App_Themes/SiteStyles.css" />   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Student Record System</h1>

    <p>Welcome to Algonquin College's Student Record System! <br /><br />

        Click the top menu to add new courses, or add student records to courses
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" Runat="Server">

</asp:Content>

 

 