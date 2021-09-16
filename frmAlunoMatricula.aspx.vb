Imports System.Data

Enum ColunasGrid_grdMatricula As Integer
    Selecionar
    ID_MATRICULA
    buttons
End Enum

Partial Class frmMatricula
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'If Session("CodigoUsuario") Is Nothing Then
            '    Response.Redirect("Default.aspx")
            'End If

            If Session("Lotacao") Is Nothing Then
                Response.Redirect("frmSelecionar.aspx")
            End If

            If Session("Periodo") Is Nothing Then
                Response.Redirect("frmSelecionar.aspx")
            End If

            Informacao()
            HabilitarSecao()

        End If

    End Sub

#Region "Funções de Cadastro"
    Private Sub HabilitarSecao(Optional hab As Boolean = False)
        cadastro.Visible = hab
        listagem.Visible = Not hab
        divMatricula.Visible = Not hab
    End Sub

    Private Function VerificarMatricula() As Boolean
        Dim objDocumento As New Matricula
        Dim Existe As Boolean = False

        With objDocumento.Pesquisar(,, drpAluno.Text)
            If .Rows.Count > 0 Then
                MsgBox("Aluno já Matriculado", eCategoriaMensagem.ALERTA)
                Existe = True
            End If
        End With

        objDocumento = Nothing
        Return Existe
    End Function

    Private Sub LimparCampos()
        ViewState("CodigoMatricula") = Nothing
        drpAluno.ClearSelection()
        drpEtapa.ClearSelection()
    End Sub

    Private Sub Salvar()
        Dim objMatricula As New Matricula(ViewState("Matricula"))

        With objMatricula
            If drpAluno.Text = "..." Then
                MsgBox("Selecione um Aluno!", eCategoriaMensagem.ALERTA)
                Exit Sub
            End If
            .Aluno = drpAluno.Text
            .Etapa = drpEtapa.Text
            .Lotacao = Session("Lotacao")
            .Periodo = Session("Periodo")

            .Salvar()
            LimparCampos()
            ListarAluno()
            MsgBox("Registro Salvo Com Sucesso !", eCategoriaMensagem.SUCESSO)
        End With
        objMatricula = Nothing
    End Sub

    Private Sub Excluir(ByVal Codigo As Integer)
        Dim objDocumento As New Matricula

        If objDocumento.Excluir(Codigo) > 0 Then
            MsgBox(eTipoMensagem.EXCLUIR_SUCESSO)
        Else
            MsgBox(eTipoMensagem.EXCLUIR_ERRO)
        End If

        objDocumento = Nothing

        CarregarGridMatricula()
    End Sub

#End Region

#Region "Eventos de Cadastro"
    Protected Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        'If VerificarMatricula() = True Then
        '    Exit Sub
        'End If
        'If VerificarCampos() = True Then
        '    Exit Sub
        'End If

        Salvar()
    End Sub

    Protected Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        HabilitarSecao()
    End Sub

#End Region

#Region "Funções de Listagem"

    Private Sub Informacao()
        Dim objLotacao As New Lotacao
        Dim objEtapa As New Etapa
        Dim objPeriodo As New Periodo
        lblPeriodo.Text = "<b>Periodo:</b> " + objPeriodo.ObterTabela(Session("Periodo")).Rows(0)("DE08_NM_PERIODO").ToString

        lblLotacao.Text = "<b>Escola:</b> " + objLotacao.ObterTabela(Session("Lotacao")).Rows(0)("DE04_NM_LOTACAO").ToString
        'lblEtapa.Text = "<b>Etapa:</b> " + objEtapa.ObterTabela(Session("Etapa")).Rows(0)("DE06_NM_ETAPA").ToString
    End Sub

    Private Sub Carregarcombo()
        ListarAluno()
        ListarLotacao()
        ListarModalidade()
    End Sub


    Private Sub ListarAluno()
        Dim objaluno As New Aluno()

        With drpAluno
            .DataSource = objaluno.ObterAluno()
            .DataValueField = "DE01_ID_ALUNO"
            .DataTextField = "DE01_NM_ALUNO"
            .DataBind()
            .Items.Insert(0, "...")
            .SelectedIndex = 0
        End With

        objaluno = Nothing
    End Sub

    Private Sub ListarLotacao()
        Dim objlotacao As New Etapa

        With drpEtapa
            .DataSource = objlotacao.ObterTabela()
            .DataValueField = "DE06_ID_ETAPA"
            .DataTextField = "DE06_NM_ETAPA"
            .DataBind()
            .Items.Insert(0, "...")
            .SelectedIndex = 0
        End With

        objlotacao = Nothing
    End Sub

    Private Sub ListarModalidade()
        'Dim objPeriodo As New Modalidade

        'With drpModalidade
        '    .DataSource = objPeriodo.ObterTabela()
        '    .DataValueField = "DE05_ID_MODALIDADE"
        '    .DataTextField = "DE05_NM_MODALIDADE"
        '    .DataBind()
        '    .Items.Insert(0, "...")
        '    .SelectedIndex = 0
        'End With

        'objPeriodo = Nothing
    End Sub

    Private Sub CarregarGridMatricula()
        Dim objMatricula As New Matricula

        With grdMatricula
            .DataSource = objMatricula.Pesquisar(ViewState("OrderBy"),,, Session("Lotacao"), , txtLocalizar.Text)
            .DataBind()
            lblRegistrosMatricula.Text = DirectCast(.DataSource, Data.DataTable).Rows.Count & " Registro(s)"
        End With
        grdMatricula = Nothing
    End Sub
#End Region

#Region "Eventos de Listagem"
    Protected Sub grdMatricula_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdMatricula.RowCommand
        If e.CommandName = "" Then
            Response.Redirect(Request.Url.ToString)
        ElseIf e.CommandName = "EXCLUIR" Then
            ViewState("Matricula") = grdMatricula.DataKeys(e.CommandArgument).Item(0)
            Excluir(ViewState("Matricula"))
        End If
    End Sub

    Private Sub grdMatricula_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdMatricula.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header

            Case DataControlRowType.DataRow

                Dim lnkExcluirMatricula As New LinkButton

                lnkExcluirMatricula = DirectCast(e.Row.Cells(ColunasGrid_grdMatricula.buttons).FindControl("lnkExcluirMatricula"), LinkButton)
                lnkExcluirMatricula.CommandArgument = e.Row.RowIndex

                lnkExcluirMatricula = Nothing

        End Select
    End Sub

    Protected Sub btnLocalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalizar.Click
        cadastro.Visible = False
        listagem.Visible = True
        divMatricula.Visible = True
        CarregarGridMatricula()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        HabilitarSecao(True)
        Carregarcombo()
    End Sub

#End Region
End Class