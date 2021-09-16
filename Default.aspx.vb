
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If

        Validacao.Livre(txtCPF, True, "CPF")
        Validacao.Finalizar(btnEntrar)

    End Sub

    Protected Sub btnEntrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntrar.Click
        Dim objUsuario As New Usuario

        With objUsuario.Pesquisar(,,, txtSenha.Text,, txtCPF.Text)
            If .Rows.Count > 0 Then
                Session("CodigoUsuario") = .Rows(0)("ED18_ID_USUARIO")
                Response.Redirect("frmSelecionar.aspx")
            Else
                MsgBox("Usuario não Encontrado!")
            End If
        End With

        objUsuario = Nothing
    End Sub


End Class
