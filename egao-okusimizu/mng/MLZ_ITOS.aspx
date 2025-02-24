<%@ Page  Language="C#"  ValidateRequest="true" MasterPageFile="_ML_POP.master" AutoEventWireup="true" CodeFile="MLZ_ITOS.aspx.cs" Inherits="_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="mng_main" class=" container-fluid" runat="server">
        
        <form name="form2" method="post" action="" class="form-horizontal" >
<input type="hidden" name="b1"/>
<input type="hidden" name="fsno" value="<%=mfsno%>"/>
<input type="hidden" name="sno" value=""/>


             <div class="col-md-12 col-lg-12">                        
                            <div class="box box-danger well ">
                                <div class="box-header"><h3>請選要移到的單元</h3>　<input type="button" class="btn btn-danger" value="移至最上層" onclick="form2.b1.value = 'SAV'; form2.sno.value = '0';form2.submit()" /></div>
                                <div class="box-body pad">
						      <div id="treeview3" class="treeview">
                                    <ul class="list-group">  
                                          <% Response.Write(menu1(0, 0, 1, "", 1, 0)); %>
						
              </ul></div>

               </div></div></div>
				

					
	</form>
    </div>
</asp:Content>

