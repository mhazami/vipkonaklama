<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDesigner.aspx.cs" Inherits="Radyn.WebApp.RptDesigner" %>
<%@ Register TagPrefix="cc2" Namespace="Stimulsoft.Report.Web" Assembly="Stimulsoft.Report.WebDesign, Version=2012.2.1304.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <a href="RptDesigner.aspx">RptDesigner.aspx</a><link href="AppCode/Fonts/Zar/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/Verdana/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/Titr/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/BNazanin/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/dastnevis/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/Yekan/stylesheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" class="fit" id="DivError" visible="False" style="color: red; font-weight: bold">
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </div>
        <div>
            <cc2:StiWebDesigner ID="StiWebDesigner1"  OnSaveReport="StiWebDesigner1_OnSaveReport" runat="server" />
        </div>
    </form>
</body>
</html>
