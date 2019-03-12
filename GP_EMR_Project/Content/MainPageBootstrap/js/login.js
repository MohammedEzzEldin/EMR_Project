$(document).ready(function() {
    $("#my-tabs li").click(function(){
		
		var myid=$(this).attr("id");
		$(this).removeClass("inactive").siblings().addClass("inactive");
		
		$(".my-container div").hide(); 
		
		$("#" + myid + "-content").fadeIn("1000"); 
		
		})
});