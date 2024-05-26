//2018beautypro 

if (location.protocol != 'https:'){
 location.href = 'https:' + window.location.href.substring(window.location.protocol.length);
}


var FBstatus = 0, fbrequest=[], thiswingift=[], ido=0;

var logset = setTimeout(loginCheck, 10000);
function loginCheck(){	
	console.log($("#fastvotearea").is(":hidden"));
	if($("#fastvotearea").is(":hidden")==true){
			location.reload(); 
	} 	
}

$(document).ready(function(){

	/*快速投票*/
	$(document).on("click",".fast-pt",function(event) {
		var $this = $(this);			
		if(!FBstatus){
			$.magnificPopup.open({
					items: { src: $('#login') },
					type: 'inline'
			});

		}else{
			/*
			var fastvoteid = (this.id).split("_")[1];
			var getptvotetotal = parseInt($(this).find("b.total").html().replace(",",""));
			var pointoffset = $("#mypointTotal").offset(), coinoffset = $(this).find("button.vote").offset(),
				flyermoney = $("<img src='images/b-money.png'>").addClass("b-money").css({top: coinoffset.top, left:coinoffset.left+10});
				flyermoney.appendTo($('body')).animate({top:pointoffset.top, left:pointoffset.left, width:20, height:20},500,function(){
					$(this).fadeOut().remove();
				});
			
			$.post( "ajax/ajax_module.asp", 
				{id:fbrequest.id, tid:fastvoteid, op:'task_fastvote'}, 
					function( data ) {
					if(data.state==0){						
						$("#mypointTotal").html(parseInt($("#mypointTotal").html())+5);							
						$this.addClass("done")
						.attr("href","brand-"+$this.data("link"));
						$this.find("vote").hide();
						$this.find("b.total").html(addCommas(getptvotetotal+1));					
					}				

			},"json");
			*/
		}
	});


	// 今日快速投票統計 Today Vote Check
	function today_myfastvote(){
		var todayclick=0;
		$.post(
			'ajax/ajax_module.asp',
			{id:fbrequest.id, op:'todayFastvote'},
			function(data){
				var showd = JSON.parse(data);					
				if(showd.state==0){					
					if(showd.taskid.length>1){
						for (var x in showd.taskid){
							$("#fv_"+showd.taskid[x]).addClass("done")
							.attr("href","brand-"+$("#fv_"+showd.taskid[x]).data("link"))
							.find("vote").hide();							
						}
					}
					$("#loadingarea").fadeOut();
					$("#fastvotearea").removeClass("d-none");					
				}		
					
		});
	
	}


	//*大賞投票單投票 確定選取投票票數 */
	var votesnumber = [0,3,2,3,2,3,2,3,2,3,2,4,4,3,2,3,2,3,3,2,3];
	$(document).on("click", ".pt-list .item", function(){ 
			
		//get cateid vote number
		var cateid = $(".pt-list").attr("id").split("-")[1];
		console.log(cateid);
			
		if( $(this).find('input[type="checkbox"]').is(':checked')){
			$(this).find('input[type="checkbox"]').prop( "checked", false );
		}else{
			$(this).find('input[type="checkbox"]').prop( "checked", true );
		}

		var total_num = $('.pt-list .item .bottom input[type="checkbox"]').length;
		var checked_num = 0;

		for(i=0;i<total_num;i++){
			if( $('.pt-list .item .bottom input[type="checkbox"]').eq(i).is(':checked')){ checked_num= checked_num+1;}
		}

		if( checked_num > votesnumber[cateid] ){
			$(this).find('input[type="checkbox"]').prop( "checked", false );
			$.notify({
				message: '已選取超過'+votesnumber[cateid]+'項產品'
				},{
					type: 'warning',
					placement: {
					from: 'bottom',
					align: 'center'
				},
				offset: {
					x: 0,
					y: 70
				}
			});
		}else{
			$('.vote-bar .send .checked-num').text(checked_num);
		}
	});


	/*大賞投票單投票 */
	$(document).on("click", ".vote-bar .send", function(){
		if(!FBstatus){
			$.magnificPopup.open({
					items: { src: $('#login') },
					type: 'inline'
			});
		}else{
			var cateid = $(".pt-list").attr("id").split("-")[1];

			var checked_num = $(this).find('.checked-num').text()*1;	
			if( checked_num < votesnumber[cateid] ){
				$(this).find('input[type="checkbox"]').prop( "checked", false );
				$.notify({
					message: '未選滿'+votesnumber[cateid]+'項產品'
					},{
						type: 'warning',
						placement: {
							from: 'bottom',
							align: 'center'
					},
					offset: {
						x: 0,
						y: 70
					}
				});
			}else{
				var cbxVehicle = new Array();
				$('input:checkbox:checked[name="chooseprods"]').each(function(i) { cbxVehicle[i] = this.value; });
				$.post("ajax/ajax_module.asp", {id:fbrequest.id, s:cateid, v: cbxVehicle, op:'basicvote'}, function(r){
					if (r.state==0){
						window.location.href='awardvote-'+r.nextmaincate+'-'+r.nextcate+'#vvv';
					}else{
						alert(r.msg);
					}
								
				},"json");
			}		
		}

	});

	/*今日基本賞別投票統計*/
	function today_mybasicvote(){
		$.post('ajax/ajax_module.asp', {id:fbrequest.id, op:'todaybasicvote'},
		function(data){
				var showd = JSON.parse(data);
				if(showd.state==0){
					for(x in showd.finishcate){
						$("#votebar-"+showd.finishcate[x]).find("button").hide();
						$("#basicvote-"+showd.finishcate[x]).find(".checklabel").empty();
						$("#votebar-"+showd.finishcate[x]+' .d-none').removeClass("d-none");
					}						
				}

			$("#loadingarea").fadeOut(function(){
				$(".pt-list").fadeIn();
			});
			

		});
	}


	/*男神投票*/
	$(document).on("click", ".boy-list .profile a.heart-btn", function(){
		if(!FBstatus){
			$.magnificPopup.open({
				items: { src: $('#login') },
				type: 'inline'
			});
		}else{

			var starid = $(this).data("num");	//男神編號

			var pointoffset = $("#mypointTotal").offset(), coinoffset = $(this).offset(),
				flyermoney = $("<img src='images/b-money.png'>").addClass("b-money").css({top: coinoffset.top, left:coinoffset.left+10});

			$.post("ajax/ajax_module.asp",
				{id:fbrequest.id, mid: starid, op:'starvote'},
				function(r){
					if(r.state==0){
						$("#startv-"+starid).find("div.total").html(formatNumber(parseInt($("#startv-"+starid).find("div.total").html().replace(/,/,""))+1));
						flyermoney.appendTo($('body')).animate({top:pointoffset.top, left:pointoffset.left, width:20, height:20},500,function(){
							$(this).fadeOut().remove();
							$("#mypointTotal").html(parseInt($("#mypointTotal").html())+5);	
						});
					}else if(r.state==101){
						$.notify(
						{	message: '今日已投3票，明天再來投下你心目中的最佳男神。'},
						{	type: 'warning',
							placement: {
								from: 'bottom',
								align: 'center'
							},
							offset: {
								x: 0,
								y: 10
							}
						});
					}else{
					}
			},"json");			
		}
	});

	/*男神今日統計*/

	var manstarclick = 0;
	function today_mystarvote(){
		$.post("ajax/ajax_module.asp", {id:fbrequest.id, op:'todaystarvote'},
			function(data){
			var showd = JSON.parse(data);
			console.log(showd);
			if(showd.state==0){
				if(showd.mystarvote>=3){
					$(".boy-list .profile a.heart-btn").addClass("d-none");
					$("#profileinfo a.starfastvote").addClass("d-none");
				}else{

					//console.log("還可以投");
				}
			}

			$("#loadingarea").fadeOut();
			$(".boy-list").removeClass("d-none");					

		});
	}

	//男神燈箱顯示
	$(document).on("click", ".boy-list .profile a.hover", function(){
		var starid = $(this).data("num"), //男神編號
			$profileinfo = $("#profileinfo");
			$profileinfo.find(".pic img").attr("src","")

		$.ajax({url: "js/star.json", 
				dataType: 'json', 
				cache:false, 
				success: function (data) {

					for ( x in data){
						if (data[x].starid==starid){
							console.log(data[x].starid);
							$profileinfo.find(".pic img").attr("src","images/boy/boy-"+data[x].starid+"-pop.jpg?q="+Math.floor((Math.random() * 100) + 1));
							$profileinfo.find(".inner h1 span").eq(0).html(data[x].star_entitle);
							$profileinfo.find(".inner h2").html(data[x].star_name);
							$profileinfo.find(".inner article").html(data[x].star_info);
							$profileinfo.find(".starfastvote").attr("data-sid", data[x].starid);
							$.magnificPopup.open({
								items: { src: $('#profileinfo') },
								type: 'inline'
							});

							break;

						}
					}
				}, 
				error: function (xhr, ajaxOptions, thrownError) {
					console.log('error');
				}
			});


	});
	//觸發男神投票
	$(document).on("click", ".starfastvote", function(){
		
		parent.$('.mfp-close').trigger("click");
		$("#profileinfo").find(".pic img").attr("src","");
		$(".boy-list #startv-"+$(this).attr("data-sid")+" a.heart-btn").trigger("click");
	});


	//FACEBOOK login 
	//先check session

	$.ajax({ url: "ajax/ajax_fbsdk.asp", type: "POST",  cache:false, dataType: 'json',
		success: function (r) {
			if (r.state==1)	{
				FBstatus = 1;		
				fbrequest = r;		
				r['ido']=ido;
				$.post("ajax/ajax_fblogin.asp", r, function(data){
					var showd = JSON.parse(data);
					if (showd.state==1 && showd.id!='')	{
						//已登入
						
						$(".user").empty().removeClass("login").attr("href","/2018beautypro/mine").html('<i class="b-money fixed"></i><b class="total" id="mypointTotal">'+showd.points+'</b>');
						$("#loginShow .user-pic").attr("src","https://graph.facebook.com/"+showd.id+"/picture");
						$("#scheduleratio").html(showd.finished);
						$("#loginShow").show();
						$(".p-mine").show();
						$(".p-logout").show();
						if (showd.wingiftcount>0){$(".giftPop").show();}
						//CHECK 登入今日資訊
						if ($(".fast-list").length>0){
							today_myfastvote();
						}

						if ($(".pt-list").length > 0) {
							today_mybasicvote();
						}

						if($(".boy-list").length>0){
							today_mystarvote();
						}
					}
				});
				$.getScript('//connect.facebook.net/zh_TW/sdk.js', function(){});

			}else{
				$.getScript('//connect.facebook.net/zh_TW/sdk.js', function(){
					FB_checkloginState();
				});
				
			}
			
		},error: function (xhr, ajaxOptions, thrownError) {}
	});

	//$.getScript('//connect.facebook.net/zh_TW/sdk.js', function(){
	
	//});



	

	$("#fblogout").click(function(){
			FB.getLoginStatus(function(response) {
				if (response.status === 'connected') {
					FB.logout(function(response) {
						$.post('ajax/ajax_logout.asp',function(r){  location.reload(); });	
					});
				}else{
					$.post('ajax/ajax_logout.asp',function(r){  location.reload(); });	
				}
			});
	});


	$("#fbfastlogin").click(function(e){
		//check ido
		if ($("input[name=ido]").prop("checked")){ ido=1;}
		FB_login();
	});


	function FB_checkloginState(){
		FB.getLoginStatus(function(response) {
			if (response.status === 'connected') {				
			//大賞END
			FB_me();
		  } else {			

			if($("#fastvotearea").length>0){
				$("#loadingarea").fadeOut();
				$("#fastvotearea").removeClass("d-none");
			}
			if($("#basicvotearea").length>0){
				$("#loadingarea").fadeOut(function(){
					$(".pt-list").fadeIn();
				});
			}
			if($(".boy-list").length>0){
				$("#loadingarea").fadeOut();
				$(".boy-list").removeClass("d-none");
			}

			

		
		}
		});
	
	}
	function FB_me(){
		FB.api('/me?fields=id,name,email', function(r){
			//console.log(r);
			FBstatus = 1;		
			fbrequest = r;		
			r['ido']=ido;

			//處理登入資訊
			$.post("ajax/ajax_fblogin.asp", r, function(data){
				var showd = JSON.parse(data);
				if (showd.state==1 && showd.id!='')	{
					//已登入
					
					$(".user").empty().removeClass("login").attr("href","/2018beautypro/mine").html('<i class="b-money fixed"></i><b class="total" id="mypointTotal">'+showd.points+'</b>');
					$("#loginShow .user-pic").attr("src","https://graph.facebook.com/"+showd.id+"/picture");
					$("#scheduleratio").html(showd.finished);
					$("#loginShow").show();
					$(".p-mine").show();
					$(".p-logout").show();
					if (showd.wingiftcount>0){$(".giftPop").show();}
					//CHECK 登入今日資訊
					if ($(".fast-list").length>0){
						today_myfastvote();
					}

					if ($(".pt-list").length > 0) {
						today_mybasicvote();
					}

					if($(".boy-list").length>0){
						today_mystarvote();
					}


					

				}
			});
		});	
	}



 	function FB_login(){
		parent.$('.mfp-close').trigger("click");

		/*if(navigator.userAgent.indexOf('Line/')!=-1){
			
			alert("不支援使用LINE內建瀏覽器登入 FACEBOOK 帳號!\n手機使用者請使用Chrome / Safari瀏覽器");
			return false;
		}*/

		FB.login(function(response) {
		  
			if(response.authResponse){
				FB_me();
			}else{
				//console.log('cancelled login or did not fully authorize.');
			}
		}, {scope: 'email'});
		
	}



});
function padLeft(str, len) {
		str = '' + str;
		if (str.length >= len) {
			return str;
		} else {
			return padLeft("0" + str, len);
		}
}


