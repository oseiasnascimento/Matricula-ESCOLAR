Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text

Public Class TurmaDisciplina

    Private DE12_ID_TURMA_DISCIPLINA As Integer
    Private DE09_ID_TURMA As Integer
    Private DE11_ID_DISCIPLINA As Integer


    Public Property Codigo() As Integer
        Get
            Return DE12_ID_TURMA_DISCIPLINA
        End Get
        Set(ByVal Value As Integer)
            DE12_ID_TURMA_DISCIPLINA = Value
        End Set
    End Property
    Public Property Turma() As Integer
        Get
            Return DE09_ID_TURMA
        End Get
        Set(ByVal Value As Integer)
            DE09_ID_TURMA = Value
        End Set
    End Property
    Public Property Disciplina() As Integer
        Get
            Return DE11_ID_DISCIPLINA
        End Get
        Set(ByVal Value As Integer)
            DE11_ID_DISCIPLINA = Value
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

        strSQL.Append(" SELECT DE09_ID_TURMA, ")
        strSQL.Append("        DE11_ID_DISCIPLINA ")
        strSQL.Append("   FROM DE12_TURMA_DISCIPLINA")
        strSQL.Append("  WHERE DE12_ID_TURMA_DISCIPLINA = " & Codigo)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE09_ID_TURMA") = ProBanco(DE09_ID_TURMA, eTipoValor.NUMERO_INTEIRO)
        dr("DE11_ID_DISCIPLINA") = ProBanco(DE11_ID_DISCIPLINA, eTipoValor.NUMERO_INTEIRO)

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

        strSQL.Append(" SELECT DE09_ID_TURMA, ")
        strSQL.Append("        DE11_ID_DISCIPLINA ")
        strSQL.Append("   FROM DE12_TURMA_DISCIPLINA")
        strSQL.Append("  WHERE DE12_ID_TURMA_DISCIPLINA = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE12_ID_TURMA_DISCIPLINA = DoBanco(dr("DE12_ID_TURMA_DISCIPLINA"), eTipoValor.CHAVE)
            DE09_ID_TURMA = DoBanco(dr("DE09_ID_TURMA"), eTipoValor.CHAVE)
            DE11_ID_DISCIPLINA = DoBanco(dr("DE11_ID_DISCIPLINA"), eTipoValor.CHAVE)

        End If
        cnn = Nothing
    End Sub

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional Codigo As Integer = 0,
                              Optional Turma As Integer = 0,
                              Optional Disciplina As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE09_ID_TURMA, ")
        strSQL.Append("        DE11_ID_DISCIPLINA ")
        strSQL.Append("   FROM DE12_TURMA_DISCIPLINA ")
        strSQL.Append("  WHERE DE12_ID_TURMA_DISCIPLINA IS NOT NULL")

        If Codigo > 0 Then
            strSQL.Append(" AND DE12_ID_TURMA_DISCIPLINA = " & Codigo)
        End If

        If Turma > 0 Then
            strSQL.Append(" AND DE09_ID_TURMA = " & Turma)
        End If

        If Disciplina > 0 Then
            strSQL.Append(" AND DE11_ID_DISCIPLINA = " & Disciplina)
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE09.DE09_NM_DISCIPLINA", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela(Optional Turma As Integer = 0,
                                Optional Pessoa As Integer = 0,
                                Optional NaoExibirEncerradas As Boolean = False,
                                Optional DisciplinaOptativa As Boolean = False) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT ")
        strSQL.Append("   FROM ")
        strSQL.Append("  WHERE ")

        dt = cnn.AbrirDataTable(strSQL.ToString)


        cnn = Nothing

        Return dt
    End Function

    Public Function ObterTurmaDisciplina(Optional Turma As Integer = 0,
                                         Optional Disciplina As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE12_ID_TURMA_DISCIPLINA, DE11.DE11_NM_DISCIPLINA ")
        strSQL.Append("   FROM DE12_TURMA_DISCIPLINA DE12 ")
        strSQL.Append("   JOIN DE11_DISCIPLINA DE11 ON DE11.DE11_ID_DISCIPLINA = DE12.DE11_ID_DISCIPLINA")
        strSQL.Append("  WHERE DE12.DE12_ID_TURMA_DISCIPLINA IS NOT NULL")

        If Turma > 0 Then
            strSQL.Append(" And DE12.DE09_ID_TURMA = " & Turma)
        End If

        If Disciplina > 0 Then
            strSQL.Append(" And DE12.DE11_ID_DISCIPLINA = " & Disciplina)
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
        strSQL.Append("   FROM DE12_TURMA_DISCIPLINA")
        strSQL.Append("  WHERE DE12_ID_TURMA_DISCIPLINA = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)


        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class


