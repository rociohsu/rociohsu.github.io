// JavaScript Document
  
$(function(){
	$('body').append('<div class="bgMask"></div>');
	
	//lightbox
	Shadowbox.init();
	
	//瀑布流排版
	var $grid = $('.grid').imagesLoaded( function() {
		// init Masonry after all images have loaded
		$grid.masonry({
			// options
			itemSelector: '.grid-item'
			//columnWidth: 300
		});
	});

});

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function() {scrollFunction()};

function scrollFunction() {
	if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
		$('#top_btn').fadeIn(500);
	} else {
		$('#top_btn').fadeOut(500);
	}
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
	document.body.scrollTop = 0;
	document.documentElement.scrollTop = 0;
}