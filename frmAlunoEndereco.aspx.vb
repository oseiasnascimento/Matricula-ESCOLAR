Imports System.Data
Partial Class frmAlunoEndereco
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then


        End If

        Validacao.Outros(txtCep, True, "CEP",, Validacao.eFormato.CEP)
        Validacao.Combo(drpZonaResidencia, True, "Zona Residência")
        Validacao.Livre(txtNumero, False, "Numero")
        Validacao.Livre(txtComplemento, False, "Complemento")
        Validacao.Livre(txtBairro, True, "Bairro")
        Validacao.Livre(txtLogradouro, True, "Logradouro")
        Validacao.Finalizar(btnSalvar, , False)

    End Sub

#Region "Funções de Cadastro"

    Private Sub LimparCampos()
        ViewState("LogradouroWeb") = Nothing
        ViewState("BairroWeb") = Nothing

        txtCep.Text = ""
        txtLogradouro.Text = ""
        txtBairro.Text = ""
        txtComplemento.Text = ""

        drpEstado.ClearSelection()
        drpMunicipio.ClearSelection()
        drpZonaResidencia.ClearSelection()

        txtLogradouro.Attributes.Remove("disabled")
        txtBairro.Attributes.Remove("disabled")
        drpEstado.Attributes.Remove("disabled")
        drpMunicipio.Attributes.Remove("disabled")

    End Sub

    Private Sub CarregarCepAluno(ByVal Aluno As Integer)
        Dim objEndereco As New Endereco

        With objEndereco
            .ObterLogradouroAluno(Aluno)
            ViewState("Endereco") = .Endereco
            txtNumero.Text = .Numero
            drpZonaResidencia.SelectedValue = .ZonaResidencia
            txtComplemento.Text = .Complemento
        End With

        objEndereco = Nothing
    End Sub



    Private Sub Salvar()
        Dim objEndereco As New Endereco(ViewState("Codigo"))

        Try
            With objEndereco

                .Aluno = Session("Aluno")
                .CEP = txtCep.Text
                .logradouro = txtLogradouro.Text
                .Numero = txtNumero.Text
                .Complemento = txtComplemento.Text
                .Bairro = txtBairro.Text
                .Estado = drpEstado.Text
                .Cidade = drpMunicipio.SelectedValue
                .ZonaResidencia = drpZonaResidencia.SelectedValue

                .Salvar()

                MsgBox(eTipoMensagem.SALVAR_SUCESSO)
            End With

        Catch ex As Exception
            MsgBox("Erro ao salvar endereço!")
        End Try

        objEndereco = Nothing

    End Sub

#End Region

#Region "Eventos de Cadastro"
    Protected Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        If drpEstado.SelectedValue = "0" Then
            MsgBox("Campo Estado Requerido")
            Exit Sub
        End If
        If drpMunicipio.SelectedItem.Text = "" Then
            MsgBox("Campo Município Requerido")
            Exit Sub
        End If
        Salvar()
        'CarregarCepAluno(Session("Aluno"))
    End Sub


    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos()
    End Sub

    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Response.Redirect("frmAluno.aspx")
    End Sub

#End Region

End Class


