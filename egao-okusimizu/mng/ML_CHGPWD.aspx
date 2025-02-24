<%@ Page  Language="C#"  ValidateRequest="false" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="ML_CHGPWD.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div id="mng_main"   runat="server">

        
       <form name="form2" method="post" class="form"><input type="hidden" name="b1" />
             
            <div class="col-lg-12 col-md-12">
                <nav class="navbar text-center well well-sm"  ><input type="submit" class="btn btn-primary active" value="儲存" onclick="form2.b1.value='SAV';" />　<input type="reset" class="btn btn-success" value="重來"/> </nav>
				</div>

           <div class="col-lg-6 col-md-6" >
			    <div class="box box-danger well ">
                               <div class="box-header"><h3 class="box-title">基本資料</h3></div>
                               <div class="box-body pad">
                                   <label>姓名</label>
                                   <div>
                                       <input type="text" name="nam" size="10" class="form-control" value="<%=mnam%>">
                                   </div>
                           
                                   <label>單位</label>
                                   <div>
                                       <input type="text" name="unt" size="10" class="form-control" value="<%=munt%>">
                                   </div>
                                   <label>E-mail</label>
                                    <div>
                                        <input type="email" name="email" size="30" class="form-control"  for="inputSuccess1" value="<%=memail%>" placeholder="(匯出資料時通知用)"/> 
                                    </div>		
                                   
                               </div>
                </div>

           </div> 
           <div class="col-lg-6 col-md-6" >
			    <div class="box box-danger well ">
                                <div class="box-header"><h3 class="box-title">密碼修改</h3></div>
                                <div class="box-body pad">
                                    <label>舊密碼</label>
                                    <div >
                                        <input type="password" class="form-control" name="opwd" />
                                    </div>
                                    <label>新密碼</label>
                                    <div >
                                        <input type="password" class="form-control"  name="npwd" placeholder="6至15字元英數混合" />
                                    </div>
                                    <label>再次確認新密碼</label>
                                    <div >
                                        <input type="password" class="form-control"  name="npwd2" placeholder="6至15字元英數混合" />
                                    </div>
                                    
			
                                </div>			
                </div>

           </div>
							 
        <div class="col-lg-12 col-md-12">
        <nav class="navbar text-center well well-sm"  ><input type="submit" class="btn btn-primary active" value="儲存" onclick="form2.b1.value='SAV';" />　<input type="reset" class="btn btn-success" value="重來"/> </nav>
			</div>					
            		
		
			</form>
	    

   
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <%
        if (msqlcom != null) msqlcom.Dispose();
        close_app(); %>
</asp:content>
