$(function()
{
	var scrollButton=$("#scroll-up");
	$(window).scroll(function(){
		
		console.log($(this).scrollTop());

			if($(this).scrollTop()>=600)
			{
				scrollButton.show();
			}
		
		})
	
});