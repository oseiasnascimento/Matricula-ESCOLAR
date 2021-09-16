<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Estudo Dirigito | Login</title>

    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="https://adminlte.io/themes/AdminLTE/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="https://adminlte.io/themes/AdminLTE/dist/css/AdminLTE.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="https://adminlte.io/themes/AdminLTE/plugins/iCheck/square/blue.css" />

    <link href="css/ionicons.min.css" rel="stylesheet" type="text/css" />

</head>


<body class="hold-transition login-page">

    <div class="login-box">
        <div class="login-logo">
            <img src="img/LogoOficialGovernoPreto.png" alt="" height="180" />
        </div>

        <div class="login-box-body">
            <div class="login-box-msg">
                <h4 class="login-box-msg">Login</h4>
            </div>

            <form id="Form1" method="post" runat="server">
                <asp:ScriptManager ID="ScriptManager" runat="server">
                    <Scripts>
                        <asp:ScriptReference Path="~/JS/Safari3AjaxHack.js" />
                    </Scripts>
                </asp:ScriptManager>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtCPF" runat="server" class="form-control" MaxLength="14" placeholder="CPF" onkeyup="formataCPF(this,event);" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="CPF" ControlToValidate="txtCPF" SetFocusOnError="true" ValidationGroup="login" Display="None" />
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtSenha" runat="server" required="true" class="form-control" MaxLength="14" placeholder="Senha" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="CPF" ControlToValidate="txtSenha" SetFocusOnError="true" ValidationGroup="login" Display="None" />
                </div>
                <div class="row">
                    <div class="col-xs-8">
                    </div>
                    <div class="col-xs-4">
                        <asp:Button ID="btnEntrar" CssClass="btn btn-primary btn-block btn-flat" runat="server" AccessKey="E" Text="Entrar" CausesValidation="true" ValidationGroup="login" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="login" ShowMessageBox="true" ShowSummary="false" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
