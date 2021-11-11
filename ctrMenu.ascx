<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctrMenu.ascx.vb" Inherits="NewExtranet_ctrMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<section class="sidebar">
    <div class="user-panel" id="divGoverno" runat="server">
        <img src="img/LogoOficialGovernoPreto.png" alt="" width="200" />
    </div>
    <div class="user-panel" id="divUsuario" runat="server">
        <div class="pull-left image">
            <img src="img/avatar.png" class="img-circle" alt="User Image" />
        </div>
        <div class="pull-left info">
            <p>
                Ola, Oseias Saboia
                <asp:Label ID="lblUsuario" runat="server" Text="" />
            </p>

            <a href="#"><i class="glyphicon glyphicon-ok-circle text-success"></i>Online</a>
            <asp:LinkButton ID="lnkSair" runat="server" Text="Sair" />
        </div>
    </div>

    <ul class="sidebar-menu">
        <li class="active">
            <li id="liPaginaInicial" runat="server" visible="True"><a href="Default.aspx"><i class="glyphicon glyphicon-home"></i>Página Inicial</a></li>
        </li>
        <li id="liTesteSeduc" runat="server" visible="True" class="treeview">
            <a href="#">
                <i class="glyphicon glyphicon-pencil"></i>
                <span>Cadastros</span>
                <i class="glyphicon glyphicon-chevron-down"></i>
            </a>
            <ul class="treeview-menu">
                 <li id="liSelecao" runat="server" visible="True">
                    <a href="frmSelecaoEscola.aspx">
                        <i class="fa fa-book"></i>Selecionar Escola
                    </a>
                </li>
                <li id="liAluno" runat="server" visible="True">
                    <a href="frmAluno.aspx">
                        <i class="fa fa-user"></i>Alunos
                    </a>
                </li>
                <li id="liAlunoEndereco" runat="server" visible="True">
                    <a href="frmAlunoEndereco.aspx">
                        <i class="fa fa-user"></i>Endereço do Aluno
                    </a>
                </li>
                <li id="liTurma" runat="server" visible="True">
                    <a href="frmAlunoTurma.aspx">
                        <i class="glyphicon glyphicon-th-list"></i>Turma 
                    </a>
                </li>
                <li id="liPreMatricular" runat="server" visible="True">
                    <a href="frmAlunoMatricula.aspx">
                        <i class="glyphicon glyphicon-file"></i>Pré-Matricula
                    </a>
                </li> 
                <li id="liMatricular" runat="server" visible="True">
                    <a href="frmAlunoEnturmacao.aspx">
                        <i class="glyphicon glyphicon-file"></i>Matricula
                    </a>
                </li>                
            </ul>
        </li>
    </ul>
</section>

