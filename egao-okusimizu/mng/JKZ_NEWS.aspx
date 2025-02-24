<%@ Page  Language="C#"  ValidateRequest="false"  MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="JKZ_NEWS.aspx.cs" Inherits="_default" %>
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
       
                    <label>分類<select name="typ" class="form-control"><option value="">全部</option><%=mselA %></select></label>
                    
                    <label>關鍵字</label>
                    <input type="text" class="form-control" name="t1" id="t1" value="<%=mt1%>">
       
                    <button type="submit" class="btn btn-info"  title="查詢"  onclick="form.b1.value='q'"><i class="fa fa-search"></i></button>
       
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
                <tr class="box">
				<th >　</th>
				<th ><%if (XXauth.IndexOf("A")>=0) {%><input type="button" class="btn btn-default  btnA" value="新增" onclick="form2.action = '<%=ZZurl%>_MOD.aspx';form2.b1.value='q';form2.submit();" title="修改"/><%}%></th>
				
                <%=aa.page_th(xord, xsot, "圖片,@分類,@標題/次標,@啟用,@起始日期,@結束日期,@異動日期")%>              
               <th  class="text-center"><a class="btn btn-default" onclick="javascript:if (confirm('確定刪除勾選的資料嗎?')){form2.b1.value='DEL';form2.submit();}" value="" title="批次大量刪除"><i class="fa fa-trash"></i></a> <input type="checkbox" id="checkAll" class="largerCheckbox"></th>
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
                         {%><input type="button" class="btn btn-default  btnA" value="修改" onclick="form2.action = '<%=ZZurl%>_MOD.aspx';form2.sno.value='<%=dv1[jj]["sno"]%>';form2.b1.value='q';form2.submit();" title="修改"/><%}%></td>
                <td class="text-left"><img src="../<%= aa.chkdbnull(dv1[jj]["img"])%>" style="max-height:50px;max-height:50px" /></td>
                <td class="text-left"><%= aa.chkdbnull(dv1[jj]["typnam"])%></td>
                <td class="text-left"><%= aa.chkdbnull(dv1[jj]["tpc"])%><br /><%= aa.chkdbnull(dv1[jj]["tpcs"])%></td>
                <td class="text-center"><%= aa.chkdbnull(dv1[jj]["onfnam"])%></td>
								        <td class="text-center"><%= aa.showdate(aa.chkdbnull(dv1[jj]["sdt"]),"yyyy/MM/dd")%></td>
                <td class="text-center"><%= aa.showdate(aa.chkdbnull(dv1[jj]["edt"]),"yyyy/MM/dd")%></td>
                
				<td class="text-center"><%= aa.chkdbnull(dv1[jj]["upd_dat"])%></td>
				<td class="text-center" colspan="3"><%if (XXauth.ToString().IndexOf("D") >= 0) {%><a class="btn btn-danger" onclick="javascript:if (confirm('確定刪除此筆資料嗎?')){form2.sno.value='<%=dv1[jj]["sno"]%>';form2.b1.value='D';form2.submit();}" value="" title="刪除"><i class="fa fa-trash"></i></a> <input type="checkbox" class="largerCheckbox" name="c_del" value="<%=dv1[jj]["sno"] %>" /><%}%></td>
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
           


    <script type="text/javascript">
        $(document).ready(function () {
        $("#checkAll").click(function () {
            if ($("#checkAll").prop("checked")) {
                $("input[name='c_del']").each(function () {
                    $(this).prop("checked", true);
                });
            } else {
                $("input[name='c_del']").each(function () {
                    $(this).prop("checked", false);
                });
            }
        });
        
        });
    </script>

        <%if (msqlcom != null) msqlcom.Dispose();
            if (msqlcom2 != null) msqlcom2.Dispose(); %>
</asp:Content>

