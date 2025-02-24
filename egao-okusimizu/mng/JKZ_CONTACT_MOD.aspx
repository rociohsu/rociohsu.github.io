<%@ Page  Language="C#"  ValidateRequest="false" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="JKZ_CONTACT_MOD.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div id="mng_main" class=""  runat="server" >
        
    <form name="form2" method="post" enctype="multipart/form-data" >
    <input type="hidden" name="b1"/>
    <input type="hidden" name="sno" value="<%=msno%>"/>
    <input type="hidden" name="xord" value="<%=xord%>"/>
    <input type="hidden" name="xsot" value="<%=xsot%>"/>
    <input type="hidden" name="page" value="<%=mpage%>"/>
    <input type="hidden" name="mc" value="<%=mc%>"/>
    <input type="hidden" name="t1" value="<%=mt1 %>" />
            
   <nav class="nav sidemenu " >
		  <input type="submit" class="btn  btnA" value="儲存" onclick="form2.b1.value='SAV';" />
      <input type="submit" class="btn  btnA" value="寄E-mail回覆" onclick="if (confirm('要將回覆內容寄給對方?')) { form2.b1.value = 'EML'; }" />
          <input type="button" onclick="form2.action='<%=YYurl %>';form2.b1.value='q';form2.submit();" class="btn btnB" value="返回"/>

	   </nav> 
        <div class="row">
            <div class="col-md-6 col-lg-6">                        
                            <div class="box box-danger well ">
                                <div class="box-header"><h3 class="box-title">聯絡我們(<%if (msno == "0") {%>新增<%} else {%>修改<%} %>)</h3></div>
                                <div class="box-body pad">
                                    <label>分　　類</label>
                                    <select name="typ" class="form-control"><%=mselA %></select>
                                    <label>電　　話</label>
                                    <div><input type="text" name="tel"  class="form-control" value="<%=mtel%>"/></div>
                                    <label>傳　　真</label>
                                    <div><input type="text" name="fax"  class="form-control" value="<%=mfax%>"/></div>
                                    <label>郵遞區號</label>
                                    <div><input type="text" name="adr_code"  class="form-control" value="<%=madr_code%>"/></div>
                                    <label>縣　　市</label>
                                    <div><input type="text" name="cty"  class="form-control" value="<%=mcty%>"/></div>
                                    <label>地　　區</label>
                                    <div><input type="text" name="ara"  class="form-control" value="<%=mara%>"/></div>
                                    <label>地　　址</label>
                                    <div><input type="text" name="adr"  class="form-control" value="<%=madr%>"/></div>
                                    <label>信　　箱</label>
                                    <div><input type="text" name="email"  class="form-control" value="<%=memail%>"/></div>
                                    <label>內　　容</label>
                                    <div><textarea name="mem" class="form-control" rows="3" cols="50"><%=mmem%></textarea></div>
                                </div>
                            </div>
            </div>
            <div class="col-md-6 col-lg-6">                        
                            <div class="box box-danger well ">
                                <div class="box-header"><h3 class="box-title"></h3></div>
                                <div class="box-body pad">
                                 
                                    <label class="form-inline">狀　態 <input type="radio" class="radio" name="sts" value="0" <%if (msts=="0") { %>checked="checked"<%} %> />待處理
                                      <input type="radio" class="radio" name="sts" value="1" <%if (msts=="1") { %>checked="checked"<%} %> />處理中
                                      <input type="radio" class="radio" name="sts" value="2" <%if (msts=="2") { %>checked="checked"<%} %> />已回覆

                                    </label>
                                    <div class="form-inline"> </div>
                                    
                                    <label>處理備註</label>
                                    <div><textarea name="rtn_mem" class="form-control" rows="5" cols="50"><%=mrtn_mem%></textarea></div>
                                </div>
                            </div>
            </div>

        </div>
          
          
           

        

           <%if (msno !="0") {%>
	        <div class="row">
		      <div class="col-md-12 col-lg-12">
                  <div class="box box-warning ">
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
    <%
        if (msqlcom != null) msqlcom.Dispose();
        close_app(); %>
</asp:content>
