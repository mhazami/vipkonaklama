<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptViewer.aspx.cs" Inherits="Radyn.WebApp.RptViewer" %>
<%@ Register TagPrefix="cc2" Namespace="Stimulsoft.Report.Web" Assembly="Stimulsoft.Report.Web, Version=2012.2.1304.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>گزارش
    </title>
    <link href="AppCode/Fonts/Vazir/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/Titr/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/BNazanin/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/dastnevis/stylesheet.css" rel="stylesheet" />
    <link href="AppCode/Fonts/Yekan/stylesheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <cc2:StiWebViewer ID="StiWebViewer1" runat="server"
                ToolBarBackColor="WhiteSmoke" ImagesPath="Images/" EnableTheming="True" Theme="Office2010"></cc2:StiWebViewer>
        </div>
    </form>
</body>
</html>
