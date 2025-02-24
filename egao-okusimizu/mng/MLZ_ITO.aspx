<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="MLZ_ITO.aspx.cs" Inherits="_default" %>
<%@ Register Src="~/mng/_fancybox.ascx" TagPrefix="wuc" TagName="_fancybox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="mng_main"    runat="server">
        
 <form name="form2" method="post" action="<%=YYurl%>" class="form-horizontal">
     <input type="hidden" name="b1" />
<input type="hidden" name="sno" value="<%=msno%>" />
<input type="hidden" name="fsno" value="<%=mfsno%>" />
<input type="hidden" name="mode" value="<%=mmode%>" />

						<%if (mmode!="") {%>
     <div class="row">
                    <div class="col-md-12 col-lg-12">                        
                            <div class="box box-danger well ">
                                <div class="box-header"><h3><%if (mmode.IndexOf("A") >= 0)
                                                                                  { %>新增<%}
    else
    { %>修改單元<%} %></h3>　</div>
                                <div class="box-body pad"><div>

						單元:<input type="text" name="web" size="16" value="<%=mweb%>" class="form-control input-md-2">
						網址 :<input type="text" name="xurl" size="50" value="<%=murl%>" class="form-control input-md-2">
						ICON :<input type="text" name="xurl2" size="50" value="<%=murl2%>"  class="form-control input-md-2 icon-picker" placeholder="只限bootstrap style">
						<input type="button" class="btn  " onclick="form2.mode.value='<%=mmode%>';form2.submit()" value="儲存">
						<input type="button" class="btn btn-default" onclick="form2.mode.value='CNL';form2.submit()" value="取消">   <br /><span class="g9n"> 說明:<font color="#FF0000">如需帶預設參數,限使用 <b>ml_para</b>,如:test.aspx?ml_para=1</font></span>
                        </div></div>
                                </div>
                        </div>
         </div>
						<%}%>	
                      
					      

                               <%if (XXauth.IndexOf("A") >= 0) {%><input type="button" class="btn btn-danger" onclick="form2.mode.value='A';form2.fsno.value='0';form2.submit();" value="新增"><%} %>  <%if (XXauth.IndexOf("E") >= 0)
                                                                                                                                                                                                                                                                 {%>　<input type="button" class="btn btn-success" onclick="form2.mode.value='SO';form2.submit()" value="儲存排序"><%} %>
                             
						
                                <div id="treeview3" class="treeview">
                                    <ul class="list-group">
                                        <%       System.Data.DataView dva;
                            msqlcom.CommandText = "select sno from CABASE_ito a where fsno=0 " + whr + " order by sot";
                            dva = aa.dv_param(msqlcom,ref mmsg);
                            if (dva!=null) Response.Write(menu1(0, 0, dva.Count, "", 1, 0));
                            else Response.Write(mmsg); %>
                                      
                                    </ul>

                                </div>
                               
				
		
					
	</form>

   
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    
        <wuc:_fancybox runat="server" />
 
    
    <%

        close_app(); %>
</asp:content>
