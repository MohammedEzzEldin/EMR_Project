$(document).ready(function(e) {
    $("#my-tabs li").click(function()
	{
		var myId=$(this).attr("id");
		$(this).addClass("activee").siblings().removeClass("activee");
		$(".register_con div").hide();
		$("#" + myId + "-content").fadeOut("slow");
		});
});