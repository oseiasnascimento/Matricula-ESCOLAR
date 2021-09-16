Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text

Public Class Endereco
    Private DE03_ID_ENDERECO As Integer
    Private DE01_ID_ALUNO As Integer
    Private DE03_NU_CEP As String
    Private DE03_NM_LOGRADOURO As String
    Private DE03_NU_NUMERO As String
    Private DE03_DS_COMPLEMENTO As String
    Private DE03_NM_BAIRRO As String
    Private DE03_NU_ESTADO As String
    Private DE03_NM_CIDADE As String
    Private DE03_TP_ZONA_RECIDENCIA As String

    Public Property Endereco As Integer
        Get
            Return DE03_ID_ENDERECO
        End Get
        Set(ByVal Value As Integer)
            DE03_ID_ENDERECO = Value
        End Set
    End Property
    Public Property Aluno As Integer
        Get
            Return DE01_ID_ALUNO
        End Get
        Set(ByVal Value As Integer)
            DE01_ID_ALUNO = Value
        End Set
    End Property
    Public Property CEP As String
        Get
            Return DE03_NU_CEP
        End Get
        Set(ByVal Value As String)
            DE03_NU_CEP = Value
        End Set
    End Property
    Public Property logradouro As String
        Get
            Return DE03_NM_LOGRADOURO
        End Get
        Set(ByVal Value As String)
            DE03_NM_LOGRADOURO = Value
        End Set
    End Property
    Public Property Numero As String
        Get
            Return DE03_NU_NUMERO
        End Get
        Set(ByVal Value As String)
            DE03_NU_NUMERO = Value
        End Set
    End Property
    Public Property Complemento As String
        Get
            Return DE03_DS_COMPLEMENTO
        End Get
        Set(ByVal Value As String)
            DE03_DS_COMPLEMENTO = Value
        End Set
    End Property
    Public Property Bairro As String
        Get
            Return DE03_NM_BAIRRO
        End Get
        Set(ByVal Value As String)
            DE03_NM_BAIRRO = Value
        End Set
    End Property

    Public Property ZonaResidencia As String
        Get
            Return DE03_NU_NUMERO
        End Get
        Set(ByVal Value As String)
            DE03_NU_NUMERO = Value
        End Set
    End Property

    Public Property Estado As String
        Get
            Return DE03_NU_ESTADO
        End Get
        Set(value As String)
            DE03_NU_ESTADO = value
        End Set
    End Property

    Public Property Cidade As String
        Get
            Return DE03_NM_CIDADE
        End Get
        Set(value As String)
            DE03_NM_CIDADE = value
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
        strSQL.Append("   FROM DE03_ENDERECO")
        strSQL.Append("  WHERE DE03_ID_ENDERECO = " & Endereco)

        dt = cnn.EditarDataTable(strSQL.ToString)

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow
        Else
            dr = dt.Rows(0)
        End If

        dr("DE01_ID_ALUNO") = ProBanco(DE01_ID_ALUNO, eTipoValor.CHAVE)
        dr("DE03_NU_CEP") = ProBanco(DE03_NU_CEP, eTipoValor.CHAVE)
        dr("DE03_NM_LOGRADOURO") = ProBanco(DE03_NM_LOGRADOURO, eTipoValor.CHAVE)
        dr("DE03_NU_NUMERO") = ProBanco(DE03_NU_NUMERO, eTipoValor.NUMERO_INTEIRO)
        dr("DE03_DS_COMPLEMENTO") = ProBanco(DE03_DS_COMPLEMENTO, eTipoValor.TEXTO)
        dr("DE03_NM_BAIRRO") = ProBanco(DE03_NM_BAIRRO, eTipoValor.TEXTO)
        dr("DE03_NU_ESTADO") = ProBanco(DE03_NU_ESTADO, eTipoValor.TEXTO)
        dr("DE03_NM_CIDADE") = ProBanco(DE03_NM_CIDADE, eTipoValor.TEXTO)
        dr("DE03_TP_ZONA_RECIDENCIA") = ProBanco(DE03_TP_ZONA_RECIDENCIA, eTipoValor.NUMERO_INTEIRO)

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
        strSQL.Append("   FROM DE03_ENDERECO")
        strSQL.Append("  WHERE DE03_ID_ENDERECO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE03_ID_ENDERECO = DoBanco(dr("DE03_ID_ENDERECO"), eTipoValor.CHAVE)
            DE01_ID_ALUNO = DoBanco(dr("DE01_ID_ALUNO"), eTipoValor.CHAVE)
            DE03_NU_CEP = DoBanco(dr("DE03_NU_CEP"), eTipoValor.CHAVE)
            DE03_NM_LOGRADOURO = DoBanco(dr("DE03_NM_LOGRADOURO"), eTipoValor.CHAVE)
            DE03_NU_NUMERO = DoBanco(dr("DE03_NU_NUMERO"), eTipoValor.NUMERO_INTEIRO)
            DE03_DS_COMPLEMENTO = DoBanco(dr("DE03_DS_COMPLEMENTO"), eTipoValor.TEXTO)
            DE03_NM_BAIRRO = DoBanco(dr("DE03_NM_BAIRRO"), eTipoValor.TEXTO)
            DE03_NU_ESTADO = DoBanco(dr("DE03_NU_ESTADO"), eTipoValor.TEXTO)
            DE03_NM_CIDADE = DoBanco(dr("DE03_NM_CIDADE"), eTipoValor.TEXTO)
            DE03_TP_ZONA_RECIDENCIA = DoBanco(dr("DE03_TP_ZONA_RECIDENCIA"), eTipoValor.TEXTO)

        End If

        cnn = Nothing
    End Sub

    Public Sub ObterLogradouroCep(ByVal Codigo As Integer)
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE03_ENDERECO")
        strSQL.Append("  WHERE DE03_NU_CEP = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE03_ID_ENDERECO = DoBanco(dr("DE03_ID_ENDERECO"), eTipoValor.CHAVE)
            DE01_ID_ALUNO = DoBanco(dr("DE01_ID_ALUNO"), eTipoValor.CHAVE)
            DE03_NU_CEP = DoBanco(dr("DE03_NU_CEP"), eTipoValor.CHAVE)
            DE03_NM_LOGRADOURO = DoBanco(dr("DE03_NM_LOGRADOURO"), eTipoValor.CHAVE)
            DE03_NU_NUMERO = DoBanco(dr("DE03_NU_NUMERO"), eTipoValor.NUMERO_INTEIRO)
            DE03_DS_COMPLEMENTO = DoBanco(dr("DE03_DS_COMPLEMENTO"), eTipoValor.TEXTO)
            DE03_NM_BAIRRO = DoBanco(dr("DE03_NM_BAIRRO"), eTipoValor.TEXTO)
        End If


        cnn = Nothing
    End Sub

    Public Sub ObterLogradouroAluno(ByVal Codigo As Integer)
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim dr As DataRow
        Dim strSQL As New StringBuilder

        strSQL.Append(" SELECT * ")
        strSQL.Append("   FROM DE03_ENDERECO")
        strSQL.Append("  WHERE DE01_ID_ALUNO = " & Codigo)

        dt = cnn.AbrirDataTable(strSQL.ToString)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)

            DE03_ID_ENDERECO = DoBanco(dr("DE03_ID_ENDERECO"), eTipoValor.CHAVE)
            DE01_ID_ALUNO = DoBanco(dr("DE01_ID_ALUNO"), eTipoValor.CHAVE)
            DE03_NU_CEP = DoBanco(dr("DE03_NU_CEP"), eTipoValor.CHAVE)
            DE03_NM_LOGRADOURO = DoBanco(dr("DE03_NM_LOGRADOURO"), eTipoValor.CHAVE)
            DE03_NU_NUMERO = DoBanco(dr("DE03_NU_NUMERO"), eTipoValor.NUMERO_INTEIRO)
            DE03_DS_COMPLEMENTO = DoBanco(dr("DE03_DS_COMPLEMENTO"), eTipoValor.TEXTO)
            DE03_NM_BAIRRO = DoBanco(dr("DE03_NM_BAIRRO"), eTipoValor.TEXTO)
        End If


        cnn = Nothing
    End Sub

    Public Function Pesquisar(Optional ByVal Sort As String = "",
                              Optional Codigo As Integer = 0,
                              Optional Aluno As Integer = 0,
                              Optional LogradouroCEP As Integer = 0,
                              Optional TipoImovel As Integer = 0,
                              Optional ZonaResidencia As String = "",
                              Optional Numero As String = "",
                              Optional Complemento As String = "") As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" select * ")
        strSQL.Append(" from DE03_ENDERECO")
        'strSQL.Append(" left join tabela on coluna1 = coluna2 ")
        strSQL.Append(" where DE03_ID_ENDERECO is not null")

        If Codigo > 0 Then
            strSQL.Append(" and DE03_ID_ENDERECO = " & Codigo)
        End If

        If Aluno > 0 Then
            strSQL.Append(" and DE01_ID_ALUNO = " & Aluno)
        End If

        If LogradouroCEP > 0 Then
            strSQL.Append(" and DE03_NU_CEP = " & LogradouroCEP)
        End If

        If TipoImovel > 0 Then
            strSQL.Append(" and DE03_NM_LOGRADOURO = " & TipoImovel)
        End If

        If ZonaResidencia <> "" Then
            strSQL.Append(" and upper(DE03_NU_NUMERO) like '%" & ZonaResidencia.ToUpper & "%'")
        End If

        If Numero <> "" Then
            strSQL.Append(" and upper(DE03_DS_COMPLEMENTO) like '%" & Numero.ToUpper & "%'")
        End If

        If Complemento <> "" Then
            strSQL.Append(" and upper(DE03_NM_BAIRRO) like '%" & Complemento.ToUpper & "%'")
        End If

        strSQL.Append(" Order By " & IIf(Sort = "", "DE03_ID_ENDERECO", Sort))

        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function PesquisarEnderecoAluno(ByVal Aluno As Integer) As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" Select DE64.DE03_ID_ENDERECO, DE64.DE01_ID_ALUNO ")
        strSQL.Append("   , TG56.DE03_NU_CEP, TG55.TG55_NU_CEP, TG54.TG54_NM_LOGRADOURO, TG16.TG16_NM_TIPO_LOGRADOURO ")
        strSQL.Append("	  , TG04.TG04_NM_BAIRRO, TG03.TG03_NM_MUNICIPIO, TG02.TG02_SG_UF ")
        strSQL.Append(" From DE03_ENDERECO As DE64 ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG56_LOGRADOURO_CEP  AS TG56 ON TG56.DE03_NU_CEP = DE64.DE03_NU_CEP ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG55_CEP AS TG55 ON TG55.TG55_ID_CEP = TG56.TG55_ID_CEP ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG54_LOGRADOURO AS TG54 ON TG54.TG54_ID_LOGRADOURO = TG56.TG54_ID_LOGRADOURO ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG16_TIPO_LOGRADOURO AS TG16 ON TG16.TG16_ID_TIPO_LOGRADOURO = TG54.TG16_ID_TIPO_LOGRADOURO ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG04_BAIRRO AS TG04 ON TG04.TG04_ID_BAIRRO = TG54.TG04_ID_BAIRRO ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG03_MUNICIPIO AS TG03 ON TG03.TG03_ID_MUNICIPIO = TG04.TG03_ID_MUNICIPIO ")
        strSQL.Append(" Left Join DBGERAL.DBO.TG02_UF AS TG02 ON TG02.TG02_ID_UF = TG03.TG02_ID_UF ")
        strSQL.Append(" where DE64.DE01_ID_ALUNO =  " & Aluno)



        Return cnn.AbrirDataTable(strSQL.ToString)
    End Function

    Public Function ObterTabela() As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder

        strSQL.Append(" select DE03_ID_ENDERECO as CODIGO, DE03_NM_LOGRADOURO as DESCRICAO")
        strSQL.Append(" from DE03_ENDERECO")
        strSQL.Append(" order by 2 ")

        dt = cnn.AbrirDataTable(strSQL.ToString)


        cnn = Nothing

        Return dt
    End Function

    Public Function ObterUltimo() As Integer
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder
        Dim CodigoUltimo As Integer

        strSQL.Append(" select max(DE03_ID_ENDERECO) from DE03_ENDERECO")

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
        strSQL.Append(" from DE03_ENDERECO")
        strSQL.Append(" where DE03_ID_ENDERECO = " & Codigo)

        LinhasAfetadas = cnn.ExecutarSQL(strSQL.ToString)


        cnn = Nothing

        Return LinhasAfetadas
    End Function

    Public Function SalvarEnderecoXML(ByVal Cep As String, ByVal Logradouro As String, ByVal LogradouroCorreios As Boolean, ByVal Bairro As String, ByVal BairroCorreios As Boolean, ByVal Estado As String, ByVal Municipio As String) As DataTable
        Dim cnn As New Conexao
        Dim dt As DataTable
        Dim strSQL As New StringBuilder


        strSQL.Append(" Declare @P_NU_CEP                  VARCHAR(8) ")
        strSQL.Append(" Declare @P_NM_LOGRADOURO           VARCHAR(MAX) ")
        strSQL.Append(" Declare @P_IN_LOGRADOURO_CORREIOS  BIT         ")
        strSQL.Append(" Declare @P_NM_BAIRRO               VARCHAR(MAX)")
        strSQL.Append(" Declare @P_IN_BAIRRO_CORREIOS      BIT         ")
        strSQL.Append(" Declare @P_NM_MUNICIPIO            VARCHAR(MAX) ")
        strSQL.Append(" Declare @P_SG_UF                   Char(2) ")
        strSQL.Append(" Declare @P_ID_LOGRADOURO_CEP       INT  ")

        strSQL.Append(" Set @P_NU_CEP = '" & Cep & "'")
        strSQL.Append(" Set @P_NM_LOGRADOURO = '" & Logradouro & "'")
        strSQL.Append(" Set @P_IN_LOGRADOURO_CORREIOS = " & IIf(LogradouroCorreios = False, 0, 1))
        strSQL.Append(" Set @P_NM_BAIRRO = '" & Bairro & "'")
        strSQL.Append(" Set @P_IN_BAIRRO_CORREIOS = " & IIf(BairroCorreios = False, 0, 1))
        strSQL.Append(" Set @P_NM_MUNICIPIO = '" & Municipio & "'")
        strSQL.Append(" Set @P_SG_UF = '" & Estado & "'")


        strSQL.Append(" exec SP_ENDERECO  @P_NU_CEP, @P_NM_TIPO_LOGRADOURO, @P_NM_LOGRADOURO, @P_IN_LOGRADOURO_CORREIOS, @P_NM_BAIRRO, @P_IN_BAIRRO_CORREIOS, @P_NM_MUNICIPIO, @P_SG_UF, @P_ID_LOGRADOURO_CEP OUT ")
        strSQL.Append(" Select @P_ID_LOGRADOURO_CEP ")

        dt = cnn.AbrirDataTable(strSQL.ToString)


        cnn = Nothing

        Return dt
    End Function

    Public Function PesquisarCepLocal(ByVal cep As String) As DataTable
        Dim cnn As New Conexao
        Dim strSQL As New StringBuilder

        strSQL.Append(" Select * from( ")
        strSQL.Append(" select uf, NomeSemAcento As cidade, '' AS logradouro, '' AS bairro, cep, '' AS tipo_logradouro ")
        strSQL.Append(" From DBCEP.dbo.cep_unico ")
        strSQL.Append(" union all ")
        strSQL.Append(" Select UF, cidade,logradouro, bairro, cep, tp_logradouro as tipo_logradouro ")
        strSQL.Append(" From DBCEP.dbo.logradouros ")
        strSQL.Append(" ) as endereco ")
        strSQL.Append(" where Replace(cep, '-', '') =  '" & cep & "' ")


        Return cnn.AbrirDataTable(strSQL.ToString)

    End Function


End Class