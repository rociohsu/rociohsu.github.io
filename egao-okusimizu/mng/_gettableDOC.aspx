<%@ Page  Language="C#"  ValidateRequest="true"  CodeFile="_gettableDOC.aspx.cs" Inherits="_default" %>
  
<html><body>
			
    <div style="margin:15px; margin-right:15px">
        <p style="text-align:center; font-size:20px; "><b>Database Schema 文件</b></p>
        <div style="text-align:right">製表人: XXX <br />版本: v1.0 <br />日期:<%=System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") %></div>
        <br /><br />TABLE 索引
     <%=sp2.ToString() %>   
        <br /><br />

     <%=sp.ToString() %>
	</div>
    </body></html>