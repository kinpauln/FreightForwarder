<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FreightForwarder.Upgrade.Service._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>代贸通物流平台升级文件管理</title>
    <style type="text/css">
        body, td {
            font-size: 11pt;
            font-family: Tahoma, 宋体;
        }

        .grid {
            border-collapse: collapse;
        }

            .grid td {
                padding: 2px;
                height: 22px;
                background: #FFF;
            }

        td {
            background: #F8F8F8;
            padding: 20px;
            line-height: 1.8em;
            height: 85px;
        }
    </style>
    <link href="../Style/Style.css" type="text/css" rel="Stylesheet" />
    <link href="../Style/Tools.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(document)
	            .bind("contextmenu", function () { return false; })
	            .bind("selectstart", function () { return false; });
        });

        function CheckSavingType() {
            if ($("#drpSavingType2").val() == "0") {
                alert("请选择保存方式");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="">
            <asp:FormView ID="FormView1" runat="server" BorderStyle="None" BorderWidth="0"
                DataSourceID="ObjectDataSource1"
                DefaultMode="Insert"
                DataKeyNames="FileVersion"
                OnItemInserting="FormView1_ItemInserting">
                <InsertItemTemplate>
                    请选择升级包文件，文件后缀为*.zip，选择文件后，点击上传按钮。<br />
                    升级文件:<asp:FileUpload ID="fileUpload" runat="server" Width="400px" /><br />
                    <asp:Button ID="btnPost" runat="server" Text="发布" OnClientClick="return CheckSavingType()" CommandName="Insert" CssClass="button" />
                </InsertItemTemplate>
            </asp:FormView>
        </div>

        <br />
        <div>
            存储方式：<asp:DropDownList ID="drpSavingType2" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnQuery" runat="server" Text="检索" OnClientClick="" OnClick="Query" CommandName="Select" CssClass="button" />
        </div>
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="false"
            DataKeyNames="Id"
            AllowPaging="true"
            DataSourceID="ObjectDataSource1"
            PageSize="20" Width="100%" CssClass="grid">
            <Columns>
                <asp:TemplateField HeaderText="Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("Id")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="版本">
                    <ItemTemplate>
                        <%# Eval("FileVersion") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="文件名">
                    <ItemTemplate>
                        <a href='Download.ashx?version=<%# Eval("FileVersion") %>'><%# Eval("FileName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发布时间">
                    <ItemTemplate>
                        <%# Eval("PostTime") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            DataObjectTypeName="FreightForwarder.Domain.Entities.UpgradePackage"
            TypeName="FreightForwarder.Upgrade.Service.UpgradePackageBusiness"
            SelectMethod="Select"
            SelectCountMethod="SelectCount"
            InsertMethod="Insert"
            DeleteMethod="Delete"
            EnablePaging="true">
            <%--<SelectParameters>
            </SelectParameters>--%>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
