<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML_POP.master" AutoEventWireup="true" CodeFile="MLZ_MULTIUPLOAD.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="mng_main" class=" container-fluid" runat="server">
<form name="form2" method="post" action="" enctype="multipart/form-data" class="form">
<input type="hidden" name="b1"/>
<input type="hidden" name="fsno" value="<%=mfsno%>"/>
<input type="hidden" name="sno" value=""/>

            <table class="table table-condensed table-striped form-inline input-sm">
            <tr class="success">
                <td colspan="7" class="text-center"><input type="button" class="btn btn-primary active" value="多檔一次上傳" onclick="window.location.replace('MLZ_multiuploads.aspx?fsno=<%=mfsno %>&unt=<%=munt %>');" />  <input type="submit" class="btn btn-primary active" value="儲存" onclick="form2.b1.value='SAV';" /> </td>
              </tr>
               <tr>
                <td class="text-center"> 序</td>
                <td class="text-center">上傳圖片</td>
                <td class="text-center">排序</td>
                <td class="text-center">呈現</td>
                <td class="text-center"> 文字說明</td>
                <td class="text-center">預覽/下載</td>
                <td class="text-center"></td>
              </tr>
              
             
            

<%
    
        for (int i = 0;i<=4;i++) {%>
              <tr>
                <td class="text-right"> 新增</td>
                <td class="text-left"><input type="file" name="nfile<%=i %>" class="form-control" style="border-color:Red" /></td>
                <td class="text-center"> <input type="text" name="nsot<%=i %>" value="" class="form-control" size="2" style="border-color:Red"/></td>
                <td class="text-center"> <input type="checkbox" name="nonf<%=i %>" value="1" checked style="border-color:Red"/></td>
                <td class="text-left"> <input type="text" name="nmem<%=i %>" value=""  class="form-control"  size="50" style="border-color:Red"/></td>
               
                <td class="text-center">&nbsp;</td>
                <td class="text-center">&nbsp;</td>
              </tr>
              <% }
                  %>
               
               <%  
                   

                   msqlcom.CommandText = "select * from CABASE_img where unt=@munt and fsno=@mfsno order by sot";
                   dv1 = aa.dv_param(msqlcom, ref mmsg);
                   if (dv1!=null) {
                       for (int i=0; i<dv1.Count; i++) {
              %>
              
              <tr>
                <td class="text-left"> &nbsp;</td>
                <td class="text-left"><input type="file" name="file<%=aa.chkdbnull(dv1[i]["sno"]) %>" class="form-control" />
                </td>
                <td class="text-center"> <input class="form-control" type="text" name="sot<%=aa.chkdbnull(dv1[i]["sno"]) %>" value="<%=aa.chkdbnull(dv1[i]["sot"]) %>"  size="2"/></td>
                <td class="text-center"> <input type="checkbox" name="onf<%=aa.chkdbnull(dv1[i]["sno"]) %>" value="1" <%if (aa.chkdbnull(dv1[i]["onf"])=="1") { %>checked<%} %>/></td>
                <td class="text-left"> <input class="form-control" type="text" name="mem<%=aa.chkdbnull(dv1[i]["sno"]) %>" value="<%=aa.chkdbnull(dv1[i]["mem"]) %>"  size="50"/></td>

                <%if (aa.chkdbnull(dv1[i]["img"]) != "")
                  {  %>
                <td class="text-left"> <a href="../<%=aa.chkdbnull(dv1[i]["img"]) %>" target="_blank"><img src="<%=aa.chkdbnull(dv1[i]["img"]) %>" border="0" height="30" /></a></td>
               <%}
                  else
                  {%>
                  <td class="text-left"></td>
               <%} %>
                <td class="text-left"> <input type="button" class="btn4" value="刪" onclick="if (confirm('確定嗎?')) {form2.b1.value='D';form2.sno.value='<%=dv1[i]["sno"] %>';form2.submit();}"/></td>
              </tr>

            <%  
                    }
                    dv1.Dispose();
                }
                if (msqlcom != null) msqlcom.Dispose();
                if (msqlcom2 != null) msqlcom2.Dispose();

             %>
              <tr class="success">
                <td colspan="7" align="center" class="page2"><input type="submit" class="btn btn-primary active" value="儲存" onclick="form2.b1.value='SAV';" /> </td>
              </tr>
              <tr><td colspan="7" align="left""><%if (Session["UploadFiles"] != null) Response.Write(@"<input type=""hidden"" name=""multi_images"" value=""" + Session["UploadFiles"].ToString() + @""">"); %></td></tr>
            </table>
            <%if (mmsg2.Length > 0) Response.Write(@"<p align=""center""><font color=""#FF0000"">" + mmsg2 + @"</font></p>"); %>
	</form></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <%
        if (msqlcom != null) msqlcom.Dispose();
        if (msqlcom2 != null) msqlcom2.Dispose();
        close_app(); %>
</asp:content>