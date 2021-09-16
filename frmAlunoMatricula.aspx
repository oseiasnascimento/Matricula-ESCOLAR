<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frmAlunoMatricula.aspx.vb" Inherits="frmMatricula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            <i class="fa fa-user-plus"></i>Pré-Matricula
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
                    <asp:Label ID="lblEtapa" runat="server" />
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
                <div class='box-header'>
                    <h3 class='box-title'><i class="glyphicon glyphicon-save"></i> Cadastro da Pré-Matricula</h3>
                </div>
                <asp:Panel ID="pnlCampos" runat="server">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    Alunos<br/>
                                    <asp:DropDownList ID="drpAluno" class="form-control" runat="server" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    Etapa<br/>
                                    <asp:DropDownList ID="drpEtapa" class="form-control" runat="server" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divBotoes" runat="server" class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnSalvar" runat="server" class="btn btn-success"><i class="fa fa-save"></i> Salvar</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnVoltar" runat="server" class="btn btn-warning"><i class="fa fa-mail-reply"></i> Voltar</asp:LinkButton>
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
                        <h3 class='box-title'><i class="fa fa-search"></i> Localizar Pré-Matriculados</h3>
                    </div>
                    <div class="box-body">
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnLocalizar">
                            <div class="row">
                                <div class="col-sm-6">
                                    Localizar pelo Nome do Aluno<br />
                                    <asp:TextBox ID="txtLocalizar" runat="server" class="form-control" />
                                </div>
                                
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnLocalizar" runat="server" class="btn btn-default"><i class="fa fa-search"></i> Localizar</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnNovo" runat="server" class="btn btn-info"><i class="fa fa-file-o"></i>  Novo</asp:LinkButton>
                        </div>
                    </div>
                    <div id="divMatricula" runat="server" class="box-footer">
                        <asp:Label ID="lblRegistrosMatricula" runat="server" CssClass="badge bg-aqua" />
                        <asp:GridView ID="grdMatricula" runat="server" CssClass="table table-bordered" PagerStyle-CssClass="paginacao" AllowSorting="True" AllowPaging="True" PageSize="20" AutoGenerateColumns="False"
                            DataKeyNames="DE09_ID_MATRICULA ">
                            <HeaderStyle CssClass="bg-aqua" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DE09_NU_MATRICULA" SortExpression="DE09_NU_MATRICULA" HeaderText="Matricula" />
                                <asp:BoundField DataField="DE01_NM_ALUNO" SortExpression="DE01_NM_ALUNO" HeaderText="Estudante" />
                                <asp:BoundField DataField="DE06_NM_ETAPA" SortExpression="DE06_NM_ETAPA" HeaderText="Etapa" />

                                <asp:TemplateField HeaderText="" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkExcluirMatricula" runat="server" class="btn btn-social-icon bg-red" CommandName="EXCLUIR" ToolTip="ExcluirMatricula">
                                                <i id="iExcluirMatricula" runat="server" class="glyphicon glyphicon-remove"></i>
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
