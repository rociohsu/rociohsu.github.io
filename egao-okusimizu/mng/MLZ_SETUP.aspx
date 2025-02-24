<%@ Page  Language="C#"  ValidateRequest="false" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_SETUP.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div id="mng_main"  runat="server">
        
       <form name="form2" method="post" action="<%=XXurl%>" enctype="multipart/form-data">
        <input type="hidden" name="sno" value="<%=msno%>" />
        <input type="hidden" name="fsno" value="<%=mfsno%>"/>
        <input type="hidden" name="b1" value=""/>

	
	                    <nav class="navbar text-center "  style="margin-left:30px;margin-right:30px"><input type="submit" class="btn btn-primary active" value="儲存" onclick="form2.b1.value='SAV';" />　<input type="reset" class="btn btn-success" value="重來"/> </nav>
				
                        
			
						<table class="table table-condensed table-hover table-striped" >						

							<tr class="box"><td class="text-center"><%if (XXauth.IndexOf("A") >= 0) {%><input type="button" onclick="form2.b1.value='ADD';form2.submit();" class="btn btn-primary" value="新增"/><%} %></td>
							<td class="title text-center">功能名稱</td>
							<td class="title text-center">設定值</td>
							<td class="title text-center">排序</td>
							<td class="title">　</td>
							<td class="title">　</td>
							</tr>
						
							
								<%  


								    dv1 = aa.dv_param(msqlcom,ref mmsg);
								    if (dv1!=null) 
                                    {
								        for (int ii = 0; ii<=dv1.Count - 1; ii++) 
                                        {
								%>
								<tr>
									<td class="text-center"><%if (XXauth.IndexOf("E") >= 0) {%><%}%></td>
									<td class="text-left"><input type="text" size="30" class="form-control input-xs" name="web<%=dv1[ii]["sno"]%>" value="<%=dv1[ii]["nam"].ToString().Trim()%>"/></td>
									<%if (aa.chkdbnull(dv1[ii]["nam"]) == "後台管理主選單格式") { %>
                                    <td class="text-left">
                                        <input type="radio"  name="xurl<%=dv1[ii]["sno"]%>" value="左" <%if (aa.chkdbnull(dv1[ii]["url"]) == "左") { %>checked<%} %>/>左展開式　
                                        <input type="radio"  name="xurl<%=dv1[ii]["sno"]%>" value="左合" <%if (aa.chkdbnull(dv1[ii]["url"]) == "左合") { %>checked<%} %>/>左閉合式　
                                        <input type="radio"  name="xurl<%=dv1[ii]["sno"]%>" value="上" <%if (aa.chkdbnull(dv1[ii]["url"]) == "上") { %>checked<%} %>/>上
                                    </td>
                                    <%}
                                        else if (aa.chkdbnull(dv1[ii]["nam"]) == "後台管理LOGO")
                                        {%>
                                            <td class="text-left">
                                                
                                                    <input type="text" class="form-control input-xs" name="xurl<%=dv1[ii]["sno"]%>" value="<%=(dv1[ii]["url"])%>"/>
                                                    <input type="file" name="filelogo" />
                                                    
                                                
                                            </td>
                                        <%}

                                        else { %>
                                    <td class="text-left"><input type="text" size="55" class="form-control input-xs" name="xurl<%=dv1[ii]["sno"]%>" value="<%=(dv1[ii]["url"])%>"/></td>
                                    <%} %>
									<td class="text-center"><input type="text" size="4" class="form-control input-xs" name="sot<%=dv1[ii]["sno"]%>" value="<%=dv1[ii]["sot"]%>"/></td>
									<td class="text-center"><%if (XXauth.IndexOf("D") >= 0) {%><input type="button" onclick="javascript:if (confirm('確定刪除嗎?')) {form2.b1.value='DEL';form2.sno.value='<%=dv1[ii]["sno"]%>';form2.submit();}" class="btn btn-danger" value="刪除"/><%}%></td>
								</tr>
								
								<% 
								}
                                   dv1.Dispose();
                                }
									
								%>						
						
						
						     				
						
						</table>										
						
		 <nav class="navbar text-center "  style="margin-left:30px;margin-right:30px"><input type="submit" class="btn btn-primary active" value="儲存" onclick="form2.b1.value='SAV';" />　<input type="reset" class="btn btn-success" value="重來"/> </nav>
				
			</form>
	

   
    </div>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <%
        if (msqlcom != null) msqlcom.Dispose();
        close_app(); %>
    </asp:Content>