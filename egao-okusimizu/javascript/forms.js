
var login_loading = false;
var click_google = false;
var isLogin = "";
var isTo = "";
var click_fb = false;


function logic_tpc(mtpc, mtyp) {

    if (login_loading) return;
    login_loading = true;
    var ntpc = $("#gtpc" + mtpc).val();
    var nmem = $("#gmem" + mtpc).val();
    if (ntpc != "" || nmem != "") {

        $.ajax({
            url: "/api/logic.ashx?rnd=" + Math.random(),
            dataType: 'json',
            type: "POST",
            data: { b1: "SAV", ID: mtpc, TPC: ntpc, CONT: nmem },
            success: function (response) {

                if (response.RS == "OK") {
                    login_loading = false;
                    return false;
                    //   $("#xheart" + skey).html(response.CNT);
                } else {
                    login_loading = false;
                    return false;
                    //  alert(response.RS);
                    //   $("#g_err_msg").html(response.RS);
                }
            }
            //error: function () {
            //    // alert("ERROR");
            //}, complete: function () {
            //    login_loading = false;
            //    // Handle the complete event
            //}
        });
    }

    return false;

}


function search()
{
    var mt2 = $("#search_key").val();
    if (mt2 != "") { window.location = '/SEARCH/' + mt2;  }
    else { alert('請輸入欲查詢的關鍵字,如:行銷'); return false;}

}

function love(skey) {
    if (login_loading) return;
    login_loading = true;
    $.ajax({
        url: "/api/love.ashx?rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        data: { b1: "HEART", ID: skey },
        success: function (response) {

            if (response.RS == "OK")
            {
                
                $("#xheart"+skey).html(response.CNT);
            } else {
                alert(response.RS);
                $("#g_err_msg").html(response.RS);
            }
        },
        error: function () {
            // alert("ERROR");
        }, complete: function () {
            login_loading = false;
            // Handle the complete event
        }
    });
    return false;

}

$("#epa_b").click(function ()
{
    
    if ($("#epa_c").prop("checked")) mtyp = 1
    else mtyp = 0;

    
    f_epa(mtyp);
});

function f_epa(mtyp) {
    if (login_loading) return;
    login_loading = true;
    var mdata = $("#epa").val();
    
    $.ajax({
        url: "/api/epaper.ashx?typ="+mtyp+"&rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        data: { b1: "NAME", mdata: mdata },
        success: function (response) {

            if (response.RS == "OK") {
                if (mtyp == "") alert('電子報訂閱成功');
                else alert('電子報取消成功');

            } else {
                alert(response.RS);
                $("#g_err_msg").html(response.RS);
            }
        },
        error: function () {
            // alert("ERROR");
        }, complete: function () {
            login_loading = false;
            // Handle the complete event
        }
    });
    return false;

}

//comtact_page
function checkform(msno) {
    var ncnam = $("#cnam").val();
    var nemail = $("#email").val();
    var ncontent = $("#content").val();
    if (ncnam == '') {swal('請輸入大名'); return;}
    if (nemail == '') { swal('請輸入E-mail'); return; }
    if (ncontent == '') { swal('請輸入內容'); return; }

        
    $.ajax({
        url: "/contact_page.ashx?rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        data: { email: nemail, cnam:ncnam, content:ncontent,fsno:msno},
        success: function (response) {
            if (response.RS == "OK") {  //FaceBook帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。Google帳號驗證成功! \n請記得點選資料修改頁面上確認送出，才真正綁定成功。
                if (response.LNK != null) { window.location = response.LNK; }
                else
                {
                       
                    swal('留言成功!');window.location.reload();
                }

            } else {
                swal(response.RS);

            }
        },
        error: function () {
            swal('error');

        }
    })

}


//contact_form
function chktyp(mtag)
{
    if (login_loading) return;
    login_loading = true;

    $.ajax({
        url: "/contact_formsel.ashx?rnd=" + Math.random(),
        dataType: 'json',
        type: "POST",
        data: { b1: "SAV", tag: mtag },
        success: function (response) {

            if (response.RS == "OK") {
                login_loading = false;
                $("#lnk option").remove();
                for (var x in response.DATA)
                {
                          
                    $("#lnk").append($("<option></option>").attr("value", response.DATA[x].LINK).text(response.DATA[x].TITLE));
                }


                return false;
                //   $("#xheart" + skey).html(response.CNT);
            } else {
                login_loading = false;
                swal(response.RS);
                return false;
                //  alert(response.RS);
                //   $("#g_err_msg").html(response.RS);
            }
        }
        ,error: function () {
            // alert("ERROR");
        }, complete: function () {
            login_loading = false;
            // Handle the complete event
        }
    });

}

function chkform()
{
            
    var nam = $("#cnam").val();
    var email = $("#email").val();
    var ntpc = $("#tpc").val();
    var content = $("#content").val();
    var mtyp = $("#typ").val();
    var mlnk = $("#lnk").val();
    if (nam=="" || ntpc=="" || email=="" || content=="" || (mtyp!="0" && mlnk=="")) {
        //swal("資料不齊");
        return false;
    }
    else
    {

        if (login_loading) return;
        login_loading = true;
               
                

        $.ajax({
            url: "/contact_form.ashx?rnd=" + Math.random(),
            dataType: 'json',
            type: "POST",
            data: { b1: "SAV", tpc:ntpc, cnam: nam, email: email, content: content, typ:mtyp, lnk:mlnk },
            success: function (response) {

                if (response.RS == "OK") {
                    login_loading = false;
                    swal('成功');
                    form2.action = 'contact.aspx'; form2.b1.value = 'q'; form2.submit();
                    return false;
                    //   $("#xheart" + skey).html(response.CNT);
                } else {
                    login_loading = false;
                    swal(response.RS);
                    return false;
                    //  alert(response.RS);
                    //   $("#g_err_msg").html(response.RS);
                }
            }
            ,error: function () {
                // alert("ERROR");
            }, complete: function () {
                login_loading = false;
                // Handle the complete event
            }
        });
               

               

               
    }
    return false;
}
