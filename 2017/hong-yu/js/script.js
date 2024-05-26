// JavaScript Document
$(function(){
	//left menu hover
	$('.leftMenu .first_current .second').css({display:'block'});
	
	$('.leftMenu .first').not('.first_current').hover(function(){
		$(this).stop(true,true).animate({'background-position':'0 top'},200);
	},function(){
		$(this).stop(true,true).animate({'background-position':'200px top'},300);
	});
	
	$('.leftMenu .first').click(function(){
		$('.leftMenu .first').removeClass('first_current');
		$('.leftMenu .first .second').css({display:'none'});

		$(this).addClass('first_current');
		$(this).children('.second').stop(true,true).show(500);
	});
});

//input style
function inputNote(formName){
	if( $('input:radio').length > 0 ){
		$('input:radio').uniform();
	}
	
	// 取得要加上提示的元素並一一設定
	$('form input:text , form input:password , form textarea').each(function(i, ele){
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