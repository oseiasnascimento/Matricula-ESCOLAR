Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Periodo

    Private DE08_ID_PERIODO As Integer
    Private DE08_NM_PERIODO As String

    Public Property Periodo() As Integer
        Get
            Return DE08_ID_PERIODO
        End Get
        Set(ByVal Value As Integer)
            DE08_ID_PERIODO = Value
        End Set
    End Property

    Public Property NomePeriodo() As String
        Get
            Return DE08_NM_PERIODO
        End Get
        Set(ByVal value As String)
            DE08_NM_PERIODO = value
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

        strSQL.Append(" SELECT DE08_ID_PERIODO, ")
        strSQL.Append("        DE08_NM_PERIODO ")
        strSQL.Append("   FROM DE08_PERIODO")
        strSQL.Append("  WHERE DE08_ID_PERIODO = " & Periodo)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE08_ID_PERIODO") = ProBanco(DE08_ID_PERIODO, eTipoValor.CHAVE)
        dr("DE08_NM_PERIODO") = ProBanco(DE08_NM_PERIODO, eTipoValor.TEXTO)

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

        strSQL.Append(" SELECT DE08_ID_PERIODO, ")
        strSQL.Append("        DE08_NM_PERIODO ")
        strSQL.Append("   FROM DE08_PERIODO")
        strSQL.Append("  WHERE DE08_ID_PERIODO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE08_ID_PERIODO = DoBanco(dr("DE08_ID_PERIODO"), eTipoValor.CHAVE)
            DE08_NM_PERIODO = DoBanco(dr("DE08_NM_PERIODO"), eTipoValor.TEXTO)

        End If

        cnn = Nothing
    End Sub

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal CodigoPeriodo As Integer = 0,
                              Optional ByVal NomePeriodo As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE08_ID_PERIODO, ")
        strSQL.Append("        DE08_NM_PERIODO ")
        strSQL.Append("   FROM DE08_PERIODO")
        strSQL.Append("  WHERE DE08_ID_PERIODO is not null")

        If CodigoPeriodo > 0 Then
            strSQL.Append(" AND DE08_ID_PERIODO = " & CodigoPeriodo)
        End If

        If NomePeriodo <> "" Then
            strSQL.Append(" AND upper(DE08_NM_PERIODO) like '%" & NomePeriodo.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE08_NM_PERIODO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela(Optional ByVal Periodo As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE08_ID_PERIODO, ")
        strSQL.Append("        DE08_NM_PERIODO ")
        strSQL.Append("   FROM DE08_PERIODO")
        If Periodo > 0 Then
            strSQL.Append("  WHERE DE08_ID_PERIODO = " & Periodo)
        End If

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE08_PERIODO")
        strSQL.Append("  WHERE DE08_ID_PERIODO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
