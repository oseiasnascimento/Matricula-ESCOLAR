<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frmAlunoEnturmacao.aspx.vb" Inherits="frmEnturmacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            <i class="fa fa-list-alt"></i>Enturmação
            <small>Matriculas</small>
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
        <asp:Panel ID="pnlTurma" runat="server">
            <div class="box box-primary">
                <asp:Panel ID="Panel2" runat="server">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    Etapa - Ano / Série<br/>
                                    <asp:DropDownList ID="drpEtapa" class="form-control" runat="server" OnSelectedIndexChanged ="OnSelectedIndexChanged" AutoPostBack="true"  />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    Turmas<br/>
                                    <asp:DropDownList ID="drpTurma" class="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    Pesquisar por Aluno:<br/>
                                    <asp:TextBox runat="server" type="text" class="form-control" ID="txtNome" name="Nome" placeholder="Ex.: Joao" MaxLength="14" />
                                </div>
                            </div>
                        </div>
                        <div id="divBotoes" class="box-footer">
                            <div class="btn-group">
                                <asp:LinkButton ID="btnLocalizar" runat="server" class="btn btn-default"><i class="fa fa-search"></i> Localizar</asp:LinkButton>
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton ID="btnVoltar" runat="server" class="btn btn-warning"><i class="fa fa-mail-reply"></i> Voltar</asp:LinkButton>
                            </div>                            
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
                        <h3 class='box-title'><i class="glyphicon glyphicon-user"></i>Alunos Matriculados</h3>
                    </div>
                    <div id="divMatricula" runat="server" class="box-footer">
                        <asp:Label ID="lblMatriculado" runat="server" CssClass="badge bg-aqua" />
                        <asp:GridView ID="grdMatriculado" runat="server"
                            CssClass="table table-bordered"
                            PagerStyle-CssClass="paginacao"
                            AllowSorting="True"
                            AllowPaging="True"
                            PageSize="20"
                            AutoGenerateColumns="False"
                            DataKeyNames="DE09_ID_MATRICULA">
                            <HeaderStyle CssClass="bg-aqua" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DE01_NM_ALUNO" SortExpression="DE01_NM_ALUNO" HeaderText="Aluno" />
                                <asp:BoundField DataField="DE10_NU_TURMA" SortExpression="DE10_NU_TURMA" HeaderText="Turma" />
                                <asp:BoundField DataField="NM_SITUACAO" SortExpression="NM_SITUACAO" HeaderText="Situação" />

                                <asp:TemplateField HeaderText="Enturmar" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkEnturmarMatricula" runat="server" class="btn btn-social-icon bg-green" CommandName="ENTURMAR" ToolTip="Enturmar">
                                                <i id="iEnturmarMatricula" runat="server" class="glyphicon glyphicon-arrow-down"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class='box-header'>
                        <h3 class='box-title'><i class="glyphicon glyphicon-user"></i>Alunos Enturmados</h3>
                    </div>
                    <div id="divEnturmado" runat="server" class="box-footer">
                        <asp:Label ID="lblEnturmado" runat="server" CssClass="badge bg-aqua" />
                        <asp:GridView ID="grdEnturmado" runat="server" CssClass="table table-bordered" PagerStyle-CssClass="paginacao" AllowSorting="True" AllowPaging="True" PageSize="20" AutoGenerateColumns="False"
                            DataKeyNames="DE11_ID_ENTURMACAO">
                            <HeaderStyle CssClass="bg-aqua" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DE01_NM_ALUNO" SortExpression="DE01_NM_ALUNO" HeaderText="Aluno" />
                                <asp:BoundField DataField="DE10_NU_TURMA" SortExpression="DE10_NU_TURMA" HeaderText="Turma" />
                                <asp:BoundField DataField="NM_SITUACAO" SortExpression="NM_SITUACAO" HeaderText="Situação" />

                                <asp:TemplateField HeaderText="" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="lnkRemoveEnturmacao" runat="server" class="btn btn-social-icon bg-red" CommandName="REMOVER" ToolTip="RemoverEnturmarcao">
                                                <i id="iRemoveEnturmacao" runat="server" class="glyphicon glyphicon-remove"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="divBotoesFooter" class="box-footer">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnVoltarTurma" runat="server" class="btn btn-warning"><i class="glyphicon glyphicon-share-alt"></i> Voltar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
