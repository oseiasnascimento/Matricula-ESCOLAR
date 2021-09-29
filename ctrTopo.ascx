<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctrTopo.ascx.vb" Inherits="NewExtranet_ctrTopo" %>

<a href="#" class="logo">
    <!-- Add the class icon to your logo image or logo icon to add the margining -->
    <span class="logo-mini"><b>MAT</b></span>
    <asp:Label ID="lblSistema" runat="server" Text="MATRICULA" />
</a>
<!-- Header Navbar: style can be found in header.less oseias -->
<nav class="navbar navbar-static-top" role="navigation">
    <!-- Sidebar toggle button-->
    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
    </a>
    <div class="navbar-right">
        <ul class="nav navbar-nav">
            <!-- User Account: style can be found in dropdown.less -->
            <li class="dropdown user user-menu">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                    <i class="fa fa-user"></i>
                    <span>
                        <asp:Label ID="lblUsuario" runat="server" /><i class="caret"></i></span>
                </a>
                <ul class="dropdown-menu" id="divUsuario" runat="server">
                    <!-- User image -->
                    <li class="user-header">
                        <img src="img/avatar.png" class="img-circle" alt="User Image" />
                        <p>
                            <asp:Label ID="lblNomeUsuario" runat="server" />
                            <small></small>
                        </p>
                    </li>
                    <!-- Menu Body -->
                    <li class="user-body">
                        <div class="col-xs-4 text-center">
                            <a href="#"></a>
                        </div>
                        <div class="col-xs-4 text-center">
                            <a href="#"></a>
                        </div>
                        <div class="col-xs-4 text-center">
                            <a href="#"></a>
                        </div>
                    </li>
                    <!-- Menu Footer-->
                    <li class="user-footer">
                        <div class="pull-left">
                            <a href="Default.aspx" class="btn btn-default btn-flat">Alterar Senha</a>
                        </div>
                        <div class="pull-right">
                            <a href="Default.aspx" class="btn btn-default btn-flat">Sair</a>
                        </div>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
</nav>
