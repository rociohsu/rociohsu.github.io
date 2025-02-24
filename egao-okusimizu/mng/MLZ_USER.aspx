<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_USER.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div id="mng_main" runat="server" >
        
       <form name="form2" id="form2" method="post" class="form" action=""  >

        <input type="hidden" name="b1" value=""/>
        <input type="hidden" name="xord" value="<%=xord%>"/>
        <input type="hidden" name="xsot" value="<%=xsot%>"/>
        <input type="hidden" name="page" value="<%=mpage%>"/>
        <input type="hidden" name="mc" value="<%=mc%>"/>
        <input type="hidden" name="sno" value=""/>
        
                   
       <div class="form form-group form-inline">
      
        <label>關鍵字</label>
        <input type="text" class="form-control" name="t1" id="t1" value="<%=mt1%>">
                    
        <button type="submit" class="btn btn-info"  title="查詢"  onclick="form.b1.value='q'"><i class="fa fa-search"></i></button>
                 
      
    </div>
          
           

           
        
        
		<%		
            int mindex = 0;
            int mpc = 1;
            int gi = 1;

            Response.Write(aa.show_page1_param(ref dv1,sqlcom, sqlcom_b,ref mpage, mc, ref mpc, ref mindex, ref mmsg));
           
            if (mmsg.Length > 0) { Response.Write(aa.alert(mmsg)); }
            %>
            
            <div class="table table-responsive">
		    <table class="table table-hover table-striped " id="mngtable">
			<thead>
                <tr class="box">
				<th >　</th>
				<th ><%if (XXauth.IndexOf("A")>=0) {%><a class="btn btn-default btnA" href="<%=ZZurl%>_MOD.aspx" title="新增一筆">新增</a><%}%></th>
				
                <% =aa.page_th(xord, xsot, "帳號,@E-mail,@姓名,@最後登入日期,@狀態,@異動日")                   %>               
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
				<td ><%if (XXauth.IndexOf("E") >= 0)
                         {%><input type="button" class="btn btn-default  btnA" value="修改" onclick="form2.action = '<%=ZZurl%>_MOD.aspx';form2.sno.value='<%=dv1[jj]["uno"]%>';form2.b1.value='q';form2.submit();" title="修改"/><%}%></td>
				<td ><%=aa.chkdbnull(dv1[jj]["uid"])%></td>
                <td ><%=aa.chkdbnull(dv1[jj]["email"])%></td>
                <td class="text-center" ><%=aa.chkdbnull(dv1[jj]["nam"])%></td>

				
				
				<td class="text-center" ><%=aa.chkdbnull(dv1[jj]["login_time"])%></td>
				<td  class="text-center"><%=aa.chkdbnull(dv1[jj]["onfnam"])%></td>
				<td  class="text-center"><%=aa.chkdbnull(dv1[jj]["upd_dat"])%></td>
				<td  colspan="2"><%if (XXauth.IndexOf("D") >= 0 && (int)dv1[jj]["uno"] != 1)
                                                    {%><input type="button" class="btn btn-danger" value="刪" onclick="javascript:if (confirm('確定刪除此筆資料嗎?')){form2.sno.value='<%=dv1[jj]["uno"]%>';form2.b1.value='D';form2.submit();}" title="刪除"/><%}%></td>
			</tr>
			
			 	<%
                                                    gi++;
                                                    if (gi > mc) break;

                              }
                              dv1.Dispose();
                          }
            		 %>  			
	</tbody>
		</table></div>		
<%=aa.show_page2(mpage,mpc)%>
			
			</form>   
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <%
        if (sqlcom != null) sqlcom.Dispose();
        if (sqlcom_b != null) sqlcom_b.Dispose();
        close_app(); %>
</asp:content>

