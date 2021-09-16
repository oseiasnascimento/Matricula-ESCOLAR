<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frmAlunoTurma.aspx.vb" Inherits="frmTurma" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <h1>
            <i class="glyphicon  glyphicon-th-list"></i> Turmas
            <small>Cadastro</small>
        </h1>
    </section>
    <section id="divInformacoes" runat="server" class="content" style="min-height: 0px !important; padding-bottom: 0px !important;">
        <div class="callout callout-info">
            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lblLotacao" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lblPeriodo" runat="server" />
                </div>
            </div>
        </div>
    </section>
    <section id="cadastro" runat="server" class="content">
        <asp:Panel ID="pnlCadastro" runat="server">
            <div class="box box-primary">
                <asp:Panel ID="pnlCampos" runat="server">
                    <div class="box-body">
                        <div id="divEtapa" runat="server" class="row">
                            
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    Número<br />
                                    <asp:TextBox ID="txtNumero" runat="server" MaxLength="10" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    Etapa - Ano/Série<br />
                                    <asp:DropDownList ID="drpEtapa" runat="server" CssClass="form-control" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divBotoes" runat="server" class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnSalvar" runat="server" class="btn btn-success"><i class="glyphicon glyphicon-save"></i> Salvar</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnVoltar" runat="server" class="btn btn-warning"><i class="glyphicon glyphicon-share-alt"></i> Voltar</asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </section>
    <section id="listagem" runat="server" class="content">
        <div class='row'>
            <div class='col-sm-12'>
                <div class='box box-blue'>
                    <div class='box-header'>
                        <h3 class='box-title'><i class="glyphicon glyphicon-search"></i> Localizar Turma</h3>
                    </div>
                    <div class="box-body">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLocalizar">
                            <div class="row">                                
                                <div class="col-sm-6">
                                    Localizar Turma<br />
                                    <asp:TextBox ID="txtLocalizar" runat="server" class="form-control" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnLocalizar" runat="server" class="btn btn-default"><i class="glyphicon glyphicon-search"></i> Localizar</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnNovo" runat="server" class="btn btn-info"><i class="glyphicon glyphicon-plus"></i>  Nova</asp:LinkButton>
                        </div>
                    </div>
                   <div id="divTurma" runat="server" class="box-footer">
                        <asp:Label ID="lblRegistros" runat="server" CssClass="badge bg-aqua" />
                        <asp:GridView ID="grdTurma" runat="server" CssClass="table table-bordered" PagerStyle-CssClass="paginacao" AllowSorting="True" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" 
                            DataKeyNames="DE10_ID_TURMA">
                            <HeaderStyle CssClass="bg-aqua" ForeColor="White" />
                            <Columns>
                                <asp:ButtonField DataTextField="DE10_NU_TURMA" SortExpression="DE10_NU_TURMA" HeaderText="Número" />
                                <asp:ButtonField DataTextField="DE06_NM_ETAPA" SortExpression="DE06_NM_ETAPA" HeaderText="Etapa" />                                
                                <asp:ButtonField DataTextField="DE04_NM_LOTACAO" SortExpression="DE04_NM_LOTACAO" HeaderText="Escola" />                                
                                <asp:ButtonField DataTextField="DE08_NM_PERIODO" SortExpression="DE08_NM_PERIODO" HeaderText="Periodo" />
                                <asp:TemplateField HeaderText="" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkExcluirTurma" runat="server" class="btn btn-social-icon bg-red" CommandName="EXCLUIR" ToolTip="ExcluirTurma">
                                                <i id="iExcluirTurma" runat="server" class="glyphicon glyphicon-remove"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </section>



</asp:Content>
