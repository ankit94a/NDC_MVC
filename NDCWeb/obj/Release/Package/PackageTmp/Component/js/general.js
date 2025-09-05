
/*---------------------------------------------------------------------*/
(function ($) {

    /*================= Global Variable Start =================*/
    var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
    var IEbellow9 = !$.support.leadingWhitespace;
    var iPhoneAndiPad = /iPhone|iPod/i.test(navigator.userAgent);
    var isIE = navigator.userAgent.indexOf('MSIE') !== -1 || navigator.appVersion.indexOf('Trident/') > 0;
    function isIEver() {
        var myNav = navigator.userAgent.toLowerCase();
        return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
    }

    //if (isIEver () == 8) {}
    /*================= Global Variable End =================*/
    ww = document.body.clientWidth;
    var mobilePort = 1200;
    if ($(".sideMenu").length) {
        var fixmeTop = $('.sideMenu').offset().top;
        $(window).scroll(function () {
            var currentScroll = $(window).scrollTop();
            $('.sideMenu').removeClass('headStick');
            if (currentScroll >= fixmeTop) {
                if ($('.sideMenu').length > 0) {
                    $('.sideMenu').addClass('headStick');
                }
                else {
                    $('.sideMenu').removeClass('headStick');
                    $('.sideMenu').addClass('headStick');
                }
            }
            else {
                $('.sideMenu').removeClass('headStick');
            }

        });
    }
    /*================= On Document Load Start =================*/
    $(document).ready(function () {


        // added by sanket on 11032016 for the pretyphoto
        if ($("*[rel^='prettyPhoto']").length > 0) {
            //alert('yes');
            function ppOpen(panel, width) {
                $.prettyPhoto.close();
                setTimeout(function () {
                    $.fn.prettyPhoto({ social_tools: false, deeplinking: false, show_title: false, default_width: width, theme: 'pp_silvercms' });
                    $.prettyPhoto.open(panel);
                }, 300);

            } // function to open different panel within the panel

            $("a[data-rel^='prettyPhoto'], .prettyphoto_link").prettyPhoto({ theme: 'pp_silvercms', social_tools: false, deeplinking: false });
            $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'pp_silvercms', allow_expand: false, deeplinking: false });
            $("a[data-rel^='prettyPhoto[login_panel]']").prettyPhoto({ theme: 'pp_silvercms', default_width: 800, social_tools: false, deeplinking: false });

            $(".prettyPhoto_transparent").click(function (e) {
                e.preventDefault();
                $.fn.prettyPhoto({ social_tools: false, deeplinking: true, show_title: false, default_width: 980, theme: 'pp_silvercms transparent', opacity: 0.95 });
                $.prettyPhoto.open($(this).attr('href'), '', '');
            });
        }
        // add ended


        $('body').removeClass('noJS').addClass('hasJS');
        $(this).scrollTop(0);

        // Index Banner Slider	
        if ($(".indexBanner").length) {
            var owl = $('.sliderBanner');
            owl.owlCarousel({
                loop: true
			, autoplay: true
			, autoplayTimeout: 8000
			, smartSpeed: 5000
			, nav: false
			, items: 1
			, dots: false
            , autoplayHoverPause: true
			//, transitionStyle: "fade"
			//, animateOut: 'fadeOut'
			, onInitialized: function () {
			    if (owl.children().length == 1) { //added missing #
			        owl.trigger('stop.owl.autoplay');
			        owl.owlCarousel({ loop: false })
			    }
			}
            });
        };

        var owlNew = $('.schemeList');
        if ($(".schemeList").length) {
            	owlNew.owlCarousel({
			 	smartSpeed: 800
				,nav: false
				,items: 5,
                loop: true,
                margin: 19,
                autoplay: true,
                autoplayTimeout: 1000,
                autoplayHoverPause: false,
				responsive:{
				0:{
					items:1,
					mouseDrag:true
				},
				480:{
					items:2,
					mouseDrag:true
				},
				639:{
					items:3,
					mouseDrag:true
				},
				991:{
					items:4,
					mouseDrag:true
				},
				1201:{
					items:5,
					mouseDrag:true
					
				}
			}
            })
            /*$('.welfareSchemes .play').on('click',function(){
            owl.trigger('autoplay.play.owl',[1000])
            })
            $('.welfareSchemes .stop').on('click',function(){
            $('.schemeList').trigger('autoplay.stop.owl')
            })*/

            $('.exControl .owlplay').on('click', function () {
                owlNew.trigger('play.owl.autoplay', [1000])
            })
            $('.exControl .owlstop').on('click', function () {
                owlNew.trigger('stop.owl.autoplay')
            })
            $(".exControl .owlnext").click(function () {
                owlNew.trigger('next.owl');
            })
            $(".exControl .owlprev").click(function () {
                owlNew.trigger('prev.owl');
            })

        };
        if ($('.ErrorPanel, .MessagePanel, .InfoPanel, .WarningPanel').length > 0) {
            $(".ErrorPanel, .MessagePanel, .InfoPanel, .WarningPanel").delay(8000).fadeOut(1500);
            //$("html, body").animate({ scrollTop: '142px' }, "fast");
            $('body').keypress(function (e) {
                if (e.keyCode == 27) {
                    $(".messagePanelTab, .ErrorPanel, .MessagePanel, .InfoPanel, .WarningPanel").delay(100).fadeOut(500);
                }
            });
            $('#wrapper').click(function () {
                $(".messagePanelTab, .ErrorPanel, .MessagePanel, .InfoPanel, .WarningPanel").delay(100).fadeOut(500);
            });
        }
        if ($(".marqueeScrolling li").length > 1) {
            var $mq = $('.marquee').marquee({
                speed: 25000
			, gap: 0
			, duplicated: true
			, pauseOnHover: true
            });
            $(".btnMPause").toggle(function () {
                $(this).addClass('play');
                $(this).text('Play');
                $mq.marquee('pause');
            }, function () {
                $(this).removeClass('play');
                $(this).text('Pause');
                $mq.marquee('resume');
                return false;
            });
        };

        // News Ticker	
        if ($("#newsTicker").length) {
            var nt = $('#newsTicker').newsTicker({
                row_height: 50
			, max_rows: 2
			, speed: 800
			, duration: 4000
			, pauseOnHover: false
			, prevButton: $('.prev')
			, nextButton: $('.next')
            });
            $('.ticker').find('.stop').click(function () {
                if ($(this).hasClass('stop')) {
                    $(this).removeClass("stop").addClass("play").attr('title', 'Play');
                } else {
                    $(this).removeClass("play").addClass("stop").attr('title', 'Pause');
                }
                return false;
            });
            $('.playPause').toggle(function () {
                nt.newsTicker('stop');
            }, function () {
                nt.newsTicker('start');
            });
        };

        // Equal Height
        $('.equalHeights > div').equalHeight();

        // Responsive Tabing Script
        if ($(".resTab").length) {
            $('.resTab').responsiveTabs({
                rotate: false
			, startCollapsed: 'tab' //accordion
			, collapsible: 'tab' //accordion
			, scrollToAccordion: true
            });
        };

        // Accordion	
        if ($(".accordion").length) {
            $('.accordion .accordDetail').hide();
            $(".accordion .accordDetail:first").show();
            $(".accordion .accTrigger:first").addClass("active");
            $('.accordion .accTrigger').click(function () {
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                    $(this).next().slideUp();
                } else {
                    if ($('body').hasClass('desktop')) {
                        $('.accordion .accTrigger').removeClass('active');
                        $('.accordion .accordDetail').slideUp();
                    }
                    $(this).addClass('active');
                    $(this).next().slideDown();
                }
                return false;
            });
        };

        // Table Data
        if ($(".tableData").length > 0) {
            $('.tableData').each(function () {
                $(this).find('tr').each(function () {
                    $(this).find('td:first').addClass('firstTd');
                    $(this).find('th:first').addClass('firstTh');
                    $(this).find('th:last').addClass('lastTh');
                });
                $(this).find('tr:last').addClass('lastTr');
                $(this).find('tr:even').addClass('evenRow');
                $(this).find('tr:nth-child(2)').find('th:first').removeClass('firstTh');
            });
        };

        // Responsive Table
        if ($(".responsiveTable").length) {
            $(".responsiveTable").each(function () {
                $(this).wrap('<div class="tableOut"></div>');
                $(this).find('td').removeAttr('width');
                //$(this).find('td').removeAttr('align');
                var head_col_count = $(this).find('tr th').size();
                // loop which replaces td
                for (i = 0; i <= head_col_count; i++) {
                    // head column label extraction
                    var head_col_label = $(this).find('tr th:nth-child(' + i + ')').text();
                    // replaces td with <div class="column" data-label="label">
                    $(this).find('tr td:nth-child(' + i + ')').attr("data-label", head_col_label);
                }
            });
        };

        // Responsive Table
        if ($(".tableScroll").length) {
            $(".tableScroll").each(function () {
                $(this).wrap('<div class="tableOut"></div>');
            });
        };

        // Back to Top function
        if ($("#backtotop").length) {
            $(window).scroll(function () {
                if ($(window).scrollTop() > 120) {
                    $('#backtotop').fadeIn('250').css('display', 'block');
                }
                else {
                    $('#backtotop').fadeOut('250');
                }
            });
            $('#backtotop').click(function () {
                $('html, body').animate({ scrollTop: 0 }, '200');
                return false;
            });
        };



        // Get Focus Inputbox
        if ($(".getFocus").length) {
            $(".getFocus").each(function () {
                $(this).on("focus", function () {
                    if ($(this).val() == $(this)[0].defaultValue) { $(this).val(""); };
                }).on("blur", function () {
                    if ($(this).val() == "") { $(this).val($(this)[0].defaultValue); };
                });
            });
        };

        // For device checking
        if (isMobile == false) {

        };

        // Video JS
        if ($(".projekktor").length) {
            $(".projekktor").each(function () {
                var $this = $(this);
                projekktor($this, {
                    poster: $this.find("img").attr("src")
			, title: $(this).find("img").attr("alt")
			, playerFlashMP4: '../videoplayer/jarisplayer.swf'
			, playerFlashMP3: '../videoplayer/jarisplayer.swf'
			, width: 640
			, height: 385
			, fullscreen: true
			, playlist: [
				{ 0: { src: $this.find("a").attr("href"), type: $this.find("a").attr("rel")} }
			]
                }, function (player) { } // on ready 
			);
            })
        };

        // Google Map gmap3 
        if ($("#gmap").length) {
            var lang = 28.6269268;
            var lati = 77.1308028;
            var contentString = '<div id="content">' +
	    '<strong>' + 'Sainik Rest House' + '</strong>' +
	    '<div id="bodyContent">' + 'Tri junction at Naraina,' + '<br/>' +
		'New Delhi,' + '<br/>' +
		'New Delhi, India 110028'
            '</div></div>';

            var map = new google.maps.Map(document.getElementById('gmap'), {
                zoom: 15
		, center: new google.maps.LatLng(lang, lati)
		, mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });
            google.maps.event.addListener(map, 'click', function () {
                infowindow.close();
            });
            var marker = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(lang, lati)
            });
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
            infowindow.open(map, marker);
        };

        // to maintain previous page state on static page added by sanket on 05072016
        $(".previousPage").click(function (event) {
            event.preventDefault();
            history.back(1);
        });
        // add ended
		// flag day regarding added 23-11-17
		$(".flagPos").attr("download","");
        // Litebox
        if ($(".litebox").length) {
            $('.litebox').liteBox();
        };
		
		
		if($(".home").length){
	 	$('img[usemap]').rwdImageMaps();
		}

        /*================= On Document Load and Resize Start =================*/
        $(window).on('resize', function () {
            ww = document.body.clientWidth;
            if (ww > mobilePort) { $('body').addClass('desktop'); $('body').removeClass('device') } else { $('body').addClass('device'); $('body').removeClass('desktop') }

        }).trigger('resize');
        /*================= On Document Load and Resize End =================*/

        // Navigation
       	if( $("#nav").length) {
		if($(".toggleMenu").length == 0){
			$(".mainNav .container").prepend('<a href="#" class="toggleMenu"><span class="mobileMenu">Menu</span><span class="iconBar"></span></a>');	
		}
		$(".toggleMenu").click(function() {
			$(this).toggleClass("active");
			$("#nav").slideToggle();
			return false;
		});
		$("#nav li a").each(function() {
			if ($(this).next().length) {
				$(this).parent().addClass("parent");
			};
		})
		$("#nav li.parent").each(function () {
			if ($(this).has(".menuIcon").length <= 0) $(this).append('<i class="menuIcon">&nbsp;</i>')
		});
		dropdown('nav', 'hover', 1);
		adjustMenu();	
	};
	
	
	
	

        // Datepicker
        setdatepicker();

        // Typewriting
        /*if($("#typeWriting").length){
        $('#typeWriting').ticker({
        cursorList:""
        ,rate:20
        ,delay:3000
        }).trigger("play").trigger("stop");
        };*/

        // Typewriting
        /*if($(".historicalBg").length){
        $('.historicalBg').find('.readMore').delay(9500).fadeIn();
        };*/

        // Message on Cookie Disabled
        /*$.cookie('test_cookie', 'cookie_value', { path: '/' });
        if ($.cookie('cookieWorked') == 'yes') {}
        else{
        if( $("div.jsRequired").length == 0){
        $("body").prepend('<div class="jsRequired">Cookies are not enabled on your browser. Need to adjust this in your browser security preferences. Please enable cookies for better user experience.</div>');	
        }
        }*/

        // Dropdown List
        if ($(".lang").length) {
            function DropDown(el) {
                this.dd = el;
                this.placeholder = this.dd.children('a');
                this.opts = this.dd.find('ul.dropdown > li');
                this.val = '';
                this.index = -1;
                this.initEvents();
            }
            DropDown.prototype = {
                initEvents: function () {
                    var obj = this;

                    obj.dd.on('click', function (event) {
                        $(this).toggleClass('active');
                        return false;
                    });

                    obj.opts.on('click', function () {
                        var opt = $(this);
                        obj.val = opt.text();
                        obj.index = opt.index();
                        obj.placeholder.text(obj.val);
                    });
                },
                getValue: function () {
                    return this.val;
                },
                getIndex: function () {
                    return this.index;
                }
            }
            var dd = new DropDown($('#dd'));
            $(document).click(function () {
                $('.lang').removeClass('active');
            });
        };

    });
    /*================= On Document Load End =================*/

    /*================= On Window Resize Start =================*/
    $(window).bind('resize orientationchange', function () {
        setTimeout(function(){														 
        adjustMenu();
		},500);
    });

    /*================= On Window Resize End =================*/

    /*================= On Window Load Start =================*/
    $(window).load(function () {

    });
    /*================= On Document Load End =================*/
})(jQuery);

function setdatepicker() {

    if ($(".datepicker").length) {
        $('.datepicker').datepicker({
            dateFormat: "dd/mm/yy"
		   , showOn: "both"
		   , buttonText: "<span class='sprite calIcon'></span>"
            //,buttonImage: "images/calendar.png"
            //,buttonImageOnly: true
		   , beforeShow: function (textbox, instance) {
		       instance.dpDiv.css({
		           marginTop: /*(textbox.offsetHeight)*/0 + 'px'
				, marginLeft: 0 + 'px'
		       });
		   }
        });
    };

    //Skip to Content - Home &amp; Inner
    if ($('.skipContent').length) {
        $('.skipContent').click(function () {
            $('html, body').animate({
                scrollTop: $('.historical').offset().top
            }, 350);
        });
    };
    if ($('.inSkipContent').length) {
        $('.inSkipContent').click(function () {
            $('html, body').animate({
                scrollTop: $('.innerContent').offset().top
            }, 350);
        });
    };

}

 
