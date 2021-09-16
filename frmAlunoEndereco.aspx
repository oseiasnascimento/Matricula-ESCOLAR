<%@ Page Language="VB" MasterPageFile="~/MasterPageAluno.master" AutoEventWireup="false" CodeFile="frmAlunoEndereco.aspx.vb" Inherits="frmAlunoEndereco" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section id="cadastro" runat="server">
                <div class="box box-primary">
                    <asp:Panel ID="pnlCadastro" runat="server" DefaultButton="btnSalvar">
                        <div class="box-body">
                            <div class='box-header'>
                                <h3 class='box-title'><i class="glyphicon glyphicon-save"></i>Endereço do Aluno</h3>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-3">
                                    Cep<br />
                                    <asp:TextBox ID="txtCep" runat="server" MaxLength="8" class="form-control" onkeyup="formataInteiro(this,event);" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-8">
                                    Logradouro<br />
                                    <asp:TextBox ID="txtLogradouro" runat="server" MaxLength="50" class="form-control" />
                                </div>
                                <div class="form-group col-sm-2">
                                    Número<br />
                                    <asp:TextBox ID="txtNumero" runat="server" MaxLength="20" class="form-control" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-8">
                                    Complemento<br />
                                    <asp:TextBox ID="txtComplemento" runat="server" MaxLength="50" class="form-control" />
                                </div>
                                <div class="form-group col-sm-4">
                                    Bairro<br />
                                    <asp:TextBox ID="txtBairro" runat="server" MaxLength="50" class="form-control" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-3">
                                    Estado<br />
                                    <asp:DropDownList ID="drpEstado" runat="server" class="form-control" AutoPostBack="true" >
                                        <asp:ListItem Value="0">...</asp:ListItem>
                                        <asp:ListItem Value="MA">MARANHAO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-3">
                                    Município<br />
                                    <asp:DropDownList ID="drpMunicipio" runat="server" class="form-control" >
                                        <asp:ListItem Value="0">...</asp:ListItem>
                                        <asp:ListItem Value="1">SAO LUIS</asp:ListItem>
                                        <asp:ListItem Value="2">RAPOSA</asp:ListItem>
                                        <asp:ListItem Value="3">RIBAMAR</asp:ListItem>
                                        <asp:ListItem Value="4">PACO DO LUMIAR</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-3">
                                    Zona Residência<br />
                                    <asp:DropDownList ID="drpZonaResidencia" runat="server" class="form-control">
                                        <asp:ListItem Value="0">...</asp:ListItem>
                                        <asp:ListItem Value="1">URBANA</asp:ListItem>
                                        <asp:ListItem Value="2">RURAL</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                        </div>
                    </asp:Panel>
                    <div id="divBotoes" runat="server" class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnNovo" runat="server" class="btn btn-info"><i class="glyphicon glyphicon-file"></i> Novo</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnSalvar" runat="server" class="btn btn-success"><i class="glyphicon glyphicon-save"></i> Salvar</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnVoltar" runat="server" class="btn btn-warning"><i class="glyphicon glyphicon-share-alt"></i> Voltar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
