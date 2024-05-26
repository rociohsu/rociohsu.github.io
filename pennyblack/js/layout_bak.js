$(function(){

	new WOW().init();
	
	//文章區整塊click
	$('article').click(function(){
		var url = $(this).find('.btn').attr('href');
		window.open(url,'_blank');
	});
	
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
          var urlLink = $(this).attr('id');
					urlids=urlLink.split('result');
					result=urlids[1]*1+1;
					if(result > 3){ result = 1; }
					resultShow('result'+result); 
        },
				swipeRight:function(event, direction, distance, duration, fingerCount) {
          var urlLink = $(this).attr('id');
					urlids=urlLink.split('result');
					result=urlids[1]*1-1;
					if(result < 1){ result = 3; }
					resultShow('result'+result);
        },
        threshold:50
  });
	
});

	//判斷矛點開關測驗結果
	urlLink=window.location.href;
	urlids=urlLink.split('#');
	result=urlids[1];
	if(result==null || result==''){
		result = 'top';
	}else{
		resultShow(result);
	}

	//切換result開關
	$(document).on('click','.lookNav > a, .lookArrow > a',function(event) {
		$(this).unbind('click');
		var urlids = $(this).attr('href').split("#");
		result=urlids[1];
		resultShow(result);
	});
	
	//關閉result, 開啟測驗畫面
	$(document).on('click','.main .btn, .resultContent .btn > .restart',function(event) {
		$('section.look').css({display:'none'});
		$('#test').css({display:'block'});
		$('html,body').animate({
				scrollTop: $('#test').offset().top
		},300);
		new WOW().init();
	});
	
	//送出表單前先觸發
	$('.testForm form').on('submit', function(event){
    var result = $('.formRow.result input[name=result]:checked').val();
		resultShow(result);
		
		return false;
	});

function resultShow(result){
		$('#test').css({display:'none'});
		$('section.look').not('#'+result).css({display:'none'});
		$('#'+result).css({display:'block'});
		$('.swiper-slide').css({height:'auto'});
		$('.lookNav > a:not([href=#'+result+'])').removeClass('current');
		$('.lookNav > a[href=#'+result+']').addClass('current');
		$('html,body').animate({
				scrollTop: $('#'+result).offset().top
		},300);
		new WOW().init();
	
		var swiper = new Swiper('#'+result+' .ptPic .swiper-container', {
			direction: 'vertical',
			spaceBetween: 3,
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