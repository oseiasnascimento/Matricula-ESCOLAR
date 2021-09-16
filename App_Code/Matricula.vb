Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Matricula
    Private DE09_ID_MATRICULA As Integer
    Private DE01_ID_ALUNO As Integer
    Private DE04_ID_LOTACAO As Integer
    Private DE06_ID_ETAPA As Integer
    Private DE08_ID_PERIODO As Integer
    Private DE09_NU_MATRICULA As String

    Public Property Matricula() As Integer
        Get
            Return DE09_ID_MATRICULA
        End Get
        Set(ByVal Value As Integer)
            DE09_ID_MATRICULA = Value
        End Set
    End Property

    Public Property Aluno() As Integer
        Get
            Return DE01_ID_ALUNO
        End Get
        Set(ByVal Value As Integer)
            DE01_ID_ALUNO = Value
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
        Set(ByVal Value As Integer)
            DE08_ID_PERIODO = Value
        End Set
    End Property

    Public Property NuMatricula() As String
        Get
            Return DE09_NU_MATRICULA
        End Get
        Set(ByVal value As String)
            DE09_NU_MATRICULA = value
        End Set
    End Property

    Public Sub New(Optional ByVal Matricula As Integer = 0)
        If Matricula > 0 Then
            Obter(Matricula)
        End If
    End Sub

    Function GeraNovoNumMatricula() As String
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder
        Dim ret_NumMatricula As String

        strSQL.Append(" SELECT NEXT VALUE FOR SQ_DE09_MATRICULA")

        dt = cnn.EditarDataTable(strSQL.ToString)
        ret_NumMatricula = dt.Rows(0)(0).ToString.Trim

        dt.Dispose()
        dt = Nothing
        cnn = Nothing
        Return ret_NumMatricula
    End Function

    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE09_MATRICULA")
        strSQL.Append("  WHERE DE09_ID_MATRICULA = " & Matricula)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            DE09_NU_MATRICULA = GeraNovoNumMatricula()
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE09_ID_MATRICULA") = ProBanco(DE09_ID_MATRICULA, eTipoValor.CHAVE)
        dr("DE01_ID_ALUNO") = ProBanco(DE01_ID_ALUNO, eTipoValor.NUMERO_INTEIRO)
        dr("DE04_ID_LOTACAO") = ProBanco(DE04_ID_LOTACAO, eTipoValor.NUMERO_INTEIRO)
        dr("DE06_ID_ETAPA") = ProBanco(DE06_ID_ETAPA, eTipoValor.NUMERO_INTEIRO)
        dr("DE08_ID_PERIODO") = ProBanco(DE08_ID_PERIODO, eTipoValor.NUMERO_INTEIRO)
        dr("DE09_NU_MATRICULA") = ProBanco(DE09_NU_MATRICULA, eTipoValor.TEXTO_LIVRE)

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
        strSQL.Append("   FROM DE09_MATRICULA")
        strSQL.Append("  WHERE DE09_ID_MATRICULA = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE09_ID_MATRICULA = DoBanco(dr("DE09_ID_MATRICULA"), eTipoValor.CHAVE)
            DE01_ID_ALUNO = DoBanco(dr("DE01_ID_ALUNO"), eTipoValor.NUMERO_INTEIRO)
            DE04_ID_LOTACAO = DoBanco(dr("DE04_ID_LOTACAO"), eTipoValor.NUMERO_INTEIRO)
            DE06_ID_ETAPA = DoBanco(dr("DE06_ID_ETAPA"), eTipoValor.NUMERO_INTEIRO)
            DE08_ID_PERIODO = DoBanco(dr("DE08_ID_PERIODO"), eTipoValor.NUMERO_INTEIRO)
            DE09_NU_MATRICULA = DoBanco(dr("DE09_NU_MATRICULA"), eTipoValor.TEXTO)

        End If

        cnn = Nothing
    End Sub

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Matricula As Integer = 0,
                              Optional ByVal Aluno As Integer = 0,
                              Optional ByVal Lotacao As Integer = 0,
                              Optional ByVal Etapa As Integer = 0,
                              Optional ByVal NomeAluno As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE09.DE09_NU_MATRICULA,")
        strSQL.Append("        DE09.DE09_ID_MATRICULA,")
        strSQL.Append("        DE01.DE01_NM_ALUNO,")
        strSQL.Append("        DE06.DE06_NM_ETAPA")
        strSQL.Append("   FROM DE09_MATRICULA DE09 ")
        strSQL.Append("   JOIN DE01_ALUNO     DE01 ON DE01.DE01_ID_ALUNO   = DE09.DE01_ID_ALUNO")
        strSQL.Append("   JOIN DE04_LOTACAO   DE04 ON DE04.DE04_ID_LOTACAO = DE09.DE04_ID_LOTACAO")
        strSQL.Append("   JOIN DE06_ETAPA     DE06 ON DE06.DE06_ID_ETAPA   = DE09.DE06_ID_ETAPA")
        strSQL.Append("  WHERE DE09.DE09_ID_MATRICULA IS NOT NULL")

        If Matricula > 0 Then
            strSQL.Append(" AND DE09.DE09_ID_MATRICULA = " & Matricula)
        End If

        If Aluno > 0 Then
            strSQL.Append(" AND DE01.DE01_ID_ALUNO = " & Aluno)
        End If

        If Lotacao > 0 Then
            strSQL.Append(" AND DE04.DE04_ID_LOTACAO = " & Lotacao)
        End If

        If Etapa > 0 Then
            strSQL.Append(" AND DE06.DE06_ID_ETAPA = " & Etapa)
        End If

        If NomeAluno <> "" Then
            strSQL.Append(" AND UPPER(DE01.DE01_NM_ALUNO) LIKE '%" & NomeAluno.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE01.DE01_NM_ALUNO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE09_MATRICULA ORDER BY DE09_ID_MATRICULA ASC")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterAluno(Optional ByVal Etapa As Integer = 0,
                               Optional ByVal Turma As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE09.DE09_ID_MATRICULA, DE01.DE01_NM_ALUNO, DE04.DE04_NM_LOTACAO, DE06.DE06_NM_ETAPA")
        strSQL.Append("   FROM DE09_MATRICULA DE09 ")
        strSQL.Append("   JOIN DE01_ALUNO     DE01 ON DE01.DE01_ID_ALUNO   = DE09.DE01_ID_ALUNO")
        strSQL.Append("   JOIN DE04_LOTACAO   DE04 ON DE04.DE04_ID_LOTACAO = DE09.DE04_ID_LOTACAO")
        strSQL.Append("   JOIN DE06_ETAPA     DE06 ON DE06.DE06_ID_ETAPA   = DE09.DE06_ID_ETAPA")
        strSQL.Append("  WHERE DE09_ID_MATRICULA IS NOT NULL")

        If Etapa > 0 Then
            strSQL.Append(" AND DE06.DE06_ID_ETAPA   = " & Etapa)
        End If

        If Turma > 0 Then
            strSQL.Append(" AND NOT EXISTS (SELECT 1 ")
            strSQL.Append("                   FROM DE11_ENTURMACAO DE09")
            strSQL.Append("                  WHERE DE11.DE09_ID_TURMA    = " & Turma)
            strSQL.Append("                    AND DE11.DE09_ID_MATRICULA  = DE09.DE09_ID_MATRICULA")
            strSQL.Append("                    AND DE11.ED10_ST_ENTURMACAO = 1 ")
            strSQL.Append("                 )")
        End If

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterGrid(Optional ByVal Etapa As Integer = 0,
                              Optional ByVal Turma As Integer = 0) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE09.DE09_ID_MATRICULA, ")
        strSQL.Append("        DE01.DE01_ID_ALUNO, ")
        strSQL.Append("        DE01.DE01_NM_ALUNO, ")
        strSQL.Append("        DE10.DE10_NU_TURMA, ")
        strSQL.Append("        DE11.DE11_ST_ENTURMACAO")
        strSQL.Append("   FROM DE11_ENTURMACAO DE11 ")
        strSQL.Append("   JOIN DE09_MATRICULA  DE09 ON DE09.DE09_ID_MATRICULA = DE11.DE09_ID_MATRICULA")
        strSQL.Append("   JOIN DE01_ALUNO      DE01 ON DE01.DE01_ID_ALUNO     = DE09.DE01_ID_ALUNO")
        strSQL.Append("   JOIN DE06_ETAPA      DE06 ON DE06.DE06_ID_ETAPA     = DE09.DE06_ID_ETAPA")
        strSQL.Append("   JOIN DE10_TURMA      DE10 ON DE10.DE10_ID_TURMA     = DE11.DE10_ID_TURMA")
        strSQL.Append("  WHERE DE11.DE11_ID_ENTURMACAO IS NOT NULL")

        If Etapa > 0 Then
            strSQL.Append("  AND DE10.DE06_ID_ETAPA = " & Etapa)
        End If

        If Turma > 0 Then
            strSQL.Append("  AND DE10.DE09_ID_TURMA = " & Turma)
        End If


        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterGridMatriculado(Optional ByVal Lotacao As Integer = 0,
                                          Optional ByVal Etapa As Integer = 0,
                                          Optional ByVal Periodo As Integer = 0,
                                          Optional ByVal Turma As Integer = 0,
                                          Optional ByVal Nome As String = "") As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE09.DE09_NU_MATRICULA,")
        strSQL.Append("        DE09.DE09_ID_MATRICULA,")
        strSQL.Append(" 	   DE01.DE01_NM_ALUNO,")
        strSQL.Append(" 	   DE10.DE10_NU_TURMA,")
        strSQL.Append(" 	   DE11.DE11_ST_ENTURMACAO,")
        strSQL.Append(" 	   CASE DE11.DE11_ST_ENTURMACAO")
        strSQL.Append(" 	     WHEN 1 THEN 'ENTURMADO'")
        strSQL.Append(" 		 WHEN 2 THEN 'TRANSFERIDO'")
        strSQL.Append(" 		 WHEN 3 THEN 'CANCELADO'")
        strSQL.Append(" 		 WHEN 4 THEN 'FINALIZADO'")
        strSQL.Append(" 	   END AS NM_SITUACAO")
        strSQL.Append("   FROM DE09_MATRICULA  DE09")
        strSQL.Append("   JOIN DE11_ENTURMACAO DE11 ON DE11.DE09_ID_MATRICULA = DE09.DE09_ID_MATRICULA   ")
        strSQL.Append("   JOIN DE01_ALUNO      DE01 ON DE01.DE01_ID_ALUNO     = DE09.DE01_ID_ALUNO    ")
        strSQL.Append("   JOIN DE10_TURMA      DE10 ON DE10.DE10_ID_TURMA     = DE11.DE10_ID_TURMA   ")
        strSQL.Append("   WHERE DE09.DE04_ID_LOTACAO  = " & Lotacao)
        strSQL.Append("     AND DE09.DE06_ID_ETAPA    = " & Etapa)
        strSQL.Append("     AND DE09.DE08_ID_PERIODO  = " & Periodo)
        strSQL.Append("     AND DE11.DE10_ID_TURMA   <> " & Turma)

        If Nome <> "" Then
            strSQL.Append(" AND UPPER(DE01.DE01_NM_ALUNO) LIKE '%" & Nome.ToUpper & "%'")
        End If

        strSQL.Append("  UNION ")
        strSQL.Append(" SELECT DE09.DE09_NU_MATRICULA, ")
        strSQL.Append("        DE09.DE09_ID_MATRICULA,")
        strSQL.Append(" 	   DE01.DE01_NM_ALUNO,")
        strSQL.Append(" 	   NULL AS DE10_NU_TURMA,")
        strSQL.Append(" 	   NULL AS DE11_ST_ENTURMACAO,")
        strSQL.Append(" 	   NULL AS NM_SITUACAO")
        strSQL.Append("   FROM DE09_MATRICULA     DE09")
        strSQL.Append("   JOIN DE01_ALUNO         DE01 ON DE01.DE01_ID_ALUNO = DE09.DE01_ID_ALUNO")
        strSQL.Append("  WHERE DE09.DE04_ID_LOTACAO = " & Lotacao)
        strSQL.Append("    AND DE09.DE06_ID_ETAPA   = " & Etapa)
        strSQL.Append("    AND DE09.DE08_ID_PERIODO = " & Periodo)
        If Nome <> "" Then
            strSQL.Append(" AND UPPER(DE01.DE01_NM_ALUNO) LIKE '%" & Nome.ToUpper & "%'")
        End If
        strSQL.Append(" AND NOT EXISTS (SELECT 1 ")
        strSQL.Append("                   FROM DE11_ENTURMACAO DE11")
        strSQL.Append("                  WHERE DE11.DE09_ID_MATRICULA  = DE09.DE09_ID_MATRICULA")
        strSQL.Append("                    AND DE11.DE11_ST_ENTURMACAO = 1 ") '1-MATRICULADO; 2-CANCELADA; 3-TRANSFERIDO; 4-FINALIZADA
        strSQL.Append("                 )")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE09_MATRICULA")
        strSQL.Append("  WHERE DE09_ID_MATRICULA = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing
        Return LinhasAfetadas
    End Function

End Class
