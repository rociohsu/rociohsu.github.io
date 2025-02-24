
var mclientid = "979194216014-fiafrgmikr9lplu99g3b6iioh31f84jc.apps.googleusercontent.com";
var mapikey = "409910159755698";
var click_fb = false;


var googleUser = {};
var startApp = function () {
    gapi.load('auth2', function () {

        // Retrieve the singleton for the GoogleAuth library and set up the client.
        auth2 = gapi.auth2.init({
            client_id: mclientid,
            cookiepolicy: 'single_host_origin',
            prompt: 'consent',
            // Request scopes in addition to 'profile' and 'email'
            scope: 'profile'
        });
        attachSignin(document.getElementById('google_connect_btn'));

    });
};

function attachSignin(element) {
    console.log(element.id);
    auth2.attachClickHandler(element, {},
        function (googleUser) {

            var authResponse = googleUser.getAuthResponse();
            owner_loginGG(googleUser.getBasicProfile().getName(), authResponse.access_token, "", "");

        }, function (error) {
            //swal(JSON.stringify(error, undefined, 2));
        });
}

startApp();


function owner_login(meml, mpwd, xckAuto) {
    Pace.track(function () {
        $.ajax({
            url: "/owner_login.ashx?rnd=" + Math.random(),
            dataType: 'json',
            type: "POST",
            data: { email: meml, pwd: mpwd , ckauto: xckAuto },
            success: function (response)
            {
                if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                    if (response.LNK != null) { window.location = response.LNK; }
                    else
                    {
                        alert('登入成功');
                        window.location = "member.aspx";
                    }

                } else {
                    swal(response.RS);

                }
            },
            error: function () {
                swal('error');

            }
        })
    });
    return false;
}


function owner_loginFB(mids, tk, xckAuto, xurl) {
    Pace.track(function () {
    $.ajax({
        url: "/owner_loginFB.ashx?rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        data: { fbid: mids, token: tk, ckAuto: xckAuto },
        success: function (response) {

            if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                //alert(response.MSG);
                //alert("登入成功");
                //if (response.REG == "OK") resfbq('track', 'CompleteRegistration');
                if (xurl != "") window.location = xurl;
                else window.location = "/member.aspx";

            } else {
                swal(response.RS );

            }
        },
        error: function () {
            $("#login_err_msg").html('ERROR LOAD loginFB');

        }
        })
    });
    return false;
}

function owner_loginGG(mids, tk, xckAuto, xurl) {
    Pace.track(function () {
    $.ajax({
        url: "/owner_loginGG.ashx?rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        data: {  fbid: mids, token: tk, ckAuto: xckAuto},
        success: function (response) {

            if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                //alert(response.MSG);
               
                //if (response.REG == "OK") resfbq('track', 'CompleteRegistration');
                if (xurl != "") window.location = xurl;
                else window.location = "/member.aspx";
             

            } else {
                swal(response.RS);

            }
        },
        error: function () {
            $("#login_err_msg").html('ERROR LOAD loginGG');

        }
        })
    });
    return false;
}

////暫不用
//// mapping customBtn for just login, PS. because of there are two google sign-in button on the same page. another register scripts is in the page.
////google plus init()
//(function () {
//    var po = document.createElement('script');
//    po.type = 'text/javascript'; po.async = true;
//    po.src = 'https://apis.google.com/js/client:plusone.js?onload=render';
//    var s = document.getElementsByTagName('script')[0];
//    s.parentNode.insertBefore(po, s);
//})();

//function render() {


//    gapi.signin.render('customBtn', {
//        'callback': 'signinCallback',
//        'clientid': mclientid,
//        'cookiepolicy': 'single_host_origin',
//        'requestvisibleactions': 'https://schemas.google.com/AddActivity',
//        'scope': 'profile email',
//        'data-accesstype': 'online'
//    });

//}


//function signinCallback(authResult)
//{   
//    alert(authResult.user_name);
//    if (authResult['access_token'])
//    {
//         if (click_google) { owner_loginGG("", authResult['access_token'], $("#ckAuto").val(), ""); } 
//    } else if (authResult['error']) {
//        click_google = false;
//    }
//}
////end of goole sign - in for login mapping


//start of facebook init
$(function () {
    FB.init({
        apiKey: mapikey,
        status: true,
        xfbml: true,
        cookie: true,
        //channelUrl: "channel.html"
    });
    FB.getLoginStatus(handleSessionResponse);
    var fbid;
    function handleSessionResponse(res) {
        console.log(res);
        if (res.status == "connected") {
            fbid = res.authResponse.userID;
            var talk = res.authResponse.accessToken;
            //var maaa = document.getElementById("autologin");
            
           if (click_fb) {  owner_loginFB( fbid, talk, $("#ckAuto").val(), ''); }
            
            //if (maaa.checked) auto_login(fbid, talk);
            //$("#fb_connect_btn").hide();
            //$("#ml_form").hide();
        }
    }
    $("#fb_connect_btn").click(function (e) {
        click_fb = true;
        isLogin = 'ON';
        //var maaa = document.getElementById("autologin"); maaa.checked = true;
        FB.login(handleSessionResponse, { scope: "" });
        return false;
    });
    $("#fb_connect_btn_to").click(function (e) {
        click_fb = true;
        isLogin = 'ON';
        isTo = '/cart.aspx'
        //var maaa = document.getElementById("autologin"); maaa.checked = true;
        FB.login(handleSessionResponse, { scope: "" });
        return false;
    });

    $("#login_btn").click(function (e) {
        var memail = $("#email").val();
        var mpwd = $("#pwd").val();
        var mckauto = $("#ckauto").val();
        if (memail == "" || mpwd == "") { swal('請輸入帳號及密碼'); return; }
        else owner_login(memail,mpwd,mckauto);
        return false;
    });
  
});
//end of facebook init