<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadSource.aspx.cs" Inherits="BigzoneBusinessCenterService.LoadSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        body{
            font-family:Tahoma, 宋体;
            font-size:12px;
            margin:0;
            padding:5px 2px;
        }
        .welcome{
            border:solid 1px Silver;
            background:#F8F8F8;
            padding:10px;
            text-align:center;
        }
        .error{
            border:solid 1px Orange;
            background:#F8F8F8;
            padding:10px;
            text-align:center;
            color:Red;
            font-weight:bold;
            display:none;
        }
        .data{
            display:none;
        }
        .hidden{
            display:none;
        }
        img{
            /*background:transparent url(Image/inner-no-image.gif) no-repeat;
            src:url(Image/inner-no-image.gif);*/
        }
        table{
            border-collapse:collapse;
            border:solid 1px #CECFCE;
        }
        td{
            border:solid 1px #CECFCE;
            padding:4px 2px;
        }
        tr{
            border:solid 1px #CECFCE;
        }
        .list_headrig{
            background:#E7F7FF;
            text-align:right;
        }
    </style>
    <script type="text/javascript" src="Script/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">

        //document.domain = "10.231.24.26";

        String.prototype.Trim = function() {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        }

        $(function() {
            var code = '<%= Request.QueryString["code"] %>';
            var idCard = '<%= Request.QueryString["idCard"] %>';
            //var idCard = '370281198601211022';
            var url = "http://10.231.24.26/baseweb/drvquery.tmri?method=showQueryResult&sfzmmc=A&sfzmhm=" + idCard + "&dabh=&scope=1&hphmt=&yzm=" + code;
            var spliter = '<script language="JavaScript" type="text/javascript">result_query()</' + 'script>';
            var sp2 = '<span id=fun_menuhide title=点击查看详细业务说明 style="DISPLAY: block; CURSOR: pointer">';
            var result = "没有检索到数据！";

            if (code == undefined || code.Trim().length == 0 || idCard == undefined || idCard.Trim().length == 0) {
                return;
            }

            $.post(url, {}, function(data) {
                if (data.toString().length > 0) {
                    var items = data.split(spliter);
                    $(".welcome").hide();

                    //判断是不是出错了
                    if (data.toString().indexOf("验证码错误") > 0) {
                        $(".error").html("验证码错误，请确保输入验证码正确！");
                        $(".error").show();
                        return;
                    }
                    else if (data.toString().indexOf("未查询到驾驶人信息") > 0) {
                        $(".error").html("未查询到驾驶人信息，请确保输入身份证号正确！");
                        $(".error").show();
                        return;
                    }
                    else {
                        if (items.length > 1) {
                            var its = items[1].split(sp2);
                            result = its[0];
                        }
                        else {
                            result = items[1];
                        }
                    }

                    $(".data").html(result);
                    $(".data").show();
                }
            });

        });
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="welcome">填写完成身份证号和验证码后，数据将出现在这里！</div>
        <div class="error">错误：</div>
        <div class="data"></div>
    </form>
</body>
</html>
