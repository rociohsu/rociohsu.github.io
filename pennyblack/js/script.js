// JavaScript Document
var fbrequest="", eventName="1809pennyblack", eventid=259, nowPersonality=0, sharecode="";
$(window).on("load", function (e) {
	//名單公布
	$.post(
		'/js/ajax_eventnamelist.asp',
		{id:eventid},
		function(r){
			if(r){
				$(".baseword").css({"fontSize":"1em","text-align":"left", "font-weight":"normal"}).empty().append(r);
			}
	});
});


$(function(){

	//送出資料
	$(document).on("click", "#chbtn_sent", function(event){
			ga('send', 'event', eventName, 'click', 'event-keyform'); 

			var dname = $("input[name=myname]").val(),
				dtel = $("input[name=mytel]").val(),
				dmail = $("input[name=myemail]").val(),
				dcounter = $("select[name=counter]").val(),
				dcheckread = $("input[name=readOK]").prop("checked"),
				dlookset = $("input[name=result]:checked").val().replace(/result/,"");



			if (dname==''){
				$("input[name=myname]").focus(); alert("請填寫姓名"); 
				return false; 
			}

			if (dtel=="")  {alert("請填寫手機");  $("input[name=mytel]").focus();  return false;  
			}else{
				re = /^[09]{2}[0-9]{8}$/;
				if (!re.test(dtel) || (dtel.length!=10)) { alert("請填寫正確手機共十碼數字"); $("input[name=mytel]").focus(); return false;  }
			}

			if (dmail=="")  { alert("請輸入信箱"); $("input[name=myemail]").focus(); return false; }
			if (dmail!=""){ 
				if(!(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{1,3})+$/.test(dmail))){
					alert("信箱格式錯誤請確實填寫!!"); $("input[name=myemail]").focus(); return false;
				}	
			}

			if (dcounter=="")  { alert("請選擇一個兌換櫃點"); $("select[name=counter]").focus(); return false; }
			if (!dcheckread)  { alert("請同意隱私權政策"); $("input[name=readOK]").focus(); return false; }
			

			$(".formbox").hide();
			$(".formprocess").fadeIn();


			//Write Data
			$.post("module.asp", {op:'typedata', myname:dname, mytel:dtel, myemail:dmail, counter:dcounter, lookset:dlookset},
				function(r){
				
				var data = JSON.parse(r);

				console.log(data);
				sharecode = data.code;
				$(".loadercode").html("");
				
				if (data.state==1){
					sharecode = data.code;

				}else{
					$(".loadercode").html("<br>已經索取過兌換卷了，不再重新寄發..<br>等待分享...");
				}

			});

			
			//check FB是否登入
			CheckLoginSttus();

	});





});


function CheckLoginSttus(){
    FB.getLoginStatus(function(response) {
		if (response.status === 'connected') {
			fbrequest = response.authResponse.userID;
			checkPression();
		} else {
			//開啟授權或登入
			fb_login();
		}
	});

}



function fb_login() {
	FB.login(function(response) {
		if (response.authResponse) {
			fbrequest = response.authResponse.userID;
			checkPression();
		} else {
			postFeed()
		}
	});
}


function checkPression() {
	var checkpublishaction = false;
	FB.api('/me/permissions', function(response) {
		for (var x in response.data) {
			if (response.data[x].status == "granted") { checkpublishaction = true;  break; }
		}
		if (checkpublishaction==true){ postFeed();
		}else{ fb_login();}
	});
}



function postFeed() {

	var dname = $("input[name=myname]").val(),
		dtel = $("input[name=mytel]").val(),
		dmail = $("input[name=myemail]").val(),
		nowPersonality = $("input[name=result]:checked").val().replace(/result/,"");
	
	var hashtag = '#PENNYBLACK英倫時尚';
		if(nowPersonality == 1){ hashtag = '#PENNYBLACK千鳥紋斗蓬大衣x及膝長靴';
		}else if(nowPersonality == 2){ hashtag = '#PENNYBLACK可愛針織x幾何色塊百褶裙';
		}else if(nowPersonality == 3){ hashtag = '#PENNYBLACK蕾絲洋裝x精緻配件'; }
	
	var obj = {
			method: 'share',
			hashtag: hashtag,
			mobile_iframe: true,
			href: 'https://www.beauty321.com/event-1809pennyblack/look'+nowPersonality
		};
		
	function callback(response) {
		ga('send', 'event', eventName, 'click', 'event-shareok'); 
		if (response && !response.error_code) {
			if(fbrequest!=''){					
				$.post("module.asp", {op:'typedataShare', fbid:fbrequest, postpage:'share_'+eventName, eventtype:1 , myname:dname, mytel:dtel, myemail:dmail},
				function(r){
					var data = JSON.parse(r);
					setTimeout(function() { $(".loadercode").html(data.msg); }, 500);
				});
			}
		}else{
			setTimeout(function() { $(".loadercode").html('取消分享無法繼續抽第二波活動贈品。'); }, 500);				
		}

		//reset Form
		setTimeout(function() { 
			$('#sendform')[0].reset();
			$(".formbox").show();
			$(".formprocess").hide();
		}, 4000);

		
	}
	
	FB.ui(obj, callback);

}


