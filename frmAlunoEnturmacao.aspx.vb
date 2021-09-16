Imports System.Data

Enum ColunasGrid_grdEnturmado As Integer
    Selecionar
    ID_DOCUMENTO
    buttons
End Enum

Enum ColunasGrid_grdMatriculado As Integer
    Selecionar
    ID_DOCUMENTO
    buttons
End Enum

Partial Class frmEnturmacao
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

            If Session("Lotacao") Is Nothing Then
                Response.Redirect("frmSelecaoEscola.aspx")
            End If

            If Session("Periodo") Is Nothing Then
                Response.Redirect("frmSelecaoEscola.aspx")
            End If

            Informacao()
            HabilitarSecao(True)
            Listar()
        End If

    End Sub

#Region "Funções de Cadastro"
    Private Function VerificarMatricula() As Boolean
        Dim objMatricula As New Matricula
        Dim Existe As Boolean = False

        With objMatricula.Pesquisar(,,)
            If .Rows.Count > 0 Then
                MsgBox("Aluno já Matriculado", eCategoriaMensagem.ALERTA)
                Existe = True
            End If
        End With

        objMatricula = Nothing
        Return Existe
    End Function

    Private Sub LimparCampos()
        ViewState("CodigoMatricula") = Nothing
    End Sub

    Private Sub HabilitarSecao(Optional hab As Boolean = False)
        cadastro.Visible = hab
        listagem.Visible = Not hab
    End Sub

    Private Sub Listar()
        ListarEtapa()
    End Sub

    Private Sub Salvar()
        Dim objMatricula As New Enturmacao()

        With objMatricula
            .Matricula = ViewState("Matricula")
            .Turma = drpTurma.Text
            .Situacao = 1
            .DataSituacao = Date.Now
            .Salvar()

        End With
        objMatricula = Nothing
    End Sub

    Private Sub Excluir(ByVal Codigo As Integer)
        Dim objEnturmacao As New Enturmacao

        If objEnturmacao.Excluir(Codigo) > 0 Then
            MsgBox(eTipoMensagem.EXCLUIR_SUCESSO)
        Else
            MsgBox(eTipoMensagem.EXCLUIR_ERRO)
        End If

        objEnturmacao = Nothing
    End Sub


#End Region

#Region "Eventos de Cadastro"

#End Region

#Region "Funções de Listagem"

    Private Sub Informacao()
        Dim objLotacao As New Lotacao
        Dim objEtapa As New Etapa
        Dim objPeriodo As New Periodo

        lblLotacao.Text = "<b>Escola:</b> " + objLotacao.ObterTabela(Session("Lotacao")).Rows(0)("DE04_NM_LOTACAO").ToString
        lblEtapa.Text = "<b>Etapa:</b> " + objEtapa.ObterTabela(ViewState("Etapa")).Rows(0)("DE06_NM_ETAPA").ToString
        lblPeriodo.Text = "<b>Periodo:</b> " + objPeriodo.ObterTabela(Session("Periodo")).Rows(0)("DE08_NM_PERIODO").ToString
    End Sub

    Private Sub ListarEtapa()
        Dim objEtapa As New Etapa
        With drpEtapa
            .DataSource = objEtapa.ObterTabela()
            .DataValueField = "DE06_ID_ETAPA"
            .DataTextField = "DE06_NM_ETAPA"
            .DataBind()
            .Items.Insert(0, "...")
            .SelectedIndex = 0
        End With

        objEtapa = Nothing
    End Sub

    Private Sub ListarTurma()
        Dim objTurma As New Turma
        With drpTurma
            .DataSource = objTurma.ObterTurma(ViewState("Etapa"), Session("Lotacao"))
            .DataValueField = "DE10_ID_TURMA"
            .DataTextField = "DE10_NU_TURMA"
            .DataBind()
            .Items.Insert(0, "...")
            .SelectedIndex = 0
        End With

        objTurma = Nothing
    End Sub

    Private Sub CarregarGridMatricula()
        Dim objMatricula As New Matricula(ViewState("Matricula"))

        'listagem.Visible = True
        grdMatriculado.DataSource = objMatricula.ObterGridMatriculado(Session("Lotacao"), ViewState("Etapa"), Session("Periodo"), ViewState("Turma"), txtNome.Text)
        grdMatriculado.DataBind()

        lblMatriculado.Text = DirectCast(grdMatriculado.DataSource, Data.DataTable).Rows.Count & " Registro(s)"

        objMatricula = Nothing
    End Sub

    Private Sub CarregarGridEnturmado()
        Dim objEnturmacao As New Enturmacao

        grdEnturmado.DataSource = objEnturmacao.ObterGridEnturmado(Session("Lotacao"), ViewState("Etapa"), Session("Periodo"), ViewState("Turma"))
        grdEnturmado.DataBind()

        lblEnturmado.Text = DirectCast(grdEnturmado.DataSource, Data.DataTable).Rows.Count & " Registro(s)"
        grdMatriculado = Nothing
    End Sub
