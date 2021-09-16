<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frmSelecaoEscola.aspx.vb" Inherits="frmSelecionar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            <i class="fa fa-book"></i>Seleção de Escola
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><i class="fa fa-user"></i>Diário</li>
            <li class="active"><i class="fa fa-list-alt"></i> Seleção de Escola</li>
        </ol>
    </section>
    <section class="content">
    </section>
    <section id="cadastro" runat="server" class="content">
        <asp:Panel ID="pnlCadastro" runat="server">
            <div class="box box-primary">
                <div class='box-header'>
                    <h3 class='box-title'><i class="fa fa-check"></i> Selecionar Escola</h3>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-5">
                                <div class="form-group">
                                    Escola<br/>
                                    <asp:DropDownList ID="drpLotacao" class="form-control" runat="server" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    Ano Letivo<br/>
                                    <asp:DropDownList ID="drpPeriodo" class="form-control" runat="server" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divBotoes" runat="server" class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnOk" runat="server" class="btn btn-success"><i class="fa fa-check"></i> Ok</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnSair" runat="server" class="btn btn-danger"><i class="fa fa-remove"></i> Sair</asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </section>

</asp:Content>
