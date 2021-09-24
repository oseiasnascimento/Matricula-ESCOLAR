<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageAluno.master" AutoEventWireup="false" CodeFile="frmAluno.aspx.vb" Inherits="frmAluno" %>

<%@ Register TagPrefix="cc1" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section id="cadastro" runat="server">
                <asp:Panel ID="pnlCadastro" runat="server">
                    <div class="box box-primary">
                        <div class='box-header'>
                            <h3 class='box-title'><i class="fa fa-save"></i>Cadastro do Aluno</h3>
                        </div>
                        <asp:Panel ID="pnlCampos" runat="server">
                            <div class="box-body">
                                <div class="box-body">
                                    <!-- dados do aluno -->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                Nome Completo*<br />
                                                <asp:TextBox ID="txtNome" runat="server" required="required" type="text" class="form-control" name="Nome" placeholder="Digite seu Nome completo" MaxLength="50" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                CPF*<br />
                                                <asp:TextBox ID="txtCPF" runat="server" required="required" type="text" class="form-control" name="CPF" placeholder="Ex.: 010.011.111-00" MaxLength="14" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                Sexo<br />
                                                <asp:DropDownList ID="drpSexo" runat="server" class="form-control">
                                                    <asp:ListItem Value="0">...</asp:ListItem>
                                                    <asp:ListItem Value="1">MASCULINO</asp:ListItem>
                                                    <asp:ListItem Value="2">FEMININO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                Email<br />
                                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" MaxLength="100" placeholder="Digite o seu Email" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                Data de Nascimento<br />
                                                <asp:TextBox ID="txtDataNascimento" runat="server" MaxLength="10" CssClass="form-control" placeholder="01/01/2021" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                Celular<br />
                                                <asp:TextBox ID="txtCelularNumero" runat="server" CssClass="form-control" MaxLength="12" />
                                            </div>
                                        </div>
                                    </div>

                                    <!-- dados do responsável -->

                                    <div class="row">
                                        <br />
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                Nome do Responsavel<br />
                                                <asp:TextBox ID="txtNomeResponsavel" runat="server" class="form-control" placeholder="Ex.: Nome do Responsavel" MaxLength="80" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                CPF do Responsavel<br />
                                                <asp:TextBox ID="txtCPF_Responsavel" runat="server" required="required" type="text" class="form-control" name="CPF" placeholder="Ex.: 010.011.111-00" MaxLength="14" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                Telefone do Responsável<br />
                                                <asp:TextBox runat="server" required="required" type="tel" class="form-control" ID="txtTelefoneResponsavel" name="Telefone" placeholder="Ex.: (11) 2020-3030" MaxLength="15" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="divBotoes" runat="server" class="box-footer">
                                    <div class="btn-group">
                                        <asp:LinkButton ID="btnSalvar" runat="server" class="btn btn-success"><i class="fa fa-save"></i> Salvar</asp:LinkButton>
                                    </div>
                                    <%--Butão para fazer push no GitHub--%>
                                    <div class="btn-group">
                                        <asp:LinkButton ID="btnNovo1" runat="server" class="btn btn-info"><i class="fa fa-plus"></i> Novo</asp:LinkButton>
                                    </div>
                                    <div class="btn-group">
                                        <asp:LinkButton ID="btnDocumento" runat="server" class="btn btn-info"><i class="fa fa-plus"></i> Documento</asp:LinkButton>
                                    </div>
                                    <div class="btn-group">
                                        <asp:LinkButton ID="btnEndereco" runat="server" class="btn btn-info"><i class="fa fa-plus"></i> Endereço</asp:LinkButton>
                                    </div>
                                    <div class="btn-group">
                                        <asp:LinkButton ID="btnVoltar" runat="server" class="btn btn-warning"><i class="fa fa-share-alt"></i> Voltar</asp:LinkButton>
                                    </div>
                                </div>
                                <!-- dados do documento -->
                                <div class="form-group">
                                    <asp:Label ID="lblAncoraModal" runat="server" />
                                    <cc1:ModalPopupExtender ID="mpeDocumento" runat="server"
                                        PopupControlID="pnlAncoraModal"
                                        TargetControlID="lblAncoraModal"
                                        BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="pnlAncoraModal" runat="server" CssClass="form-group col-sm-8">
                                        <div class="box">
                                            <div class="box-body">
                                                <div class="box-blue">
                                                    <asp:UpdatePanel ID="upDocumento" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-md-12">
                                                                <b>Anexar arquivo</b><br />
                                                                <asp:FileUpload ID="fluDocumento" runat="server" />
                                                                <div class="btn-group">
                                                                    <asp:LinkButton ID="lnkSalvarDocumento" runat="server" class="btn btn-success"><i class="glyphicon glyphicon-save"></i> Salvar Documento</asp:LinkButton>
                                                                </div>
                                                                <div class="btn-group">
                                                                    <asp:LinkButton ID="btnVoltarDocumento" runat="server" class="btn btn-warning"><i class="glyphicon glyphicon-share-alt"></i> Voltar</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="lblRegistrosDocumento" runat="server" CssClass="badge bg-aqua" />
                                                                <asp:GridView ID="grdDocumento" runat="server"
                                                                    CssClass="table table-bordered"
                                                                    PagerStyle-CssClass="paginacao"
                                                                    AllowSorting="True"
                                                                    AllowPaging="True"
                                                                    PageSize="20"
                                                                    AutoGenerateColumns="False"
                                                                    DataKeyNames="DE01_ID_ALUNO">
                                                                    <HeaderStyle CssClass="bg-aqua" ForeColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DE02_DS_ARQUIVO" SortExpression="DE02_DS_ARQUIVO" HeaderText="ARQUIVO" />
                                                                        <asp:BoundField DataField="DE02_DH_CADASTRO" SortExpression="DE02_DH_CADASTRO" HeaderText="DATA" DataFormatString="{0:dd/MM/yyyy}" />

                                                                        <asp:TemplateField HeaderText="" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                                            <ItemTemplate>
                                                                                <div class="btn-group">
                                                                                    <asp:LinkButton ID="lnkExcluirDoc" runat="server" class="btn btn-social-icon bg-red" CommandName="EXCLUIR" ToolTip="Excluir Documento">
                                                                                        <i id="iExcluirDoc" runat="server" class="glyphicon glyphicon-remove"></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </section>

            <section id="listagem" runat="server">
                <div class='row'>
                    <div class='col-sm-12'>
                        <div class='box box-blue'>
                            <div class='box-header'>
                                <h3 class='box-title'><i class="glyphicon glyphicon-search"></i>Localizar Aluno</h3>
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
                                    <asp:LinkButton ID="btnLocalizar" runat="server" class="btn btn-default"><i class="glyphicon glyphicon-search"></i> Localizar</asp:LinkButton>
                                </div>
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnNovo" runat="server" class="btn btn-info"><i class="glyphicon glyphicon-plus"></i>  Novo</asp:LinkButton>
                                </div>
                            </div>

                            <div id="divAluno" runat="server" class="box-footer">
                                <asp:Label ID="lblRegistros" runat="server" CssClass="badge bg-aqua" />
                                <asp:GridView ID="grdAluno" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="paginacao"
                                    AllowSorting="True"
                                    AllowPaging="True"
                                    PageSize="20"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="DE01_ID_ALUNO">
                                    <HeaderStyle CssClass="bg-aqua" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="DE01_NM_ALUNO" SortExpression="DE01_NM_ALUNO" HeaderText="Nome" />
                                        <asp:BoundField DataField="DE01_NU_CPF" SortExpression="DE01_NU_CPF" HeaderText="CPF" DataFormatString="{0:###.###.###-##}" />
                                        <asp:BoundField DataField="DE01_NU_CELULAR" SortExpression="DE01_NU_CELULAR" HeaderText="Celular" />

                                        <asp:TemplateField HeaderText="" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkExcluirAluno" runat="server" class="btn btn-social-icon bg-red" CommandName="EXCLUIR" ToolTip="ExcluirAluno">
                                                        <i id="iExcluirAluno" runat="server" class="glyphicon glyphicon-trash"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" SortExpression="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <div class="btn-group">
                                                    <asp:LinkButton ID="lnkEditarAluno" runat="server" class="btn btn-social-icon bg-blue" CommandName="EDITAR" ToolTip="EditarAluno">
                                                        <i id="iEditarAluno" runat="server" class="glyphicon glyphicon-pencil"></i>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
