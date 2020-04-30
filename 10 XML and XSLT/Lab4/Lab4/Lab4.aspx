<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="True" CodeBehind="Lab4.aspx.cs" Inherits="Lab4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 style="text-align: center;">Ottawa Restaurant Review</h1>
    <asp:Label runat="server">Search for Restaurant with Rating above: </asp:Label>
    <asp:TextBox ID="txtMinRating" runat="server"></asp:TextBox>
    <asp:RangeValidator ID="rngVldRating" runat="server"  Type="Integer" 
            ErrorMessage="Invalid Rating" ForeColor="Red" 

            ControlToValidate="txtMinRating" Display="Dynamic" MaximumValue="5" MinimumValue="0" ></asp:RangeValidator>
    <asp:RequiredFieldValidator runat="server" ID="rqdvldRating" 
            ErrorMessage="Can not be blank" ForeColor="Red"
            ControlToValidate="txtMinRating" Display="Dynamic" >
    </asp:RequiredFieldValidator>
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"/>
    <div runat="server" id="divRestaurantReviews">

    </div>
    <br /><br /><br />
</asp:Content>

