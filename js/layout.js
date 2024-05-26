// JavaScript Document

$(document).ready(function(){
	
	//hamburger menu
	$('.hamburger').click(function(){
		$(this).toggleClass('is-active');
		$(this).next('ul').slideToggle();
	});
	if( $(window).width() > 768 ){
		$('.hamburger').click();
	}
	
	if( $('.wow').length > 0 ){
		new WOW().init();
	}
	
	//index
	if( $('.index').length > 0 ){
		$('.index h1').fitText(0.3).textillate({ 
			loop: true, 
			minDisplayTime: 5000,
			in: { effect: 'flash' },
			out: { effect: 'flash', reverse: true }
		});
		$('.index h2').textillate({ 
			loop: true, 
			minDisplayTime: 5000,
			initialDelay: 500, 
			in: { effect: 'zoomIn' }, 
			out: { effect: 'zoomOut', reverse: true }
		});
	}
	
	//元件隨機排序
	$.fn.shuffle = function() {
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
	
	if( $('.owl-carousel').length > 0 ){
		$('.owl-carousel').on('initialize.owl.carousel', function(e) {
				$(this).children().shuffle();
		});
		$('.experience .owl-carousel').owlCarousel({   
			items: 1,
			margin: 0,
			nav: true,
			dots: true,
			dotsData: true,
			//dotsContainer: '.owl-dots',
			autoHeight: true,
			//autoplay : true,
			//startPosition: 2,
			//loop: true,
			//autoplayHoverPause: true,
			//autoplayTimeout: 6000
		});
		
	}
	
	if( $('.photography').length > 0 ){
		var length = $('.gallery-thumbs .swiper-slide').length;
		var firstSlide = Math.floor(Math.random() * length);
		
		var galleryThumbs = new Swiper('.gallery-thumbs', {
      spaceBetween: 5,
      slidesPerView: 5,
			breakpoints: {
        992: {
          slidesPerView: 4
        },
        640: {
          slidesPerView: 3
        }
      },
      //loop: true,
      freeMode: true,
      loopedSlides: 5, //looped slides should be the same
      watchSlidesVisibility: true,
      watchSlidesProgress: true,
    });
    var galleryTop = new Swiper('.gallery-main', {
			//autoHeight: true,
      spaceBetween: 0,
      loop:true,
      loopedSlides: 5, //looped slides should be the same
			initialSlide: firstSlide,
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },
      thumbs: {
        swiper: galleryThumbs,
      },
    });
		
	}
	
	$('.img-w').each(function() {
    $(this).wrap('<div class="img-c"></div>');
    var imgSrc = $(this).find('img').attr('src');
     $(this).css('background-image', 'url(' + imgSrc + ')');
  })

  $('.img-c').click(function() {
    var w = $(this).outerWidth();
    var h = $(this).outerHeight();
    var x = $(this).offset().left;
    var y = $(this).offset().top;

    $('.active').not($(this)).remove();
    var copy = $(this).clone();
    copy.insertAfter($(this)).height(h).width(w).delay(500).addClass('active');
    $('.active').css('top', y - 15);
    $('.active').css('left', x - 15);
    
    setTimeout(function() {
			copy.addClass('positioned');
		}, 0);
  });
	
	//網站設計作品整塊click
	/*$('.project').click(function(){
		var url = $(this).find('.btn').attr('href');
		window.open(url,'_blank');
	});*/
});

$(document).on('click', '.img-c.active', function() {
  var copy = $(this);
  copy.removeClass('positioned active').addClass('postactive');
  setTimeout(function(){
    copy.remove();
  }, 500);
})

function playPause(myVideo) { 
	if (document.getElementById(myVideo).paused) {
		document.getElementById(myVideo).play();
		$('#'+myVideo).parent('.mobile').children('.play').fadeOut();	
		$('#'+myVideo).parent('.mobile').children('.pause').fadeIn();
	}else{
		document.getElementById(myVideo).pause(); 
		$('#'+myVideo).parent('.mobile').children('.pause').fadeOut();	
		$('#'+myVideo).parent('.mobile').children('.play').fadeIn();
	}
}