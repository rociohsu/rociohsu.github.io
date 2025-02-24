$(function(){

	$('body').jpreLoader({
		splashID: "#loading",
		loaderVPos: '50%',
		autoClose: true 
	});

	//nav menu click
	$('.navIcon').click(function(){
		$(this).toggleClass('open');
		$('nav .menu').stop(true,true).animate({
	    	height: 'toggle'
	    });
	    $('footer .social-icon').stop(true,true).animate({
	    	height: 'toggle'
	    });
	});

	$(window).scroll(function() {
		if($(window).scrollTop()>30){
			$('nav').addClass('opacity');
		}else{
			$('nav').removeClass('opacity');
		}
	});
	

	//auto animate
	new WOW().init();


	//藏頭詩hover效果
	$('#about-brand .container .tab > div').hover(function(){
		$('#about-brand .container .pane > div').css({'display':'none'});
		var target = $(this).attr('name');
		$('#about-brand .container .pane').children('.'+target).stop(false,true).fadeIn(300);
	},function(){
		
	});

	//top至頂button
	$('body').append('<a class="top-btn hvr-grow">↑</a>');
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
		owl.on('initialize.owl.carousel', function(e) {
				owl.children().shuffle();
		});
		owl.owlCarousel({            
			items: 1,
			margin: 0,
			autoHeight: true,
			autoplay : true,
			loop: false,
			autoplayHoverPause: true,
			autoplayTimeout: 5000
		});
	}

	//product detail open
	if( $('.product-list').length > 0 ){
		$('.product-list').magnificPopup({
			delegate: 'a',
			type:'inline',
	  		midClick: true,
	  		callbacks: {
		        open: function() {
		            $('body').css('overflow', 'hidden');
		        },
		        close: function() {
		            $('body').css('overflow', '');
		        }                                   
	    	}
		});
	}

});