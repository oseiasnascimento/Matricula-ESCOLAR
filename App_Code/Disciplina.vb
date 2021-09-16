Imports Microsoft.VisualBasic
Imports System.Data

Public Class Disciplina

    Private DE11_ID_DISCIPLINA As Integer
    Private DE11_NM_DISCIPLINA As String
    Private DE11_DS_EMENTA As String

    Public Property Disciplina() As Integer
        Get
            Return DE11_ID_DISCIPLINA
        End Get
        Set(ByVal value As Integer)
            DE11_ID_DISCIPLINA = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return DE11_NM_DISCIPLINA
        End Get
        Set(ByVal value As String)
            DE11_NM_DISCIPLINA = value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return DE11_DS_EMENTA
        End Get
        Set(ByVal value As String)
            DE11_DS_EMENTA = value
        End Set
    End Property

    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE11_DISCIPLINA ")
        strSQL.Append("  WHERE DE11_ID_DISCIPLINA = " & Disciplina)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE11_ID_DISCIPLINA") = ProBanco(DE11_ID_DISCIPLINA, eTipoValor.CHAVE)
        dr("DE11_NM_DISCIPLINA") = ProBanco(DE11_NM_DISCIPLINA, eTipoValor.TEXTO)
        dr("DE11_DS_EMENTA") = ProBanco(DE11_DS_EMENTA, eTipoValor.TEXTO_LIVRE)

        cnn.SalvarDataTable(dr)
        dt.Dispose()
        dt = Nothing
        cnn = Nothing
    End Sub

    Public Sub Obter(ByVal Codigo As Integer)
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11_ID_DISCIPLINA, ")
        strSQL.Append("        DE11_NM_DISCIPLINA, ")
        strSQL.Append("        DE11_DS_EMENTA ")
        strSQL.Append("   FROM DE11_DISCIPLINA ")
        strSQL.Append("  WHERE DE11_ID_DISCIPLINA = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE11_ID_DISCIPLINA = DoBanco(dr("DE11_ID_DISCIPLINA"), eTipoValor.CHAVE)
            DE11_NM_DISCIPLINA = DoBanco(dr("DE11_NM_DISCIPLINA"), eTipoValor.TEXTO)
            DE11_DS_EMENTA = DoBanco(dr("DE11_DS_EMENTA"), eTipoValor.TEXTO_LIVRE)

        End If

        cnn = Nothing
    End Sub

    Public Function ObterTabela(Optional ByVal Disciplina As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11_ID_DISCIPLINA, ")
        strSQL.Append("        DE11_NM_DISCIPLINA, ")
        strSQL.Append("        DE11_DS_EMENTA ")
        strSQL.Append("   FROM DE11_DISCIPLINA ")

        If Disciplina > 0 Then
            strSQL.Append("  WHERE DE11_ID_DISCIPLINA = " & Disciplina)
        End If

        strSQL.Append("  ORDER BY DE11_ID_DISCIPLINA ASC  ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterDisciplina(Optional codigoTurma As Integer = 0,
                                    Optional codigoDisciplina As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11.DE11_ID_DISCIPLINA, ")
        strSQL.Append("        DE11.DE11_NM_DISCIPLINA ")
        strSQL.Append("   FROM DE11_DISCIPLINA DE11")

        If codigoTurma > 0 Then
            strSQL.Append("  WHERE ED10.ED07_ID_TURMA = ")
        End If

        If codigoDisciplina > 0 Then
            strSQL.Append("    AND DE11.DE11_ID_DISCIPLINA = ")
        End If

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Codigo As Integer = 0,
                              Optional ByVal Nome As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11_ID_DISCIPLINA, ")
        strSQL.Append("        DE11_NM_DISCIPLINA, ")
        strSQL.Append("        DE11_DS_EMENTA ")
        strSQL.Append("   FROM DE11_DISCIPLINA")
        strSQL.Append("  WHERE DE11_ID_DISCIPLINA IS NOT NULL")

        If Codigo > 0 Then
            strSQL.Append(" and  = " & Codigo)
        End If

        If Nome <> "" Then
            strSQL.Append(" and upper( ) like '%" & Nome.ToUpper & "%'")
        End If

        strSQL.Append(" Order By " & IIf(Sort = "", " ", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE11_DISCIPLINA")
        strSQL.Append("  WHERE DE11_ID_DISCIPLINA = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
