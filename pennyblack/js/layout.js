$(function(){

	new WOW().init();
	
	//文章區整塊click
	$('article').click(function(){
		var url = $(this).find('.btn').attr('href');
		$(this).find('.btn').unbind('click');
		window.open(url,'_blank');
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

		var owl = $('.owl-carousel');
		owl.on('initialize.owl.carousel', function(e) {
				owl.children().shuffle();
		});
		owl.owlCarousel({   
			items: 1,
			margin: 0,
			nav: false,
			dots: false,
			autoHeight: true,
			autoplay : true,
			loop: true,
			autoplayHoverPause: true,
			autoplayTimeout: 6000
		});
	}
	
	//popup
	$('.popupClick').click(function(){
		$('#'+$(this).attr('name')).stop(true,true).show();
	});
	$('.popup .overlay, .pop-btnbox .close').click(function(){
		$('.popup').stop(true,true).hide();
	});
	
	//input keyup contrl
	$('.formRow input:not([type=radio])').keyup(function(){
		if( $(this).val() == ''){
			$(this).prev('label.tip').hide();
			$('.formRow input[name='+$(this).attr('name')+']').val('');
		}else{
			$(this).prev('label.tip').show();
			$('.formRow input[name='+$(this).attr('name')+']').val( $(this).val() );
		}
	});
	$('input:not([type=radio])').blur(function() {
		$(this).prev('label.tip').hide();
	});
	
	$('section.look').swipe({
        swipeLeft:function(event, direction, distance, duration, fingerCount) {
          clearTimeout(meter);
					var urlLink = $(this).attr('id');
					urlids=urlLink.split('result');
					result=urlids[1]*1+1;
					if(result > 3){ result = 1; }
					resultShow('result'+result,0); 
        },
				swipeRight:function(event, direction, distance, duration, fingerCount) {
          clearTimeout(meter);
					var urlLink = $(this).attr('id');
					urlids=urlLink.split('result');
					result=urlids[1]*1-1;
					if(result < 1){ result = 3; }
					resultShow('result'+result,0);
        },
        threshold:50
  });
	
});

var randpoint = Math.floor((Math.random() * 3) + 1);
runIt(randpoint,0);

	//判斷矛點開關測驗結果
	/*urlLink=window.location.href;
	urlids=urlLink.split('#');
	result=urlids[1];
	if(result==null || result==''){
		result = 'top';
	}else{
		resultShow(result,0);
	}*/

	//切換result開關
	$(document).on('click','.lookNav > a, .lookArrow > a',function(event) {
		clearTimeout(meter);
		$(this).unbind('click');
		var urlids = $(this).attr('href').split("#");
		result=urlids[1];
		resultShow(result,0);
	});
	
	
	//關閉result, 開啟測驗畫面
	$(document).on('click','.main .btn, .resultContent .btn > .restart',function(event) {
		//$('section.look').css({display:'none'});
		$('.resultContent').stop(true,true).fadeOut();
		$('.lookNav, .lookPic').addClass('none');
		for(i=0;i<3;i++){
			$('.lookNav.none:eq('+i+') > a:eq(0)').text('LOOK 1');
			$('.lookNav.none:eq('+i+') > a:eq(1)').text('LOOK 2');
			$('.lookNav.none:eq('+i+') > a:eq(2)').text('LOOK 3');
		}
		$('#test').css({display:'block'});
		$('html,body').animate({
				scrollTop: $('#test').offset().top
		},300);
		new WOW().init();
	});
	
	//送出表單前先觸發
	$('.testForm form').on('submit', function(event){
		clearTimeout(meter);
    var result = $('.formRow.result input[name=result]:checked').val();
		resultShow(result,1);
		
		return false;
	});

function resultShow(result,done){
		$('section.look').not('#'+result).css({display:'none'});
		$('#'+result).css({display:'block'});
		$('.swiper-slide').css({height:'auto'});
		$('.lookNav > a:not([href=#'+result+'])').removeClass('current');
		$('.lookNav > a[href=#'+result+']').addClass('current');
		if(done == 1){
			$('#test').css({display:'none'});
			$('.resultContent').stop(true,true).fadeIn();
			$('.lookNav, .lookPic').removeClass('none');
			for(i=0;i<3;i++){
				$('.lookNav:eq('+i+') > a:eq(0)').text('RESULT 1');
				$('.lookNav:eq('+i+') > a:eq(1)').text('RESULT 2');
				$('.lookNav:eq('+i+') > a:eq(2)').text('RESULT 3');
			}
			$('html,body').animate({
					scrollTop: $('#'+result).offset().top
			},300);
		}
	
		new WOW().init();
	
		var swiper = new Swiper('#'+result+' .ptPic .swiper-container', {
			direction: 'vertical',
			spaceBetween: 10,
			slidesPerView: 4,
			slidesPerGroup: 1,
			loop: false,
			loopFillGroupWithBlank: true,
			autoplay: {
			delay: 2500,
			disableOnInteraction: false,
			}
		});
}

function runIt(tabid,done){
		if( tabid == 4 ){ tabid = 1; }
		if( tabid < 4  ){
			resultShow('result'+tabid,done);
			if(done==0){
				var nextNum = tabid + 1;
				meter=setTimeout('runIt('+ nextNum +','+ done +')', 6000 );
			}
		}
}

//取消WOW amazing
$(window).scroll(function(){
	if( $(window).scrollTop()+200 > $('section.article').offset().top ) {
		$('section.article article').delay(1000).removeClass('wow');
	}
});