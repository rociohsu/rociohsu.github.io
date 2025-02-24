
var mapikey = "409910159755698";
var click_fb = false;


function owner_loginFB(mids, tk,  xurl) {
    
    $.ajax({
        url: "/api/member/login?rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        async: true,
        data: JSON.stringify({
            Type: "FBID",
            SO_Token: tk,
            SO_ID: parseInt(mids)
            //now: new Date().getTime() // 注意不要在此行增加逗号
        }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                window.location = "/register.aspx";

            } else {
                swal(response.RS);

            }
        },
        error: function () {
            swal('ERROR LOAD loginFB');

        }
    });
    
    return false;
}

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
            
            if (click_fb) { owner_loginFB( fbid, talk,''); }
            
            //if (maaa.checked) auto_login(fbid, talk);
            //$("#fb_connect_btn").hide();
            //$("#ml_form").hide();
        }
    }
    $("#fb_connect_btn").click(function (e) {
        click_fb = true;
        isLogin = 'ON';
        //var maaa = document.getElementById("autologin"); maaa.checked = true;
        FB.login(handleSessionResponse, { scope: "email" });
        return false;
    });
    $("#fb_connect_btn_to").click(function (e) {
        click_fb = true;
        isLogin = 'ON';
        isTo = '/cart.aspx'
        //var maaa = document.getElementById("autologin"); maaa.checked = true;
        FB.login(handleSessionResponse, { scope: "email" });
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
//line start
function LineAuth() {
    var URL = 'https://access.line.me/oauth2/v2.1/authorize?';
    URL += 'response_type=code';
    URL += '&client_id=1615553367';
    URL += '&redirect_uri=http://devmng.plus9.tw/line_callback.ashx';
    URL += '&state=50767772';
    URL += '&scope=openid%20profile%20email';
    window.location.href = URL;
}
//line end

function register() {
    var err = "";
    var xfbid = $("#fbid").val();
    var xgoid = $("#goid").val();
    var xemail = $("#email").val(); if (xemail == "") err += '電子郵件必填' + '\n';
    var xcname = $("#cname").val(); if (xcname == "") err += '姓名為必填' + '\n';
    var xpwd1 = $("#pwd1").val(); if (xpwd1 == "") err += '密碼必填' + '\n';
    var xpwd2 = $("#pwd2").val(); if (xpwd2 == "") err += '確認密碼必填' + '\n';
    if (xpwd1 != xpwd2) err += '密碼確認不符' + '\n';
    var xphone = $("#phone").val(); 
    var xtyp = $("#typ").val(); if (xtyp == "") err += '身分類型必填' + '\n';
    var xapp = $("#app").is(':checked');
    var xweb = $("#web").is(':checked');
    var xmob = $("#mob").is(':checked');
    var xels = $("#else").is(':checked');
    if (!xapp && !xweb && !xmob && !xels) err += '選至少選一應用類型' + '\n';
    var xels_mem = $("#els_mem").val();

    var xunit = $("#unit").val(); if (xunit == "") err += '服務單位名稱必填' + '\n';
    var xunit_phone = $("#unit_phone").val(); if (xunit_phone == "") err += '服務單位電話必填' + '\n';
    var xapp_name = $("#app_name").val();
    var xapp_ip = $("#app_ip").val();
    var xapp_mem = $("#app_mem").val(); if (xapp_mem == "") err += '加值應用概述' + '\n';
    var xremark = $("#remark").val(); if (xremark == "") err += '申請服務概述' + '\n';
    var xred = $("#red").is(':checked'); if (!xred) err += '請確認【已閱讀會員權益與聲明並願意遵守相關規定】' + '\n';

    if (err != "") { swal('請檢查以下欄位', err); return false; }
    else {

        var xapp_typ = "";
        if (xapp) xapp_typ += "app,";
        if (xweb) xapp_typ += "web,";
        if (xmob) xapp_typ += "mob,";
        if (xels) xapp_typ += "els,";
        Pace.track(function () {
            $.ajax({
                url: "/owner_register.ashx?rnd=" + Math.random(),
                dataType: 'json',
                type: "POST",
                data: { email: xemail, fbid: xfbid, goid: xgoid, nam: xcname, pwd: xpwd1, mob: xphone, typ: xtyp, app_typ: xapp_typ, unit: xunit, unit_tel: xunit_phone, app_nam: xapp_name, app_ip: xapp_ip, app_mem: xapp_mem, app_typ_mem: xels_mem, mem: xremark },
                success: function (response) {
                    if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                        if (response.LNK != null) { window.location = response.LNK; }
                        else
                        {
                            alert('註冊成功!系統已寄出帳號啟用信至您的信箱,請開啟郵件並點選連結啟用您的會員帳號,謝謝~'); window.location = "index.aspx";
                        }

                    } else {
                        swal('錯誤訊息', response.RS, 'error');

                    }
                },
                error: function () {
                    swal('error');

                }
            })
        });
        return false;
    }
}
