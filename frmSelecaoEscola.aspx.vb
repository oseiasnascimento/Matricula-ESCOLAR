Partial Class frmSelecionar
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Listagem()
        End If
    End Sub

#Region "Funções de Redirecionamento"

    Private Sub Ok()

        If drpLotacao.SelectedIndex = 0 Then
            MsgBox("Selecione uma Escola!", eCategoriaMensagem.ALERTA)
            Exit Sub
        ElseIf drpPeriodo.SelectedIndex = 0 Then
            MsgBox("Selecione uma Periodo!", eCategoriaMensagem.ALERTA)
            Exit Sub
        End If

        Session("Lotacao") = drpLotacao.Text
        Session("Periodo") = drpPeriodo.Text
        Response.Redirect("frmAlunoMatricula.aspx")
    End Sub

#End Region

#Region "Eventos de Redirecionamento"
    Protected Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Ok()
    End Sub

    Protected Sub btnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click
        Sair()
    End Sub
#End Region

#Region "Funções de Listagem"

    Private Sub Sair()
        Session("Lotacao") = Nothing
        Session("Etapa") = Nothing
        Session("Periodo") = Nothing
        Response.Redirect("Default.aspx")
    End Sub

    Private Sub Listagem()
        ListarLotacao()
        ListarPeriodo()
    End Sub

    Private Sub ListarLotacao()
        Dim objlotacao As New Lotacao

        With drpLotacao
            .DataSource = objlotacao.ObterTabela()
            .DataValueField = "DE04_ID_LOTACAO"
            .DataTextField = "DE04_NM_LOTACAO"
            .DataBind()
            .Items.Insert(0, "...")
            .SelectedIndex = 0
        End With

        objlotacao = Nothing
    End Sub

    Private Sub ListarPeriodo()
        Dim objPeriodo As New Periodo

        With drpPeriodo
            .DataSource = objPeriodo.ObterTabela()
            .DataValueField = "DE08_ID_PERIODO"
            .DataTextField = "DE08_NM_PERIODO"
            .DataBind()
            .Items.Insert(0, "...")
            .SelectedIndex = 0
        End With

        objPeriodo = Nothing
    End Sub

#End Region

#Region "Eventos de Listagem"

#End Region
End Class
