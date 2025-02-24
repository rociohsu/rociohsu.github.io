$(function(){

	//nav menu click
	$('.navIcon').click(function(){
		$(this).toggleClass('open');
		$('nav .menu').stop(true,true).animate({
	    	opacity: 'toggle'
	    });
	});

	$('.scroll-btn').click(function(){
		var tabName = $(this).attr('name');
		$('html,body').animate({
			scrollTop: $('#'+tabName).offset().top
		},300);
	});

	//top至頂button
	$('body').append('<a href="#top" class="top-btn hvr-grow"><img src="images/icon-up.svg"></a>');
	$('a.top-btn').click(function(){
		var $body = (window.opera) ? (document.compatMode == "CSS1Compat" ? $('html') : $('body')) : $('html,body');
		$body.animate({
			scrollTop: 0
		},300);
	});
	/*$(window).scroll(function(){
		if( $(window).scrollTop() > 300 ) {
			$('a.top-btn').stop(true,true).fadeIn();
		} else {
			$('a.top-btn').stop(true,true).fadeOut();
		}
	});*/

	//auto animate
	AOS.init();

	//index切換效果
	if( $('#fullpage').length > 0 ){
		var myFullpage = new fullpage('#fullpage', {
		    menu: '#menu',
		    verticalCentered: false,
		    navigation: true,
		    navigationPosition: 'right',
		    //navigationTooltips: ['高橋酒造', '製酒の關鍵', '職人精神', '自慢のお酒', '最新消息', '名人推薦', '知名合作通路'],
		    anchors: ['top', 'home1', 'home2', 'home3', 'home4', 'home5', 'home6', 'home7'],
		    continuousVertical: false,
		    lazyLoad: true,

		    //to avoid problems with css3 transforms and fixed elements in Chrome, 
		    //as detailed here: https://github.com/alvarotrigo/fullPage.js/issues/208
		    css3:false
		});
	}

	//product切換效果
	if( $('#fullpage-pt').length > 0 ){
		var myFullpage = new fullpage('#fullpage-pt', {
		    menu: '#menu',
		    verticalCentered: false,
		    navigation: true,
		    navigationPosition: 'right',
		    anchors: ['top', 'pt1', 'pt1d', 'pt2', 'pt2d', 'pt3', 'pt3d', 'pt4', 'pt4d'],
		    continuousVertical: false,
		    lazyLoad: true,

		    //to avoid problems with css3 transforms and fixed elements in Chrome, 
		    //as detailed here: https://github.com/alvarotrigo/fullPage.js/issues/208
		    css3:false
		});
	}

	//banner輪播區
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

		var owl = $('.owl-carousel');
		/*owl.on('initialize.owl.carousel', function(e) {
			owl.children().shuffle();
		});*/
		owl.owlCarousel({            
			items: 1,
			margin: 0,
			autoHeight: true,
			autoplay : true,
			loop: true,
			autoplayHoverPause: true,
			autoplayTimeout: 5000
		});
	}

	//關於高橋
	if( $('.timeline').length > 0 ){
		var swiper = new Swiper('.timeline .swiper-container', {
		  	slidesPerView: 3,
		    spaceBetween: 0,
		    slidesPerGroup: 1,
		    loop: false,
		    loopFillGroupWithBlank: true,
		    pagination: {
		    	el: '.swiper-pagination',
		    	clickable: true,
		    },
		    autoplay: {
			    delay: 6000,
			    disableOnInteraction: false,
		    },
		    navigation: {
		    	nextEl: '.timeline .swiper-button-next',
		    	prevEl: '.timeline .swiper-button-prev',
		    },
		    breakpoints: {
		        768: {
		          slidesPerView: 3
			    },
			    640: {
			      slidesPerView: 2
			    }
			}
		});
	}

	$('.store-list .btn').click(function(){
		$(this).hide();
		$('.store-list .hide').fadeIn(500);
	});
});