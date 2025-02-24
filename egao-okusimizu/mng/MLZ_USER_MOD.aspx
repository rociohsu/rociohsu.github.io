<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_USER_MOD.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!--input type=text, password, datetime, datetime-local, date, month, time, week, number, email, url, search, tel, and color.-->
    
			    	
    <div id="mng_main"    runat="server" >
        
       <form name="form2" id="form2" method="post" >
        <input type="hidden" name="sno" value="<%=msno%>"/>
        <input type="hidden" name="b1" value="<%=mb1%>"/>
        <input type="hidden" name="xord" value="<%=xord%>"/>
        <input type="hidden" name="xsot" value="<%=xsot%>"/>
        <input type="hidden" name="page" value="<%=mpage%>"/>
        <input type="hidden" name="mc" value="<%=mc%>"/>
        
        <input type="hidden" name="t1" id="t1" value="<%=mt1 %>" />

       <nav class="nav sidemenu"><input type="submit" class="btn btn-success " value="儲存" onclick="form2.b1.value='SAV';" />　<input type="button" onclick="form2.action='<%=YYurl %>';form2.b1.value='q';form2.submit();" class="btn btnB" value="返回"/></nav>    
        <div class="row">

        
                        <div class="col-md-6 col-lg-6">                        
                            <div class="box box-danger ">
                                <div class="box-header"><h3>帳號資訊(<%if (msno == "0") {%>新增<%} else {%>修改<%} %>)</h3></div>
                                <div class="box-body pad">
                               <div class="input-group">
                                       <span class="input-group-addon">*帳　　號</span>
                                   <input type="text" name="uid" size="10" class="form-control" value="<%=muid%>"  autocomplete="off">
                               </div>
                           
                           
                               <div class="input-group">
                                       <span class="input-group-addon">*密　　碼</span>
                                   <input type="password" name="pwd" size="10" class="form-control" value="" autocomplete="off" aira-title="<%=aa.decrypt(mpwd) %>">
                               </div>
                          
                                <div class="input-group">
                                       <span class="input-group-addon">*信　　箱</span>
                                    <input type="email" name="email" size="30" class="form-control"  for="inputSuccess1" value="<%=memail%>" placeholder="(匯出資料時通知用)"/> 
                                </div>
                           
                                <div class="input-group">
                                       <span class="input-group-addon">帳號啟用</span><input type="radio" name="onf" value="0" <%if (monf=="0") { %>checked<%} %> />否 <input type="radio" name="onf" value="1" <%if (monf=="1") { %>checked<%} %> />是</div>
                                    <%if (Session["mmadm"].ToString() == "2")
                                        { %>
                                  <div class="input-group">
                                       <span class="input-group-addon">SA管理員</span><input type="checkbox" class="largerCheckbox" value="2" name="adm" <%if (madm == "2")
                                                                         { %>checked<%}%> /></div>
                                <%} %>

                           </div></div>
                        </div>
                        
                        <div class="col-md-6 col-lg-6">
                            <div class="box box-warning  ">
                                <div class="box-header"><h3></h3></div>
                                <div class="box-body">
                          
                                    <div class="input-group">
                                       <span class="input-group-addon">姓　　名</span>
                                       <input type="text" name="nam" size="10" class="form-control" value="<%=mnam%>">
                                   </div>
                                   
                          　<hr /> 
                            <div class="input-group">
                                       <span class="input-group-addon">所屬群組</span>
                                  <% 
    
                                    //if (mb1==null || mb1=="") 
                                    //{
                                        sqlcom = new System.Data.SqlClient.SqlCommand();
                                      sqlcom.Parameters.Add("@sno", System.Data.SqlDbType.Int, 8).Value = msno;
                                      sqlcom.CommandText = "select * from CABASE_user_group where uno=@sno";
                                      dv1 = aa.dv_param(sqlcom,ref mmsg);
                                        mc = dv1.Count;
                                        Array.Resize(ref bb,mc);
                                        for (int mtempi = 0 ; mtempi<= dv1.Count - 1; mtempi++ )
                                        {
                                            bb[ii] = dv1[mtempi]["grno"].ToString().Trim();
                                            ii = ii + 1;
            
                                        }
                                        dv1.Dispose();
                                    //}
                                    //else
                                    //{
                                    //    if (ii == 0 && mgrnos!=null)  bb = mgrnos.Split(',');
                                    //    else Array.Resize(ref bb,0);
                                    //}
    
                                      sqlcom.CommandText = "select * from CABASE_group order by sot";
                                      dv1 = aa.dv_param(sqlcom,ref mmsg);

    
                                    if (dv1!=null)
                                    {
                                        for (int mtempi = 0; mtempi<=dv1.Count - 1; mtempi++) 
                                        {
                                            ii = 0;
                                            for (int mtempj = 0 ; mtempj<bb.Length; mtempj++) 
                                            {
                                                if (dv1[mtempi]["sno"].ToString().Trim() == bb[mtempj]) ii = 1;
                                            }
                                            if (ii != 0) Response.Write(@"<input type=""checkbox"" name=""grnos"" value=""" + dv1[mtempi]["sno"].ToString().Trim() + @""" checked>" + dv1[mtempi]["nam"].ToString().Trim() + "　");
                                            else Response.Write(@"<input type=""checkbox"" name=""grnos"" value=""" + dv1[mtempi]["sno"].ToString().Trim() + @""">" + dv1[mtempi]["nam"].ToString().Trim() + "　");
            
                                        }
        
    
                                    }
					    %>	

                            </div>
                           


                                <div class="input-group">
                                       <span class="input-group-addon">登入身份</span>
                                       <input type="radio" name="unt_mng" value="0" <%if (munt_mng=="0") { %>checked<%} %> />職員 <input type="radio" name="unt_mng" value="1" <%if (munt_mng=="1") {%>checked<%}%>/>主管 <input type="radio" name="unt_mng" value="2" <%if (munt_mng=="2") {%>checked<%}%>/>總經理 
                                </div>
                                
                                
                                    

                            </div></div>
                        </div>
  </div>
               <div class="row">
                     <div class="col-md-12 col-lg-12">                        
                            <div class="box box-info  ">
                                <div class="box-header"><h3>群組的單元權限</h3></div>
                                <div class="box-body pad">
                                    <div>		
                                        <div>（您可以利用群組功能省下每次幫每個帳號勾選以下權限的麻煩;但您也可以利用以下各別勾選開放原本群組裡原本沒有的權限給此帳號。）</div>
                                        
                                         <div id="treeview3" class="treeview">
                                    <ul class="list-group">    
                                        <% Response.Write(menu1(0, 0, 1, "", 1, 0)); %>        </ul></div>
                           
                                    </div>
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
    <script type="text/javascript">
        function selAll(mfsno,msno)
        {
            
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