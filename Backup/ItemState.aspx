<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemState.aspx.cs" Inherits="BigzoneBusinessCenterService.ItemState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>业务进展</title>
    <script type="text/javascript" src="Script/jquery-1.3.2.min.js"></script>
    <link href="Style/Nav.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Script/Script.js"></script>
    <script type="text/javascript">
        $(function() {
            $(document)
	                .bind("contextmenu", function() { return false; })
	                .bind("selectstart", function() { return false; });
    	            
            var per = getQueryValue("per");
            if (per == undefined || per.length == 0) {
                per = "0000000001";
            }
            updateUI(per);
            go();
        });

        var begin;
        var start = false;
        function UpdateNow() {
            var item;
            if (!start) {
                item = $(".step[class='step done now']");
                item.addClass("now-n");
                start = true;
            }
            else {
                item = $(".step[class='step done now now-n']");
                item.removeClass("now-n");
                start = false;
            }
        }

        function go() {
            begin = setInterval(UpdateNow, 500);
        }

        function updateUI(per) {
            //clear done or selected item.
            $(".step").each(function() {
                $(this).removeClass("done");
                $(this).removeClass("now");
            });

            var item = null;
            var now = null;     //current step
            for (var i = 1; i <= 8; i++) {
                item = $(".step[id=" + i + "]");
                if (per.toString().substr(per.length - i, 1) == "1") {
                    item.addClass("done");
                    item.attr("title", item.attr("innerTEXT"));
                    now = item;
                }
            }
            now.addClass("now");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="nav-frame">
  	        <div class="step done" id="1">申请<br /><span></span></div>
            <div class="step done" id="2">新业务<br /><span><asp:Literal ID="ltReg" runat="server"></asp:Literal></span></div>
            <div class="step now" id="3">档案员确认<br /><span><asp:Literal ID="ltFilerConfirm" runat="server"></asp:Literal></span></div>
            <div class="step" id="4">档案员已邮寄<br /><span><asp:Literal ID="ltFilerPost" runat="server"></asp:Literal></span></div>
            <div class="step" id="5">制证员收到材料<br /><span><asp:Literal ID="ltMakerAcceptFiles" runat="server"></asp:Literal></span></div>
  	        <div class="step" id="6">制证完成<br /><span><asp:Literal ID="ltMakerDone" runat="server"></asp:Literal></span></div>
            <div class="step" id="7">证件已邮寄<br /><span><asp:Literal ID="ltMakerPost" runat="server"></asp:Literal></span></div>
            <div class="step" id="8">申请人已签收<br /><span><asp:Literal ID="ltApplyConfirm" runat="server"></asp:Literal></span></div>
            <div style="clear:both"></div>
        </div>
    </form>
</body>
</html>
