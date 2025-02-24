<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="ML_INDEXMAIN.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <link rel="stylesheet" href="theme/bower_components/Ionicons/css/ionicons.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="mng_main" runat="server">
         <form name="form2" method="post" action="" class="form" >
        
        <input type="hidden" name="b1" value=""/>
        <input type="hidden" name="sno" value=""/>

       
             </form>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    
  <% close_app(); %>
    </asp:Content>
