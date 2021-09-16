Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class LotacaoEtapa
    Private DE06_ID_LOTACAO_ETAPA As Integer
    Private DE03_ID_LOTACAO As Integer
    Private DE05_ID_ETAPA As Integer

    Public Property LotacaoEtapa() As Integer
        Get
            Return DE06_ID_LOTACAO_ETAPA
        End Get
        Set(ByVal Value As Integer)
            DE06_ID_LOTACAO_ETAPA = Value
        End Set
    End Property
    Public Property Lotacao() As Integer
        Get
            Return DE03_ID_LOTACAO
        End Get
        Set(ByVal Value As Integer)
            DE03_ID_LOTACAO = Value
        End Set
    End Property
    Public Property Etapa() As Integer
        Get
            Return DE05_ID_ETAPA
        End Get
        Set(ByVal Value As Integer)
            DE05_ID_ETAPA = Value
        End Set
    End Property

    Public Sub New(Optional ByVal Codigo As Integer = 0)
        If Codigo > 0 Then
            Obter(Codigo)
        End If
    End Sub

    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE06_ID_LOTACAO_ETAPA, ")
        strSQL.Append("        DE03_ID_LOTACAO, ")
        strSQL.Append("        DE05_ID_ETAPA ")
        strSQL.Append("   FROM ED06_LOTACAO_ETAPA")
        strSQL.Append("  WHERE DE06_ID_LOTACAO_ETAPA = " & LotacaoEtapa)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE06_ID_LOTACAO_ETAPA") = ProBanco(DE06_ID_LOTACAO_ETAPA, eTipoValor.CHAVE)
        dr("DE03_ID_LOTACAO") = ProBanco(DE03_ID_LOTACAO, eTipoValor.TEXTO)
        dr("DE05_ID_ETAPA") = ProBanco(DE05_ID_ETAPA, eTipoValor.TEXTO)


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

        strSQL.Append(" SELECT DE06_ID_LOTACAO_ETAPA, ")
        strSQL.Append("        DE03_ID_LOTACAO, ")
        strSQL.Append("        DE05_ID_ETAPA ")
        strSQL.Append("   FROM ED06_LOTACAO_ETAPA")
        strSQL.Append("  WHERE DE06_ID_LOTACAO_ETAPA = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE06_ID_LOTACAO_ETAPA = DoBanco(dr("DE06_ID_LOTACAO_ETAPA"), eTipoValor.CHAVE)
            DE03_ID_LOTACAO = DoBanco(dr("DE03_ID_LOTACAO"), eTipoValor.TEXTO)
            DE05_ID_ETAPA = DoBanco(dr("DE05_ID_ETAPA"), eTipoValor.TEXTO)

        End If

        cnn = Nothing

    End Sub

    Public Function ObterTabela() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE06_ID_LOTACAO_ETAPA, ")
        strSQL.Append("        DE03_ID_LOTACAO, ")
        strSQL.Append("        DE05_ID_ETAPA ")
        strSQL.Append("   FROM ED06_LOTACAO_ETAPA ORDER BY ED04_ID_LOTACAO ASC")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal LotacaoEtapa As Integer = 0,
                              Optional ByVal Lotacao As Integer = 0,
                              Optional ByVal Etapa As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE06_ID_LOTACAO_ETAPA, ")
        strSQL.Append("        DE03_ID_LOTACAO, ")
        strSQL.Append("        DE05_ID_ETAPA ")
        strSQL.Append("   FROM ED06_LOTACAO_ETAPA")
        strSQL.Append("  WHERE DE06_ID_LOTACAO_ETAPA IS NOT NULL")

        If LotacaoEtapa > 0 Then
            strSQL.Append("AND DE06_ID_LOTACAO_ETAPA = " & LotacaoEtapa)
        End If

        If Lotacao > 0 Then
            strSQL.Append("AND DE03_ID_LOTACAO = " & Lotacao)
        End If

        If Etapa > 0 Then
            strSQL.Append("AND DE05_ID_ETAPA = " & Etapa)
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE03_ID_LOTACAO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM ED06_LOTACAO_ETAPA")
        strSQL.Append("  WHERE DE06_ID_LOTACAO_ETAPA = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
