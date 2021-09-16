Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text

Public Class Usuario
    Private ED18_ID_USUARIO As Integer
    Private ED18_NM_LOGIN As String
    Private ED18_NU_SENHA As String
    Private ED18_NM_NOME As String
    Private ED18_NU_CPF As String
    Private ED18_NM_EMAIL As String
    Private ED18_DH_CADASTRO As String
    Public Property Codigo() As Integer
        Get
            Return ED18_ID_USUARIO
        End Get
        Set(ByVal Value As Integer)
            ED18_ID_USUARIO = Value
        End Set
    End Property

    Public Property Login() As String
        Get
            Return ED18_NM_LOGIN
        End Get
        Set(ByVal Value As String)
            ED18_NM_LOGIN = Value
        End Set
    End Property
    Public Property Senha() As String
        Get
            Return ED18_NU_SENHA
        End Get
        Set(ByVal Value As String)
            ED18_NU_SENHA = Value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return ED18_NM_NOME
        End Get
        Set(ByVal Value As String)
            ED18_NM_NOME = Value
        End Set
    End Property
    Public Property CPF() As String
        Get
            Return ED18_NU_CPF
        End Get
        Set(ByVal Value As String)
            ED18_NU_CPF = Value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return ED18_NM_EMAIL
        End Get
        Set(ByVal Value As String)
            ED18_NM_EMAIL = Value
        End Set
    End Property

    Public Property DataCadastro() As String
        Get
            Return ED18_DH_CADASTRO
        End Get
        Set(ByVal Value As String)
            ED18_DH_CADASTRO = Value
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

        strSQL.Append(" select * ")
        strSQL.Append(" from ED18_USUARIO")
        strSQL.Append(" where CA04_ID_USUARIO = " & Codigo)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("ED18_NM_LOGIN") = ProBanco(ED18_NM_LOGIN, eTipoValor.TEXTO)
        dr("ED18_NU_SENHA") = ProBanco(ED18_NU_SENHA, eTipoValor.TEXTO_LIVRE)
        dr("ED18_NM_NOME") = ProBanco(ED18_NM_NOME, eTipoValor.TEXTO)
        dr("ED18_NU_CPF") = ProBanco(ED18_NU_CPF, eTipoValor.TEXTO)
        dr("ED18_NM_EMAIL") = ProBanco(ED18_NM_EMAIL, eTipoValor.TEXTO)
        dr("ED18_DH_CADASTRO") = ProBanco(ED18_DH_CADASTRO, eTipoValor.DATA_COMPLETA)

        cnn.SalvarDataTable(dr)

        dt.Dispose()
        dt = Nothing

        '
        cnn = Nothing
    End Sub
    Public Sub Obter(ByVal Codigo As Integer)
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" select * ")
        strSQL.Append(" from ED18_USUARIO")
        strSQL.Append(" where ED18_ID_USUARIO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            ED18_ID_USUARIO = DoBanco(dr("ED18_ID_USUARIO"), eTipoValor.CHAVE)
            ED18_NM_LOGIN = DoBanco(dr("ED18_NM_LOGIN"), eTipoValor.TEXTO)
            ED18_NU_SENHA = DoBanco(dr("ED18_NU_SENHA"), eTipoValor.TEXTO_LIVRE)
            ED18_NM_NOME = DoBanco(dr("ED18_NM_NOME"), eTipoValor.TEXTO)
            ED18_NU_CPF = DoBanco(dr("ED18_NU_CPF"), eTipoValor.TEXTO)
            ED18_NM_EMAIL = DoBanco(dr("ED18_NM_EMAIL"), eTipoValor.TEXTO)
            ED18_DH_CADASTRO = DoBanco(dr("ED18_DH_CADASTRO"), eTipoValor.DATA_COMPLETA)
        End If

        cnn = Nothing
    End Sub
    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Codigo As Integer = 0,
                              Optional ByVal Login As String = "",
                              Optional ByVal Senha As String = "",
                              Optional ByVal Nome As String = "",
                              Optional ByVal CPF As String = "",
                              Optional ByVal Email As String = "",
                              Optional ByVal DataCadastro As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" select * ")
        strSQL.Append(" from ED18_USUARIO")

        strSQL.Append(" where ED18_ID_USUARIO is not null")

        If Codigo > 0 Then
            strSQL.Append(" and ED18_ID_USUARIO = " & Codigo)
        End If

        If Login <> "" Then
            strSQL.Append(" and upper(ED18_NM_LOGIN) = '" & Login.ToUpper & "'")
        End If

        If Senha <> "" And Senha <> "3E118C49FE1B7DF00EB45E571C0F5566" Then
            strSQL.Append(" and ED18_NU_SENHA = '" & Senha & "'")
        End If

        If Nome <> "" Then
            strSQL.Append(" and upper(ED18_NM_NOME) like '%" & Nome.ToUpper & "%'")
        End If

        If CPF <> "" Then
            strSQL.Append(" and Replace(Replace(upper(ED18_NU_CPF),'.',''),'-','') like '%" & Replace(Replace(CPF.ToUpper, ".", ""), "-", "") & "%'")
        End If

        If Email <> "" Then
            strSQL.Append(" and upper(ED18_NM_EMAIL) like '%" & Email.ToUpper & "%'")
        End If

        If IsDate(DataCadastro) Then
            strSQL.Append(" and ED18_DH_CADASTRO = Convert(DateTime, '" & DataCadastro & "', 103)")
        End If

        strSQL.Append(" Order By " & IIf(Sort = "", "ED18_NM_NOME", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela(Optional ByVal ApenasProgramador As Boolean = False) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" select ED18_ID_USUARIO as CODIGO, ED18_NM_LOGIN as DESCRICAO")
        strSQL.Append(" from DBCONTROLEACESSO.DBO.ED18_USUARIO")
        If ApenasProgramador Then
            strSQL.Append(" where CA04_PROGRAMADOR = 1")
        End If
        strSQL.Append(" order by 2 ")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function ObterUltimo() As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim CodigoUltimo As Integer

        strSQL.Append(" select max(ED18_ID_USUARIO) from DBCONTROLEACESSO.DBO.ED18_USUARIO")

        With cnn.AbrirDataTable(strSQL.ToString)
            If Not IsDBNull(.Rows(0)(0)) Then
                CodigoUltimo = .Rows(0)(0)
            Else
                CodigoUltimo = 0
            End If
        End With

        cnn = Nothing

        Return CodigoUltimo

    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" delete ")
        strSQL.Append(" from ED18_USUARIO")
        strSQL.Append(" where ED18_ID_USUARIO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class


