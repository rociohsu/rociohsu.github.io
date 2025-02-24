<%@ Page  Language="C#"  ValidateRequest="false" MasterPageFile="_ML.master" AutoEventWireup="true" CodeFile="JKZ_NEWS_MOD.aspx.cs" Inherits="_default" %>
<%@ Register Src="~/mng/_fancybox.ascx" TagPrefix="wuc" TagName="_fancybox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    

<script type="text/javascript" src="<%=mmroot%>/javascript/tiny_mce/tinymce.min.js"></script>
<script type="text/javascript">
    tinymce.init({
        selector: "textarea.mceEditor",
        language: 'zh_TW',
        plugins: "textcolor colorpicker",
        //theme: "small",
        //width: 300,
        //height: 300,
        //resize: "both",
        extended_valid_elements: "div[*],i[*],b[*],span[*],img[*]", //不過濾掉的tag
        invalid_elements: "h1,h2", //過濾掉的tag
        menubar: false,
        toolbar1: "bold forecolor | fontsizeselect ",
        fontsize_formats: '24px 21px 18px 16px',
        textcolor_map: [
            "F36E51", "", "55846B", "", "FED27F", "", "CEAC73", "", "8CB09D", "", "292245", "", "212529", "", "515151", "", "EA9984", "", "82ACDE", "", "7E6775", ""],
        setup: function (editor) {
            editor.on('init', function () { this.getDoc().body.style.fontSize = '16px'; });
            editor.addButton('Image', {
                text: '插入圖片',
                icon: false,

                onclick: function () {
                    editor.windowManager.open({
                        title: "Upload Images",

                        width: 500,
                        height: 400,
                        url: "tinymce_uploads.aspx?unt=news"
                    }, {
                        someArg: "someValue"
                    });
                }
            });

        },

        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
            "save table contextmenu directionality emoticons template paste textcolor imagetools importcss"
        ],
        importcss_file_filter: "css/bootstrap.min.css"
        //content_css: "css/content.css",

        //    style_formats: [
        //    { title: 'Bold text', inline: 'b' },
        //    { title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
        //    { title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
        //    { title: 'Example 1', inline: 'span', classes: 'example1' },
        //    { title: 'Example 2', inline: 'span', classes: 'example2' },
        //    { title: 'Example 3', inline: '', classes: 'example3' },
        //    { title: 'Table styles' },
        //    { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
        //]


    });

    function toggleEditor(id) {
        var elm = document.getElementById(id);

        if (tinyMCE.getInstanceById(id) == null)
            tinyMCE.execCommand('mceAddControl', false, id);
        else
            tinyMCE.execCommand('mceRemoveControl', false, id);
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="mng_main"   runat="server">
        
    <form name="form2" method="post" enctype="multipart/form-data" >
    <input type="hidden" name="b1"/>
    <input type="hidden" name="sno" value="<%=msno%>"/>
    <input type="hidden" name="xord" value="<%=xord%>"/>
    <input type="hidden" name="xsot" value="<%=xsot%>"/>
    <input type="hidden" name="page" value="<%=mpage%>"/>
    <input type="hidden" name="mc" value="<%=mc%>"/>
    <input type="hidden" name="t1" id="t1" value="<%=mt1 %>" />
            
     <nav class="nav sidemenu">

       

         <input type="submit" class="btn btn-success " value="儲存" onclick="form2.b1.value='SAV';" /> 
                        

       <input type="button" onclick="form2.action='<%=YYurl %>';form2.b1.value='q';form2.submit();" class="btn btnB" value="返回"/>


     </nav>       
           <div class="row">
               <div class="col-lg-6 col-md-6">
                    <div class="box box-danger well">                   
                        <div class="box-body pad">
                            <label>狀　　態</label>
                            <label class="radio-inline control-label"> <input type="radio" class="radio" name="onf" value="0" <%if (monf == "0") { %>checked="checked"<%} %> />下架</label>
                            <label class="radio-inline control-label"><input type="radio" class="radio" name="onf" value="1" <%if (monf == "1") { %>checked="checked"<%} %>/>上架</label>
                            <div></div>
                            <label class="form-inline">種　　類
                            <select name="typ" class="form-control"><%=mselA %></select>
                            </label>
                            <div></div>
                            <label class="form-inline">標　　題
                            <input type="text" name="tpc"  class="form-control" value="<%=mtpc%>" />
                            </label>
                            <label class="form-inline">次標　題
                            <input type="text" name="tpcs"  class="form-control" value="<%=mtpcs%>" />
                            </label>
                            <div></div>
                            <label class="form-inline">上下架日
                            <input type="date" name="sdt"  class="form-control" value="<%=msdt%>" />~<input type="date" name="edt"  class="form-control" value="<%=medt%>" />
                            </label>
                           
                            <div></div>
                            <label class="form-inline">連　　結
                            <input type="text" name="lnk" class="form-control" value="<%=mlnk%>" /><input type="checkbox" name="lnk_tgt" class="largerCheckbox" value="_blank" <%if (mlnk_tgt != "") Response.Write("checked"); %>/>另開視窗</label>
                          <div></div>
                                    <label class="form-inline">列表圖片 <input type="file" name="file1" class="form-control" /><span>規格：360x480, 250K以下</span></label>
                                        <%if (mimg != "") { Response.Write("<br><img src=\"" + mimg + "\" class=\" col-lg-3 col-md-3\" />");Response.Write("<input type=\"button\" class=\"btn btn-danger\" onclick=\"if (confirm('確定刪除圖片嗎?')) {form2.b1.value='DEL1';form2.submit();}\" value=\"刪圖\">"); } %>
                                    
                        </div>
                    </div>
                   </div>
               <div class="col-lg-6 col-md-6">
                    <div class="box box-danger well">                   
                        <div class="box-body pad">
                          
                            <label>內　　文</label>
                            <div>
                            <textarea name="mem" cols="50" rows="15"><%=mmem %></textarea>
                            </div>
                           
                        </div>
                    </div>
                   </div>  
            </div>
            <div class="row">
            <div class="col-md-12 col-lg-12">                        
                                <div class="box box-danger well  ">
                                <div class="box-header"><h3 class="box-title">新聞HTML內容</h3></div>
                                <div class="box-body pad">
                                  
                                         <!--CMS  前四個參數為key視別用,"../"為圖片的相對路徑, "912345678為要出現的功能代碼"-->
                                        <%   Response.Write( aa.cms_edit("新聞","",int.Parse(msno),1,"../","1234578") );   %>
                                      <!--CMS-->
                                       <a name="hash2"></a>
                                    </div>
                                    </div>
                                    
            </div> </div>
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
     <wuc:_fancybox runat="server" />
 <!--以下是sortable的JS-->
<script type="text/javascript">


    function removeLine(obj) {
        $(obj).parents("tr:eq(0)").remove();
    }

        </script>
          
<!--以上是sortable的JS-->
    <%
        if (msqlcom != null) msqlcom.Dispose();
        close_app(); %>
</asp:content>
