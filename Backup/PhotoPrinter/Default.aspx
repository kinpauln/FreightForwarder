<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="ui/cupertino/jquery-ui-1.8.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="ui/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
    <script type="text/javascript">
        $(function() {
            $(document)
	                .bind("contextmenu", function() { return false; })
	                .bind("selectstart", function() { return false; });
        });
    </script>
</head>
<body>
    <div id="tabs">
	    <ul>
		    <li><a href="#tabs-1">行车证</a></li>
		    <li><a href="#tabs-2">驾驶证</a></li>
	    </ul>
	    <div id="tabs-1" class="ui-helper-clearfix">
		    <iframe id="frm1" frameborder="0" scrolling="no" src="print1.html" class="preview"></iframe>
		    <div class="workarea">
		        <% for (int i = 1; i <= 2; ++i) { %>
		        <fieldset>
		            <legend rel="1<%= i %>">照片<%= i %></legend>
		            <table>
		                <tr>
		                    <td><label for="file1<%= i %>">路径:</label></td>
		                    <td><input type="file" id="file1<%= i %>" rel="1<%= i %>" /></td>
		                </tr>
		                <tr>
		                    <td><label>尺寸:</label></td>
		                    <td>
		                        <table cellpadding="0" cellspacing="0">
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-plus"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-minus"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="hidden button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-arrow-4-diag"></span></a>
		                                </td>
		                            </tr>
		                        </table>
		                    </td>
		                </tr>
		                <tr>
		                    <td><label>位置:</label></td>
		                    <td>
		                        <table cellpadding="0" cellspacing="0">
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-nw"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-n"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-ne"></span></a>
		                                </td>
		                            </tr>
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-w"></span></a>
		                                </td>
		                                <td>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-e"></span></a>
		                                </td>
		                            </tr>
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-sw"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-s"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="1<%= i %>"><span class="ui-icon ui-icon-triangle-1-se"></span></a>
		                                </td>
		                            </tr>
		                        </table>
		                    </td>
		                </tr>
		            </table>
		        </fieldset>
	            <% } %>
		    </div>
	    </div>
	    <div id="tabs-2" class="ui-helper-clearfix">
		    <iframe id="frm2" frameborder="0" scrolling="no" src="print2.html" class="preview"></iframe>
		    <div class="workarea">
		        <% for (int i = 1; i <= 9; ++i) { %>
		        <fieldset>
		            <legend rel="2<%= i %>">照片<%= i %></legend>
		            <table>
		                <tr>
		                    <td><label for="file2<%= i %>">路径:</label></td>
		                    <td><input type="file" id="file2<%= i %>" rel="2<%= i %>" /></td>
		                </tr>
		                <tr>
		                    <td><label>尺寸:</label></td>
		                    <td>
		                        <table cellpadding="0" cellspacing="0">
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-plus"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-minus"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="hidden button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-arrow-4-diag"></span></a>
		                                </td>
		                            </tr>
		                        </table>
		                    </td>
		                </tr>
		                <tr>
		                    <td><label>位置:</label></td>
		                    <td>
		                        <table cellpadding="0" cellspacing="0">
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-nw"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-n"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-ne"></span></a>
		                                </td>
		                            </tr>
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-w"></span></a>
		                                </td>
		                                <td>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-e"></span></a>
		                                </td>
		                            </tr>
		                            <tr>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-sw"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-s"></span></a>
		                                </td>
		                                <td>
		                                    <a href="#" class="button ui-state-default ui-corner-all" rel="2<%= i %>"><span class="ui-icon ui-icon-triangle-1-se"></span></a>
		                                </td>
		                            </tr>
		                        </table>
		                    </td>
		                </tr>
		            </table>
		        </fieldset>
	            <% } %>
		    </div>
	    </div>
    </div>
    <div class="command">
        <a href="#" class="button" id="print">打印</a>
    </div>
</body>
</html>
