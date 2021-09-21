// Vertical  tab
				
				 var indicator = $(''),
                        indicatorHalfWidth = indicator.width()/2,
						lis = $('.vertTabNav').children('li');
				
				
                $(".vertTabNav").tabs(".vertTabContainer .tabContent", {
                    effect: 'fade',
                    fadeOutSpeed: 0,
                    fadeInSpeed: 400,
                    onBeforeClick: function(event, index) {
                        var li = lis.eq(index),
                            newPos = li.position().left + (li.width()/2) - indicatorHalfWidth;
                        indicator.stop(true).animate({ left: newPos }, 400, 'easeInOutExpo');
                    }
                });	