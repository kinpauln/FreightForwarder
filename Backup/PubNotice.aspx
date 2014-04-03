<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PubNotice.aspx.cs" Inherits="BigzoneBusinessCenterService.PubNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title></title>
    <style type="text/css">
        html{
            overflow-x:hidden;
            overflow-y:auto;
        }
        body{
            margin:0;
            padding:10px;
            font-size:12px;
            font-family:Tahoma, 宋体;
        }
        .button{
            padding:3px 5px;
        }
    </style>
    <script type="text/javascript" charset="utf-8" src="Editor/kindeditor.js"></script>
    <script type="text/javascript" src="Script/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        $(function() {
            $(document)
	            .bind("contextmenu", function() { return false; })
	            .bind("selectstart", function() { return false; });
	            
            //editor init
            KE.show({
                id: 'content1',
                imageUploadJson: 'asp.net/upload_json.aspx',
                fileManagerJson: 'asp.net/file_manager_json.aspx',
                allowFileManager: true,
                resizeMode: 0,
                width: '100%',
                height: '340px',
                afterCreate: function(id) {
                    KE.event.ctrl(document, 13, function() {
                        KE.util.setData(id);
                        document.forms['form1'].submit();
                    });
                    KE.event.ctrl(KE.g[id].iframeDoc, 13, function() {
                        KE.util.setData(id);
                        document.forms['form1'].submit();
                    });
                    //加载现有内容
                    $.post("Handler/Handler.ashx?op=get-notice", {}, function(data) {
                        var id = "content1";
                        KE.util.focus(id);
                        KE.util.selection(id);
                        KE.util.insertHtml(id, data);
                        //KE.util.setFullHtml(id, data);
                    });
                }
            });

            //绑定事件
            $("#btnPub").click(function() {
                var content = KE.util.getData('content1');
                if (content.length == 0) {
                    alert("请填写公告信息!");
                    return;
                }

                $.post("Handler/Handler.ashx?op=pub-notice", { content: content }, function(data) {
                    if (data.toString().length == 0) {
                        alert("公告信息发布成功！");
                        return;
                    }
                    else {
                        alert(data.toString().substr(2, data.length));
                        return;
                    }
                });
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <textarea id="content1" cols="100" rows="8" style="width:700px;height:200px;visibility:hidden;" runat="server"></textarea><br />
     <input type="button" value="发布" id="btnPub" class="button" />
    </form>
</body>
</html>
