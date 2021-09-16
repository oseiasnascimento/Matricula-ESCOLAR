Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Lotacao
    Private DE04_ID_LOTACAO As Integer
    Private DE04_NM_LOTACAO As String



    Public Property Lotacao() As Integer
        Get
            Return DE04_ID_LOTACAO
        End Get
        Set(ByVal Value As Integer)
            DE04_ID_LOTACAO = Value
        End Set
    End Property

    Public Property NomeLotacao() As String
        Get
            Return DE04_NM_LOTACAO
        End Get
        Set(ByVal Value As String)
            DE04_NM_LOTACAO = Value
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

        strSQL.Append(" SELECT DE04_ID_LOTACAO,")
        strSQL.Append("        DE04_NM_LOTACAO")
        strSQL.Append("   FROM DE04_LOTACAO")
        strSQL.Append("  WHERE DE04_ID_LOTACAO = " & Lotacao)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE04_ID_LOTACAO") = ProBanco(DE04_ID_LOTACAO, eTipoValor.CHAVE)
        dr("DE04_NM_LOTACAO") = ProBanco(DE04_NM_LOTACAO, eTipoValor.TEXTO)


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

        strSQL.Append(" SELECT DE04_ID_LOTACAO,")
        strSQL.Append("        DE04_NM_LOTACAO")
        strSQL.Append("   FROM DE04_LOTACAO")
        strSQL.Append("  WHERE DE04_ID_LOTACAO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE04_ID_LOTACAO = DoBanco(dr("DE04_ID_LOTACAO"), eTipoValor.CHAVE)
            DE04_NM_LOTACAO = DoBanco(dr("DE04_NM_LOTACAO"), eTipoValor.TEXTO)

        End If

        cnn = Nothing

    End Sub

    Public Function ObterTabela(Optional ByVal Lotacao As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE04_ID_LOTACAO,")
        strSQL.Append("        DE04_NM_LOTACAO")
        strSQL.Append("   FROM DE04_LOTACAO")

        If Lotacao > 0 Then
            strSQL.Append("  WHERE DE04_ID_LOTACAO = " & Lotacao)
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

        strSQL.Append(" SELECT DE04_ID_LOTACAO,")
        strSQL.Append("        DE04_NM_LOTACAO")
        strSQL.Append("   FROM DE04_LOTACAO")
        strSQL.Append("  WHERE DE04_ID_LOTACAO IS NOT NULL")

        If Lotacao > 0 Then
            strSQL.Append("AND DE04_ID_LOTACAO = " & Lotacao)
        End If

        If NomeLotacao <> "" Then
            strSQL.Append("AND UPPER(DE04_NM_LOTACAO) LIKE '%" & NomeLotacao.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE04_ID_LOTACAO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE04_LOTACAO")
        strSQL.Append("  WHERE DE04_ID_LOTACAO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
