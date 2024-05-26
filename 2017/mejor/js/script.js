// JavaScript Document

include('js/jquery.easing.1.3.js'); //animate�ʺA

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
	// ���o�n�[�W���ܪ������ä@�@�]�w
	$('input:text , input:password , textarea').each(function(i, ele){
		// ����ثe�����ഫ�� jQuery �����O���_��
		// �A���o title �� class �� value �ݩʭ�
		var _text = $(ele),
			_title = _text.attr('title'),
			_class = _text.attr('class') || "",
			_value = _text.attr('value') || "";

		// �p�G�� title �άO className �Ȫ��ܡA�h�i���y
		if(!!_title || !!_class){
			// �b body �����J�@�Ӫťժ� Div �϶��ӷ��ܰ϶�
			var _water = $('<div class="note"></div>').appendTo($(this).parent('label'));
			
			_water.html(_title).click(function(){
				// ���ܰ϶��Q�I���ɡAĲ�o��J�ت� focus �ƥ�
				_text.trigger('focus');
			// �̫�[�W�B�~�� className
			}).addClass(_class);
			
			//���P�_�@�}�lvalue���S����
			if(!!_value){
				// ���ô��ܰ϶�
				_water.hide();
			}else{
				// ��ܴ��ܰ϶�
				_water.show();
			}
				
			// �]�w��J�ت� focus �� blur �ƥ�
			_text.focus(function(){
				// ���ô��ܰ϶�
				_water.hide();
			}).blur(function(){
				// �p�G��J�ؤ��S���ȫh�A��ܴ��ܰ϶�
				if(this.value=="") _water.show();
			});
		}
	});
}