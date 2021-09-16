Enum ColunasGrid_grdAluno As Integer
    Selecionar
    ID_ALUNO
    buttons
End Enum
Partial Class frmAluno
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            HabilitarSecao()
        End If

        Validacao.Outros(txtCPF, False, "CPF do Aluno", "ALUNO", Validacao.eFormato.CPF)
        Validacao.Data(txtDataNascimento, True, "Data Nascimento", "ALUNO")
        Validacao.Outros(txtCPF_Responsavel, True, "CPF do Responsavel", "ALUNO", Validacao.eFormato.CPF)
        Validacao.Outros(txtCelularNumero, False, "Celular do Aluno", "ALUNO", Validacao.eFormato.CELULAR)
        Validacao.Outros(txtTelefoneResponsavel, False, "Celular do Responsavel", "ALUNO", Validacao.eFormato.CELULAR)
        Validacao.Outros(txtEmail, False, "Email do Aluno", "ALUNO", Validacao.eFormato.EMAIL)
        Validacao.Finalizar(btnSalvar, "ALUNO")

        Validacao.Finalizar(lnkSalvarDocumento, "DOCUMENTO")
    End Sub

#Region "Funções de Cadastro"
    Private Sub HabilitarSecao(Optional hab As Boolean = False)
        cadastro.Visible = hab
        listagem.Visible = Not hab
        btnDocumento.Visible = Not hab
        btnEndereco.Visible = Not hab
    End Sub

    Private Function VerificarCpf() As Boolean
        Dim objAluno As New Aluno
        Dim Existe As Boolean = False

        With objAluno.Pesquisar(,,, Replace(Replace(txtCPF.Text, ".", ""), "-", ""))
            If .Rows.Count > 0 Then
                MsgBox("CPF já Cadastrado", eCategoriaMensagem.ALERTA)
                Existe = True
            End If
        End With

        objAluno = Nothing
        Return Existe
    End Function

    Private Sub VerificarCpfTest(Optional Existe As Boolean = True)
        Dim objAluno As New Aluno
        If Existe = True Then
            With objAluno.Pesquisar(,,, Replace(Replace(txtCPF.Text, ".", ""), "-", ""))
                If .Rows.Count > 0 Then
                    MsgBox("CPF já Cadastrado", eCategoriaMensagem.ALERTA)
                End If
            End With
        End If
        objAluno = Nothing
    End Sub

    Private Sub LimparCampos()
        ViewState("Aluno") = Nothing

        txtNome.Text = ""
        txtCPF.Text = ""
        drpSexo.SelectedIndex = 0
        txtEmail.Text = ""
        txtDataNascimento.Text = ""
        txtCelularNumero.Text = ""
        txtNomeResponsavel.Text = ""
        txtCPF_Responsavel.Text = ""
        txtTelefoneResponsavel.Text = ""

        txtNome.Focus()
    End Sub

    Private Sub Salvar()
        Dim objAluno As New Aluno(ViewState("Aluno"))

        With objAluno
            VerificarCpfTest(True)
            .Nome = txtNome.Text
            .CPF = ProBanco(txtCPF.Text, eTipoValor.TEXTO) 'Replace(Replace(txtCPF.Text, ".", ""), "-", "")
            .Telefone = txtCelularNumero.Text
            .NomeResponsavel = txtNomeResponsavel.Text
            .CpfResponsavel = ProBanco(txtCPF_Responsavel.Text, eTipoValor.TEXTO) 'Replace(Replace(txtCPF_Responsavel.Text, ".", ""), "-", "")
            .CelularResponsavel = txtTelefoneResponsavel.Text
            .Sexo = drpSexo.Text
            .DataNascimento = txtDataNascimento.Text
            .Email = txtEmail.Text

            .Salvar()
            If ViewState("Aluno") Is Nothing Then
                ViewState("Aluno") = .Ultimo()
            Else
                ViewState("Aluno") = .Aluno
            End If
        End With

        objAluno = Nothing
    End Sub

    Private Sub Excluir(ByVal Aluno As Integer)
        Dim objAluno As New Aluno

        If objAluno.Excluir(Aluno) > 0 Then
            MsgBox(eTipoMensagem.EXCLUIR_SUCESSO)
        Else
            MsgBox(eTipoMensagem.EXCLUIR_ERRO)
        End If

        objAluno = Nothing

        LimparCampos()
        CarregarGrid()
    End Sub

    Private Sub Voltar()
        Response.Redirect(Request.Url.ToString)
        LimparCampos()
    End Sub

    Private Sub CarregarAluno(ByVal Aluno As Integer)
        Dim objAluno As New Aluno(Aluno)

        With objAluno
            ViewState("Aluno") = .Aluno
            txtNome.Text = .Nome
            txtCPF.Text = .CPF
            txtDataNascimento.Text = .DataNascimento
            txtEmail.Text = .Email
            txtCelularNumero.Text = .Telefone
            txtNomeResponsavel.Text = .NomeResponsavel
            txtCPF_Responsavel.Text = .CpfResponsavel
            txtTelefoneResponsavel.Text = .CelularResponsavel
            'drpSexo.SelectedValue = .Sexo
            SelecionarCombo(drpSexo, .Sexo)
        End With

        objAluno = Nothing
    End Sub


#End Region

#Region "Eventos de Cadastro"

    Protected Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Salvar()
        LimparCampos()
    End Sub

    Private Sub btnDocumento_Click(sender As Object, e As EventArgs) Handles btnDocumento.Click
        mpeDocumento.Show()
    End Sub

