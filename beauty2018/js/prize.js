// JavaScript Document

$(function(){
	
	//mobile menu
	$('.sidenav .openBtn').click(function(){
		$('.sidenav').stop(true,true).animate({left:0});
		$('.openBtn, .closeBtn, .overlay').stop(true,true).fadeToggle();
	});
	$('.sidenav .closeBtn, .overlay').click(function(){
		$('.sidenav').stop(true,true).animate({left:-200});
		$('.openBtn, .closeBtn, .overlay').stop(true,true).fadeToggle();
	});
	
	$('.scrollBtn > a.top').click(function(){
		$('html,body').animate({
			scrollTop: 0
		},300);
	});
	$('.scrollBtn > a.bottom').click(function(){
		$('html,body').animate({
			scrollTop: $('.pane.current .group.bottom').offset().top-10
		},300);
	});
	
	if( $('.owl-carousel').length > 0 ){
		
		$.fn.shuffle = function(){
			var allElems = this.get(),
					getRandom = function(max) {
							return Math.floor(Math.random() * max);
					},
					shuffled = $.map(allElems, function(){
							var random = getRandom(allElems.length),
									randEl = $(allElems[random]).clone(true)[0];
							allElems.splice(random, 1);
							return randEl;
				 });
			this.each(function(i){
					$(this).replaceWith($(shuffled[i]));
			});
			return $(shuffled);
		};

		var total = $('.owl-carousel').length;
		
		for(i=0;i<total;i++){
			$('.owl-carousel').eq(i).on('initialize.owl.carousel', function(e) {
					$(this).children().shuffle();
			});
			$('.owl-carousel').eq(i).owlCarousel({   
				items: 1,
				margin: 0,
				nav: true,
				dots: true,
				dotsData: true,
				//dotsContainer: '.owl-dots',
				autoHeight: true,
				//autoplay : true,
				//startPosition: 2,
				loop: true,
				autoplayHoverPause: true,
				autoplayTimeout: 6000
			});
		}
		
	}
	
});

	//判斷矛點開關賞別	
	urlLink=window.location.href;
	urlids=urlLink.split('#');
	urlid=urlids[1];
	if(urlid==null || urlid==''){
		var total = $('.pane').length;
		var randpoint = Math.floor((Math.random() * total) + 1);
		var classNum = 0;
		if(total == 6){ classNum = 1; }
		if(total == 4){ classNum = 2; }
		urlid = 'prize'+classNum+'-'+randpoint;
	}
	if( $(window).width() < 641 ){
		if(urlid == 'prize2-3'){
			$('.scrollBtn').stop(true,true).fadeOut();
		}else{
			$('.scrollBtn').stop(true,true).fadeIn();
		}
	}

	$('#'+urlid).addClass('current').stop(true,true).fadeIn();
	$('.secondMenu > a[href*='+urlid+']').addClass('current');
	$('html,body').animate({
			scrollTop: 0
	},300);

//切換賞別
$(document).on('click','.secondMenu > a, .navigation > a',function(event) {
	urlLink = $(this).attr('href');
	urlids=urlLink.split('#');
	urlid=urlids[1];
	$('.secondMenu > a.current').removeClass('current');
	$('.secondMenu > a[href*='+urlid+']').addClass('current');
	$('.pane').not('#'+urlid).removeClass('current').stop(true,true).fadeOut();
	$('#'+urlid).addClass('current').stop(true,true).fadeIn();
	$('html,body').animate({
		scrollTop: 0
	},300);
	
	if( $(window).width() < 769 ){
		$('.sidenav').stop(true,true).animate({left:-200});
		$('.openBtn').stop(true,true).fadeIn();
		$('.closeBtn, .overlay').stop(true,true).fadeOut();
	}
	if( $(window).width() < 641 ){
		if(urlid == 'prize2-3'){
			$('.scrollBtn').stop(true,true).fadeOut();
		}else{
			$('.scrollBtn').stop(true,true).fadeIn();
		}
	}
});

$(document).on('click','.owl-dot',function(event) {
		$('html,body').animate({
			scrollTop: $(this).closest('.owl-dots').offset().top-60
		},300);
});

//滾動至百貨或非百貨
$(window).scroll(function(){
	if( $(window).width() < 769 & $(window).scrollTop() > 40 ) {
		$('.topBg').stop(true,true).fadeIn();
	} else {
		$('.topBg').stop(true,true).fadeOut();
	}
	if( $(window).scrollTop()+150 > $('.pane.current .group.bottom').offset().top ) {
		$('.scrollBtn > a.top').removeClass('disable');
		$('.scrollBtn > a.bottom').addClass('disable');
	}else{
		$('.scrollBtn > a.top').addClass('disable');
		$('.scrollBtn > a.bottom').removeClass('disable');
	}
});