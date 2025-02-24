<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_GROUPS.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="mng_main"  runat="server">
        
      <form name="form2" method="post" action="" class="form" >
        <input type="hidden" name="xord" value="<%=xord%>"/>
        <input type="hidden" name="xsot" value="<%=xsot%>"/>
        <input type="hidden" name="page" value="<%=mpage%>"/>
        <input type="hidden" name="mc" value="<%=mc%>"/>
        <input type="hidden" name="sno" value=""/>
        <input type="hidden" name="b1" value=""/>

	
			
						
						
		<%
      
      int mindex=0;
      int mpc=1;
      int gi = 1;
      Response.Write(aa.show_page1_param(ref dv1, sqlcom,sqlcom_b,ref mpage, mc, ref mpc, ref mindex, ref mmsg));
      if (mmsg.Length > 0) { Response.Write(aa.alert(mmsg)); }
      %>  
					
		<div class="table table-responsive">
		    <table class="table table-hover table-striped " id="mngtable">
			<thead>
                <tr class="box">
				<th >　</th>
				<th class="text-center"><%if (XXauth.IndexOf("A")>=0) {%><a class="btn btn-default" href="<%=ZZurl%>_MOD.aspx" title="新增一筆">新增</a><%}%></th>
				
                <%=aa.page_th(xord, xsot, "群組名稱,@排序,@異動日")%>          
               <th></th>
			 </tr>
			</thead>
            <tbody>
          
         
					  <%
                          if (dv1 != null)
                          {
                              for (int jj = mindex; jj <= dv1.Count - 1; jj++)
                              {
                      %>			
			<tr>
				<td ><%=gi%></td>
				<td class="text-center"><%if (XXauth.IndexOf("E") >= 0)
                         {%><input type="button" class="btn btnA" value="修改" onclick="form2.action = '<%=ZZurl%>_MOD.aspx';form2.sno.value='<%=dv1[jj]["sno"]%>';form2.b1.value='q';form2.submit();" title="修改"/><%}%></td>
                <td class="text-center"><font color="#808080"><%=aa.chkdbnull(dv1[jj]["nam"])%></font></td>
				<td class="text-center"><%=aa.chkdbnull(dv1[jj]["sot"])%></td>	
				<td class="text-center" ><%=aa.chkdbnull(dv1[jj]["upd_dat"])%></td>	
							
				<td class="text-center"colspan="2"><%if (XXauth.IndexOf("D") >= 0)
                                                    {%><input type="button" class="btn btn-danger" value="刪" onclick="javascript:if (confirm('確定刪除此筆資料嗎?')){form2.sno.value='<%=dv1[jj]["sno"]%>';form2.b1.value='D';form2.submit();}"/><%}%></td>
			</tr>
			 	<%
                                                    gi++;
                                                    if (gi > mc) break;

                              }
                              dv1.Dispose();
                          }
            		 %>  		 			
                </tbody>
		</table>
		</div>
 						
						
			<%=aa.show_page2(mpage,mpc) %>
		
			</form>
	 <%
          if (sqlcom != null) { sqlcom.Dispose(); }
          if (sqlcom_b != null) { sqlcom_b.Dispose(); }
             %>
        <%close_app(); %>
    </div>
</asp:Content>

