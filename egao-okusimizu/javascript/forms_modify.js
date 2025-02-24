
var click_fb = false;


function modify() {
    var err = "";
    
    var xemail = $("#email").val(); if (xemail == "") err += '電子郵件必填' + '\n';
    var xcname = $("#cname").val(); if (xcname == "") err += '姓名為必填' + '\n';
    var xpwd1 = $("#pwd1").val(); 
    var xpwd2 = $("#pwd2").val(); if (xpwd1 != "" && xpwd2 == "") err += '確認密碼必填' + '\n';
    if (xpwd1!="" && xpwd1 != xpwd2) err += '密碼確認不符' + '\n';
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
    

    if (err != "") { swal('請檢查以下欄位', err); return false; }
    else {

        var xapp_typ = "";
        if (xapp) xapp_typ += "app,";
        if (xweb) xapp_typ += "web,";
        if (xmob) xapp_typ += "mob,";
        if (xels) xapp_typ += "els,";
        Pace.track(function () {
            $.ajax({
                url: "/member_modify.ashx?rnd=" + Math.random(),
                dataType: 'json',
                type: "POST",
                data: { email: xemail, nam: xcname, pwd: xpwd1, mob: xphone, typ: xtyp, app_typ: xapp_typ, unit: xunit, unit_tel: xunit_phone, app_nam: xapp_name, app_ip: xapp_ip, app_mem: xapp_mem, app_typ_mem: xels_mem, mem: xremark },
                success: function (response) {
                    if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                        if (response.LNK != null) { window.location = response.LNK; }
                        else
                        {
                            alert('修改成功'); window.location = "member.aspx";
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