function formatNumber(n) {
    n += "";
    var arr = n.split(".");
    var re = /(\d{1,3})(?=(\d{3})+$)/g;
    return arr[0].replace(re, "$1,") + (arr.length == 2 ? "." + arr[1] : "");
}


function addCommas(val) {
    //根據`.`作為分隔，將val值轉換成一個數組
    var aIntNum = val.toString().split('.');
    var iIntPart = aIntNum[0];
    var iFlootPart = aIntNum.length > 1 ? '.' + aIntNum[1] : '';
    var rgx = /(\d+)(\d{3})/;
    if (iIntPart.length >= 5) {
        // 根據正則要求，將整數部分用逗號每三位分隔
        while (rgx.test(iIntPart)) {
            iIntPart = iIntPart.replace(rgx, '$1' + ',' + '$2');
        }
    }
    return iIntPart + iFlootPart;
}


function SetCwinHeight(){
	var iframeid=document.getElementById("happyhourad"); //iframe id
	if (document.getElementById){  
		if (iframeid && !window.opera){  
			if (iframeid.contentDocument && iframeid.contentDocument.body.offsetHeight){  
			   iframeid.height = iframeid.contentDocument.body.offsetHeight;  
			}else if(iframeid.Document && iframeid.Document.body.scrollHeight){  
			   iframeid.height = iframeid.Document.body.scrollHeight;  
			}  
		}
	}
}
