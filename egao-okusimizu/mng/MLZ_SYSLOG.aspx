<%@ Page  Language="C#"  ValidateRequest="false"  MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_SYSLOG.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 
 <div id="mng_main"  runat="server">
        
<form name="form2" method="post" action="" class="form" >
<input type="hidden" name="xord" value="<%=xord%>"/>
<input type="hidden" name="xsot" value="<%=xsot%>"/>
<input type="hidden" name="page" value="<%=mpage%>"/>
<input type="hidden" name="mc" value="<%=mc%>"/>
<input type="hidden" name="b1" value=""/>
<input type="hidden" name="sno" value=""/>
    <input type="hidden" name="xpop" value="<%=xpop %>" />
    <input type="hidden" name="xappnam" value="<%=xappnam %>" />
    <input type="hidden" name="xurl" value="<%=xurl %>" />
    <input type="hidden" name="xxid" value="<%=xxid %>" />
			<%close_app();%>
 <div class="form form-group form-inline">
        <div class="row">
            <div class="col-lg-4 col-md-4">
                
                    <label>關鍵字</label>
                    <input type="text" class="form-control" name="t1" id="t1" value="<%=mt1%>">
                    <button type="submit" class="btn btn-info"  title="查詢"  onclick="form.b1.value='q'"><i class="fa fa-search"></i></button>
            </div>
       <div class="col-lg-8 col-md-8">
       <%if (Session["mmadm"].ToString() == "2")
           { %><input type="button" class="btn btnB" value="清空" onclick="form2.b1.value='CLR';form2.submit();" />
       <%if (xurl != "")
           { %><input type="button" onclick="window.location='<%=xurl%>'" class="btn btn-warning" value="返回" title="回前一頁"  /> <%} %>       
     單元:<select name="xxunt" class="form-control"><option value="">全部</option>
                <% =mselA                     %>
            </select>
        人員:<select name="xxcrt_usr" class="form-control"><option value="">全部</option>
                <%=mselB                     %>
            </select>
     <input type="date" name="sdt" value="<%=msdt %>" class="form-control pickdate" placeholder="起日" size="10"> - <input type="date" name="edt" value="<%=medt %>" class="form-control pickdate" placeholder="止日" size="10">
     <%} %>
     </div> </div>
       </div>    
		<hr />
    			<%		
		    
              
      int mindex=0;
      int mpc=1;
      int gi = 1;
      Response.Write(aa.show_page1_param(ref dv1, msqlcom, msqlcom_b,ref mpage, mc, ref mpc, ref mindex, ref mmsg));
      if (mmsg.Length > 0) { Response.Write(aa.alert(mmsg)); }
      %>  
   

    

    <div class="table table-responsive">
		    <table class="table table-hover table-striped" id="mngtable">
			<thead >
                <tr class="box">
				<th >　</th>
				
				
                <%=aa.page_th(xord, xsot, "來源IP位置,@程式,@單元,@後台帳號,@事件內容,@日期")%>              
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
				        <td class="text-center"><%= aa.chkdbnull(dv1[jj]["crt_ip"])%></td>
				        <td class="text-left"><%= aa.chkdbnull(dv1[jj]["appnam"])%></td>
                        <td class="text-left"><%= aa.chkdbnull(dv1[jj]["unt"])%></td>
                        <td class="text-left"><%= aa.chkdbnull(dv1[jj]["crt_usr"])%></td>
                        <td class="text-left"><%= aa.chkdbnull(dv1[jj]["mem"])%></td>
                        <td class="text-center"><%= aa.chkdbnull(dv1[jj]["crt_dat"])%></td>
                        <td class="text-center" colspan="2"><%if (Session["mmadm"].ToString() == "2") {%><input type="button" class="btn btn-danger active" onclick="javascript:if (confirm('確定刪除此筆資料嗎?')){form2.sno.value='<%=dv1[jj]["sno"]%>';form2.b1.value='D';form2.submit();}" value="刪" title="刪除"/><%}%></td>
    
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
 <%
          if (msqlcom != null) { msqlcom.Dispose(); }
          
          if (msqlcom_b != null) { msqlcom_b.Dispose(); }

             %>

   
    </div>
</asp:Content>

