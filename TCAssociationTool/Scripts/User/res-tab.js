 // tabbed content
    // http://www.entheosweb.com/tutorials/css/tabs.asp
    $(".tab_content").hide();
    $(".tab_content:first").show();

  /* if in tab mode */
    $("ul.tabs li").click(function() {
		
      $(".tab_content").hide();
      var activeTab = $(this).attr("rel"); 
      $("#"+activeTab).fadeIn();		
		
      $("ul.tabs li").removeClass("active");
      $(this).addClass("active");

	  $(".tab_drawer_heading").removeClass("d_active");
	  $(".tab_drawer_heading[rel^='"+activeTab+"']").addClass("d_active");
	  
    });
	/* if in drawer mode */
	$(".tab_drawer_heading").click(function() {
      
      $(".tab_content").hide();
      var d_activeTab = $(this).attr("rel"); 
      $("#"+d_activeTab).fadeIn();
	  
	  $(".tab_drawer_heading").removeClass("d_active");
      $(this).addClass("d_active");
	  
	  $("ul.tabs li").removeClass("active");
	  $("ul.tabs li[rel^='"+d_activeTab+"']").addClass("active");
    });
	
	
	/* Extra class "tab_last" 
	   to add border to right side
	   of last tab */
	$('ul.tabs li').last().addClass("tab_last");
	
	
	
	$(document).ready(function () {
            $('#changemail').dialog
            ({
                autoOpen: false,
                width: 420,
                modal: true
            }); 
			 $('#changepic').dialog
            ({
                autoOpen: false,
                width: 420,
                modal: true
            });
			
			$('#changepassword').dialog
            ({
                autoOpen: false,
                width: 400,
                modal: true
            }); 
			
 }); 
 
 jQuery(document).ready(function ($) {
     $('body').addClass('js');
            var $menu = $('#profile'),
                $menulink = $('.menu-link'),
                $menuTrigger = $('.has-submenu > a');

            $menulink.click(function (e) {
                e.preventDefault();
                $menulink.toggleClass('active');
                $menu.toggleClass('active');
            });

            $menuTrigger.click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.toggleClass('active').next('ul').toggleClass('active');
            });
			});
 
function editbtnshow() {
            $('#viewarticle').show();
            $('#editarticle').hide();
            $('#editprofilebtn').show('slow');
        }
        function editbtnhide() {
            $('#editprofilebtn').hide('slow'); 
        }
        function editprofile() {
            $('#editprofilebtn').hide('slow');
            $('#editprofilebtn1').hide('slow');
            $('#editarticle').show('slow');
            $('#viewarticle').hide('slow'); 
        }
        function viewprofile() {
            $('#editprofilebtn').show('slow'); 
            $('#editarticle').hide('slow');
            $('#viewarticle').show('slow');
        }
 
 
        function postview() {
            $('#postbtn').hide();
            $('#postview').show('slow'); 
			$('#postclosebtn').show();
      }
	 function postclose() {
            $('#postbtn').show();
            $('#postview').hide('slow');  
			$('#postclosebtn').hide();
      }
	  
	  
$('.top').on('click', function() {
	$parent_box = $(this).closest('.box');
	$parent_box.siblings().find('.bottom').slideUp();
	$parent_box.find('.bottom').slideToggle(500, 'swing');
});         
 
  // tabbed content
    // http://www.entheosweb.com/tutorials/css/tabs2.asp
    $(".tab_content2").hide();
    $(".tab_content2:first").show();

  /* if in tab mode */
    $("ul.tabs2 li").click(function() {
		
      $(".tab_content2").hide();
      var activeTab = $(this).attr("rel"); 
      $("#"+activeTab).fadeIn();		
		
      $("ul.tabs2 li").removeClass("active");
      $(this).addClass("active");

	  $(".tab_drawer_heading2").removeClass("d_active");
	  $(".tab_drawer_heading2[rel^='"+activeTab+"']").addClass("d_active");
	  
    });
	/* if in drawer mode */
	$(".tab_drawer_heading2").click(function() {
      
      $(".tab_content2").hide();
      var d_activeTab = $(this).attr("rel"); 
      $("#"+d_activeTab).fadeIn();
	  
	  $(".tab_drawer_heading2").removeClass("d_active");
      $(this).addClass("d_active");
	  
	  $("ul.tabs2 li").removeClass("active");
	  $("ul.tabs2 li[rel^='"+d_activeTab+"']").addClass("active");
    });
	
	
	/* Extra class "tab_last" 
	   to add border to right side
	   of last tab */
	$('ul.tabs2 li').last().addClass("tab_last");
	
	
	
	$(document).ready(function () {
            $('#changemail').dialog
            ({
                autoOpen: false,
                width: 420,
                modal: true
            }); 
			 $('#changepic').dialog
            ({
                autoOpen: false,
                width: 420,
                modal: true
            });
			
			$('#changepassword').dialog
            ({
                autoOpen: false,
                width: 400,
                modal: true
            }); 
			
 }); 
 
 jQuery(document).ready(function ($) {
     $('body').addClass('js');
            var $menu = $('#profile'),
                $menulink = $('.menu-link'),
                $menuTrigger = $('.has-submenu > a');

            $menulink.click(function (e) {
                e.preventDefault();
                $menulink.toggleClass('active');
                $menu.toggleClass('active');
            });

            $menuTrigger.click(function (e) {
                e.preventDefault();
                var $this = $(this);
                $this.toggleClass('active').next('ul').toggleClass('active');
            });
			});
 
function editbtnshow() {
            $('#viewarticle').show();
            $('#editarticle').hide();
            $('#editprofilebtn').show('slow');
        }
        function editbtnhide() {
            $('#editprofilebtn').hide('slow'); 
        }
        function editprofile() {
            $('#editprofilebtn').hide('slow');
            $('#editprofilebtn1').hide('slow');
            $('#editarticle').show('slow');
            $('#viewarticle').hide('slow'); 
        }
        function viewprofile() {
            $('#editprofilebtn').show('slow'); 
            $('#editarticle').hide('slow');
            $('#viewarticle').show('slow');
        }
 
 
        function postview() {
            $('#postbtn').hide();
            $('#postview').show('slow'); 
			$('#postclosebtn').show();
      }
	 function postclose() {
            $('#postbtn').show();
            $('#postview').hide('slow');  
			$('#postclosebtn').hide();
      }
	  
	  
$('.top').on('click', function() {
	$parent_box = $(this).closest('.box');
	$parent_box.siblings().find('.bottom').slideUp();
	$parent_box.find('.bottom').slideToggle(500, 'swing');
});         
 
 
 