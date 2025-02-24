<%@ Page Title="" Language="C#" MasterPageFile="_ML_POP.master" AutoEventWireup="true" CodeFile="ML_IMGJCROP.aspx.cs" Inherits="mng_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!--以下是sortable的JS-->
<script src="<%=mmroot%>/javascript/sortable/jquery.ui.core.js" type="text/javascript" charset="utf-8"></script>
<script src="<%=mmroot%>/javascript/sortable/jquery.ui.widget.js" type="text/javascript" charset="utf-8"></script>
<script src="<%=mmroot%>/javascript/sortable/jquery.ui.mouse.js" type="text/javascript" charset="utf-8"></script>
<script src="<%=mmroot%>/javascript/sortable/jquery.ui.sortable.js" type="text/javascript" charset="utf-8"></script>

<script src="<%=mmroot%>/javascript/jcrop/jquery.Jcrop.js" type="text/javascript" charset="utf-8"></script>
<link href="<%=mmroot%>/javascript/jcrop/jquery.Jcrop.css" rel="stylesheet">
<!--以上是sortable的JS-->
<link href="<%=mmroot%>/javascript/uploadfile.css" rel="stylesheet">
<script src="<%=mmroot%>/javascript/jquery.uploadfile.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form name="form2" method="post" action="" enctype="multipart/form-data">
    <div align="center">
    <input type="hidden" name="imgSrc" value="<%=imgSrc %>" />
    <input type="hidden" name="b1" value="" />
    <label> <input type="hidden" size="4" id="x1" name="x1" /></label>
	<label> <input type="hidden" size="4" id="y1" name="y1" /></label>
	<label> <input type="hidden" size="4" id="x2" name="x2" /></label>
	<label> <input type="hidden" size="4" id="y2" name="y2" /></label>
	<label> <input type="hidden" size="4" id="w" name="w" /></label>
	<label> <input type="hidden" size="4" id="h" name="h" /></label>
    <label> <input type="hidden" size="4" id="ow" name="ow" value="<%=ow %>" /></label>
	<label> <input type="hidden" size="4" id="oh" name="oh" value="<%=oh %>"/></label>
    
    <table>
    <tr>
        <td width="100%">
        <img id="blah" src="<%=imgSrc + "?" + aa.randomname("") %>" alt="" />
        </td>
    </tr>
    </table>
    <br />
    <input type="submit" class="btn btnsave" value="儲存" onclick="form2.b1.value='SAV';" />
    <script type="text/javascript">
        $(function () {
            $('#blah').Jcrop({  
                onChange: showCoords,
                onSelect: showCoords
                <%if (Request.QueryString["setSelect"] != null){%>,setSelect: <%=Request.QueryString["setSelect"] %><%}%>
                <%if (Request.QueryString["aspectRatio"] != null){%>,aspectRatio: <%=Request.QueryString["aspectRatio"] %><%}%>
                <%if (Request.QueryString["minSize"] != null){%>,minSize: <%=Request.QueryString["minSize"] %><%}%>
            });
        });

        function showCoords(c) {
            var mx1=Math.round(c.x);
            var my1=Math.round(c.y);
            var mx2=Math.round(c.x2);
            var my2=Math.round(c.y2);
            var mw=Math.round(c.w);
            var mh=Math.round(c.h);

            //這邊取得的 c 就是座標相關資料，包含: 
            $('#x1').val(mx1); //c.x  --> 左上角的 x  
            $('#y1').val(my1); //c.y  --> 左上角的 y  
            $('#x2').val(mx2);  //c.x2 --> 右下角的 x 
            $('#y2').val(my2);  //c.y2 --> 右下角的 y  
            $('#w').val(mw); //c.w  --> 選取範圍的寬度  
            $('#h').val(mh); //c.h  --> 選取範圍的高度



//            var rx = 100 / c.w;
//            var ry = 100 / c.h;

//            $('#preview').css({
//                width: Math.round(rx * 1024) + 'px',
//                height: Math.round(ry * 768) + 'px',
//                marginLeft: '-' + Math.round(rx * c.x) + 'px',
//                marginTop: '-' + Math.round(ry * c.y) + 'px'
//            });

//            var rx = 100 / c.w;
//            var ry = 100 / c.h;

//            $('#preview').css({
//                width: Math.round(c.w) + 'px',
//                height: Math.round(c.h) + 'px'
//            });
        };
    </script>
    </div>
</form>
</asp:Content>

