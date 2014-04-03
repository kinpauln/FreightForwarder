<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusDetail.aspx.cs" Inherits="BigzoneBusinessCenterService.BusDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="Style/Style.css" rel="Stylesheet" type="text/css" />
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
        <asp:Repeater ID="rep" runat="server" DataSourceID="ssData">
            <ItemTemplate>
                <div style="width:100%;text-align:right;padding-bottom:10px;">
                    <img src='Handler/Handler.ashx?op=barcode&BarCode=<%# Eval("BarCode") %>' border="0" />
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                  <tr align="center" valign="middle">
                    <td height="50" colspan="4" style="font-size:1.8em;text-align:center;">业务受理回执</td>
                  </tr>
                  <tr valign="middle">
                    <td class="field">申请人姓名：</td>
                    <td><%# Eval("ApplyName") %></td>
                    <td class="field">业务类型：</td>
                    <td><%# Eval("BusTypeText") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field">身份证号：</td>
                    <td height="30"><%# Eval("IdCard") %></td>
                    <td class="field" >电子邮件：</td>
                    <td><%# Eval("Mail") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field">手机号：</td>
                    <td height="30"><%# Eval("Tel") %></td>
                    <td class="field" >联系电话：</td>
                    <td><%# Eval("OfficeTel") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field" >家庭电话：</td>
                    <td><%# Eval("FamilyTel") %></td>
                    <td class="field" >邮政编码：</td>
                    <td><%# Eval("Code") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field" >邮寄地址：</td>
                    <td colspan="3"><%# Eval("Addr") %></td>
                  </tr>
                  <tr valign="middle" style="border-style:none">
                    <td height="30" colspan="4" style="text-align:right;border-style:none;padding:10px 0;">快递签字：__________</td>
                  </tr>
                </table>                
            </ItemTemplate>
        </asp:Repeater>
        <div class="spliter">&nbsp;</div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ssData">
            <ItemTemplate>
                <div style="width:100%;text-align:right;padding-bottom:10px;">
                    <img src='Handler/Handler.ashx?op=barcode&BarCode=<%# Eval("BarCode") %>' border="0" />
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                  <tr align="center" valign="middle">
                    <td height="50" colspan="4" style="font-size:1.8em;text-align:center;">业务受理回执</td>
                  </tr>
                  <tr valign="middle">
                    <td class="field">申请人姓名：</td>
                    <td><%# Eval("ApplyName") %></td>
                    <td class="field">业务类型：</td>
                    <td><%# Eval("BusTypeText") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field">身份证号：</td>
                    <td height="30"><%# Eval("IdCard") %></td>
                    <td class="field" >电子邮件：</td>
                    <td><%# Eval("Mail") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field">手机号：</td>
                    <td height="30"><%# Eval("Tel") %></td>
                    <td class="field" >联系电话：</td>
                    <td><%# Eval("OfficeTel") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field" >家庭电话：</td>
                    <td><%# Eval("FamilyTel") %></td>
                    <td class="field" >邮政编码：</td>
                    <td><%# Eval("Code") %></td>
                  </tr>
                  <tr valign="middle">
                    <td class="field" >邮寄地址：</td>
                    <td colspan="3"><%# Eval("Addr") %></td>
                  </tr>
                </table>                
                <div style="padding:10px 0; text-align:right;">档案签字：__________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;送到时间：__________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;申请人签字：__________</div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="ssData" runat="server"
             ConnectionString="<%$ ConnectionStrings:TransPad %>"
             SelectCommand="">
        </asp:SqlDataSource>
    </form>
</body>
</html>
