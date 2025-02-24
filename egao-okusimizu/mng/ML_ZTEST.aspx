<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="ML_ZTEST.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mng_main" class="forPrinted" runat="server">

        <form name="form2" method="post" class="form-inline"><input type="hidden" name="b1" />
            <input type="text" name="domain" class="form-control" placeholder="如:http://localhost:49747" value="<%=mdomain %>" />
            <input type="text" name="dir" class="form-control input-sm" placeholder="如:/xml" /><input type="button" class="btn btn-primary" value="自動載入檔案" onclick="form2.b1.value = 'lst'; form2.submit();"/>
            <input type="button" value="清除" class="btn btn-warning" onclick="if (confirm('確定刪除以下全部嗎?')) { form2.b1.value = 'DELALL'; form2.submit();}" />
            <input type="button" value="儲存" class="btn btn-primary" onclick="form2.b1.value = 'SAV';form2.submit()" />
            <input type="button" value="測試" class="btn  btn" onclick="form2.b1.value = 'TEST';form2.submit()" />

            <table class="table table-hover table-striped"><tr><td>項次</td><td>連結</td><td>方式</td><td>傳入參數</td><td>測試結果</td><td>記錄</td></tr>
            <%

                msqlcom.CommandText = "select * from CABASE_UNIT_TEST order by lnk";
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {   for (int i = 0; i < dv1.Count; i++)
                    {
                        Response.Write("<tr>");
                        Response.Write("<td>" + (i + 1) + "</td><td><input name=\"lnk" + aa.chkdbnull(dv1[i]["sno"]) + "\" class=\"form-control input-sm\" type=\"text\" value=\"" + aa.chkdbnull(dv1[i]["lnk"]) + "\"><br><textarea  class=\"form-control\" name=\"tpc" + aa.chkdbnull(dv1[i]["sno"]) + "\" rows=\"3\" cols=\"50\" placeholder=\"程式說明\">" + aa.chkdbnull(dv1[i]["tpc"]) + "</textarea></td>");
                        Response.Write("<td>");
                        Response.Write("<input class=\"form-control\" type=\"radio\" name=\"method" + aa.chkdbnull(dv1[i]["sno"]) + "\" value=\"GET\" ");
                        if (aa.chkdbnull(dv1[i]["method"])=="GET") Response.Write("checked");
                        Response.Write(">GET ");
                        Response.Write("<input class=\"form-control\" type=\"radio\" name=\"method" + aa.chkdbnull(dv1[i]["sno"]) + "\" value=\"POST\" ");
                        if (aa.chkdbnull(dv1[i]["method"])=="POST")  Response.Write("checked");
                        Response.Write(">POST");
                        Response.Write("</td>");
                        Response.Write("<td><textarea  class=\"form-control\" name=\"para" + aa.chkdbnull(dv1[i]["sno"]) + "\" rows=\"3\" cols=\"50\" placeholder=\"如:?aaa=1&bbb=2\">" + aa.chkdbnull(dv1[i]["para"]) + "</textarea><br><textarea  class=\"form-control\" name=\"para_mem" + aa.chkdbnull(dv1[i]["sno"]) + "\" rows=\"3\" cols=\"50\" placeholder=\"參數說明\">" + aa.chkdbnull(dv1[i]["para_mem"]) + "</textarea></td>");
                        Response.Write("<td>" + aa.chkdbnull(dv1[i]["sts"]) + "</td>");
                        Response.Write("<td><textarea  class=\"form-control\" rows=\"6\" cols=\"50\">" + aa.chkdbnull(dv1[i]["log"]) + "</textarea></td>");
                        Response.Write("</tr>");
                    }

                    dv1.Dispose();
                }
            %>
            </table>
                


        </form>

  
   
    </div>
    
     <%   Response.Write("<hr><br>API說明文件如下:<br>" + medm);
         if (msqlcom != null) msqlcom.Dispose();
         close_app(); %>       
</asp:Content>

