<%@ Page  Language="C#"  ValidateRequest="true"  AutoEventWireup="true" CodeFile="ML_FORGOTPWD.aspx.cs" Inherits="_default" %>

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

<body class="hold-transition login-page" style="background-color:#3F5B67">
 <div class="login-box">
  <div class="login-logo">

  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
        <div class="text-center mt-4 mb-5">
          <h4>Forgot your password?</h4>
          <p>請輸入您的Ｅ-mail, 系統將自動寄送新密碼至您信箱。</p>
        </div>
        <form name="form2" id="form2" method="post" onsubmit="return chk();">  <input type="hidden" name="b1" />
          <div class="form-group">
            <input class="form-control" id="exampleInputEmail1" type="email" aria-describedby="emailHelp" placeholder="輸入您的E-mail" name="aaa">
          </div>
            <div class="form-group form-inline">

                   <input type="number" class="form-control" id="vcode"  name="ccc" size="5">　<img src="ML_VCODE.ashx?rnd=<%=aa.rnd(1000000).ToString() %>" alt="驗證碼" />

            </div>
          <a class="btn btn-primary btn-block" href="#"  onclick="form2.b1.value='SAV';form2.submit();">重設密碼</a>
            <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label><br />
        </form>
        <div class="text-center">
          
          <a class="d-block small" href="ml_login.aspx">Login Page</a>
        </div>
      </div>
    </div>




    <!-- Bootstrap core JavaScript-->
  <!-- jQuery 3 -->
<script src="theme/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="theme/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- AdminLTE App -->
<script src="theme/dist/js/adminlte.min.js"></script>
    </body>
</html>




