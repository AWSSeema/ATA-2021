$(function() {
                
                // Horizontal  tab
				
				var indicator = $(''),
                        indicatorHalfWidth = indicator.width()/2,
						lis = $('.horz-tabs').children('li');
    
                
                $(".horizatalTab").tabs(".horz-tab-content .tab-content", {
                    effect: 'fade',
                    fadeOutSpeed: 0,
                    fadeInSpeed: 400,
                    onBeforeClick: function(event, index) {
                        var li = lis.eq(index),
                            newPos = li.position().left + (li.width()/2) - indicatorHalfWidth;
                        indicator.stop(true).animate({ left: newPos }, 400, 'easeInOutExpo');
                    }
                });	
				
});