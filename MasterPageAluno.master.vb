
Partial Class MasterPageAluno
    Inherits System.Web.UI.MasterPage
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub CarregarAluno()
        Dim objAluno As New Aluno(Session("CodigoAluno"))
        objAluno = Nothing
    End Sub


#Region "Funções de Listagem"

    Private Sub CarregarGrid()
        Dim objAluno As New Aluno

        'grdAluno.DataSource = objAluno.Pesquisar()
        'grdAluno.DataBind()

        'objAluno = Nothing

        'lblRegistros.Text = DirectCast(grdAluno.DataSource, Data.DataTable).Rows.Count & " registro(s)"
    End Sub

#End Region

#Region "Eventos de Listagem"

    'Protected Sub btnCadastrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCadastrar.Click
    '    Cadastro.Visible = True
    '    Listagem.Visible = False
    'End Sub

    'Protected Sub btnLocalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLocalizar.Click
    '    CarregarGrid()
    'End Sub

    'Protected Sub grdAluno_RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAluno.RowCommand
    'If e.CommandName = "" Then
    '    Session("CodigoAluno") = grdAluno.DataKeys(e.CommandArgument).Item(0)
    '    Response.Redirect(Request.Url.ToString)
    'ElseIf e.CommandName = "TURMA" Then
    '    Session("CodigoAluno") = grdAluno.DataKeys(e.CommandArgument).Item(0)
    '    Response.Redirect("frmAlunoTurmas.aspx")
    'End If

    'End Sub

    'Private Sub grdAluno_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdAluno.PageIndexChanging
    '    grdAluno.PageIndex = e.NewPageIndex
    '    CarregarGrid()
    'End Sub

    'Private Sub grdAluno_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAluno.Sorting
    '    ViewState("OrderByDirection") = IIf(ViewState("OrderByDirection") = "asc", "desc", "asc")
    '    ViewState("OrderBy") = e.SortExpression & " " & ViewState("OrderByDirection")
    '    CarregarGrid()
    'End Sub

#End Region


End Class

