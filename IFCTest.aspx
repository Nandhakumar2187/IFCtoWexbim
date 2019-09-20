<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IFCTest.aspx.cs" Inherits="WebApplication2.IFCTest" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles.css" rel="stylesheet" />
    <title>Conversion from IFC</title>
    <script type="text/javascript">
        function checkUpload() {
            var id_value = document.getElementById("<%=FileUploadIFC.ClientID %>").value;
            if (id_value == "") {
                alert("Please select the IFC file");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>

    <body leftmargin="0" rightmargin="0" topmargin="0">
    <form id="Form1" runat="server">
        <table id="AutoNumber4" style="border-collapse: collapse" cellspacing="0" cellpadding="0"
            width="100%" border="0">
            <tr>
                <td width="100%" class="tblbi" height="25px">
                    <table id="AutoNumber5" style="border-collapse: collapse" cellspacing="0" cellpadding="0"
                        width="100%" border="0">
                        <tr>
                            <td style="padding-right: 15px; padding-left: 15px; height: 25px; background-color: #3D7862" align="left" width="100%">
                                <table id="AutoNumber1" style="border-collapse: collapse" cellspacing="0" cellpadding="0"
                                    width="100%" border="0">
                                    <tr>
                                        <td style="font-weight: bold; font-size: 10pt; color: white; font-family: Verdana, Tahoma; height: 14px;"
                                            align="left" width="50%">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Georgia">Conversion From IFC</asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table id="Table1" style="border-collapse: collapse" height="94%" cellspacing="0"
            align="center" cellpadding="0" width="100%" border="0">
            <tr>
                <td width="2" class="tblsub">
                    <table id="AutoNumber2" style="border-collapse: collapse" height="100%" cellspacing="0"
                        cellpadding="0" width="2" class="tbllrb_sub" border="0">
                        <tr>
                            <td width="2" height="100%">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="100%" class="tblbg" style="height: 100%; vertical-align: top" align="center">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%">
                                <table id="Table2" align="center" border="1" class="tblon" style="width: 100%; height: 98%; position: static;">

                                    <tr id="trfileupload" runat="server">
                                        <td align="left" class="toprw" width="50%">
                                            <asp:Label ID="lblBaseLineUpload" runat="server" Text="Choose IFC File:" CssClass="lbltxt"></asp:Label>
                                        </td>
                                        <td align="left" class="toprw" width="50%">

                                            <asp:FileUpload ID="FileUploadIFC" runat="server" Width="350px" />
                                    </tr>
   
                                    <tr id="trpropertybtn1" runat="server">
                                        <td colspan="2" align="center" class="altrw" width="100%">
                                            <asp:Button ID=btnConversionfromIFC runat="server" Text="Convert" OnClick="btnConversionfromIFC_Click"
                                                OnClientClick="return checkUpload();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tblsub" style="width: 4px; height: 492px;">
                    <table id="AutoNumber3" style="border-collapse: collapse" height="100%" cellspacing="0"
                        cellpadding="0" width="2" class="tbllrb_sub" border="0">
                        <tr>
                            <td width="2" height="100%">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="AutoNumber6" style="border-collapse: collapse" cellspacing="0" cellpadding="0"
            width="100%" border="0">
            <tr>
                <td width="100%" class="tblsub" style="height: 2px"></td>
            </tr>
        </table>

    </form>
</body>
</html>
