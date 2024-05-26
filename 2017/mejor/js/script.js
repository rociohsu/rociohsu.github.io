// JavaScript Document

include('js/jquery.easing.1.3.js'); //animate動態

//Include-Function
function include(url){ 
  document.write('<script src="'+ url + '" type="text/javascript" ></script>'); 
}

$(function(){	
		//lavaMenu hover
		//for more transition, goto http://gsgd.co.uk/sandbox/jquery/easing/
		var style = 'easeOutQuint';
		
		//Retrieve the selected item position and width
		var default_left = Math.round($('#lavaMenu li.selected').offset().left - $('#lavaMenu').offset().left);
		var default_width = $('#lavaMenu li.selected').width();

		//Set the floating bar position and width
		$('#lavaMenuHover').css({left: default_left});
		$('#lavaMenuHover .bg').css({width: default_width});

		//if mouseover the menu item
		$('#lavaMenu li').hover(function () {
			
			//Get the position and width of the menu item
			left = Math.round($(this).offset().left - $('#lavaMenu').offset().left);
			width = $(this).width(); 

			//Set the floating bar position, width and transition
			$('#lavaMenuHover').stop(false, true).animate({left: left},{duration:1000, easing: style});	
			$('#lavaMenuHover .bg').stop(false, true).animate({width:width},{duration:1000, easing: style});	
		
		//if user click on the menu
		}).click(function () {
			
			//reset the selected item
			$('#lavaMenu li').removeClass('selected');	
			
			//select the current item
			$(this).addClass('selected');
	
		});
		
		//If the mouse leave the menu, reset the floating bar to the selected item
		$('#lavaMenu').mouseleave(function () {

			//Retrieve the selected item position and width
			default_left = Math.round($('#lavaMenu li.selected').offset().left - $('#lavaMenu').offset().left);
			default_width = $('#lavaMenu li.selected').width();
			
			//Set the floating bar position, width and transition
			$('#lavaMenuHover').stop(false, true).animate({left: default_left},{duration:1500, easing: style});	
			$('#lavaMenuHover .bg').stop(false, true).animate({width:default_width},{duration:1500, easing: style});		
			
		});
});

//input style
function inputNote(){
	// 取得要加上提示的元素並一一設定
	$('input:text , input:password , textarea').each(function(i, ele){
		// 先把目前元素轉換成 jQuery 物件後記錄起來
		// 再取得 title 及 class 及 value 屬性值
		var _text = $(ele),
			_title = _text.attr('title'),
			_class = _text.attr('class') || "",
			_value = _text.attr('value') || "";

		// 如果有 title 或是 className 值的話，則進行改造
		if(!!_title || !!_class){
			// 在 body 中插入一個空白的 Div 區塊來當提示區塊
			var _water = $('<div class="note"></div>').appendTo($(this).parent('label'));
			
			_water.html(_title).click(function(){
				// 當提示區塊被點擊時，觸發輸入框的 focus 事件
				_text.trigger('focus');
			// 最後加上額外的 className
			}).addClass(_class);
			
			//先判斷一開始value有沒有值
			if(!!_value){
				// 隱藏提示區塊
				_water.hide();
			}else{
				// 顯示提示區塊
				_water.show();
			}
				
			// 設定輸入框的 focus 及 blur 事件
			_text.focus(function(){
				// 隱藏提示區塊
				_water.hide();
			}).blur(function(){
				// 如果輸入框中沒有值則再顯示提示區塊
				if(this.value=="") _water.show();
			});
		}
	});
}