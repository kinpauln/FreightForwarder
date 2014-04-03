<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageImport.aspx.cs" Inherits="BigzoneBusinessCenterService.ImageImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Style/Style.css" type="text/css" rel="Stylesheet" />
    <link href="../Style/Tools.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Script/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        $(function() {
            $(document)
	            .bind("contextmenu", function() { return false; })
	            .bind("selectstart", function() { return false; });
        });
    </script>
</head>
<body>
    <form id="form1" enctype="multipart/form-data" method="post" action="../Handler/Handler.ashx?op=import-image">
    <div class="frame">
        请选择由外网网站导出的压缩包文件，文件后缀为*.zip，选择文件后，点击上传按钮。<br />
        <input type="file" name="fp" id="fp" />
        <input type="submit" value="上传" id="btnUploads" class="button" />
    </div>
    </form>
</body>
</html>
