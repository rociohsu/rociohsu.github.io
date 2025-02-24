<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_GROUPS_MOD.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="mng_main"   runat="server">
<form name="form2" id="form2" method="post" action="" >
<input type="hidden" name="sno" value="<%=msno%>"/>
<input type="hidden" name="b1" value="<%=mb1%>"/>
<input type="hidden" name="xord" value="<%=xord%>"/>
<input type="hidden" name="xsot" value="<%=xsot%>"/>
<input type="hidden" name="page" value="<%=mpage%>"/>
<input type="hidden" name="mc" value="<%=mc%>"/>        
<input type="hidden" name="t1" id="t1" value="<%=mt1 %>" />


      
    <nav class="nav sidemenu"><input type="submit" class="btn btn-success " value="儲存" onclick="form2.b1.value='SAV';" />　<input type="button" onclick="form2.action='<%=YYurl %>';form2.b1.value='q';form2.submit();" class="btn btnB" value="返回"/></nav>     
    
	<div class="row">
      <div class="col-md-12 col-lg-12">                        
                            <div class="box box-danger  ">
                                <div class="box-header"><h3>群組(<%if (msno == "0") {%>新增<%} else {%>修改<%} %>)</h3></div>
                                <div class="box-body pad">
                               <label>群組名稱</label>
                               <div>
                                   <input type="text" name="nam" size="10" class="form-control" value="<%=mnam%>">
                               </div>
    	                    <label>排　　序</label>
                               <div>
                                   <input type="text" name="sor" size="26" class="form-control" value="<%=msor%>" />
                               </div>
    	        </div></div>
        </div>
        </div>
    <div class="row">
        <div class="col-md-12 col-lg-12">                        
                            <div class="box box-info well ">
                                <div class="box-header"><h3>群組的單元權限</h3></div>
                                <div id="treeview3" class="treeview">
                                    <ul class="list-group">
                                            <%
 
   
                                                Response.Write(menu1(0, 0, 1, "", 1, 0));

                                            %>	
                                        </ul>
                                    </div>
    	                        
                            </div>
                     
        </div>
        </div>
            
	       <%if (msno !="0") {%>
	   <div class="row">
		      <div class="col-md-12 col-lg-12 box">
                  <div class="box box-warning well ">
                      <div class="box-header"><h3>修改記錄</h3></div>
                      <div class="box-body pad"><label>建檔：<%=mcrt_usr %>　＠<%=mcrt_dat%></label> <label>異動：<%=mupd_usr %>　＠<%=mupd_dat%></label></div>
                   </div>

             </div>
               </div>
		   
		<%}%>				
						
						
						

					
		</form> 

   
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
     <script type="text/javascript">
         function selAll(mfsno, msno) {

             if ($("#parent" + msno).prop("checked")) {

                 $(".parent" + msno).each(function () {
                     $(this).prop("checked", true);
                 });
             } else {
                 $(".parent" + msno).each(function () {
                     $(this).prop("checked", false);
                 });
             }

             if ($("#self" + msno).prop("checked")) {

                 $(".self" + msno).each(function () {
                     $(this).prop("checked", true);
                 });
             } else {
                 $(".self" + msno).each(function () {
                     $(this).prop("checked", false);
                 });
             }




         }

    </script>
    <%
        if (sqlcom != null) sqlcom.Dispose();
        if (sqlcom_s != null) sqlcom_s.Dispose();
        close_app(); %>
</asp:content>