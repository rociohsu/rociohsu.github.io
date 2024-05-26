// JavaScript Document

$(document).ready(function(){
	
			//判斷矛點開關首頁得獎公布/快速投票
			/*urlLink=window.location.href;
			urlids=urlLink.split('#');
			urlid=urlids[1];
			if(urlid==null || urlid==""){
				urlid = "top";
			}
			index_change(urlid);
	
			$('.navbar a[href*="#"]').click(function(){
				var urlids = $(this).attr('href').split('#');
				urlid=urlids[1];
				index_change(urlid);
			});
	
			function index_change(urlid){
				if( urlid == 'fast-vote'){
					$('.main.index').show();
				}else{
					$('.main.index').hide();
				}
			}*/
	
			new WOW().init();
	
			$('body').append('<a class="top-btn">↑</a>');
			$('a.top-btn').click(function(){
				var $body = (window.opera) ? (document.compatMode == "CSS1Compat" ? $('html') : $('body')) : $('html,body');
				$body.animate({
					scrollTop: 0
				},300);
			});
			$(window).scroll(function(){
				if( $(window).scrollTop() > 300 ) {
					$('a.top-btn').stop(true,true).fadeIn();
				} else {
					$('a.top-btn').stop(true,true).fadeOut();
				}
			});
	
			//全站 lightbox #login
			$('*[href="#login"]').magnificPopup({
				type:'inline',
  			midClick: true
			});
	
			/*$(document).on("click",".login", function(event){
				$.magnificPopup.open({
					items: { src: $('#login') },
					type: 'inline'
				});
			});*/
	
	
			//男神競技場 lightbox #profile
			$('.boy-list .profile .hover').magnificPopup({
  			type:'inline',
  			midClick: true
			});
	
	
			//個人頁 lightbox #edit
			$('*[href="#edit"]').magnificPopup({
  			type:'inline',
  			midClick: true
			});
			
	
			//首頁 主視覺調整版面
			if( $('.main.index').length > 0 ){
				$('.designfocus').removeClass('inside-page');
			}
	
	
			//快速投票區 點擊按鈕 票數++B幣++ 導向去品牌館
			$('.fast-pt[href!="#login"]').not('.done').click(function(){
				var num = $(this).attr('data-num');
				var href = $(this).attr('dara-href');
				var offset1 = $(this).find('.vote').offset();
				var offset2 = $('.navbar .user').offset();
				var money1 = $(this).find('.total').text()*1+1;
				var money2 = $('.navbar .user .total').text()*1+1;
				
				$('<div class="b-money '+'item'+num+'"></div>').appendTo($('body'));
				$('.b-money:not(".fixed").item'+num).css({ width: 40, height: 40, top : offset1.top-80 , left : offset1.left+10 })
				.animate({ width: 24, height: 24, top : offset2.top+12 , left : offset2.left },300).fadeOut();
				
				$(this).animate({ top : 0 }, 100 ,function(){
					$(this).addClass('done').attr('href', href ).unbind('click');
					$(this).find('.total').text(money1);  //票數
					$('.navbar .user .total').text(money2);  //B幣
				});
			});
	
	
			//美妝投票區 切換大賞分類/通路選單
			$('.second-menu .tabs > div').click(function(){
				$('.second-menu .tabs > div.current').removeClass('current');
				$('.second-menu .tabs > div .path-bar').css({'margin-left':-10});
				$(this).addClass('current').find('.path-bar').animate({'margin-left':0});
			});
	
			if( $('.main.awardvote').length > 0 ){
				for(i=0;i<3;i++){
					var currentName = $('.first-menu > a[class*="current"]').text();
					if( currentName == '彩妝大賞' ){
						$('.second-menu, .vote-bar').addClass('make-up');
					}else if( currentName == '綜合保養賞' ){
						$('.second-menu, .vote-bar').addClass('others');
					}
				}
			}
	
			//美妝投票區 選取商品投票計算
			$('.pt-list .item[href!="#login"]').click(function(){
				if( $(this).find('input[type="checkbox"]').is(':checked')){
					$(this).find('input[type="checkbox"]').prop( "checked", false );
				}else{
					$(this).find('input[type="checkbox"]').prop( "checked", true );
				}

				var total_num = $('.pt-list .item .bottom input[type="checkbox"]').length;
				var checked_num = 0;

				for(i=0;i<total_num;i++){
					if( $('.pt-list .item .bottom input[type="checkbox"]').eq(i).is(':checked')){
						checked_num= checked_num+1;
					}
				}

				if( checked_num > 3 ){
					$(this).find('input[type="checkbox"]').prop( "checked", false );
					$.notify({
						message: '已選取超過3項產品'
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
			$('.vote-bar .send').click(function(){
				var checked_num = $(this).find('.checked-num').text()*1;	
					if( checked_num < 3 ){
						$(this).find('input[type="checkbox"]').prop( "checked", false );
						$.notify({
							message: '未選滿3項產品'
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
					}
			});
			
			//美妝投票區 出現HappyHour調整版面
			if( $('.main.awardvote').length > 0 && $('#happyhourad').length > 0 ){
				$('body').addClass('adjustment');
			}
	
			//男神競技場 先計算已投票數回傳
			function vote_num_calc(){
				var total_num = 0;
				for(i=0;i<9;i++){
					var vote_num = $('.boy-list .profile .heart-btn:eq('+i+')').text()*1;
					total_num = total_num + vote_num;
				}
				return(total_num);
			}
			var total_num = vote_num_calc();
			if( total_num == 3 ){
				$('.heart-btn.done').removeClass('hvr-pulse').unbind('click');
				$('.heart-btn').not('.done').removeClass('hvr-pulse').unbind('click').hide();
			}
			
			//男神競技場 投票計算 B幣++
			$('.boy-list .profile .heart-btn[href!="#login"]').click(function(){
				var num = $(this).attr('data-num');
				var offset1 = $(this).offset();
				var offset2 = $('.navbar .user').offset();
				var money = $('.navbar .user .total').text()*1+1;
				
				var vote_num_self = $(this).text()*1+1;
				var ckecked_num = $(this).parent('.profile').children('.total').text()*1+1;
				var id = $(this).prevAll('.hover').attr('href').replace(/#/g, "");
				
				var total_num = vote_num_calc();
				
				if( total_num < 2 ){				 //投票小於3票時執行
					$('<div class="b-money '+'item'+num+'"></div>').appendTo($('body'));
					$('.b-money:not(".fixed").item'+num).css({ width: 40, height: 40, top : offset1.top-80 , left : offset1.left+10 })
					.animate({ width: 24, height: 24, top : offset2.top+12 , left : offset2.left },300).fadeOut();
					
					$(this).parent('.profile').find('.total').text(ckecked_num);  //票數
					$('.navbar .user .total').text(money);  //B幣
					$(this).addClass('done').attr('data-vote-num',vote_num_self).text(vote_num_self);

				}else if( total_num < 3 ){   //投票已投3票時執行
					$('<div class="b-money '+'item'+num+'"></div>').appendTo($('body'));
					$('.b-money:not(".fixed").item'+num).css({ width: 40, height: 40, top : offset1.top-80 , left : offset1.left+10 })
					.animate({ width: 24, height: 24, top : offset2.top+12 , left : offset2.left },300).fadeOut();
					
					$(this).parent('.profile').find('.total').text(ckecked_num);  //票數
					$('.navbar .user .total').text(money);  //B幣
					$('.heart-btn.done').removeClass('hvr-pulse').unbind('click');
					$(this).addClass('done').removeClass('hvr-pulse').attr('data-vote-num',vote_num_self).text(vote_num_self).unbind('click');
					$('.heart-btn').not('.done').removeClass('hvr-pulse').unbind('click').hide();
					
					var total_num = vote_num_calc();
					$.notify({
						message: '今日已投3票，明天再來投下你心目中的最佳男神。'
					},{
						type: 'warning',
						placement: {
							from: 'bottom',
							align: 'center'
						},
						offset: {
							x: 0,
							y: 10
						}
					});
				}
			});
			$('.popup.profile .box .content .inner .heart-btn[href!="#login"]').click(function(){
				var id = $(this).closest('.popup.profile').attr('id');
				$('.boy-list .profile .hover[href="#'+id+'"]').nextAll('.heart-btn').click();
				$.magnificPopup.close();
			});
	
	
			//個人頁禮物清單滾動至中獎區
			$('.calendar .icon-daygift, .calendar .icon-weekgift').click(function(){
				$('html,body').animate({
					scrollTop: $('.info-group').offset().top
				},500);
			});
			
	
			//活動說明 好禮清單切換
			$('.gift-menu > a').click(function(){
				var num = $(this).attr('data-num')*1-1;
				$('.gift-menu > a.current').removeClass('current');
				$(this).addClass('current');
				$(this).closest('.info-group').find('.gift-pane.current').removeClass('current');
				$(this).closest('.info-group').find('.gift-pane:eq('+num+')').addClass('current');
			});
			if( $('.main.rule').length > 0 ){
				$('.gift-menu > a:eq(0)').click();
				$('.weekgift').parent('.gift-list').css({'border-color':'#7a4eb1'});
				
				//活動說明 評審陣容輪播
				if( $(window).width() > 768 ){
					var swiper = new Swiper('.reviewers .swiper-container', {
						slidesPerView: 4,
						spaceBetween: 5,
						slidesPerGroup: 4,
						loop: true,
						loopFillGroupWithBlank: true,
						pagination: {
							el: '.swiper-pagination',
							clickable: true,
						},
						autoplay: {
						delay: 5000,
						disableOnInteraction: false,
						},
						navigation: {
							nextEl: '.reviewers .swiper-button-next',
							prevEl: '.reviewers .swiper-button-prev',
						}
					});
				}else{
					var swiper = new Swiper('.reviewers .swiper-container', {
						slidesPerView: 2,
						spaceBetween: 5,
						slidesPerGroup: 2,
						loop: true,
						loopFillGroupWithBlank: true,
						pagination: {
							el: '.swiper-pagination',
							clickable: true,
						},
						autoplay: {
						delay: 5000,
						disableOnInteraction: false,
						},
						navigation: {
							nextEl: '.reviewers .swiper-button-next',
							prevEl: '.reviewers .swiper-button-prev',
						}
					});
				}
			}
	
	
			//品牌館 調整獎項名稱
			if( $('.main.brand').length > 0 ){
				var total_num = $('.finalist:eq(0) .award-badge').length;
				var checked_num = 0;
				
				for(i=0;i<total_num;i++){
					var currentName = $('.finalist:eq(0) .award-badge').eq(i).text();
					if( currentName == '粉餅(氣墊、粉霜)賞' ){
						$('.finalist:eq(0) .award-badge').eq(i).text('粉餅賞');
					}
				}
				
				var brand_img = $('.custom-kv').attr('style'); 
				var brand_num = brand_img.substring(40, brand_img.length-6);  
				if( $(window).width() < 700 ){
					$('.custom-kv').attr('style','background-image:url(images/brand/brand-m-'+brand_num+'.png);')
				}
			}
});