#End Region

#Region "Funções de Listagem - Aluno"
    Private Sub CarregarGrid()
        Dim objAluno As New Aluno

        grdAluno.DataSource = objAluno.Pesquisar(ViewState("OrderBy"),, txtLocalizar.Text)
        grdAluno.DataBind()

        objAluno = Nothing

        lblRegistros.Text = DirectCast(grdAluno.DataSource, Data.DataTable).Rows.Count & " registro(s)"
    End Sub
#End Region

#Region "Eventos de Listagem - Aluno"
    Protected Sub grdAluno_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAluno.RowCommand
        If e.CommandName = "" Then
            Response.Redirect(Request.Url.ToString)
        ElseIf e.CommandName = "EXCLUIR" Then
            Excluir(grdAluno.DataKeys(e.CommandArgument).Item(0))
        ElseIf e.CommandName = "EDITAR" Then
            ViewState("Aluno") = grdAluno.DataKeys(e.CommandArgument).Item(0)
            Session("Aluno") = ViewState("Aluno")
            CarregarAluno(ViewState("Aluno"))
            HabilitarSecao(True)
            CarregarGridDocumento()

        End If
    End Sub

    Private Sub grdAluno_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAluno.PageIndexChanging
        grdAluno.PageIndex = e.NewPageIndex
        CarregarGrid()
    End Sub

    Private Sub grdAluno_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAluno.Sorting
        ViewState("OrderByDirection") = IIf(ViewState("OrderByDirection") = "asc", "desc", "asc")
        ViewState("OrderBy") = e.SortExpression & " " & ViewState("OrderByDirection")
        CarregarGrid()
    End Sub

    Private Sub grdAluno_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdAluno.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header

            Case DataControlRowType.DataRow

                Dim lnkExcluirAluno As New LinkButton
                Dim lnkEditarAluno As New LinkButton

                lnkExcluirAluno = DirectCast(e.Row.Cells(ColunasGrid_grdAluno.buttons).FindControl("lnkExcluirAluno"), LinkButton)
                lnkExcluirAluno.CommandArgument = e.Row.RowIndex

                lnkEditarAluno = DirectCast(e.Row.Cells(ColunasGrid_grdAluno.buttons).FindControl("lnkEditarAluno"), LinkButton)
                lnkEditarAluno.CommandArgument = e.Row.RowIndex

                lnkExcluirAluno = Nothing
                lnkEditarAluno = Nothing
        End Select
    End Sub

    Protected Sub btnLocalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalizar.Click
        HabilitarSecao()
        CarregarGrid()
    End Sub

    Protected Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        HabilitarSecao()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos()
        HabilitarSecao(True)
    End Sub

#End Region



#Region "Funções de Cadastro - Documento"
    Private Sub LimparDocumento()
        ViewState("Documento") = Nothing
        txtTelefoneResponsavel.Text = ""

        drpSexo.ClearSelection()

    End Sub

    Private Sub SalvarDocumento()
        Dim objDocumento As New Documento(ViewState("Documento"))

        With objDocumento
            .Aluno = ViewState("Aluno")

            .DataCadastro = ProBanco(Now, eTipoValor.DATA_COMPLETA)
            .DescricaoArquivo = fluDocumento.FileName
            .Salvar()
        End With
        'TODO: SALVAR ARQUIVO EM DISCO.

        objDocumento = Nothing
    End Sub

    Private Sub CarregarDocumento(ByVal Documento As Integer)
        Dim objDocumento As New Documento(Documento)
        With objDocumento
            ViewState("Documento") = DoBanco(.Documento, eTipoValor.CHAVE)
            'TODO: LEMBRAR ARRUMAR.
        End With
        objDocumento = Nothing
    End Sub

#End Region

#Region "Eventos do Cadastro - Documento"

    Private Sub btnVoltarDocumento_Click(sender As Object, e As EventArgs) Handles btnVoltarDocumento.Click
        mpeDocumento.Hide()
    End Sub

#End Region

#Region "Funções de Listagem - Documento"

    Private Sub CarregarGridDocumento()
        Dim objDocumento As New Documento
        btnDocumento.Visible = True
        btnEndereco.Visible = True

        grdDocumento.DataSource = objDocumento.Pesquisar(ViewState("OrderBy"),, ViewState("Aluno"))
        grdDocumento.DataBind()

        objDocumento = Nothing

        lblRegistrosDocumento.Text = DirectCast(grdDocumento.DataSource, Data.DataTable).Rows.Count & " registro(s)"
    End Sub

    Private Sub lnkSalvarDocumento_Click(sender As Object, e As EventArgs) Handles lnkSalvarDocumento.Click
        If fluDocumento.FileName Is Nothing Then
            MsgBox("Necessário anexar arquivo.")
        Else
            SalvarDocumento()
        End If
    End Sub

    Private Sub grdDocumento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDocumento.RowCommand

    End Sub

    Private Sub btnEndereco_Click(sender As Object, e As EventArgs) Handles btnEndereco.Click
        If Session("Aluno") Is Nothing Then
            MsgBox("Selecione primeiro um aluno!")
        Else
            Response.Redirect("frmAlunoEndereco.aspx")
        End If
    End Sub


#End Region

#Region "Eventos de Listagem - Documento"

#End Region

End Class
