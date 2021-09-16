Enum ColunasGrid_grdTurma As Integer
    Selecionar
    ID_TURMA
    buttons
End Enum

Partial Class frmTurma
	Inherits System.Web.UI.Page

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            If Session("Lotacao") Is Nothing Then
                Response.Redirect("frmSelecaoEscola.aspx")
            End If

            If Session("Periodo") Is Nothing Then
                Response.Redirect("frmSelecaoEscola.aspx")
            End If

            HabilitarSecao()
            Informacao()
        End If
    End Sub

#Region "Função de Cadastro"
    Private Sub HabilitarSecao(Optional hab As Boolean = False)
        cadastro.Visible = hab
        listagem.Visible = Not hab
    End Sub

    Private Sub LimparCampos()
        txtNumero.Text = ""
        drpEtapa.ClearSelection()

    End Sub

    Private Sub Salvar()
        Dim objTurma As New Turma()

        With objTurma
            If drpEtapa.Text = "..." Then
                MsgBox("Selecione uma Etapa!", eCategoriaMensagem.ALERTA)
                Exit Sub
            End If
            .NumeroTurma = txtNumero.Text
            .Etapa = drpEtapa.Text
            .Lotacao = Session("Lotacao")
            .Periodo = Session("Periodo")

            .Salvar()
            LimparCampos()
            MsgBox("Registro Salvo Com Sucesso !", eCategoriaMensagem.SUCESSO)
        End With
        objTurma = Nothing
    End Sub

    Private Sub Excluir(ByVal Codigo As Integer)
        Dim objTurma As New Turma

        If objTurma.Excluir(Codigo) > 0 Then
            MsgBox(eTipoMensagem.EXCLUIR_SUCESSO)
        Else
            MsgBox(eTipoMensagem.EXCLUIR_ERRO)
        End If
        objTurma = Nothing
        CarregarGridTurma()
    End Sub

#End Region

#Region "Eventos de Cadastro"

    Protected Sub grdTurma_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdTurma.RowCommand
        If e.CommandName = "" Then
            Response.Redirect(Request.Url.ToString)
        ElseIf e.CommandName = "EXCLUIR" Then
            ViewState("Turma") = grdTurma.DataKeys(e.CommandArgument).Item(0)
            Excluir(ViewState("Turma"))
        End If
    End Sub

    Private Sub grdTurma_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdTurma.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.Header

            Case DataControlRowType.DataRow

                Dim lnkExcluirTurma As New LinkButton

                lnkExcluirTurma = DirectCast(e.Row.Cells(ColunasGrid_grdTurma.buttons).FindControl("lnkExcluirTurma"), LinkButton)
                lnkExcluirTurma.CommandArgument = e.Row.RowIndex

                lnkExcluirTurma = Nothing

        End Select
    End Sub

    Protected Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        HabilitarSecao()
    End Sub

#End Region


#Region "Função de Listagem"

    Private Sub Informacao()
        Dim objLotacao As New Lotacao
        Dim objPeriodo As New Periodo

        lblLotacao.Text = "<b>Escola:</b> " + objLotacao.ObterTabela(Session("Lotacao")).Rows(0)("DE04_NM_LOTACAO").ToString
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

    Private Sub CarregarGridTurma()
        Dim objTurma As New Turma
        With grdTurma
            .DataSource = objTurma.Pesquisar(ViewState("OrderBy"),, Session("Lotacao"),, Session("Periodo"), txtLocalizar.Text)
            .DataBind()
            lblRegistros.Text = DirectCast(.DataSource, Data.DataTable).Rows.Count & " Registro(s)"
        End With
        objTurma = Nothing
    End Sub
#End Region

#Region "Eventos de Listagem"

    Protected Sub btnLocalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalizar.Click
        CarregarGridTurma()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        HabilitarSecao(True)
        ListarEtapa()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Salvar()
    End Sub

    Private Sub grdTurma_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTurma.PageIndexChanging

    End Sub

    Private Sub grdTurma_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTurma.Sorting
        ViewState("OrderByDirection") = IIf(ViewState("OrderByDirection") = "asc", "desc", "asc")
        ViewState("OrderBy") = e.SortExpression & " " & ViewState("OrderByDirection")
        CarregarGridTurma()
    End Sub


#End Region

End Class


