


function chg_pwd()
{
    //alert('ok');
    
    var mpwd0 = $("#chg_pwd0").val();
    var mpwd1 = $("#chg_pwd1").val();
    var mpwd2 = $("#chg_pwd2").val();
    var memail = $("#chg_email").val();
    if (mpwd1!=mpwd2) { swal('新密碼二次輸入不合,請重新輸入新密碼');return false;}

    $.ajax({
        url: "ml_chgpwd.ashx",
        dataType: 'json',            
        type: "POST",
        data: {b1:'chg_pwd',email:memail,pwd0:mpwd0,pwd1:mpwd1,pwd2:mpwd2},
        success: function (response) {
            //alert(response);
            if (response.RS == "OK") {
                swal('修改成功');
            }
            else { swal(response.RS); }
        },
        error: function () {
            swal("伺服掛點!");
        }
    });
    return false;
}

//sweetalert範例
//swal("大標","內容","success"); //可不填,2,3參數
//sweetalert confirm 範例
//swal({
//    title: "Are you sure?",
//    text: "You will not be able to recover this imaginary file!",
//    type: "warning",
//    showCancelButton: true,
//    confirmButtonColor: "#DD6B55",
//    confirmButtonText: "Yes, delete it!",
//    closeOnConfirm: false
//},
//       function () {
//           swal("Deleted!", "Your imaginary file has been deleted.", "success");
//       });