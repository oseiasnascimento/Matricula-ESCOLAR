Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Modalidade
    Private DE05_ID_MODALIDADE As Integer
    Private DE05_NM_MODALIDADE As String



    Public Property Modalidade() As Integer
        Get
            Return DE05_ID_MODALIDADE
        End Get
        Set(ByVal Value As Integer)
            DE05_ID_MODALIDADE = Value
        End Set
    End Property

    Public Property NomeModalidade() As String
        Get
            Return DE05_NM_MODALIDADE
        End Get
        Set(ByVal Value As String)
            DE05_NM_MODALIDADE = Value
        End Set
    End Property


    Public Sub New(Optional ByVal Lotacao As Integer = 0)
        If Lotacao > 0 Then
            Obter(Lotacao)
        End If
    End Sub

    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE05_ID_MODALIDADE,")
        strSQL.Append("        DE05_NM_MODALIDADE")
        strSQL.Append("   FROM DE05_MODALIDADE")
        strSQL.Append("  WHERE DE05_ID_MODALIDADE = " & Modalidade)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE05_ID_MODALIDADE") = ProBanco(DE05_ID_MODALIDADE, eTipoValor.CHAVE)
        dr("DE05_NM_MODALIDADE") = ProBanco(DE05_NM_MODALIDADE, eTipoValor.TEXTO)


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

        strSQL.Append(" SELECT DE05_ID_MODALIDADE,")
        strSQL.Append("        DE05_NM_MODALIDADE")
        strSQL.Append("   FROM DE05_MODALIDADE")
        strSQL.Append("  WHERE DE05_ID_MODALIDADE = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE05_ID_MODALIDADE = DoBanco(dr("DE05_ID_MODALIDADE"), eTipoValor.CHAVE)
            DE05_NM_MODALIDADE = DoBanco(dr("DE05_NM_MODALIDADE"), eTipoValor.TEXTO)

        End If

        cnn = Nothing

    End Sub

    Public Function ObterTabela(Optional ByVal Lotacao As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE05_ID_MODALIDADE,")
        strSQL.Append("        DE05_NM_MODALIDADE")
        strSQL.Append("   FROM DE05_MODALIDADE")

        If Lotacao > 0 Then
            strSQL.Append("  WHERE DE05_ID_MODALIDADE = " & Lotacao)
        End If


        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Lotacao As Integer = 0,
                              Optional ByVal NomeLotacao As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE05_ID_MODALIDADE,")
        strSQL.Append("        DE05_NM_MODALIDADE")
        strSQL.Append("   FROM DE05_MODALIDADE")
        strSQL.Append("  WHERE DE05_ID_MODALIDADE IS NOT NULL")

        If Lotacao > 0 Then
            strSQL.Append("AND DE05_ID_MODALIDADE = " & Lotacao)
        End If

        If NomeLotacao <> "" Then
            strSQL.Append("AND UPPER(DE05_NM_MODALIDADE) LIKE '%" & NomeLotacao.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE05_ID_MODALIDADE", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE05_MODALIDADE")
        strSQL.Append("  WHERE DE05_ID_MODALIDADE = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
