// JavaScript Document

$(function(){
	if( $('.wow').length > 0 ){
		new WOW().init();
	}
	
	//header contrl
	if( $(document).width() <= 640 ){
		$('.navBar .rightMenu .search').insertBefore( $('.navBar .logo') );
	}
	$('.navIcon').click(function(){
		$(this).toggleClass('open');
		$('.leftMenu').animate({
      width: "toggle"
    });
		$('.ptContent .addCart').appendTo('body');
	});
	
	$('.searchOpen').click(function(){
		$(this).hide();
		$('.searchToggle').fadeIn();
	});
	$('.searchBar .close').click(function(){
		$('.searchToggle').fadeOut(function(){
			$('.searchOpen').show();
		});
	});
	
	//indexKv
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
			loop: true,
			autoplayHoverPause: true,
			autoplayTimeout: 5000
		});
	}
	
	//productContent num+-
	if( $('.ptContent').length > 0 || $('.orderList.detail').length > 0){
		
		var total = $('.buyNum').length;
		for(i=0;i<total;i++){
			var t = $('.buyNum').eq(i).val()*1;
			var m = $('.buyNum').eq(i).attr('max')*1;
			
			if(t >= m){
				$('.buyNum').eq(i).val(m);
				$('.buyNum').eq(i).next('.add').attr('disabled',true);
			}else if(t <= 1){
				$('.buyNum').eq(i).val(1);
				$('.buyNum').eq(i).prev('.min').attr('disabled',true); 
			}
		}
		
		$('.add').click(function(){
			var t = $(this).prev('.buyNum').val()*1;
			var m = $(this).prev('.buyNum').attr('max')*1;
			$(this).prev('.buyNum').val(t+1);
			if((t+1) >= m){
				$(this).prev('.buyNum').val(m);
				$(this).attr('disabled',true);
				$(this).prev('.buyNum').prev('.min').attr('disabled',false);
			}else if((t+1) <= 1){
				$(this).prev('.buyNum').val(1);
				$(this).attr('disabled',false);
				$(this).prev('.buyNum').prev('.min').attr('disabled',true);
			}else{
				$(this).attr('disabled',false);
				$(this).prev('.buyNum').prev('.min').attr('disabled',false);
			}
		});
		$('.min').click(function(){
			var t = $(this).next('.buyNum').val()*1;
			var m = $(this).next('.buyNum').attr('max')*1;
			$(this).next('.buyNum').val(t-1);
			if((t-1) >= m){
				$(this).next('.buyNum').val(m);
				$(this).attr('disabled',false);
				$(this).next('.buyNum').next('.add').attr('disabled',true);
			}else if((t-1) <= 1){
				$(this).next('.buyNum').val(1);
				$(this).attr('disabled',true);
				$(this).next('.buyNum').next('.add').attr('disabled',false);
			}else{
				$(this).attr('disabled',false);
				$(this).next('.buyNum').next('.add').attr('disabled',false);
			}
   	});
	}
	
	//product open sizeGuide
	$('*[href="#sizeGuide"]').magnificPopup({
		type:'inline',
  	midClick: true
	});
	$('*[href="#moreDetail"]').magnificPopup({
		type:'inline',
  	midClick: true
	});
	
	//input keyup contrl
	$('input:not([type=checkbox])').keyup(function(){
		if( $(this).val() == ''){
			$(this).prev('label.tip').hide();
		}else{
			$(this).prev('label.tip').show();
		}
	});
	
	//member contrl
	$('.memberMenu > a[rel], a[rel=forgetPW]').click(function(){
		var t = $(this).attr('rel');
		$('.memberMenu > a.current').removeClass('current');
		$(this).addClass('current');
		$('.memberArea.current').removeClass('current');
		$('#'+t).addClass('current');
	});
	
	//cart order contrl
	$('#getWay').change(function(){
		var v = $(this).children('option:selected').val();
    if( v=='7-11取貨付款' || v=='全家取貨付款'){
      $('#payWay1').hide();
			$('#payWay2').show();
			$('.addrSelf').hide();
			$('.superBusi').css({display:'inline-flex'});
    }else{
			$('#payWay2').hide();
			$('#payWay1').show();
			$('.addrSelf').css({display:'inline-flex'});
			$('.superBusi').hide();
		}
	});
	$('#sameAll').change(function(){
		if ( $(this).is(':checked') ){
			$('#name2').val( $('#name').val() );
			$('#mobile2').val( $('#mobile').val() );
			$('#zipcode1, #zipcode2').val( $('#zipcode').val() );
			$('#city1, #city2').val( $('#city').val() );
			$('#county1, #county2').val( $('#county').val() );
			$('#addr1, #addr2').val( $('#addr').val() );
		}else{
			$('#name2').val('');
			$('#mobile2').val('');
			$('#zipcode1, #zipcode2').val('');
			$('#city1, #city2').val( $('#city1 option:eq(0)').text() );
			$('#county1, #county2').val( $('#county1 option:eq(0)').text() );
			$('#addr1, #addr2').val('');
		}
	});
});

$(window).resize(function(){
	//header contrl
	if( $(document).width() <= 640 ){
		$('.navBar .rightMenu .search').insertBefore( $('.navBar .logo') );
	}else{
		$('.navBar .search').insertBefore( $('.navBar .rightMenu .login') );
	}
});