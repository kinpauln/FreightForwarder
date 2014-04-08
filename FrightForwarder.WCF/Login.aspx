<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FreightForwarder.WCF.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>货代Mini</title>
    <link type="text/css" rel="stylesheet" href="style/login.css" />
    <script type="text/javascript" src="script/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        $(function() {
            $(document)
	            .bind("contextmenu", function() { return false; })
	            .bind("selectstart", function() { return false; });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
	    <table cellpadding="0" cellspacing="0" style="border-collapse:collapse" width="100%">
		    <tr>
			    <td>
				    <strong>欢迎使用《货代Mini》</strong>
			    </td>
			    <td align="right">
				    <a href="http://localhost/FFUpgrade.Service/" target="_blank">官方主页</a>&nbsp;|&nbsp;
				    <a href="http://localhost/FFUpgrade.Service/FFDB/down/setup.zip" target="_blank">下载最新版</a>
			    </td>
		    </tr>
	    </table>
    </div>
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ssData">
        <ItemTemplate>
            <table class="content">
	            <tr>
		            <td valign="top" class="note">
        			    <%# Eval("InfoContent") %>
		            </td>
        </ItemTemplate>
    </asp:Repeater>
    
    <asp:Repeater ID="rpUpdate" runat="server" DataSourceID="ssUpgrade">
        <ItemTemplate>
            <td valign="top" class="info">
                <p class="title">最新版本：</p>
                <p class="data"><%# Eval("FileVersion") %></p>
                <p class="title">更新日期：</p>
                <p class="data"><%# Convert.ToDateTime(Eval("PostTime")).ToString("yyyy年MM月dd日") %></p>
                <p class="title">联系电话：</p>
                <p class="data">0532-88888888</p>
            </td>
        </tr>
        </table>
        </ItemTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="ssUpgrade" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FFDBContext %>"
        SelectCommand="select top 1 * from UpgradePackage order by PostTime desc">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="ssData" runat="server" 
         ConnectionString="<%$ ConnectionStrings:FFDBContext %>"
         SelectCommand="select * from Announcement">
    </asp:SqlDataSource>
    </form>
</body>
</html>
