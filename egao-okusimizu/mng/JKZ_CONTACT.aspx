<%@ Page  Language="C#"  ValidateRequest="false"  MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="JKZ_CONTACT.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="mng_main" runat="server">
        
        <form name="form2" method="post" action="" class="form" >
        <input type="hidden" name="xord" value="<%=xord%>"/>
        <input type="hidden" name="xsot" value="<%=xsot%>"/>
        <input type="hidden" name="page" value="<%=mpage%>"/>
        <input type="hidden" name="mc" value="<%=mc%>"/>
        <input type="hidden" name="b1" value=""/>
        <input type="hidden" name="sno" value=""/>
            <div class="form form-group form-inline">
                <label>關鍵字</label><input type="text" class="form-control input-sm " name="t1" value="<%=mt1 %>" /><input type="submit" class="btn btn-sm btn-primary active" value="查詢" title="" onclick="form2.b1.value='q'"  />
            </div>    
				
           
            <%		
		    
              
      int mindex=0;
      int mpc=1;
      int gi = 1;
      Response.Write(aa.show_page1_param(ref dv1, msqlcom, msqlcom2, ref mpage, mc, ref mpc, ref mindex, ref mmsg));
      if (mmsg.Length > 0) { Response.Write(aa.alert(mmsg)); }
      %>  
				
		
		
		 <div class="table table-responsive">
		    <table class="table table-hover table-striped" id="mngtable">
			<thead>
                <tr >
				<th >　</th>
				<th ><%if (XXauth.IndexOf("A")>=0) {%><a class="btn btn-primary fancybox fancybox.iframe" href="<%=ZZurl%>_MOD.aspx?" title="">新增</a><%}%></th>
				
                <%=aa.page_th(xord, xsot, "問題分類,@內容,@狀態,@建立日期,@異動日")%>              
               <th></th>
			 </tr>
			</thead>
            <tbody>

	
					  <%if (dv1!=null) {
                      for (int jj = mindex; jj<=dv1.Count - 1; jj++) 
                      {
                      %>			
			<tr>
				<td ><%=gi%></td>
				<td ><%if (XXauth.IndexOf("E") >= 0)
                         {%><a class="btn btn-primary fancybox fancybox.iframe" href="<%=ZZurl%>_MOD.aspx?sno=<%=dv1[jj]["sno"]%>" title="修改資料">修改</a>
                    
                    <%}%></td>
				        <td class="text-left"><%= aa.decrypt(aa.chkdbnull(dv1[jj]["typnam"]))%></td>
                <td class="text-left"><%= aa.chkdbnull(dv1[jj]["tpcs"])%></td>
                <td class="text-left"><%= aa.chkdbnull(dv1[jj]["stsnam"])%></td>
                
        				<td class="text-center"><%= aa.chkdbnull(dv1[jj]["crt_dat"])%></td>
				<td class="text-center"><%= aa.chkdbnull(dv1[jj]["upd_dat"])%></td>
				<td class="text-center" colspan="3"><%if (XXauth.ToString().IndexOf("D") >= 0) {%><input type="button" class="btn btn-danger active" onclick="javascript:if (confirm('確定刪除此筆資料嗎?')){form2.sno.value='<%=dv1[jj]["sno"]%>';form2.b1.value='D';form2.submit();}" value="刪" title="刪除"/> <%}%></td>
			</tr>
			 	<%
			 	    gi++;
			 	    if (gi > mc)  break;
			 	    
                	}
			 	dv1.Dispose();
                }
            		 %>  			
                </tbody>
		</table>
		</div>
						
						
		<%=aa.show_page2(mpage,mpc)%>
									
					
	
    </form>

   
    </div>
</asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">


        <%if (msqlcom != null) msqlcom.Dispose();
            if (msqlcom2 != null) msqlcom2.Dispose(); %>
</asp:Content>

