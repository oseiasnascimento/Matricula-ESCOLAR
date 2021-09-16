Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text


Public Class Documento
    Private DE02_ID_DOCUMENTO As Integer
    Private DE01_ID_ALUNO As Integer
    Private DE02_DS_ARQUIVO As String
    Private DE02_DH_CADASTRO As String
    Private DE02_NU_RG As String
    Private DE02_SG_UF_EMISSOR_RG As String
    Private DE62_DT_EMISSAO_RG As String

    Public Property Documento() As Integer
        Get
            Return DE02_ID_DOCUMENTO
        End Get
        Set(ByVal Value As Integer)
            DE02_ID_DOCUMENTO = Value
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

    Public Property DescricaoArquivo() As String
        Get
            Return DE02_DS_ARQUIVO
        End Get
        Set(value As String)
            DE02_DS_ARQUIVO = value
        End Set
    End Property

    Public Property NuRG As String
        Get
            Return DE02_NU_RG
        End Get
        Set(ByVal value As String)
            DE02_NU_RG = value
        End Set
    End Property

    Public Property EmissorRG() As String
        Get
            Return DE02_SG_UF_EMISSOR_RG
        End Get
        Set(ByVal value As String)
            DE02_SG_UF_EMISSOR_RG = value
        End Set
    End Property

    Public Property DataEmissaoRG() As String
        Get
            Return DE62_DT_EMISSAO_RG
        End Get
        Set(ByVal value As String)
            DE62_DT_EMISSAO_RG = value
        End Set
    End Property

    Public Property DataCadastro() As String
        Get
            Return DE02_DH_CADASTRO
        End Get
        Set(value As String)
            DE02_DH_CADASTRO = value
        End Set
    End Property

    Public Sub New(Optional ByVal Documento As Integer = 0)
        If Documento > 0 Then
            Obter(Documento)
        End If
    End Sub
    Public Sub Salvar()
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE02_DOCUMENTO")
        strSQL.Append("  WHERE DE02_ID_DOCUMENTO = " & Documento)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE02_ID_DOCUMENTO") = ProBanco(DE02_ID_DOCUMENTO, eTipoValor.CHAVE)
        dr("DE01_ID_ALUNO") = ProBanco(DE01_ID_ALUNO, eTipoValor.NUMERO_INTEIRO)
        dr("DE02_DS_ARQUIVO") = ProBanco(DE02_DS_ARQUIVO, eTipoValor.TEXTO)
        dr("DE02_DH_CADASTRO") = ProBanco(DE02_DH_CADASTRO, eTipoValor.DATA_COMPLETA)

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
        strSQL.Append("   FROM DE02_DOCUMENTO")
        strSQL.Append("  WHERE DE02_ID_DOCUMENTO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE02_ID_DOCUMENTO = DoBanco(dr("DE02_ID_DOCUMENTO"), eTipoValor.CHAVE)
            DE01_ID_ALUNO = DoBanco(dr("DE01_ID_ALUNO"), eTipoValor.TEXTO_LIVRE)
            DE02_DS_ARQUIVO = DoBanco(dr("DE02_DS_ARQUIVO"), eTipoValor.TEXTO)
            DE02_DH_CADASTRO = DoBanco(dr("DE02_DH_CADASTRO"), eTipoValor.DATA_COMPLETA)
        End If

        cnn = Nothing

    End Sub

    Public Function ObterTabela() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE02_ID_DOCUMENTO AS CODIGO,  DE02_DS_ARQUIVO AS DESCRICAO")
        strSQL.Append("   FROM DE02_DOCUMENTO")
        strSQL.Append("  WHERE DE02_ID_DOCUMENTO IS NOT NULL ")
        strSQL.Append("  ORDER BY DE02_ID_DOCUMENTO")

        dt = cnn.AbrirDataTable(strSQL.ToString)

        cnn = Nothing

        Return dt
    End Function

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional ByVal Codigo As Integer = 0,
                              Optional ByVal Aluno As Integer = 0,
                              Optional ByVal NomeResponsavel As String = "",
                              Optional ByVal CPF_Responsavel As String = "",
                              Optional ByVal TelefoneResposavel As String = "",
                              Optional ByVal SexoAluno As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT DE02.DE02_ID_DOCUMENTO,")
        strSQL.Append("        DE01.DE01_ID_ALUNO,")
        strSQL.Append("        DE01. DE01_NM_RESPONSAVEL,")
        strSQL.Append("        DE01.DE01_NU_CPF_RESPONSAVEL,")
        strSQL.Append("        DE01.DE01_NU_CELULAR_RESPONSAVEL,")
        strSQL.Append("        DE01.DE01_TP_SEXO, ")
        strSQL.Append("        DE02.DE02_DS_ARQUIVO, ")
        strSQL.Append("        DE02.DE02_DH_CADASTRO ")
        strSQL.Append("   FROM DE02_DOCUMENTO DE02 ")
        strSQL.Append("   INNER JOIN DE01_ALUNO DE01 ON DE01.DE01_ID_ALUNO = DE02.DE01_ID_ALUNO ")
        strSQL.Append("  WHERE DE02.DE01_ID_ALUNO IS NOT NULL")

        If Codigo > 0 Then
            strSQL.Append(" AND DE02.DE02_ID_DOCUMENTO = " & Codigo)
        End If

        If Aluno > 0 Then
            strSQL.Append(" AND DE02.DE01_ID_ALUNO = " & Aluno)
        End If

        strSQL.Append(" ORDER BY " & IIf(Sort = "", "DE01_ID_ALUNO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function Excluir(ByVal Codigo As Integer) As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim LinhasAfetadas As Integer

        strSQL.Append(" DELETE ")
        strSQL.Append("   FROM DE02_DOCUMENTO")
        strSQL.Append("  WHERE DE02_ID_DOCUMENTO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)

        cnn = Nothing

        Return LinhasAfetadas
    End Function

End Class
