<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="ml_ztool.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mng_main" runat="server">
        
       <form id="form1" runat="server">

	
		<table style="border-collapse: collapse" class="table table-responsive"><tr><td><%=mmtitle %></td><td style="width:30px"></td></tr></table>
		
            <div class="col-lg-4 col-md-6"><asp:TextBox ID="TextBox1" runat="server" Height="114px" TextMode="MultiLine" Width="100%"></asp:TextBox></div>
            
            <div class="col-lg-4 col-md-6"><asp:TextBox ID="TextBox2" runat="server" Height="114px" TextMode="MultiLine" Width="100%"></asp:TextBox></div>
            
            <div class="col-lg-4 col-md-6"><asp:TextBox ID="TextBox3" runat="server" Height="114px" TextMode="MultiLine" Width="100%"></asp:TextBox></div>
            
           <div class="col-lg-4 col-md-6">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="執行" />  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="列出table" /> <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="列出Trigger" /> <asp:Button ID="Button5" runat="server" OnClick="Button4_Click" Text="列出Store Procedure" />
            <select name="tables"><option value=""></option><%

                                                                msqlcom.Parameters.Clear();
                                                                msqlcom.CommandText = "exec sp_tables @table_type = \"'TABLE'\"";
                                                                dv1 = aa.dv_param(msqlcom,ref mmsg);
                                                                if (dv1!=null)
                                                                {
                                                                    for (int i = 0; i<=dv1.Count - 1; i++)
                                                                    {
                                                                        Response.Write("<option>" + aa.chkdbnull(dv1[i]["table_name"]) + "</option>");
                                                                    }
                                                                    dv1.Dispose();
                                                                }
                                                                if (msqlcom != null) msqlcom.Dispose();
                                      %></select>
                                      <asp:Button ID="Button6" runat="server" OnClick="Button5_Click" Text="重設" />
                                      <asp:Button ID="Button7" runat="server" OnClick="Button6_Click" Text="改dbo" />
            </div>
            <div class="table table-responsive"><asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            </div>
  </form>

   <%
        
       close_app(); %>       
   
    </div>
</asp:Content>

