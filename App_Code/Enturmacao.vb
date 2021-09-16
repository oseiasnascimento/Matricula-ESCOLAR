Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Enturmacao
    Private DE11_ID_ENTURMACAO As Integer
    Private DE09_ID_MATRICULA As Integer
    Private DE10_ID_TURMA As Integer
    Private DE11_ST_ENTURMACAO As Integer
    Private DE11_DH_ST_ENTURMACAO As String

    Public Property Enturmacao() As Integer
        Get
            Return DE11_ID_ENTURMACAO
        End Get
        Set(ByVal Value As Integer)
            DE11_ID_ENTURMACAO = Value
        End Set
    End Property

    Public Property Matricula() As Integer
        Get
            Return DE09_ID_MATRICULA
        End Get
        Set(ByVal Value As Integer)
            DE09_ID_MATRICULA = Value
        End Set
    End Property

    Public Property Turma() As Integer
        Get
            Return DE10_ID_TURMA
        End Get
        Set(ByVal Value As Integer)
            DE10_ID_TURMA = Value
        End Set
    End Property


    Public Property Situacao() As Integer
        Get
            Return DE11_ST_ENTURMACAO
        End Get
        Set(ByVal value As Integer)
            DE11_ST_ENTURMACAO = value
        End Set
    End Property


    Public Property DataSituacao() As String
        Get
            Return DE11_DH_ST_ENTURMACAO
        End Get
        Set(ByVal value As String)
            DE11_DH_ST_ENTURMACAO = value
        End Set
    End Property

    Public Sub New(Optional ByVal CodigoMatricula As Integer = 0)
        If CodigoMatricula > 0 Then
            Obter(CodigoMatricula)
        End If
    End Sub

    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE11_ENTURMACAO")
        strSQL.Append("  WHERE DE11_ID_ENTURMACAO = " & Enturmacao)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE11_ID_ENTURMACAO") = ProBanco(DE11_ID_ENTURMACAO, eTipoValor.CHAVE)
        dr("DE09_ID_MATRICULA") = ProBanco(DE09_ID_MATRICULA, eTipoValor.NUMERO_INTEIRO)
        dr("DE10_ID_TURMA") = ProBanco(DE10_ID_TURMA, eTipoValor.NUMERO_INTEIRO)
        dr("DE11_ST_ENTURMACAO") = ProBanco(DE11_ST_ENTURMACAO, eTipoValor.NUMERO_INTEIRO)
        dr("DE11_DH_ST_ENTURMACAO") = ProBanco(DE11_DH_ST_ENTURMACAO, eTipoValor.DATA_COMPLETA)

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
        strSQL.Append("   FROM DE10_ENTURMACAO")
        strSQL.Append("  WHERE DE11_ID_ENTURMACAO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE10_ID_TURMA = DoBanco(dr("DE10_ID_TURMA"), eTipoValor.CHAVE)
            DE09_ID_MATRICULA = DoBanco(dr("DE09_ID_MATRICULA"), eTipoValor.NUMERO_INTEIRO)
            DE10_ID_TURMA = DoBanco(dr("DE10_ID_TURMA"), eTipoValor.NUMERO_INTEIRO)
            DE11_ST_ENTURMACAO = DoBanco(dr("DE11_ST_ENTURMACAO"), eTipoValor.NUMERO_INTEIRO)
            DE11_DH_ST_ENTURMACAO = DoBanco(dr("DE11_DH_ST_ENTURMACAO"), eTipoValor.DATA_COMPLETA)

        End If

        cnn = Nothing
    End Sub

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Enturmacao As Integer = 0,
                              Optional ByVal Matricula As Integer = 0,
                              Optional ByVal Turma As Integer = 0,
                              Optional ByVal Situacao As Integer = 0,
                              Optional ByVal DataSituacao As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11_ID_ENTURMACAO, ")
        strSQL.Append("        DE09_ID_MATRICULA, ")
        strSQL.Append("        DE10_ID_TURMA, ")
        strSQL.Append("        DE11_ST_ENTURMACAO, ")
        strSQL.Append("        DE11_DH_ST_ENTURMACAO ")
        strSQL.Append("   FROM DE10_ENTURMACAO")
        strSQL.Append("  WHERE DE11_ID_ENTURMACAO is not null")

        If Enturmacao > 0 Then
            strSQL.Append(" AND DE11_ID_ENTURMACAO = " & Enturmacao)
        End If

        If Matricula > 0 Then
            strSQL.Append(" AND ED05_ID_TURMA = " & Matricula)
        End If

        If Turma > 0 Then
            strSQL.Append(" AND DE10_ID_TURMA =" & Turma)
        End If

        If Situacao > 0 Then
            strSQL.Append(" AND DE11_ST_ENTURMACAO =" & Situacao)
        End If

        If DataSituacao <> "" Then
            strSQL.Append(" AND DE11_DH_ST_ENTURMACAO =" & DataSituacao)
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE10_ID_TURMA", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11_ID_ENTURMACAO, ")
        strSQL.Append("        DE09_ID_MATRICULA, ")
        strSQL.Append("        DE10_ID_TURMA, ")
        strSQL.Append("        DE11_ST_ENTURMACAO, ")
        strSQL.Append("        DE11_DH_ST_ENTURMACAO ")
        strSQL.Append("   FROM DE11_ENTURMACAO ORDER BY DE11_ID_ENTURMACAO ASC")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterAluno(Optional ByVal Etapa As Integer = 0,
                               Optional ByVal Turma As Integer = 0,
                               Optional ByVal Situacao As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE09.DE09_ID_MATRICULA, ED01.ED01_NM_ALUNO, ED02.ED02_NM_LOTACAO, DE06.DE06_NM_ETAPA")
        strSQL.Append("   FROM DE09_MATRICULA DE09 ")
        strSQL.Append("   JOIN DE01_ALUNO     DE01 ON DE01.DE01_ID_ALUNO   = DE09.DE01_ID_ALUNO")
        strSQL.Append("   JOIN DE04_LOTACAO   DE04 ON DE04.DE04_ID_LOTACAO = DE09.DE04_ID_LOTACAO")
        strSQL.Append("   JOIN DE06_ETAPA     DE06 ON DE06.DE06_ID_ETAPA   = DE09.DE06_ID_ETAPA")
        strSQL.Append("  WHERE DE09_ID_MATRICULA IS NOT NULL")
        strSQL.Append("    AND DE06.DE06_ID_ETAPA   = " & Etapa)
        strSQL.Append("    AND NOT EXISTS (SELECT 1 ")
        strSQL.Append("                      FROM DE11_ENTURMACAO DE11")
        strSQL.Append("                     WHERE DE11.DE10_ID_TURMA    = " & Turma)
        strSQL.Append("                       AND DE11.DE09_ID_MATRICULA  = DE09.DE09_ID_MATRICULA")
        strSQL.Append("                       AND DE11.DE11_ST_ENTURMACAO = " & Situacao)
        strSQL.Append("                   )")
        strSQL.Append("  ORDER BY DE01.DE01_NM_ALUNO ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterGridEnturmado(Optional ByVal Lotacao As Integer = 0,
                                       Optional ByVal Etapa As Integer = 0,
                                       Optional ByVal Periodo As Integer = 0,
                                       Optional ByVal Turma As Integer = 0,
                                       Optional ByVal Nome As String = "") As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE11.DE11_ID_ENTURMACAO,")
        strSQL.Append("        DE09.DE09_ID_MATRICULA,")
        strSQL.Append("        DE01.DE01_NM_ALUNO,")
        strSQL.Append("        DE10.DE10_NU_TURMA,")
        strSQL.Append("        DE11.DE11_ST_ENTURMACAO,")
        strSQL.Append("        CASE DE11.DE11_ST_ENTURMACAO")
        strSQL.Append("          WHEN 1 THEN 'ENTURMADO'")
        strSQL.Append("          WHEN 2 THEN 'TRANSFERIDO'")
        strSQL.Append("          WHEN 3 THEN 'CANCELADO'")
        strSQL.Append("          WHEN 4 THEN 'FINALIZADO'")
        strSQL.Append("        END AS NM_SITUACAO")
        strSQL.Append("   FROM DE09_MATRICULA  DE09 ")
        strSQL.Append("   JOIN DE11_ENTURMACAO DE11 ON DE11.DE09_ID_MATRICULA = DE09.DE09_ID_MATRICULA")
        strSQL.Append("   JOIN DE01_ALUNO      DE01 ON DE01.DE01_ID_ALUNO     = DE09.DE01_ID_ALUNO")
        strSQL.Append("   JOIN DE10_TURMA      DE10 ON DE10.DE10_ID_TURMA     = DE11.DE10_ID_TURMA")
        If Lotacao > 0 Then
            strSQL.Append("  WHERE DE09.DE04_ID_LOTACAO = " & Lotacao)
        End If

        If Etapa > 0 Then
            strSQL.Append("    AND DE09.DE06_ID_ETAPA   = " & Etapa)
        End If

        If Periodo > 0 Then
            strSQL.Append("    AND DE09.DE08_ID_PERIODO = " & Periodo)
        End If

        If Turma > 0 Then
            strSQL.Append("    AND DE10.DE10_ID_TURMA   = " & Turma)
        End If

        If Nome <> "" Then
            strSQL.Append("    AND UPPER(DE01.DE01_NM_ALUNO) LIKE '%" & Nome.ToUpper & "%'")
        End If

        strSQL.Append("  ORDER BY DE01.DE01_NM_ALUNO ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE11_ENTURMACAO")
        strSQL.Append("  WHERE DE11_ID_ENTURMACAO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
