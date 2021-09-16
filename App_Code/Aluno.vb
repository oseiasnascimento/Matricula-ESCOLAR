Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Aluno

    Private DE01_ID_ALUNO As Integer
    Private DE01_NM_ALUNO As String
    Private DE01_NU_CPF As String
    Private DE01_NU_CELULAR As String
    Private DE01_NM_RESPONSAVEL As String
    Private DE01_NU_CPF_RESPONSAVEL As String
    Private DE01_NU_CELULAR_RESPONSAVEL As String
    Private DE01_TP_SEXO As Integer
    Private DE01_DT_NASCIMENTO As String
    Private DE01_NM_EMAIL As String

    Public Property Aluno As Integer
        Get
            Return DE01_ID_ALUNO
        End Get
        Set(ByVal Value As Integer)
            DE01_ID_ALUNO = Value
        End Set
    End Property

    Public Property Nome As String
        Get
            Return DE01_NM_ALUNO
        End Get
        Set(ByVal Value As String)
            DE01_NM_ALUNO = Value
        End Set
    End Property

    Public Property CPF As String
        Get
            Return DE01_NU_CPF
        End Get
        Set(ByVal Value As String)
            DE01_NU_CPF = Value
        End Set
    End Property

    Public Property DataNascimento As String
        Get
            Return DE01_DT_NASCIMENTO
        End Get
        Set(ByVal value As String)
            DE01_DT_NASCIMENTO = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return DE01_NM_EMAIL
        End Get
        Set(ByVal value As String)
            DE01_NM_EMAIL = value
        End Set
    End Property

    Public Property Telefone As String
        Get
            Return DE01_NU_CELULAR
        End Get
        Set(ByVal Value As String)
            DE01_NU_CELULAR = Value
        End Set
    End Property

    Public Property NomeResponsavel As String
        Get
            Return DE01_NM_RESPONSAVEL
        End Get
        Set(value As String)
            DE01_NM_RESPONSAVEL = value
        End Set
    End Property

    Public Property CpfResponsavel As String
        Get
            Return DE01_NU_CPF_RESPONSAVEL
        End Get
        Set(value As String)
            DE01_NU_CPF_RESPONSAVEL = value
        End Set
    End Property

    Public Property CelularResponsavel As String
        Get
            Return DE01_NU_CELULAR_RESPONSAVEL
        End Get
        Set(value As String)
            DE01_NU_CELULAR_RESPONSAVEL = value
        End Set
    End Property

    Public Property Sexo As Integer
        Get
            Return DE01_TP_SEXO
        End Get
        Set(value As Integer)
            DE01_TP_SEXO = value
        End Set
    End Property

    Public Sub New(Optional ByVal Codigo As Integer = 0)
        If Codigo > 0 Then
            Obter(Codigo)
        End If
    End Sub

    Public Sub Capitalize()
        ' Escrever com letra maiúscula.
        DE01_NM_ALUNO = UCase(DE01_NM_ALUNO)
    End Sub



    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT *")
        strSQL.Append("   FROM DE01_ALUNO")
        strSQL.Append("  WHERE DE01_ID_ALUNO = " & Aluno)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE01_ID_ALUNO") = ProBanco(DE01_ID_ALUNO, eTipoValor.CHAVE)
        dr("DE01_NM_ALUNO") = ProBanco(DE01_NM_ALUNO, eTipoValor.TEXTO)
        dr("DE01_NU_CPF") = ProBanco(DE01_NU_CPF, eTipoValor.TEXTO_LIVRE)
        dr("DE01_NU_CELULAR") = ProBanco(DE01_NU_CELULAR, eTipoValor.TEXTO)
        dr("DE01_NM_RESPONSAVEL") = ProBanco(DE01_NM_RESPONSAVEL, eTipoValor.TEXTO)
        dr("DE01_NU_CPF_RESPONSAVEL") = ProBanco(DE01_NU_CPF_RESPONSAVEL, eTipoValor.TEXTO)
        dr("DE01_NU_CELULAR_RESPONSAVEL") = ProBanco(DE01_NU_CELULAR_RESPONSAVEL, eTipoValor.TEXTO)
        dr("DE01_TP_SEXO") = ProBanco(DE01_TP_SEXO, eTipoValor.NUMERO_INTEIRO)
        dr("DE01_DT_NASCIMENTO") = ProBanco(DE01_DT_NASCIMENTO, eTipoValor.TEXTO)
        dr("DE01_NM_EMAIL") = ProBanco(DE01_NM_EMAIL, eTipoValor.TEXTO)

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
        strSQL.Append("   FROM DE01_ALUNO")
        strSQL.Append("  WHERE DE01_ID_ALUNO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE01_ID_ALUNO = DoBanco(dr("DE01_ID_ALUNO"), eTipoValor.CHAVE)
            DE01_NM_ALUNO = DoBanco(dr("DE01_NM_ALUNO"), eTipoValor.TEXTO)
            DE01_NU_CPF = DoBanco(dr("DE01_NU_CPF"), eTipoValor.TEXTO_LIVRE)
            DE01_DT_NASCIMENTO = DoBanco(dr("DE01_DT_NASCIMENTO"), eTipoValor.TEXTO)
            DE01_NM_EMAIL = DoBanco(dr("DE01_NM_EMAIL"), eTipoValor.TEXTO)
            DE01_NU_CELULAR = DoBanco(dr("DE01_NU_CELULAR"), eTipoValor.TEXTO_LIVRE)
            DE01_NM_RESPONSAVEL = DoBanco(dr("DE01_NM_RESPONSAVEL"), eTipoValor.TEXTO)
            DE01_NU_CPF_RESPONSAVEL = DoBanco(dr("DE01_NU_CPF_RESPONSAVEL"), eTipoValor.TEXTO)
            DE01_NU_CELULAR_RESPONSAVEL = DoBanco(dr("DE01_NU_CELULAR_RESPONSAVEL"), eTipoValor.TEXTO)
            DE01_TP_SEXO = DoBanco(dr("DE01_TP_SEXO"), eTipoValor.NUMERO_INTEIRO)

        End If

        cnn = Nothing
    End Sub

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Codigo As Integer = 0,
                              Optional ByVal NomeAluno As String = "",
                              Optional ByVal CPF As String = "",
                              Optional ByVal CEP As String = "",
                              Optional ByVal Estado As String = "",
                              Optional ByVal Cidade As String = "",
                              Optional ByVal Logradouro As String = "",
                              Optional ByVal Telefone As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE01_ID_ALUNO,")
        strSQL.Append("        DE01_NM_ALUNO,")
        strSQL.Append("        DE01_NU_CPF,")
        strSQL.Append("        DE01_DT_NASCIMENTO,")
        strSQL.Append("        DE01_NM_EMAIL,")
        strSQL.Append("        DE01_NU_CELULAR ")
        strSQL.Append("   FROM DE01_ALUNO")
        strSQL.Append("  WHERE DE01_ID_ALUNO IS NOT NULL")

        If Codigo > 0 Then
            strSQL.Append(" AND DE01_ID_ALUNO = " & Codigo)
        End If

        If NomeAluno <> "" Then
            strSQL.Append(" AND UPPER(DE01_NM_ALUNO) LIKE '%" & NomeAluno.ToUpper & "%'")
        End If

        If CPF <> "" Then
            strSQL.Append(" AND DE01_NU_CPF = '" & CPF & "'")
        End If

        If Estado <> "" Then
            strSQL.Append(" AND UPPER(DE01_DT_NASCIMENTO) LIKE '%" & Estado.ToUpper & "%'")
        End If

        If Cidade <> "" Then
            strSQL.Append(" AND UPPER(DE01_NM_EMAIL) LIKE '%" & Cidade.ToUpper & "%'")
        End If

        If Logradouro <> "" Then
            strSQL.Append(" AND UPPER(DE01_NM_LOGRADOURO) LIKE '%" & Logradouro.ToUpper & "%'")
        End If

        If Telefone <> "" Then
            strSQL.Append(" and UPPER(DE01_NU_CELULAR) like '%" & Telefone.ToUpper & "%'")
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE01_NM_ALUNO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE01_ID_ALUNO , DE01_NM_ALUNO ")
        strSQL.Append("   FROM DE01_ALUNO ")
        strSQL.Append("  WHERE DE01_ID_ALUNO IS NOT NULL ")
        strSQL.Append("  ORDER BY DE01_NM_ALUNO ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterAluno() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append("  SELECT DE01.DE01_ID_ALUNO , DE01.DE01_NM_ALUNO ")
        strSQL.Append("    FROM DE09_MATRICULA     DE09 ")
        strSQL.Append("    JOIN DE04_LOTACAO       DE04 ON DE04.DE04_ID_LOTACAO = DE09.DE04_ID_LOTACAO ")
        strSQL.Append("   RIGHT JOIN DE01_ALUNO    DE01 ON DE01.DE01_ID_ALUNO   = DE09.DE01_ID_ALUNO ")
        strSQL.Append("   WHERE DE04.DE04_NM_LOTACAO IS NULL ")
        strSQL.Append("ORDER BY DE01.DE01_NM_ALUNO ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE01_ALUNO")
        strSQL.Append("  WHERE DE01_ID_ALUNO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

    Public Function Ultimo() As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim retorno As Integer

        strSQL.Append(" Select Max(DE01_ID_ALUNO) from DE01_ALUNO ")

        retorno = cnn.AbrirDataTable(strSQL.ToString)(0)(0)

        cnn = Nothing
        strSQL = Nothing

        Return retorno
    End Function

End Class
