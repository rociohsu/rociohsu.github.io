<%@ Control Language="C#" AutoEventWireup="true" CodeFile="_fancybox.ascx.cs" Inherits="mng_fancybox" %>
<!--以下是fancybox pop iframe-->
    
    <script type="text/javascript" src="/javascript/fancybox/jquery.fancybox.js?v=2.1.5"></script>
	<link rel="stylesheet" type="text/css" href="/javascript/fancybox/jquery.fancybox.css?v=2.1.5" media="screen" />
    <script type="text/javascript">
        function Reload_Page() { form2.b1.value = 'q';form2.submit();}

        $(document).ready(function() {
            /*
			 *  Simple image gallery. Uses default settings
			 */
            /*以下控制pop大小尺寸跟邊宽*/
            $('.fancybox').fancybox({
                afterClose: function () {
                    //alert(window.top.b1.value);
                    var mchk_reload = document.getElementById("chk_reload");
                    
                    if (mchk_reload=="undefined" || mchk_reload==null || (mchk_reload!=null && mchk_reload.value=="")) {
                    //var murl = window.location.toString();
                        if (mchk_reload != null)
                        {
                            var murl = mchk_reload.value;
                            if (murl.indexOf(".aspx") >= 0) window.location.href = murl;
                        }
                        else window.location.reload();
                       // Reload_Page();
                    }

                },
                
                //modal: true,  //若不想自動關閉請註解本行
                closeBtn: true,
                padding: 0,
                margin: [20, 10, 20, 20],
                fitToView: true,
                width: 1600,
                scrollOutside: false //scroll-x 強制不隱藏, fancybox預設true, X會被隱藏(尤其遇到RWD時)            
            });


            /*
			 *  Different effects
			 */

            // Change title type, overlay closing speed
            $(".fancybox-effects-a").fancybox({
                helpers: {
                    title : {
                        type : 'outside'
                    },
                    overlay : {
                        speedOut : 0
                    }
                }
            });

            // Disable opening and closing animations, change title type
            $(".fancybox-effects-b").fancybox({
                openEffect  : 'none',
                closeEffect	: 'none',

                helpers : {
                    title : {
                        type : 'over'
                    }
                }
            });

            // Set custom style, close if clicked, change title type and overlay color
            $(".fancybox-effects-c").fancybox({
                wrapCSS    : 'fancybox-custom',
                closeClick : true,

                openEffect : 'none',

                helpers : {
                    title : {
                        type : 'inside'
                    },
                    overlay : {
                        css : {
                            'background' : 'rgba(238,238,238,0.85)'
                        }
                    }
                }
            });

            // Remove padding, set opening and closing animations, close if clicked and disable overlay
            $(".fancybox-effects-d").fancybox({
                padding: 0,

                openEffect : 'elastic',
                openSpeed  : 10,

                closeEffect : 'elastic',
                closeSpeed  : 10,

                closeClick : true,

                helpers : {
                    overlay : null
                }
            });

            /*
			 *  Button helper. Disable animations, hide close button, change title type and content
			 */

            $('.fancybox-buttons').fancybox({
                openEffect  : 'none',
                closeEffect : 'none',

                prevEffect : 'none',
                nextEffect : 'none',

                closeBtn  : false,

                helpers : {
                    title : {
                        type : 'inside'
                    },
                    buttons	: {}
                },

                afterLoad : function() {
                    this.title = 'Image ' + (this.index + 1) + ' of ' + this.group.length + (this.title ? ' - ' + this.title : '');
                }
            });


            /*
			 *  Thumbnail helper. Disable animations, hide close button, arrows and slide to next gallery item if clicked
			 */

            $('.fancybox-thumbs').fancybox({
                prevEffect : 'none',
                nextEffect : 'none',

                closeBtn  : false,
                arrows    : false,
                nextClick : true,

                helpers : {
                    thumbs : {
                        width  : 50,
                        height : 50
                    }
                }
            });

            /*
			 *  Media helper. Group items, disable animations, hide arrows, enable media and button helpers.
			*/
            $('.fancybox-media')
				.attr('rel', 'media-gallery')
				.fancybox({
				    openEffect : 'none',
				    closeEffect : 'none',
				    prevEffect : 'none',
				    nextEffect : 'none',

				    arrows : false,
				    helpers : {
				        media : {},
				        buttons : {}
				    }
				});

            /*
			 *  Open manually
			 */

            $("#fancybox-manual-a").click(function() {
                $.fancybox.open('1_b.jpg');
            });

            $("#fancybox-manual-b").click(function() {
                $.fancybox.open({
                    href : 'iframe.html',
                    type : 'iframe',
                    padding : 5
                });
            });

            $("#fancybox-manual-c").click(function() {
                $.fancybox.open([
					{
					    href : '1_b.jpg',
					    title : 'My title'
					}, {
					    href : '2_b.jpg',
					    title : '2nd title'
					}, {
					    href : '3_b.jpg'
					}
                ], {
                    helpers : {
                        thumbs : {
                            width: 75,
                            height: 50
                        }
                    }
                });
            });


        });
        $('#close-button').click(function () {
            $.fancybox.close(true);
        });
	</script>
	<style type="text/css">
		.fancybox-custom .fancybox-skin {
			box-shadow: 0 0 50px #222;
		}

		body {
			<!--max-width: 700px;
			margin: 0 auto;
		}
	</style>

<!--以上是fancybox pop iframe-->
