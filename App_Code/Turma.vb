Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Turma
    Private DE10_ID_TURMA As Integer
    Private DE04_ID_LOTACAO As Integer
    Private DE06_ID_ETAPA As Integer
    Private DE08_ID_PERIODO As Integer
    Private DE10_NU_TURMA As String

    Public Property Turma() As Integer
        Get
            Return DE10_ID_TURMA
        End Get
        Set(ByVal Value As Integer)
            DE10_ID_TURMA = Value
        End Set
    End Property

    Public Property Lotacao() As Integer
        Get
            Return DE04_ID_LOTACAO
        End Get
        Set(ByVal Value As Integer)
            DE04_ID_LOTACAO = Value
        End Set
    End Property

    Public Property Etapa() As Integer
        Get
            Return DE06_ID_ETAPA
        End Get
        Set(ByVal Value As Integer)
            DE06_ID_ETAPA = Value
        End Set
    End Property
    Public Property Periodo() As Integer
        Get
            Return DE08_ID_PERIODO
        End Get
        Set(ByVal value As Integer)
            DE08_ID_PERIODO = value
        End Set
    End Property

    Public Property NumeroTurma() As String
        Get
            Return DE10_NU_TURMA
        End Get
        Set(ByVal value As String)
            DE10_NU_TURMA = value
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

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE10_TURMA")
        strSQL.Append("  WHERE DE10_ID_TURMA = " & Turma)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE10_ID_TURMA") = ProBanco(DE10_ID_TURMA, eTipoValor.CHAVE)
        dr("DE04_ID_LOTACAO") = ProBanco(DE04_ID_LOTACAO, eTipoValor.NUMERO_INTEIRO)
        dr("DE06_ID_ETAPA") = ProBanco(DE06_ID_ETAPA, eTipoValor.NUMERO_INTEIRO)
        dr("DE08_ID_PERIODO") = ProBanco(DE08_ID_PERIODO, eTipoValor.NUMERO_INTEIRO)
        dr("DE10_NU_TURMA") = ProBanco(DE10_NU_TURMA, eTipoValor.TEXTO)

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

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE10_TURMA")
        strSQL.Append("  WHERE DE10_ID_TURMA = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE10_ID_TURMA = DoBanco(dr("DE10_ID_TURMA"), eTipoValor.CHAVE)
            DE04_ID_LOTACAO = DoBanco(dr("DE04_ID_LOTACAO"), eTipoValor.NUMERO_INTEIRO)
            DE06_ID_ETAPA = DoBanco(dr("DE06_ID_ETAPA"), eTipoValor.NUMERO_INTEIRO)
            DE08_ID_PERIODO = DoBanco(dr("DE08_ID_PERIODO"), eTipoValor.NUMERO_INTEIRO)
            DE10_NU_TURMA = DoBanco(dr("DE10_NU_TURMA"), eTipoValor.TEXTO)

        End If

        cnn = Nothing
    End Sub

    Public Function ObterTabela(Optional ByVal Turma As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE10_ID_TURMA, ")
        strSQL.Append("        DE04_ID_LOTACAO, ")
        strSQL.Append("        DE06_ID_ETAPA, ")
        strSQL.Append("        DE08_ID_PERIODO, ")
        strSQL.Append("        DE10_NU_TURMA ")
        strSQL.Append("   FROM DE10_TURMA")

        If Turma > 0 Then
            strSQL.Append(" WHERE DE10_ID_TURMA = " & Turma)
        End If

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Turma As Integer = 0,
                              Optional ByVal Lotacao As Integer = 0,
                              Optional ByVal Etapa As Integer = 0,
                              Optional ByVal Periodo As Integer = 0,
                              Optional ByVal Nome As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE10.DE10_ID_TURMA, DE10.DE10_NU_TURMA, DE08.DE08_NM_PERIODO, DE06.DE06_NM_ETAPA, DE04.DE04_NM_LOTACAO ")
        strSQL.Append("   FROM DE10_TURMA   DE10")
        strSQL.Append("   JOIN DE04_LOTACAO DE04 ON DE04.DE04_ID_LOTACAO = DE10.DE04_ID_LOTACAO")
        strSQL.Append("   JOIN DE06_ETAPA   DE06 ON DE06.DE06_ID_ETAPA   = DE10.DE06_ID_ETAPA")
        strSQL.Append("   JOIN DE08_PERIODO DE08 ON DE08.DE08_ID_PERIODO = DE10.DE08_ID_PERIODO")
        strSQL.Append("  where DE10_ID_TURMA IS NOT NULL")

        If Turma > 0 Then
            strSQL.Append(" AND DE10.DE10_ID_TURMA = " & Turma)
        End If

        If Lotacao <> 0 Then
            strSQL.Append(" AND DE10.DE04_ID_LOTACAO = " & Lotacao)
        End If

        If Etapa <> 0 Then
            strSQL.Append(" AND DE10.DE06_ID_ETAPA = " & Etapa)
        End If

        If Periodo <> 0 Then
            strSQL.Append(" AND DE10.DE08_ID_PERIODO = " & Periodo)
        End If

        If Nome <> "" Then
            strSQL.Append(" AND UPPER(DE10.DE10_NU_TURMA) LIKE '%" & Nome.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE10_ID_TURMA", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTurma(Optional ByVal Etapa As Integer = 0,
                               Optional ByVal Lotacao As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE10_ID_TURMA, DE10_NU_TURMA")
        strSQL.Append("   FROM DE10_TURMA DE10 ")
        strSQL.Append("   JOIN DE08_PERIODO DE08 ON DE08.DE08_ID_PERIODO = DE10.DE08_ID_PERIODO ")
        strSQL.Append("   JOIN DE04_LOTACAO DE04 ON DE04.DE04_ID_LOTACAO = DE10.DE04_ID_LOTACAO ")
        strSQL.Append("  WHERE DE10.DE06_ID_ETAPA   = " & Etapa)
        strSQL.Append("    AND DE04.DE04_ID_LOTACAO = " & Lotacao)
        strSQL.Append("  ORDER BY DE10_ID_TURMA ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterTabelaNotaTurma(Optional ByVal Avaliacao As Integer = 0,
                                         Optional ByVal Turma As Integer = 0,
                                         Optional ByVal Disciplina As Integer = 0,
                                         Optional ByVal Etapa As Integer = 0,
                                         Optional ByVal Nome As String = "") As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT ED01.ED01_NM_ALUNO, ")
        strSQL.Append("        ED14.ED13_VL_NOTA   ")
        strSQL.Append("   FROM ED14_NOTA       ED14   ")
        strSQL.Append("   JOIN ED01_ALUNO      ED01 ON ED01.ED01_ID_ALUNO      = ED14.ED01_ID_ALUNO")
        strSQL.Append("   JOIN ED13_AVALIACAO  ED13 ON ED13.ED13_ID_AVALIACAO  = ED14.ED13_ID_AVALIACAO")
        strSQL.Append("   JOIN DE10_TURMA	   DE10 ON DE10.DE10_ID_TURMA      = ED13.DE10_ID_TURMA")
        strSQL.Append("   JOIN ED09_DISCIPLINA ED09 ON ED09.ED09_ID_DISCIPLINA = ED13.ED09_ID_DISCIPLINA")
        strSQL.Append("   JOIN DE06_ETAPA      DE06 ON DE06.DE06_ID_ETAPA      = DE10.DE06_ID_ETAPA")
        strSQL.Append("  WHERE ED13.ED13_ID_AVALIACAO IS NOT NULL")

        If Avaliacao > 0 Then
            strSQL.Append("    AND ED13.ED13_ID_AVALIACAO  = " & Avaliacao)
        End If

        If Turma > 0 Then
            strSQL.Append("    AND DE10.DE10_ID_TURMA      = " & Turma)
        End If

        If Disciplina > 0 Then
            strSQL.Append("    AND ED09.ED09_ID_DISCIPLINA = " & Disciplina)
        End If

        If Etapa > 0 Then
            strSQL.Append("    AND DE06.DE06_ID_ETAPA      = " & Etapa)
        End If

        If Nome <> "" Then
            strSQL.Append("    AND UPPER(ED01.ED01_NM_ALUNO)      LIKE '% " & Nome.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY ED01.ED01_NM_ALUNO ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE10_TURMA")
        strSQL.Append("  WHERE DE10_ID_TURMA = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
