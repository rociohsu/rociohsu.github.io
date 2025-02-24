<%@ Page  Language="C#"  ValidateRequest="true" AutoEventWireup="true" CodeFile="ML_LOGIN.aspx.cs" Inherits="_default" %>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
   <title><%=Session["後台管理標題"] %></title>   
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="theme/bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="theme/bower_components/font-awesome/css/font-awesome.min.css">
  <!-- Custom styles for this template-->
  <link rel="stylesheet" href="theme/dist/css/AdminLTE.min.css">
</head>


    
<body class="" style="background-color:#3F5B67">
 <div class="login-box">
  <div class="login-logo">
   
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">POS系統登入</p>

    <form action="" method="post" name="form2">
         <input type="hidden" name="b1" />
              <input type="hidden" name="xurl" value="<%=xurl %>" />
      <div class="form-group">
        <input type="text" class="form-control"  name="aaa" placeholder="輸入帳號" value="<%=maaa %>" <%if (maaa == "")   { %>autofocus<%} %> >
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
      </div>
      <div class="form-group">
        <input type="password" class="form-control" placeholder="輸入密碼"  name="bbb" autocomplete="off"  <%if (maaa != "")   { %>autofocus<%} %>>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
    
 <%--        <div class="form-group">
        <input type="password" class="form-control"  placeholder="圖像驗證"  name="ccc" id="vcode">　<img src="ML_VCODE.ashx?rnd=<%=aa.rnd(1000000).ToString() %>" alt="驗證碼" />
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>--%>
      <div class="form-group">
        <div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
              <input type="checkbox" value="on" name="atg" <%if (matg != "") Response.Write("checked"); %> /> Remember Me
            </label>
          </div>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
          <button type="submit" class="btn btn-primary btn-block btn-flat" onclick="javascript:form2.b1.value='SAV';" value="Login">登入</button>
            
        </div>
        <!-- /.col -->
      </div>
        
        <div class="form-group">
            <a href="ml_forgotpwd.aspx"">忘記密碼</a>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>
    </form>

    

    
    

  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->
  <!-- jQuery 3 -->
<script src="theme/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="theme/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- AdminLTE App -->
<script src="theme/dist/js/adminlte.min.js"></script>
    </body>

</html>
