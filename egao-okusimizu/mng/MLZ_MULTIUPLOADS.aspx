<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MLZ_MULTIUPLOADS.aspx.cs" Inherits="jkz_multiupload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form name="form2" method="post" enctype="multipart/form-data">
   <input type="hidden" name="b1" />
        <input type="hidden" name="fsno" value="<%=mfsno %>" />
        <input type="hidden" name="unt" value="<%=munt %>" />
    <div>
        
        <span><font color="blue">請選擇您需要上傳的檔案</font>：</span><br />
        <input type="file" name="file" multiple />
        <input type="button" value="開始上傳" class="btn btn-danger" onclick="form2.b1.value = 'SAV';form2.submit()" />
        <p></p>
        <p></p>
    </div>
    <div><%=mmsg2 %></div>
    </form>
   
</body>
</html>