#End Region

#Region "Eventos de Listagem"
    Protected Sub grdMatriculado_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdMatriculado.RowCommand
        If e.CommandName = "" Then
            Response.Redirect(Request.Url.ToString)
        ElseIf e.CommandName = "ENTURMAR" Then
            ViewState("Matricula") = grdMatriculado.DataKeys(e.CommandArgument).Item(0)
            Salvar()
            CarregarGridMatricula()
            CarregarGridEnturmado()
        End If
    End Sub

    Protected Sub grdEnturmado_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdEnturmado.RowCommand
        If e.CommandName = "" Then
            Response.Redirect(Request.Url.ToString)
        ElseIf e.CommandName = "REMOVER" Then
            ViewState("Enturmacao") = grdEnturmado.DataKeys(e.CommandArgument).Item(0)
            Excluir(ViewState("Enturmacao"))
            CarregarGridEnturmado()
            CarregarGridMatricula()
        End If
    End Sub

    Private Sub grdMatriculado_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdMatriculado.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header

            Case DataControlRowType.DataRow

                Dim lnkEnturmarMatricula As New LinkButton

                lnkEnturmarMatricula = DirectCast(e.Row.Cells(ColunasGrid_grdMatriculado.buttons).FindControl("lnkEnturmarMatricula"), LinkButton)
                lnkEnturmarMatricula.CommandArgument = e.Row.RowIndex

                lnkEnturmarMatricula = Nothing


        End Select
    End Sub

    Private Sub grdEnturmado_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdEnturmado.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header

            Case DataControlRowType.DataRow

                Dim lnkRemoveEnturmacao As New LinkButton

                lnkRemoveEnturmacao = DirectCast(e.Row.Cells(ColunasGrid_grdEnturmado.buttons).FindControl("lnkRemoveEnturmacao"), LinkButton)
                lnkRemoveEnturmacao.CommandArgument = e.Row.RowIndex

                lnkRemoveEnturmacao = Nothing

        End Select
    End Sub

    Private Sub grdEnturmado_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdEnturmado.PageIndexChanging
        grdEnturmado.PageIndex = e.NewPageIndex
        CarregarGridEnturmado()
    End Sub

    Private Sub grdEnturmado_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdEnturmado.Sorting
        ViewState("OrderByDirection") = IIf(ViewState("OrderByDirection") = "asc", "desc", "asc")
        ViewState("OrderBy") = e.SortExpression & " " & ViewState("OrderByDirection")
        CarregarGridMatricula()
    End Sub

    Protected Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        Response.Redirect("frmSelecaoEscola.aspx")

    End Sub

    Protected Sub btnVoltarTurma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltarTurma.Click
        HabilitarSecao(True)

    End Sub

    Protected Sub btnLocalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalizar.Click
        If drpTurma.Text = "..." Then
            MsgBox("Selecione uma Turma!", eCategoriaMensagem.ALERTA)
            Exit Sub
        End If
        ViewState("Turma") = drpTurma.Text
        HabilitarSecao()
        CarregarGridMatricula()
        CarregarGridEnturmado()
    End Sub

    Protected Sub OnSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles drpEtapa.SelectedIndexChanged
        If drpEtapa.Text = "..." Then
            drpTurma.ClearSelection()
            drpEtapa.ClearSelection()
            Exit Sub
        End If
        ListarTurma()
        Informacao()
        ViewState("Etapa") = drpEtapa.Text

    End Sub

#End Region
End Class