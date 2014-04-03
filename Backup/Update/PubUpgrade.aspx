<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PubUpgrade.aspx.cs" Inherits="BigzoneBusinessCenterService.PubUpgrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发布升级包</title>
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
    <form id="form1" runat="server">
        <div class="frame">
            请选择升级包文件，文件后缀为*.zip，选择文件后，点击上传按钮。<br />
            升级包：<asp:FileUpload ID="fu" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="发布" CssClass="button" 
                onclick="btnUpload_Click" />
        </div>
        <br />
        <div class="upgrade-list">
            <asp:GridView ID="gv" runat="server" DataSourceID="ssUpgrade"
                 AutoGenerateColumns="false" CssClass="grid">
                <Columns>
                    <asp:TemplateField HeaderText="版本">
                        <ItemTemplate>
                            <%# Eval("FileVersion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="文件名称">
                        <ItemTemplate>
                            <a href='Download.ashx?version=<%# Eval("FileVersion") %>'><%# Eval("FileName") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="发布时间">
                        <ItemTemplate>
                            <%# Eval("PostTime")%>
                        </ItemTemplate>
                    </asp:TemplateField>             
                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>                         
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="ssUpgrade" runat="server"
                 ConnectionString="<%$ ConnectionStrings:TransPad %>"
                 SelectCommand="select * from updatefiles">
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
