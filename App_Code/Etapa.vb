Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Etapa
    Private DE06_ID_ETAPA As Integer
    Private DE05_ID_MODALIDADE As Integer
    Private DE06_NM_ETAPA As Integer

    Public Property Etapa As Integer
        Get
            Return DE06_ID_ETAPA
        End Get
        Set(ByVal Value As Integer)
            DE06_ID_ETAPA = Value
        End Set
    End Property

    Public Property Nome As String
        Get
            Return DE06_NM_ETAPA
        End Get
        Set(ByVal Value As String)
            DE06_NM_ETAPA = Value
        End Set
    End Property

    Public Property Modalidade As Integer
        Get
            Return DE05_ID_MODALIDADE
        End Get
        Set(value As Integer)
            DE05_ID_MODALIDADE = value
        End Set
    End Property

    Public Sub New(Optional ByVal Etapa As Integer = 0)
        If Etapa > 0 Then
            Obter(Etapa)
        End If
    End Sub

    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE06_ID_ETAPA,")
        strSQL.Append("        DE06_NM_ETAPA")
        strSQL.Append("   FROM DE06_ETAPA")
        strSQL.Append("  WHERE DE06_ID_ETAPA = " & Etapa)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE06_ID_ETAPA") = ProBanco(DE06_ID_ETAPA, eTipoValor.CHAVE)
        dr("DE05_ID_MODALIDADE") = ProBanco(DE05_ID_MODALIDADE, eTipoValor.CHAVE)
        dr("DE06_NM_ETAPA") = ProBanco(DE06_NM_ETAPA, eTipoValor.TEXTO)


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

        strSQL.Append(" SELECT DE06_ID_ETAPA,")
        strSQL.Append("        DE06_NM_ETAPA")
        strSQL.Append("   FROM DE06_ETAPA")
        strSQL.Append("  WHERE DE06_ID_ETAPA = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE06_ID_ETAPA = DoBanco(dr("DE06_ID_ETAPA"), eTipoValor.CHAVE)
            DE05_ID_MODALIDADE = DoBanco(dr("DE05_ID_MODALIDADE"), eTipoValor.CHAVE)
            DE06_NM_ETAPA = DoBanco(dr("DE06_NM_ETAPA"), eTipoValor.TEXTO)

        End If

        cnn = Nothing

    End Sub

    Public Function ObterTabela(Optional ByVal Etapa As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE06_ID_ETAPA,")
        strSQL.Append("        DE06_NM_ETAPA")
        strSQL.Append("   FROM DE06_ETAPA")
        If Etapa > 0 Then
            strSQL.Append("  WHERE DE06_ID_ETAPA = " & Etapa)
        End If

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Etapa As Integer = 0,
                              Optional ByVal Modalidade As Integer = 0,
                              Optional ByVal Nome As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE06_ID_ETAPA,")
        strSQL.Append("        DE06_NM_ETAPA")
        strSQL.Append("   FROM DE06_ETAPA")
        strSQL.Append("  WHERE DE06_ID_ETAPA IS NOT NULL")

        If Etapa > 0 Then
            strSQL.Append("AND DE06_ID_ETAPA = " & Etapa)
        End If

        If Modalidade > 0 Then
            strSQL.Append("AND DE05_ID_MODALIDADE = " & Modalidade)
        End If

        If Nome <> "" Then
            strSQL.Append("AND UPPER(DE06_NM_ETAPA) LIKE '%" & Nome.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "ED02_ID_LOTACAO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE06_ETAPA")
        strSQL.Append("  WHERE DE06_ID_ETAPA = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
