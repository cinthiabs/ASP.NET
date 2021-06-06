#region 'Inicio'  
--Para compilar no SQL Management Studio, tanto o #region, quanto o #endregion devem estar comentados (--#region --#endregion)  
  
--Para usar o grupamento por #region, tanto o #region, quanto o #endregion devem estar descomentados e a linguagem do Notepad++ deve ser C#.  
  
  
  
  
  
CREATE PROCEDURE [dbo].[spi_ExportaMaravilhas_da_Terra_Ocorren_Reenvia]                          
 @REMUFID Varchar(2),                        
 @ArquivoName  VARCHAR(200) = NULL OUTPUT,                                            
 @Producao INT = 1                                           
                    
                  
 /*                   
declare                    
 @REMUFID Varchar(2),                        
 @ArquivoName  VARCHAR(200),                                            
 @Producao INT                                           
                      
                    
 SET @REMUFID = 'SP'        
-- SET @REMUFID = 'ES'        
-- SET @REMUFID = 'PE'        
       
      
 SET @ArquivoName   = NULL                    
 SET @Producao  = 1                                           
  */                   
                    
                    
AS                                            
SET NOCOUNT ON                                            
                                          
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED                                            
                                          
/************************************************************************************************************************                                        
Autor:  Rogerio Jordano                                    
Data:  12/01/2021                                        
Objetivo: Exportar os arquivos de remessas Maravilhas da Terra padrao proceda 3.1                                           
Exec:  spi_ExportaMaravilhas_da_Terra_Ocorren    
  
--************************************************************************************************************************     
Alteração  
--************************************************************************************************************************     
Autor:  Edesoft N2                                    
Data:  13/04/2021                                        
Objetivo: Ajustes incluindo #region para facilitar o grupamento e manuntenção / Correções dos sql das etapas                                         
Exec:  spi_ExportaMaravilhas_da_Terra_Ocorren_Debug    
                               
************************************************************************************************************************/                                        
                                  
                                            
/*********************************************************************************                                            
--Declaracão de Variaveis                                            
--*********************************************************************************/                                            
DECLARE                                           
  @Header  CHAR(204) ,                                           
  @Trailer CHAR(204)  ,                                            
  @Linha   CHAR(204) ,                                           
  @Linha123 CHAR(204) ,                                          
  @Empresa  CHAR(15)   ,                                            
  @NomeEmpresa CHAR(30)                          
                          
                       
                                          
/*********************************************************************************                                            
-- Variaveis para Seleção                                            
--*********************************************************************************/                                            
DECLARE                                           
  @RemRazaoSocial VARCHAR(50),                                           
  @DesRazaoSocial VARCHAR(50),                                          
  @RemCGC         VARCHAR(20)  ,                                           
  @DesCGC         VARCHAR(20),                                           
  @CGC_CLI        VARCHAR(20),                                           
  @CGCRet         VARCHAR(20) ,                                           
  @numArEBCT      VARCHAR(20),                                          
  @Rem000         VARCHAR(50)  ,                                           
  @Des000         VARCHAR(50),                                           
  @Codigo         VARCHAR(2)   ,                                           
  @Ult_Status     VARCHAR(2),                                          
  @Documento      VARCHAR(25) ,                                           
  @UltStatus      INT ,                                 
  @DtEmissao      DATETIME,                                           
  @PedidoID       INT ,                            
  @Desistencia    TINYINT  ,                                           
  @PedidoOriId    INT,                   
  @DtMovimento    CHAR(8),                                           
  @DtBaixa        DATETIME ,                                           
  @Conta          VARCHAR(7),                                           
  @Tentativa      TINYINT ,                                        
  @LoteExportaID  INT,                                           
  @CodigoStatus   CHAR(5) ,                                  @LoteExportaIDCP INT ,                                           
  @NomeRecep      VARCHAR(40),                                           
  @dtMudancaSt    CHAR(12),                                        
  @CodigoStatus1  CHAR(5),                                           
  @CodigoStatus2  CHAR(5) ,                                          
  @CodigoStatus3  CHAR(5),                                           
  @dtMudancaSt1   CHAR(12),                                          
  @dtMudancaSt2   CHAR(12),                                           
  @dtMudancaSt3   CHAR(12),                                          
  @Envia INT,                                          
  @Cont INT,                          
                    
  @descricaocli VARCHAR(70),                  
  @CodigoSequoia VARCHAR(2)                  
                  
                                  
/*********************************************************************************                                            
-- Vari?veis Locais                                            
--*********************************************************************************/                                            
DECLARE                                           
  @CodigoCliente   INT,                                           
  @CodigoProduto   INT,                                            
  @Comando         VARCHAR(1000),                                
  @DtHoje          DATETIME,                                           
  @DtIni           DATETIME,                                          
  @DtFim           DATETIME,                                           
  @DtAux           CHAR(8) ,                                          
  @Arquivo         VARCHAR(100) ,                                           
  @ArquivoC        VARCHAR(100),                                          
  @DtHojeDDMMAA    CHAR(6),                                           
  @HoraHHMM        CHAR(4),                                          
  @SiglaFilial     CHAR(5),                                           
  @Serie           CHAR(5) ,                                          
  @Numero          CHAR(12),                                     
  @DataEmissao     CHAR(8),                                          
  @SerieNF         CHAR(3),                                           
  @NF              CHAR(10),                                          
  @DataEntrega     DATETIME,                                       
  @RGRecep         CHAR(20),                                            
  @DtExp_Tentativa1 DATETIME,                                           
  @DtExp_Tentativa2 DATETIME,                                            
  @DtExp_Tentativa3 DATETIME,                                           
  @DataEntrega1     DATETIME,                                          
  @DataEntrega2     DATETIME,                                           
  @DataEntrega3     DATETIME,                                          
  @Dia      VARCHAR(15)  ,                                           
  @Hora     VARCHAR(15),                                          
  @Minuto   VARCHAR(15)  ,                                           
  @Segundo  VARCHAR(15),                                          
  @Sequencia  INT  ,                                           
 @SequenciaC INT                                           
                            
                      
                                          
/*********************************************************************************                            
-- Variaveis de Codigo Interno do Cliente  --                                             
--*********************************************************************************/                                            
DECLARE                                           
  @ClienteID      INT ,                          
  @ProdutoID      INT,                                          
                                         
                                         
  @Prod          INT ,                                           
  @Categoria INT,                                           
  @ReceitaID INT                                            
                                          
/*********************************************************************************                                            
-- Variaveis de Calculo                                            
--*********************************************************************************/                                            
DECLARE                                           
  @QtdeEnt  INT  ,                                  
  @QtdeDev  INT ,                                          
  @QtdeCol  INT  ,                                           
  @QtdeTri  INT ,                                          
  @Seq      INT  ,                                           
  @QtdeTot  INT ,                                          
  @QtdeTotCPP  INT ,                                       
  @SeqCPP     INT,                                          
  @QtdeEntCPP INT ,                                           
  @QtdeDevCPP  INT,                                          
  @QtdeColCPP  INT                                            
                                          
/**************************************************************                                            
Vari?vel para acumular o nome do Arquivo, para gerar arquivo de saida para envio de e-mail                                            
**************************************************************/                                            
DECLARE @ArquivoSaida VarChar(500)                                            
                                 
-- Configuracao de Variaveis do Servidor e FileExchange Para usar no BCP                                            
Declare @_Servidor  VarChar(20)                                            
Declare @_Usuario   VarChar(10)                                            
Declare @_Senha     VarChar(15)                                            
Declare @_PathArquivos   VarChar(40)                                            
Declare @_PathSistemas   VarChar(40)                                            
Declare @_PathFormato    VarChar(40)                                            
                                          
Select                          
  @_Servidor = rTrim(Servidor),                                            
  @_Usuario = rTrim(Usuario),                                            
  @_Senha = rTrim(Senha),                                            
  @_PathArquivos = rTrim(PathArquivos),                                            
  @_PathSistemas = rTrim(PathSistemas)                                            
From                                   
  dbo.Fn_GetConfiguracaoControleArquivos(1) -- 0=Objetos 1=Encomendas                                            
                                          
Set @_PathFormato = 'Lay30.fmt'                                             
                                          
/*********************************************************************************                                            
--Inicio da Exportação                             
--*********************************************************************************/                                            
                                          
 Set @ArquivoSaida = ''                                        
 SET @SiglaFilial  = ' '                                            
 SET @Serie        = ' '                                           
 SET @Numero       = ' '                                          
 SET @DtEmissao    = ' '                                            
                                          
/*********************************************************************************                                            
-- Verifica Data                           
--*********************************************************************************/                                            
SET @DtHoje = GETDATE()                                            
SET @DtIni  = dbo.AchaDataInic(DATEADD(Day, -1, @DtHoje))                                            
SET @DtAux  = dbo.fmtDataAAAAMMDD(@DtIni)                                            
SET @DtIni  = CAST(@DtAux AS DATETIME)                                            
SET @DtAux  = dbo.fmtDataAAAAMMDD(@DtHoje)                                            
SET @DtFim  = CAST(@DtAux AS DATETIME)                                            
                                  
SET @DtHojeDDMMAA = SUBSTRING(CAST((100 + DAY(@DtHoje)) AS CHAR(3)), 2, 2) +                                            
                    SUBSTRING(CAST((100 + MONTH(@DtHoje)) AS CHAR(3)), 2, 2) +                                            
                    SUBSTRING(CAST(YEAR(@DtHoje) AS CHAR(4)), 3, 2)                                            
                                          
SET @HoraHHMM  =    SUBSTRING(CAST((100 + DATEPART(Hour, @DtHoje)) AS CHAR(3)), 2, 2) +                                            
                    SUBSTRING(CAST((100 + DATEPART(Minute, @DtHoje)) AS CHAR(3)), 2, 2)                                            
                                          
DECLARE @DT_SEMANA_PASSADA DATETIME SET @DT_SEMANA_PASSADA = DATEADD(DAY, -15, GETDATE())                  
  
--SET @DT_SEMANA_PASSADA = '2021-01-13 09:14:03.240'  
   
  
--Dados do cliente                                  
SET @CodigoCliente = 1157                  
--clienteid = 1161                  
--set @REMUFID = 'SP'                        
                                          
                      
SELECT @ClienteID = ClienteID ,@CGC_CLI = CGC FROM Cliente (NOLOCK) WHERE Codigo = @CodigoCliente --1157                                            
                        
                      
select top 1 @ProdutoID = ProdutoID                         
from produto p (nolock)                         
inner join cidade c (nolock) on p.cidadeid_coleta = c.cidadeid                        
Inner join uf uf (nolock) on c.ufid = uf.ufid                        
where p.clienteid = @Clienteid  and receitaid = 11 and uf.sigla = @REMUFID and bativoprod = 1                                       
                      
              
select top 1 @ProdutoID = ProdutoID                         
from produto p (nolock)                         
inner join cidade c (nolock) on p.cidadeid_coleta = c.cidadeid                        
Inner join uf uf (nolock) on c.ufid = uf.ufid                        
where p.clienteid = @Clienteid  and receitaid = 11 and uf.sigla = @REMUFID and bativoprod = 1                                       
                      
                      
                      
                                          
/*********************************************************************************                                            
-- Cria Tabelas Tempor?rias                                            
--*********************************************************************************/                                            
IF EXISTS (select * FROM   dbo.SysObjects WHERE  id = Object_id(N'[tmpExpCliente]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)                                    
  DROP TABLE tmpExpCliente                                    
                          
IF EXISTS (select * FROM   dbo.SysObjects WHERE  id = Object_id(N'[tmpExpCliente342]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)                                    
  DROP TABLE tmpExpCliente342                                    
                          
IF EXISTS (select * FROM   dbo.SysObjects WHERE  id = Object_id(N'[tmpClienteUpdate]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)                                    
  DROP TABLE tmpClienteUpdate                                    
                          
IF EXISTS (select * FROM   dbo.SysObjects WHERE  id = Object_id(N'[tmpLogStatus]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)                                    
  DROP TABLE tmpLogStatus                                    
           
                          
CREATE TABLE tmpExpCliente (                                            
  IdTexto  INT IDENTITY (1, 1) NOT NULL,                                            
  Texto    CHAR(225) NOT NULL ,                                     
  seq INT                                          
 )                                            
                            
 CREATE TABLE tmpExpCliente342 (                                              
  IdTexto  INT IDENTITY (1, 1) NOT NULL,                                              
  Texto    CHAR(225), --NOT NULL                                           
  seq INT                                          
 )                                            
                                          
 CREATE TABLE tmpClienteUpdate (                      
  PedidoID    INT,                                              
  EhReentrega  BIT                                            
 )                                            
                         
 CREATE TABLE tmpLogStatus (                                            
  nrt int,                  
  status int,                  
  json varchar(max),                  
  retorno [varchar](max),                  
  data datetime,                  
  sucesso int,                  
  clienteid int,                  
  nf varchar(50),                  
  chaveNF varchar(50),                  
  serieNF varchar(50),                  
  Protocolo varchar(50),                  
  Tiposervico int,                  
  Doc_envio varchar(5),                  
  Numerodoc int                  
)                  
                  
                    
/*********************************************************************************                                            
-- Nome do Arquivo                                            
--*********************************************************************************/                                            
/*--EPP */                                              
SELECT TOP 1                                  
   @Sequencia = ISNULL(MAX(ISNULL(sequencia,0)),0)+1                                   
FROM                          
  TB_Sequencia (nolock)                                            
Where                                  
  clienteID = @ClienteID                                  
                                  
IF @Sequencia IS NULL                                            
  SET @Sequencia = 1                                            
                                          
                                            
SET @Dia   = Left(dbo.fmtDataDDMMAAAA(getdate()),8)                                            
SET @Hora   = SUBSTRING(CAST((100 + DATEPART(Hour, getdate())) AS CHAR(3)), 2, 2) -- Hora                                            
SET @Minuto   = SUBSTRING(CAST((100 + DATEPART(Minute, getdate())) AS CHAR(3)), 2, 2) -- Minuto                        
                                          
SET @Arquivo = 'Maravilhas_da_Terra\Exporta\Ocorren\Reenvia\OCO_'+@REMUFID+ Replace(Replace(Convert(Varchar, getDate(), 104) + Substring(Convert(Varchar, getDate(), 120),12,5),'.',''),':','') + '.txt'                                            
SET @ArquivoName = @Arquivo                                            
  
                                    
IF @Producao = 1                                          
  BEGIN                                  
    IF NOT EXISTS (SELECT 1 FROM TB_Sequencia WHERE Clienteid = @ClienteID)                                            
      BEGIN                                  
        INSERT INTO TB_Sequencia (Sequencia, Clienteid) VALUES (@Sequencia,@ClienteID)                                            
      END                                  
    ELSE                                  
      BEGIN                                  
        UPDATE TB_Sequencia                                  
        SET Sequencia =  @Sequencia                                  
        WHERE                                  
       Clienteid = @ClienteID                                  
      END                                  
  END                                  
                                  
SET @Cont = 1   
                                      
#region 'Informações para o Header do Arquivo'                                        
/**********************************************************************************                                            
-- Cria Lote de Exportação                                            
--*********************************************************************************/                                             
                      
IF @Producao = 1                                          
  BEGIN                                          
    INSERT INTO  LoteExporta (                                          
      DtExporta,                                           
      ClienteID ,                                          
      ProdutoID,                                           
      Usuario ,                                          
      FilialID,                                          
      Qtde ,                                          
      QtdeEntregue,                                           
      QtdeDev,                                          
      Arquivo,                                           
      bExporta ,             
      Versao)                                            
                                            
    VALUES (                                       
      @DtHoje,                                           
      @ClienteID,                                             
      @ProdutoID,                                           
      'export'  ,                                             
      34,                                           
      0,                                                
      0,                                           
      0,                                              
      @Arquivo,                                           
      0,                                              
 1)                                          
                                            
    SELECT @LoteExportaID = @@IDENTITY                        
                                    
  END                                          
ELSE                                          
  BEGIN                                          
    SET @LoteExportaID = 203                                           
  END                                          
#endregion 'Cria Lote de Exportação '                                         
#region 'Cria Lote de Exportação '   
/*********************************************************************************/                                            
/*-- Informações para o Header do Arquivo  */                                 
/*********************************************************************************/                                            
SET @Rem000  = 'Sequoia Logistica e Transportes S.A.'                                            
SET @Des000  = 'MaravilhasTerra'                           
                        
if @remufid = 'SP' SET @Empresa = '01599101000193'                            
Else if  @remufid = 'PE' SET @Empresa = '01599101000517'                         
Else if  @remufid = 'ES' SET @Empresa = '01599101006124'                         
else set @Empresa = '00000000000000'                        
                        
SET @NomeEmpresa = 'Sequoia Logistica e Transportes S.A.'                                            
SET @Header   = '1' + @Empresa  + SPACE(40)                                            
#endregion 'Informações para o Header do Arquivo'  
#region 'Inicia Gravacao das linhas que sao iguais para todos os registros'      
/****************************************************************************************************************                                            
--Inicia Gravacao das linhas que sao iguais para todos os registros                         
--****************************************************************************************************************/                                             
SET @Linha = '000'       -- Codigo Registro                                            
  + dbo.fmtCampo(@Rem000, 35, 1) /*-- Razao Social Remetente  */                                          
  + dbo.fmtCampo(@Des000, 35, 1) /*-- Razao Social Destinatario */                                           
  + @DtHojeDDMMAA /*-- Data */                                           
  + @HoraHHMM /*-- Hora */                                           
  + 'OCO' /*-- Nome do arquivo  */                                          
  + SUBSTRING(CAST((100 + DAY(@DtHoje)) AS CHAR(3)), 2, 2)         /*-- Dia */                                           
  + SUBSTRING(CAST((100 + MONTH(@DtHoje)) AS CHAR(3)), 2, 2)      /* -- M?s */                                           
  + SUBSTRING(CAST((100 + DATEPART(Minute, @DtHoje)) AS CHAR(3)), 2, 1)  /* -- Minuto */                                            
  + SPACE(107)                                          -- Filler                         
  +dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
SET @Cont = @Cont+1                                          
                                          
INSERT INTO tmpExpCliente (Texto, seq) VALUES (@Linha, @Cont) /*-- Grava Registro '000' */                                           
#endregion 'Inicia Gravacao das linhas que sao iguais para todos os registros'       
#region 'Gravacao do Registro '340' '                                            
/*******************************************************************************************************                                            
---Gravacao do Registro '340'                                            
--******************************************************************************************************/        
SET @Linha = '340' -- Codigo Registro                                          
 + 'OCO'   /*-- Identifica??o do documento */                                             
 + SUBSTRING(CAST((100 + DAY(@DtHoje)) AS CHAR(3)), 2, 2) /*-- Dia */                                             
 + SUBSTRING(CAST((100 + MONTH(@DtHoje)) AS CHAR(3)), 2, 2)   /*-- Mês*/                                              
 + '20'   /*-- ANO  select SUBSTRING(CAST((100 + YEAR(getdate())) AS CHAR(3)), 2, 2)   */                                             
 + SUBSTRING(CAST((100 + DATEPART(Hour, @DtHoje)) AS CHAR(3)), 2, 2)     /*-- Hora */                                           
 + SUBSTRING(CAST((100 + DATEPART(Minute, @DtHoje)) AS CHAR(3)), 2, 2)   /*-- Minuto */                                             
 + SUBSTRING(CAST((100 + DATEPART(Second, @DtHoje)) AS CHAR(3)), 2, 1)   /*-- Segundo */                              
 + SPACE(181) /*-- Filler  */                                         
 +dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
SET @Cont = @Cont+1                                          
                                          
INSERT INTO tmpExpCliente (Texto, seq) VALUES (@Linha, @Cont) -- Insere Linha registro 340    
                                          
#endregion 'Gravacao do Registro '340' '     
#region 'Gravacao do Registro '341' '                                               
/****************************************************************************************                                             
--Gravacao do Registro '341'                                              
--****************************************************************************************/                                            
SET @Linha = '341' /*-- Codigo Registro  */                                          
  + dbo.fmtCampo(@Empresa, 14, 1)/* -- CNPJ Transportadora  */                                          
  + dbo.fmtCampo(@NomeEmpresa, 40, 1) /*-- Nome Transportadora   */                                   
  + SPACE(141) /*-- Filler  */                                          
  + dbo.fmtCampo(@Cont, 4, 0)                                    
                                  
SET @Cont = @Cont+1                                          
                                          
INSERT INTO tmpExpCliente (Texto, seq) VALUES (@Linha, @Cont) /*-- Insere Linha registro 340 */                                        
#endregion 'Gravacao do Registro '341' '   
#region 'Inicializa Variaveis'                                            
/*********************************************************************************                                            
-- Inicializa Variaveis                                            
--*********************************************************************************/                                            
SET @QtdeTot = 0                   
SET @Seq     = 1                                              
SET @QtdeEnt = 0                                              
SET @QtdeDev = 0                                         
SET @QtdeCol = 0                                              
SET @QtdeTotCPP = 0                                              
SET @SeqCPP     = 1                                              
SET @QtdeEntCPP = 0                                              
SET @QtdeDevCPP = 0                                              
SET @QtdeColCPP = 0                                            
                     
#endregion 'Inicializa Variaveis  '                                     
  
#endregion 'Inicio'      
  
#region '1.    Busca Remessas coletadas                                  -  (Recepcao Fisica)      '         
SET @CodigoSequoia = '90'                  
                                      
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                           
 SELECT      PED.RemRazaoSocial                                                                     AS RemRazaoSocial,  
             PED.DesRazaoSocial                                                                            AS DesRazaoSocial,   
             ISNULL(PR.CGC_Cli_DPC, PED.RemCGC)                                                            AS RemCGC,           
             PED.Documento                                                                                 AS Documento,        
             PED.PedidoID                                                                                  AS PedidoID,         
             PED.Desistencia                                                                               AS Desistencia,      
             dbo.fmtCampo(@CodigoSequoia,5,0)                                                              AS CodigoStatus,     
             dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)                            AS NomeRecep,                                              
             dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)                               AS RGRecep,                                           
             PED.dtEmissao                                                                                 AS DataEntrega,      
             dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa3, ISNULL(PED.DtAtuBaixa2, PED.DtAtuBaixa1)))    AS dtMudancaSt,                                              
             PD.CampoAlfa1                                                                                 AS SerieNF,          
             PD.CampoAlfa2                                                                                 AS NF,               
             dbo.fn_AchaStatusCliente (@ClienteID,@CodigoSequoia)                                          AS Codigo,                    
             PED.Tentativa                                                                                 AS Tentativa ,       
             PED.Produtoid                                                                                 AS Prod,             
             PED.DesCGC                                                                                    AS DesCGC,          
             PED.numArEBCT                                                                                 AS numArEBCT,        
             SUBSTRING(ocSq.descricaocli,1 ,70)                                                            AS descricaocli      
 FROM        PedidoMestre            AS  PED  (NOLOCK)        
 --INNER JOIN  FILTRA_NRT              AS  F             ON  F.NRT          = PED.PedidoId      
 INNER JOIN  PedidoDetalhe           AS  PD   (NOLOCK) ON PED.PedidoId    = PD.PedidoId                                            
 INNER JOIN  ControleExportaTracking AS  ET   (NOLOCK) ON PED.PedidoID    = ET.PedidoID                                            
 INNER JOIN  Produto                 AS  Pro  (NOLOCK) ON PED.ProdutoID   = Pro.ProdutoID                                           
 LEFT  JOIN  PedidoRedespacho        AS  PR   (NOLOCK) ON PED.PedidoID    = PR.PedidoID                       
 inner join  ocorrenciassequoia      AS  ocSq (NOLOCK) ON (ocSq.idCLiente = @ClienteID and ocSq.statusseg = @CodigoSequoia)                  
 WHERE      1 = 1                                             
        AND  PED.ClienteID  = @ClienteID                              
        AND  ped.remUF      = @REMUFID       --UF                 
          AND  PD.CampoAlfa2 IS NOT NULL                                            
          AND  Pro.ReceitaID IN (11,31)                                            
        AND  ET.DtExp_RecepcaoCD IS NULL     --IMPEDE REENVIO                                        
          AND  PED.dtemissao  IS NOT NULL      --Data da Recepcao Fisica                                         
                                          
                   
 OPEN tmpCurIbex               
                                          
 FETCH NEXT FROM tmpCurIbex INTO                                            
                 @RemRazaoSocial ,                                           
                 @DesRazaoSocial,                                            
                 @RemCGC,                                           
                 @Documento,                                            
                 @PedidoID,                                           
                 @Desistencia,                         
                 @CodigoStatus,                                           
                 @NomeRecep,                                            
                 @RGRecep,                                           
                 @DataEntrega,                                            
                 @dtMudancaSt,                                           
                 @SerieNF,                                            
                 @NF,                                           
                 @Codigo,                                            
                 @Tentativa ,                                           
                 @Prod,                                           
                 @DesCGC,          
                 @numArEBCT,                  
                 @descricaocli                  
                              
/*********************************************************************************************************************                                            
-- Monta Linha de Detalhe - Retorno do Cliente --                                             
--********************************************************************************************************************/                                             
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
  SET @Seq       = @Seq + 1                                            
  SET @QtdeEnt    = @QtdeEnt + 1                                            
  SET @QtdeTot    = @QtdeTot + 1                                              
  SET @CGCRet = @RemCGC                                             
                                          
  /*******************************************************************************************************************                                            
  --Gravacao do Registro '342'                      
  --*******************************************************************************************************************/                                            
  /*1.IDENTIFICADOR DE REGISTRON 3 01 M "342" */                              
  SET @Linha123 = '342'                                                         
                       
     /*2.CGC DA EMPRESA EMISSORA DA NOTA FISCAL N 14 04 C */                                
     + dbo.fmtCampo(@CGCRet, 14, 1)                                       
                                     
  /*3.SÉRIE DA NOTA FISCAL A 3 18 C */                                
  + dbo.fmtCampo(@SerieNF, 3, 3)                                          
                                
     /*4.NÚMERO DA NOTA FISCAL N 8 21 M  */                                
     + dbo.fmtCampo(@NF, 8, 0)                                         
                                     
     /*5.CÓDIGO DE OCORRÊNCIA NA ENTREGA N 2 29 M */                                  
  + dbo.fmtCampo(@Codigo, 2,0)                                          
                                
     /*6.DATA DA OCORRÊNCIA N 8 31 M DDMMAAAA  */                                
  + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)                                      
     + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)                                 
  + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)                                        
       
  /*7.HORA DA OCORRÊNCIA N 4 39 C HHMM  */                                
     + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)                             
     + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)                           
                                
     /*8.CÓDIGO DE OBSERVAÇÃO DE OCORRÊNCIA NA ENTRADA N 2 43 C                                 
                                 01 = Devolução/recusa total                                 
                                 02 = Devolução/recusa parcial 03 = Aceite/entrega por acordo  */                                
     + SPACE(2)                                              
                                
     /*9.TEXTO EM FORMATO LIVRE A 70 45 C TEXTO COMPLEMENTAR EXPLICATIVO  */                                
  + dbo.fmtCampo(@descricaocli,70,1)                              
                          
      
 + dbo.fmtCampo(@Documento, 9, 2)                                             
     + SPACE(62)                                                                
     + dbo.fmtCampo(@numArEBCT, 13,1)                                          
     + dbo.fmtCampo(@Cont, 4, 0)                                          
                      
                      
/*10.FILLER A 6 115 C PREENCHER COM BRANCOS */                                
                                
  SET @Cont = @Cont+1                                          
                                          
  INSERT INTO tmpExpCliente (Texto, seq) VALUES (@Linha123, @Cont)                                          
            
  /*******************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*******************************************************************************************************************/                                           
  IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                        
                                          
  IF @Producao = 1                                          
  BEGIN                                          
    /*******************************************************************************************************************                                            
    -- Inseri na ExportaPedido                                            
    --*******************************************************************************************************************/                             
    IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
    BEGIN                                          
      INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                          
    END              
                                          
    /*******************************************************************************************************************                                        
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
                      
 --1 tmpLogStatus                  
                   
 --Revisar                  
   UPDATE ControleExportaTracking SET DtExp_RecepcaoCD = GETDATE() WHERE PedidoID = @PedidoID                     
                     
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                    
  END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
     @RemRazaoSocial ,                                           
     @DesRazaoSocial,                                            
     @RemCGC,                                           
     @Documento,                                            
     @PedidoID,                                           
     @Desistencia,                                            
     @CodigoStatus,                                           
     @NomeRecep,                                            
     @RGRecep,                                           
     @DataEntrega,                                            
     @dtMudancaSt,                                           
     @SerieNF,                                            
@NF,                                           
     @Codigo,                                            
     @Tentativa ,                                           
     @Prod,                                            
     @DesCGC,                                           
     @numArEBCT,                  
  @descricaocli                  
                                         
END /*-- FIM WHILE   */                                         
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                     
                     
                 
                  
/*******************************************************************************************************************                                
-- Update DtExporta                                            
--*******************************************************************************************************************/                                            
IF @Producao = 1                      
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
      
END                                          
           
        
      
      
TRUNCATE TABLE tmpClienteUpdate                                          
                                          
#endregion '1.    Busca Remessas coletadas                                  -  (Recepcao Fisica)      '        
#region '1.1.  Busca Remessas em transferencia de filial                 -  (Confeccao de MT)      '        
SET @CodigoSequoia = 86                   
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                            
  SELECT      PED.RemRazaoSocial ,                                           
              PED.DesRazaoSocial,                                            
              ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
              PED.Documento,                                            
              PED.PedidoID ,                                           
              PED.Desistencia,                                            
              dbo.fmtCampo(ISNULL(PED.status3, ISNULL(PED.status2, PED.status1)),5,0) AS CodigoStatus  ,                                            
              dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)      AS Receptor      ,                                           
              dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)      AS RGReceptor    ,                        
              tt.DtInclusao AS DataEntrega   ,                                            
              dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa3, ISNULL(PED.DtAtuBaixa2, PED.DtAtuBaixa1))) ,                                            
              PD.CampoAlfa1 ,                                           
              PD.CampoAlfa2  ,                                            
              dbo.fn_AchaStatusCliente (@ClienteID,@CodigoSequoia),                                          
              PED.Tentativa  ,                                            
              PED.Produtoid  ,                                           
              PED.DesCGC,                                           
              PED.numArEBCT,                                           
              SUBSTRING(ocSq.descricaocli,1 ,70)                              
  FROM        PedidoMestre            AS  PED (NOLOCK)    
 -- INNER JOIN  FILTRA_NRT              AS  F            ON  F.NRT          = PED.PedidoId       
  INNER JOIN  PedidoDetalhe           AS  PD  (NOLOCK) ON PED.PedidoId    = PD.PedidoId                 
  INNER JOIN  ControleExportaTracking AS  ET  (NOLOCK) ON PED.PedidoID    = ET.PedidoID                                            
  INNER JOIN  Produto                 AS Pro  (NOLOCK) ON PED.ProdutoID   = Pro.ProdutoID                                            
  INNER JOIN  TrackTransf             AS  TT  (NOLOCK) ON PED.PedidoID    = TT.PedidoID --and tt.bativo = 1 --Edesoft N2 13/04/2021                                           
  LEFT  JOIN  PedidoRedespacho        AS  PR  (NOLOCK) ON PED.PedidoID    = PR.PedidoID                                          
  INNER JOIN  ocorrenciassequoia    ocSq      (NOLOCK) ON (ocSq.idCLiente = @ClienteID and ocSq.statusseg = @CodigoSequoia)                              
  WHERE       1 = 1                                         
          AND PED.ClienteID  = @ClienteID                            
          and ped.remUF      = @REMUFID                        
          AND PD.CampoAlfa2 IS NOT NULL                                            
          AND Pro.ReceitaID IN (11,31)                                         
          AND ET.DtExp_RecepcaoCD IS NOT NULL         -- Recepcao Fisica  
    AND (tt.dtInclusao IS NOT NULL)             -- Determina pedidos da etapa    
   AND ET.DtExp_Transferencia IS NULL          -- IMPEDE REENVIO                              
                    
  OPEN tmpCurIbex                                            
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,               
    @RemCGC,                                           
    @Documento,                                            
    @PedidoID,                                           
    @Desistencia,                                            
    @CodigoStatus,                                           
    @NomeRecep,                                            
    @RGRecep,                                           
    @DataEntrega,                                            
    @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                            
    @Tentativa ,                                           
    @Prod,                        
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                        
/********************************************************************************************************************                                            
-- Monta Linha de Detalhe - Retorno Ibex --                                             
--********************************************************************************************************************/                                             
WHILE @@FETCH_STATUS = 0                    
BEGIN                                            
      
  SET @Seq       = @Seq + 1                                            
  SET @QtdeEnt    = @QtdeEnt + 1                                            
  SET @QtdeTot    = @QtdeTot + 1                                              
  SET @CGCRet = @RemCGC                                             
                                          
  /*******************************************************************************************************************                                            
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************************/                                            
  SET @Linha123 = '342'                     /* -- Codigo Registro  */                        
   + dbo.fmtCampo(@CGCRet, 14, 1)           /* -- CNPJ Remetente  */                                          
   + dbo.fmtCampo(@SerieNF, 3, 3)           /* -- S?rie da Nota Fiscal */                                              
   + dbo.fmtCampo(@NF, 8, 0)                /* -- NF   */                                           
   + dbo.fmtCampo(@Codigo, 2,0)            /*  -- Codigo de OCO?ncia */                            
   + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)  /* -- Dia Entrega  */                                            
   + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2) /* -- M?s Entrega */                                             
   + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)          /* -- Ano Entrega */                                             
   + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)     /*-- Hora Entrega  */                                            
   + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)    /*-- Minuto Entrega  */                                            
   + SPACE(2)                                              
   + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status */                                             
   + dbo.fmtCampo(@Documento, 9, 2)                                              
   + SPACE(62)                              
   + dbo.fmtCampo(@numArEBCT, 13,1)                                          
   + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
   SET @Cont = @Cont+1                                     
                    
   INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                           
                                          
   /*******************************************************************************************************************                                            
   --Grava na Tabela Temporaria os Registro Encontrados                                            
   --*******************************************************************************************************************/                                           
   IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
     INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
   IF @Producao = 1                                          
   BEGIN                                          
     /*******************************************************************************************************************                                            
     -- Insere na ExportaPedido                                            
     --*******************************************************************************************************************/                                          
     IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
       INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                          
                                          
     /*******************************************************************************************************************                                            
     -- Update ControleExportaTracking                                            
     --*******************************************************************************************************************/                                          
--2 tmpLogStatus                      
                   
 --Revisar                  
 UPDATE ControleExportaTracking SET DtExp_Transferencia = GETDATE() WHERE PedidoID = @PedidoID                                           
                  
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
,NULL, 1, 'OCO', 1)                  
                                     
   END                                          
                                          
   FETCH NEXT FROM tmpCurIbex INTO             
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                 
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli              
                                          
END                                           
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/*******************************************************************************************************************                                            
-- Update DtExporta                                            
--*******************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
TRUNCATE TABLE tmpClienteUpdate                                          
                                          
#endregion '1.1.  Busca Remessas em transferencia de filial                 -  (Confeccao de MT)      '   
#region '1.1.a Busca Remessas em transferencia de filial                 -  (Recepcao de MT)       '   
SET @CodigoSequoia = '87'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                            
  SELECT      PED.RemRazaoSocial ,                                           
              PED.DesRazaoSocial,                                            
              ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
              PED.Documento,                                            
              PED.PedidoID ,                                 PED.Desistencia,                                            
              dbo.fmtCampo(ISNULL(PED.status3, ISNULL(PED.status2, PED.status1)),5,0) AS CodigoStatus  ,                                            
              dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)      AS Receptor      ,                                           
              dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)      AS RGReceptor    ,                                              
              tt.DtInclusao AS DataEntrega   ,                                            
              dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa3, ISNULL(PED.DtAtuBaixa2, PED.DtAtuBaixa1))) ,                                  
              PD.CampoAlfa1 ,                                           
              PD.CampoAlfa2  ,                       
              dbo.fn_AchaStatusCliente (@ClienteID,@CodigoSequoia),                                          
              PED.Tentativa  ,                                            
              PED.Produtoid  ,                                           
              PED.DesCGC,                                           
              PED.numArEBCT,                                           
              SUBSTRING(ocSq.descricaocli,1 ,70)                              
  FROM        PedidoMestre            AS  PED (NOLOCK)  
  --INNER JOIN  FILTRA_NRT              AS  F            ON  F.NRT          = PED.PedidoId       
  INNER JOIN  PedidoDetalhe           AS  PD  (NOLOCK) ON PED.PedidoId    = PD.PedidoId                                            
  INNER JOIN  ControleExportaTracking AS  ET  (NOLOCK) ON PED.PedidoID    = ET.PedidoID                    
  INNER JOIN  Produto                 AS  Pro (NOLOCK) ON PED.ProdutoID   = Pro.ProdutoID                                            
  INNER JOIN  TrackTransf             AS  TT  (NOLOCK) ON PED.PedidoID    = TT.PedidoID --and tt.bativo = 1  Edesoft N2 13/04/2021                                          
  LEFT  JOIN  PedidoRedespacho        AS  PR  (NOLOCK) ON PED.PedidoID    = PR.PedidoID                                          
  INNER JOIN  ocorrenciassequoia      AS ocSq (NOLOCK) ON (ocSq.idCLiente = @ClienteID and ocSq.statusseg = @CodigoSequoia)                              
  WHERE       1 = 1                                         
         AND  PED.ClienteID  = @ClienteID                            
         AND  ped.remUF = @REMUFID                  --UF                    
         AND  PD.CampoAlfa2 IS NOT NULL                                            
         AND  Pro.ReceitaID IN (11,31)                                         
         AND  ET.DtExp_RecepcaoCD IS NOT NULL        -- Recepcao Fisica  
     
         --AND  ET.DtExp_Transferencia IS NULL       -- Estava errado, não pode enviar recepcao se não exportou a transferencia  
         --AND  ET.DtExp_Transferencia IS NOT NULL   -- Estava errado, esse campo é usado para controlar o ocorren de transferencia   
          
         AND  (tt.DtRecep IS NOT NULL)           -- Determina pedidos da etapa   
   AND  ET.DtExp_RecepcaoTransferencia IS NOT NULL   -- IMPEDE REENVIO   
  
    
  OPEN tmpCurIbex                                            
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                         
    @DesRazaoSocial,                                            
    @RemCGC,                                           
    @Documento,                                            
    @PedidoID,        
    @Desistencia,                                            
    @CodigoStatus,                                           
    @NomeRecep,                                            
    @RGRecep,                      
    @DataEntrega,                                            
    @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                            
    @Tentativa ,                                       
    @Prod,                        
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                        
/********************************************************************************************************************                                            
-- Monta Linha de Detalhe - Retorno Ibex --                                             
--********************************************************************************************************************/                                             
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
  SET @Seq       = @Seq + 1                                            
  SET @QtdeEnt    = @QtdeEnt + 1                                            
  SET @QtdeTot    = @QtdeTot + 1                                              
  SET @CGCRet = @RemCGC                                             
                                          
  /*******************************************************************************************************************                                            
  --Gravacao do Registro '342'                                --*******************************************************************************************************************/                                            
  SET @Linha123 = '342'                     /* -- Codigo Registro  */                                         
   + dbo.fmtCampo(@CGCRet, 14, 1)           /* -- CNPJ Remetente  */                                          
   + dbo.fmtCampo(@SerieNF, 3, 3)           /* -- S?rie da Nota Fiscal */                                              
   + dbo.fmtCampo(@NF, 8, 0)                /* -- NF   */                                           
   + dbo.fmtCampo(@Codigo, 2,0)            /*  -- Codigo de OCO?ncia */                      
   + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)  /* -- Dia Entrega  */                                            
   + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2) /* -- M?s Entrega */                                             
   + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)          /* -- Ano Entrega */                                             
   + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)     /*-- Hora Entrega  */                                            
   + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)    /*-- Minuto Entrega  */                                            
   + SPACE(2)                                              
   + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status */                                             
   + dbo.fmtCampo(@Documento, 9, 2)                                              
   + SPACE(62)                                          
   + dbo.fmtCampo(@numArEBCT, 13,1)                                          
   + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
   SET @Cont = @Cont+1                                          
                                             
   INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                           
                                          
   /*******************************************************************************************************************                                            
   --Grava na Tabela Temporaria os Registro Encontrados                                            
   --*******************************************************************************************************************/                                       
   IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
     INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
   IF @Producao = 1                                          
   BEGIN                                          
     /*******************************************************************************************************************                                            
     -- Insere na ExportaPedido                                            
     --*******************************************************************************************************************/                                          
     IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
       INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                          
                                          
     /*******************************************************************************************************************                                            
     -- Update ControleExportaTracking                                            
     --*******************************************************************************************************************/                                          
--2 tmpLogStatus                      
                   
 -- Corrigido pois estava errado  13/04/2021 - Edesoft N2     
 -- UPDATE ControleExportaTracking SET DtExp_Transferencia = GETDATE() WHERE PedidoID = @PedidoID   
   
    UPDATE ControleExportaTracking SET DtExp_RecepcaoTransferencia = GETDATE() WHERE PedidoID = @PedidoID                
  
         
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)   
          ,NULL, 1, 'OCO', 1)                  
                                        
   END                                          
                                          
   FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                         
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli                  
                                          
END                                           
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/*******************************************************************************************************************                                            
-- Update DtExporta                                            
--*******************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
TRUNCATE TABLE tmpClienteUpdate                                          
                                   
#endregion '1.1.a Busca Remessas em transferencia de filial                 -  (Recepcao de MT)       '  
#region '2.    Busca Remessas que estão em Rota para a Entrega ou Coleta -  (FAE)                  '  
SET @CodigoSequoia = '94'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                            
  SELECT      PED.RemRazaoSocial ,                                           
              PED.DesRazaoSocial,                                            
              ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
              PED.Documento,                  
              PED.PedidoID ,                                           
              PED.Desistencia,                                            
              dbo.fmtCampo(ISNULL(PED.status3, ISNULL(PED.status2, PED.status1)),5,0) AS CodigoStatus  ,                                            
              dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)      AS Receptor      ,                                            
              dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)      AS RGReceptor    ,                                              
              T.dtAtribuicao AS DataEntrega   ,                                            
              dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa3, ISNULL(PED.DtAtuBaixa2, PED.DtAtuBaixa1))) ,                                            
              PD.CampoAlfa1 ,                                           
              PD.CampoAlfa2 ,                                            
              dbo.fn_AchaStatusCliente (@ClienteID,@CodigoSequoia),                                          
              PED.Tentativa  ,                        
              PED.Produtoid  ,                                           
              PED.DesCGC,                                           
              PED.numArEBCT,                  
              SUBSTRING(ocSq.descricaocli,1 ,70)                              
  FROM        PedidoMestre             AS  PED (NOLOCK)     
  --INNER JOIN  FILTRA_NRT               AS  F            ON  F.NRT          = PED.PedidoId       
  INNER JOIN  PedidoDetalhe            AS  PD  (NOLOCK) ON PED.PedidoId    = PD.PedidoId         
  INNER JOIN  Produto                  AS Pro  (NOLOCK) ON PED.ProdutoID   = Pro.ProdutoID                                               
  INNER JOIN  Texlog_Malha..TrackFAE   AS  T   (NOLOCK) ON PED.PedidoID    = T.PedidoID -- and t.bativo = 1 Edesoft N2 13/04/2021                                            
  INNER JOIN  Texlog_Malha..FAE        AS  FA  (NOLOCK) ON T.FAEID         = FA.FAEID                                              
  INNER JOIN  ControleExportaTracking  AS  ET  (NOLOCK) ON PED.PedidoID    = ET.PedidoID                                             
  LEFT  JOIN  PedidoRedespacho         AS  PR  (NOLOCK) ON PED.PedidoID    = PR.PedidoID         
  INNER JOIN  ocorrenciassequoia       AS ocSq (NOLOCK) ON (ocSq.idCLiente = @ClienteID and ocSq.statusseg = @CodigoSequoia)                  
  WHERE   1 = 1                                              
       AND    PED.ClienteID     = @ClienteID                                
       AND    ped.remUF         = @REMUFID                        
       AND    Pro.ReceitaID in (11,22)                                              
       AND    ET.DtExp_Transferencia IS NOT NULL     -- Tem que já ter enviado ocorren de MT                                                           
       AND    T.DtAtribuicao   IS NOT NULL           -- Determina pedidos da etapa                                        
       AND    ET.DtExp_EmRota  IS NULL               -- IMPEDE REENVIO        
      
OPEN tmpCurIbex                                            
                                          
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,                                            
    @RemCGC,                                           
    @Documento,                             
    @PedidoID,                                           
    @Desistencia,                                            
    @CodigoStatus,                                           
    @NomeRecep,                                            
    @RGRecep,                                           
    @DataEntrega,                                     
   @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                            
    @Tentativa ,                                           
    @Prod,                                            
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                          
/***********************************************************************************************************************                                            
 -- Monta Linha de Detalhe - Retorno Cliente --              
--***********************************************************************************************************************/                                            
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                             
      
 IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) )  /*--(@ProdutoID,@ProdutoID_1) -- EPP   */                                         
  BEGIN                                            
    SET @CGCRet  = @RemCGC                                          
    SET @Seq  = @Seq + 1                                            
    SET @QtdeTot  = @QtdeTot + 1                                            
  END                                            
  ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaid in (22,70)) /*--(@ProdutoID_2,@ProdutoID_3)   */                                         
  BEGIN                                            
    SET @CGCRet    = @RemCGC                                           
    SET @SeqCPP     = @SeqCPP + 1                                            
    SET @QtdeTotCPP = @QtdeTotCPP + 1                                            
  END                                            
                                          
 /* --SET @Codigo = '05'  */                                          
             
  /*************************************************************************************************************                                            
  --Gravacao do Registro '342'                                
  --*************************************************************************************************************/                                            
  SET @Linha123 = '342'                   /*-- Codigo Registro  */                                         
    + dbo.fmtCampo(@CGCRet, 14, 1)       /* -- CNPJ Remetente */                                           
    + dbo.fmtCampo(@SerieNF, 3, 3)        /*-- S?rie da Nota Fiscal   */                                         
    + dbo.fmtCampo(@NF, 8, 0)    /* -- NF       */                                     
    + dbo.fmtCampo(@Codigo, 2,0)         /* -- Codigo de OCO?ncia */                                           
    + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)  /* -- Dia Entrega   */                                         
    + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)/* -- M?s Entrega  */                                          
    + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4) /*-- Ano Entrega  */                                          
    + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2) /* -- Hora Entrega   */                                         
    + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)  /*-- Minuto Entrega  */                                         
    + SPACE(2)                                              
    + dbo.fmtCampo(@descricaocli,70,1)    /*-- Descricao Status */                                             
    + dbo.fmtCampo(@Documento, 9, 2)                                             
    + SPACE(62)                                    /*  -- Filler   */                                           
    + dbo.fmtCampo(@numArEBCT, 13,1)                                          
    + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
    SET @Cont = @Cont+1              
                                              
    INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                 
    /*******************************************************************************************************************                                            
    --Grava na Tabela Temporaria os Registro Encontrados                                            
    --*******************************************************************************************************************/                                           
    IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
      INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
    IF @Producao = 1                                          
    BEGIN                                          
      /*************************************************************************************************************                                            
      -- Inseri na ExportaPedido                                            
      --*************************************************************************************************************/                                            
      IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
      BEGIN                                           
        IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
        BEGIN                                            
          INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                            
        END                                            
                                          
        IF @Desistencia = 1                                     
          SET @QtdeDev = @QtdeDev + 1                   
      END                                             
      ELSE IF @Prod IN  (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaid in (22,70)) /*--(@ProdutoID_2,@ProdutoID_3) -- CPP*/                                            
      BEGIN                                            
        IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaIDCP )                                            
        BEGIN                                  
          INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaIDCP, @PedidoID)                                   
        END                                            
                                          
        IF @Desistencia = 1                                            
          SET @QtdeDevCPP = @QtdeDevCPP + 1                                            
      END                                           
                                          
      /*******************************************************************************************************************                                            
      -- Update ControleExportaTracking                                            
      --*******************************************************************************************************************/                                            
     --3 tmpLogStatus                  
 --Revisar                  
      UPDATE ControleExportaTracking SET DtExp_EmRota = GETDATE() WHERE PedidoID = @PedidoID                                           
                  
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                
    END                                          
                                          
    FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,               
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                     @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
  @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli                  
                     
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
                                          
/*************************************************************************************************************                                            
-- Update DtExporta                                            
--*************************************************************************************************************/               
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
TRUNCATE TABLE tmpClienteUpdate                                          
                                  
#endregion '2.    Busca Remessas que estão em Rota para a Entrega ou Coleta -  (FAE)                  '   
#region '3.A   Busca Remessas Entregues (Status 1)                       -  (Entregue 1a Tentativa)'  
SET @CodigoSequoia = '0'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                 
  SELECT                                           
              PED.RemRazaoSocial ,                                           
              PED.DesRazaoSocial,                                            
              ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
              PED.Documento,                                            
              PED.PedidoID ,                                           
              PED.Desistencia,                                            
              dbo.fmtCampo(ISNULL(PED.status1,0),5,0) AS CodigoStatus  ,                                            
              dbo.fmtCampo(ISNULL(Receptor1,''),30,1)  AS Receptor      ,                                            
              dbo.fmtCampo(ISNULL(RGRecep1,''),20,1)      AS RGReceptor    ,                                              
              Isnull(PED.dtentrega1,''),                                
              dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa1,'')),                                            
              PD.CampoAlfa1 ,                                           
              PD.CampoAlfa2  ,                                            
              dbo.fn_AchaStatusCliente (@ClienteID,PED.status1),                                          
              PED.Tentativa  ,                                            
              PED.Produtoid  ,                                   
              PED.DesCGC,                                           
              PED.numArEBCT,                  
              PED.status1,                  
              SUBSTRING(ocSq.descricaocli,1 ,70)                               
  FROM        PedidoMestre            AS  PED (NOLOCK)   
  --INNER JOIN  FILTRA_NRT              AS  F            ON  F.NRT       = PED.PedidoId       
  INNER JOIN  PedidoDetalhe           AS  PD  (NOLOCK) ON PED.PedidoId = PD.PedidoId                                              
  INNER JOIN  ControleExportaTracking AS  ET  (NOLOCK) ON PED.PedidoID = ET.PedidoID                                              
  INNER JOIN  Produto                 AS  Pro (NOLOCK) ON PED.ProdutoID = Pro.ProdutoID                         
  LEFT  JOIN  PedidoRedespacho        AS   PR (nolock) ON PED.PedidoID = PR.PedidoID                                           
  INNER JOIN  ocorrenciassequoia      AS ocSq          on (ocSq.idCLiente = @ClienteID and ocSq.statusseg = PED.status1)                  
  WHERE  1   = 1                                                
         AND  PED.ClienteID  = @ClienteID                             
         and  ped.remUF = @REMUFID                        
         AND  Pro.ReceitaID IN (11,31)                                             
         AND  ET.DtExp_Tentativa1 IS NULL     
         AND  PED.DtEntrega1  IS NOT NULL    
      --   AND  PED.Status   = 2            -- Finalizado -- Adicionado Edesoft N2 13/04/2021  
      --   AND  PED.Status1  = 1        -- Entregue   -- Adicionado Edesoft N2 13/04/2021  
                                
OPEN tmpCurIbex                                            
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,         
    @DesRazaoSocial,                                  
@RemCGC       ,                                           
    @Documento  ,                                              
    @PedidoID      ,                                           
    @Desistencia   ,                                            
    @CodigoStatus   ,                        
    @NomeRecep     ,                                            
    @RGRecep      ,                                           
    @DataEntrega   ,             
    @dtMudancaSt    ,                                    
    @SerieNF       ,                                            
    @NF         ,                                           
    @Codigo        ,                                            
    @Tentativa   ,                                           
    @Prod       ,                                            
    @DesCGC    ,                                          
    @numArEBCT,                  
 @CodigoSequoia,                  
 @descricaocli                  
                                          
/********************************************************************************************************************                                            
-- Monta Linha de Detalhe - Retorno Ibex -- 27                                            
--********************************************************************************************************************/                                            
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
      
  SET @Seq       = @Seq + 1                                            
  SET @QtdeEnt    = @QtdeEnt + 1                                        
  SET @QtdeTot    = @QtdeTot + 1                                              
  SET @CGCRet = @RemCGC                                            
                                          
  /*******************************************************************************************************************                                            
  --Gravacao do Registro '342'                                      
  --*******************************************************************************************************************/                                            
  SET @Linha123 = '342'                        /*   -- Codigo Registro  */                                         
    + dbo.fmtCampo(@CGCRet, 14, 1)             /* -- CNPJ Remetente  */                                          
    + dbo.fmtCampo(@SerieNF, 3, 3)            /*     -- S?rie da Nota Fiscal  */                                          
    + dbo.fmtCampo(@NF, 8, 0)                  /*    -- NF              */                              
    + dbo.fmtCampo(@Codigo, 2,0)                /* -- Codigo de OCO?ncia */                                           
    + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)           /* -- Dia Entrega */                                           
    + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)        /*  -- M?s Entrega */                                           
    + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega  */                          
    + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)    /* -- Hora Entrega  */                                          
    + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)   /* -- Minuto Entrega */                                          
    + SPACE(2)                                              
    + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status   */                                           
    + dbo.fmtCampo(@Documento, 9, 2)                                             
    + SPACE(62)                                      /*-- Filler*/                                              
    + dbo.fmtCampo(@numArEBCT, 13,1)                                          
    + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
  SET @Cont = @Cont+1                                          
                                          
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /*******************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*******************************************************************************************************************/                                 
  IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN                                          
    /*******************************************************************************************************************                                            
    -- Insere na ExportaPedido                                          
    --*******************************************************************************************************************/                                          
    IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
      INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                          
                                          
    /*******************************************************************************************************************                                            
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--4 tmpLogStatus                      
 --Revisar                  
    UPDATE ControleExportaTracking SET DtExp_Tentativa1 = @DtHoje, export = 1 WHERE PedidoID = @PedidoID                                            
                       
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                 END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                                         
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,       
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @CodigoSequoia,                  
   @descricaocli                  
                                   
                                
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                   
/*******************************************************************************************************************                        
-- Update DtExporta                                            
--*******************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
                                
TRUNCATE TABLE tmpClienteUpdate                                          
                  
                  
#endregion '3.A   Busca Remessas Entregues (Status 1)                       -  (Entregue 1a Tentativa)'   
#region '3.B   Busca Remessas Entregues (Status 2)                       -  (Entregue 2a Tentativa)'  
SET @CodigoSequoia = '0'                  
      
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                 
  SELECT                                           
              PED.RemRazaoSocial ,                                           
              PED.DesRazaoSocial,                                            
              ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
              PED.Documento,                                            
              PED.PedidoID ,                                           
              PED.Desistencia,                                            
              dbo.fmtCampo(ISNULL(PED.status2,0),5,0) AS CodigoStatus  ,                                            
              dbo.fmtCampo(ISNULL(Receptor2,''),30,1)  AS Receptor      ,                                            
              dbo.fmtCampo(ISNULL(RGRecep2,''),20,1)      AS RGReceptor    ,                                              
              Isnull(PED.dtentrega2,''),                                
              dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa2,'')),                                            
              PD.CampoAlfa1 ,                                           
              PD.CampoAlfa2  ,                                            
              dbo.fn_AchaStatusCliente (@ClienteID,PED.status2),                                          
              PED.Tentativa  ,                                            
              PED.Produtoid  ,                                           
              PED.DesCGC,                   
              PED.numArEBCT,                  
              PED.status2,                  
              SUBSTRING(ocSq.descricaocli,1 ,70)                       
  FROM        PedidoMestre            AS  PED (NOLOCK)  
  --INNER JOIN  FILTRA_NRT              AS  F            ON F.NRT        = PED.PedidoId       
  INNER JOIN  PedidoDetalhe           AS  PD  (NOLOCK) ON PED.PedidoId = PD.PedidoId                                              
  INNER JOIN  ControleExportaTracking AS  ET  (NOLOCK) ON PED.PedidoID = ET.PedidoID                                              
  INNER JOIN  Produto                 AS Pro  (NOLOCK) ON PED.ProdutoID = Pro.ProdutoID                                             
  LEFT  JOIN  PedidoRedespacho        AS PR   (nolock) ON PED.PedidoID = PR.PedidoID                   
  INNER JOIN ocorrenciassequoia       AS ocSq          on (ocSq.idCLiente = @ClienteID and ocSq.statusseg = PED.status2)                  
  WHERE      1   = 1                                                
        AND  PED.ClienteID  = @ClienteID                             
        AND  ped.remUF = @REMUFID                        
        AND  Pro.ReceitaID IN (11,31)                                             
        AND  ET.DtExp_Tentativa2 IS NULL      
        AND  PED.DtEntrega2 IS NOT NULL                                                
        --  AND  PED.Status   = 2            -- Finalizado -- Adicionado Edesoft N2 13/04/2021  
        --  AND  PED.Status2  = 1       -- Entregue   -- Adicionado Edesoft N2 13/04/2021  
     
OPEN tmpCurIbex                                            
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,                                  
    @RemCGC       ,                                           
    @Documento  ,                          
    @PedidoID      ,                                           
    @Desistencia   ,                                            
    @CodigoStatus   ,                        
    @NomeRecep     ,                                            
    @RGRecep      ,                                           
    @DataEntrega   ,                                              
    @dtMudancaSt    ,                                           
    @SerieNF       ,                                            
    @NF         ,                                           
    @Codigo        ,                                            
    @Tentativa   ,                                           
    @Prod       ,                                            
    @DesCGC    ,                                          
    @numArEBCT,                  
 @CodigoSequoia,                  
 @descricaocli                  
                                          
/********************************************************************************************************************                                            
-- Monta Linha de Detalhe - Retorno Ibex -- 27                                            
--********************************************************************************************************************/                                            
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
      
  SET @Seq       = @Seq + 1             
  SET @QtdeEnt    = @QtdeEnt + 1                                        
  SET @QtdeTot    = @QtdeTot + 1                                              
  SET @CGCRet = @RemCGC                                            
                        
  /*******************************************************************************************************************                                            
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************************/                                            
  SET @Linha123 = '342'                        /*   -- Codigo Registro  */                                         
    + dbo.fmtCampo(@CGCRet, 14, 1)             /* -- CNPJ Remetente  */                                          
    + dbo.fmtCampo(@SerieNF, 3, 3)            /*     -- S?rie da Nota Fiscal  */                                          
    + dbo.fmtCampo(@NF, 8, 0)                  /*    -- NF              */                              
    + dbo.fmtCampo(@Codigo, 2,0)                             /* -- Codigo de OCO?ncia */                                           
    + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)           /* -- Dia Entrega */                                           
    + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)        /*  -- M?s Entrega */                                           
    + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega  */                          
    + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)    /* -- Hora Entrega  */                                          
    + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)   /* -- Minuto Entrega */                                          
    + SPACE(2)                                              
    + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status   */                                           
    + dbo.fmtCampo(@Documento, 9, 2)                                             
    + SPACE(62)     /*-- Filler*/                                              
    + dbo.fmtCampo(@numArEBCT, 13,1)                                          
    + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
  SET @Cont = @Cont+1                                          
                                          
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /*******************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*******************************************************************************************************************/                                           
  IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN              
    /*******************************************************************************************************************                                            
    -- Insere na ExportaPedido                                          
    --*******************************************************************************************************************/                                          
    IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
      INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                          
                                          
    /*******************************************************************************************************************                                            
  -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--4 tmpLogStatus                      
 --Revisar                  
    UPDATE ControleExportaTracking SET DtExp_Tentativa2 = @DtHoje, export = 1 WHERE PedidoID = @PedidoID                                            
                                          
                  
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                  
      
  END                                          
              
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
  @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
    @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                                      
      @DesCGC,                                           
      @numArEBCT,                  
   @CodigoSequoia,                  
   @descricaocli                  
                                   
                                
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/*******************************************************************************************************************                                           
-- Update DtExporta                                            
--*******************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                 
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                       
END                                          
                                
TRUNCATE TABLE tmpClienteUpdate                                          
                  
#endregion '3.B   Busca Remessas Entregues (Status 2)                    -  (Entregue 2a Tentativa)'      
#region '3.C   Busca Remessas Entregues (Status 3)                       -  (Entregue 3a Tentativa)'     
SET @CodigoSequoia = '0'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                 
  SELECT                                           
               PED.RemRazaoSocial ,                                           
               PED.DesRazaoSocial,                                            
               ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
               PED.Documento,                                            
               PED.PedidoID ,                                           
               PED.Desistencia,                                            
               dbo.fmtCampo(ISNULL(PED.status3,0),5,0) AS CodigoStatus  ,                           
               dbo.fmtCampo(ISNULL(Receptor3,''),30,1)  AS Receptor      ,                                            
               dbo.fmtCampo(ISNULL(RGRecep3,''),20,1)      AS RGReceptor    ,                                              
               Isnull(PED.dtentrega3,''),                                
               dbo.fmtDataAAAAMMDDHHMM(ISNULL(PED.DtAtuBaixa3,'')),                                            
               PD.CampoAlfa1 ,                                           
               PD.CampoAlfa2  ,                                            
               dbo.fn_AchaStatusCliente (@ClienteID,PED.status3),                                          
               PED.Tentativa  ,     
               PED.Produtoid  ,                                           
               PED.DesCGC,                                           
               PED.numArEBCT,                  
               PED.status3,                  
               SUBSTRING(ocSq.descricaocli,1,70)                  
                   
  FROM         PedidoMestre            AS  PED (NOLOCK)      
  --INNER JOIN   FILTRA_NRT              AS  F             ON  F.NRT       = PED.PedidoId      
  INNER JOIN   PedidoDetalhe           AS  PD  (NOLOCK)  ON PED.PedidoId = PD.PedidoId                                              
  INNER JOIN   ControleExportaTracking AS  ET  (NOLOCK)  ON PED.PedidoID = ET.PedidoID                                              
  INNER JOIN   Produto                 AS  Pro (NOLOCK)  ON PED.ProdutoID = Pro.ProdutoID                                             
  LEFT  JOIN   PedidoRedespacho        AS  PR  (nolock)  ON PED.PedidoID = PR.PedidoID                                           
  INNER JOIN   ocorrenciassequoia ocSq on (ocSq.idCLiente = @ClienteID and ocSq.statusseg = PED.status3)                  
  WHERE  1   = 1                                                
          AND  PED.ClienteID  = @ClienteID                             
          AND  PED.remUF      = @REMUFID                        
          AND  PD.CampoAlfa2 IS NOT NULL                                              
          AND  Pro.ReceitaID IN (11,31)                                             
          AND  ET.DtExp_Tentativa3 IS NULL      
          AND  PED.DtEntrega3 IS NOT NULL                                               
         -- AND  PED.Status   = 2          -- Finalizado -- Adicionado Edesoft N2 13/04/2021  
         -- AND  PED.Status3  = 1       -- Entregue   -- Adicionado Edesoft N2 13/04/2021  
    
OPEN tmpCurIbex                                            
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                 
    @DesRazaoSocial,                                  
    @RemCGC       ,                                           
    @Documento  ,                                              
    @PedidoID      ,                             
    @Desistencia   ,                                            
    @CodigoStatus   ,                        
    @NomeRecep     ,                                            
    @RGRecep      ,                                           
    @DataEntrega   ,                                              
    @dtMudancaSt    ,                                           
    @SerieNF       ,                                            
    @NF         ,                                           
    @Codigo        ,                                            
    @Tentativa   ,                                           
    @Prod       ,                                            
    @DesCGC    ,                                          
    @numArEBCT,                  
 @CodigoSequoia,                  
 @descricaocli                  
                                          
/********************************************************************************************************************                                            
-- Monta Linha de Detalhe - Retorno Ibex -- 27                                            
--********************************************************************************************************************/                                            
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
      
  SET @Seq       = @Seq + 1                                            
  SET @QtdeEnt    = @QtdeEnt + 1                                        
  SET @QtdeTot    = @QtdeTot + 1                                              
  SET @CGCRet = @RemCGC                     
                                          
  /*******************************************************************************************************************                                            
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************************/                                            
  SET @Linha123 = '342'                        /*   -- Codigo Registro  */                                         
    + dbo.fmtCampo(@CGCRet, 14, 1)             /* -- CNPJ Remetente  */                                          
    + dbo.fmtCampo(@SerieNF, 3, 3)            /*     -- S?rie da Nota Fiscal  */                                          
    + dbo.fmtCampo(@NF, 8, 0)                  /*    -- NF              */                              
    + dbo.fmtCampo(@Codigo, 2,0)                             /* -- Codigo de OCO?ncia */                                           
    + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)           /* -- Dia Entrega */                                           
    + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)        /*  -- M?s Entrega */                                           
    + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega  */                          
    + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)    /* -- Hora Entrega  */                                          
    + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)   /* -- Minuto Entrega */                                          
    + SPACE(2)                                              
    + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status   */                                           
    + dbo.fmtCampo(@Documento, 9, 2)                                             
    + SPACE(62)                                      /*-- Filler*/                                              
 + dbo.fmtCampo(@numArEBCT, 13,1)                                          
    + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
  SET @Cont = @Cont+1                                          
                                          
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /*******************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*******************************************************************************************************************/                                           
  IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN                                          
    /*******************************************************************************************************************                                            
    -- Insere na ExportaPedido                                          
    --*******************************************************************************************************************/                                          
    IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
      INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                          
                                          
    /*******************************************************************************************************************                                            
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--4 tmpLogStatus                      
 --Revisar                  
    UPDATE ControleExportaTracking SET DtExp_Tentativa3 = @DtHoje, export = 1 WHERE PedidoID = @PedidoID                         
                       
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                  
  END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                        
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                  
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @CodigoSequoia,                  
   @descricaocli                  
                                   
                                
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/*******************************************************************************************************************                                           
-- Update DtExporta                                            
--*******************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
                                
TRUNCATE TABLE tmpClienteUpdate                                          
                  
                  
#endregion '3.C   Busca Remessas Entregues (Status 3)                    -  (Entregue 3a Tentativa)'         
#region '16.   Busca Remessas devolvidas e recebidas no CD do Cliente    -  (Recepcao de MDC)      '       
SET @CodigoSequoia = '97'            
SET @DT_SEMANA_PASSADA = DATEADD(DAY, -60, GETDATE())           
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                            
  SELECT         PED.RemRazaoSocial ,                                           
                 PED.DesRazaoSocial,                                            
                 ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                         
                 PED.Documento,                                            
                 PED.PedidoID ,                                           
                 PED.Desistencia,                                            
                 dbo.fmtCampo(@CodigoSequoia,5,0) AS CodigoStatus  ,                                            
                 dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)     AS Receptor      ,                                            
                 dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)        AS RGReceptor    ,                                              
                 Isnull(MC.DtManif,'') AS DataEntrega   ,                                            
                 dbo.fmtDataAAAAMMDDHHMM(ISNULL(MC.DtRecep,'')) ,                                            
                 PD.CampoAlfa1 ,                                           
                 PD.CampoAlfa2  ,                                            
                 dbo.fn_AchaStatusCliente (PED.ClienteID,@CodigoSequoia),                                          
                 PED.Tentativa  ,                                            
                 PED.Produtoid  ,                                           
                 PED.DesCGC,                                           
                 PED.numArEBCT,                  
                 SUBSTRING(ocSq.descricaocli,1,70)                  
  FROM           PedidoMestre               AS  PED (NOLOCK)    
  --INNER JOIN     FILTRA_NRT                 AS  F            ON  F.NRT          = PED.PedidoId       
  INNER JOIN     PedidoDetalhe              AS PD   (NOLOCK) ON PED.PedidoId    = PD.PedidoId                                            
  INNER JOIN     TrackDevolucao             AS TC   (NOLOCK) ON PED.PedidoID    = TC.PedidoID                                            
  INNER JOIN     ManifestoDevolucao         AS MC   (NOLOCK) ON TC.ManifDevID   = MC.ManifDevID                                            
  INNER JOIN     ControleExportaTracking    AS ET   (NOLOCK) ON PED.PedidoID    = ET.PedidoID                                            
  INNER JOIN     PedidoIDEtiqueta           AS PE   (NOLOCK) ON PED.PedidoID    = PE.PedidoID       
  INNER JOIN     Produto                    AS Pro  (NOLOCK) ON PED.ProdutoID   = Pro.ProdutoID                                            
  LEFT  JOIN     PedidoRedespacho           AS PR   (nolock) ON PED.PedidoID    = PR.PedidoID                                          
  INNER JOIN     ocorrenciassequoia         AS ocSq          ON (ocSq.idCLiente = @ClienteID and ocSq.statusseg = @CodigoSequoia)                  
  WHERE          1 = 1                                           
           AND   PED.ClienteID       = @ClienteID            
           AND   PED.remUF           = @REMUFID                        
           AND   ET.DtExp_RecepcaoCD IS NOT NULL              -- Recepcao Fisica                                                                                                                       
           AND   MC.DtManif IS NOT NULL                       -- Determina pedidos da etapa      
           AND   MC.DtRecep          IS NOT NULL              -- Que ainda não tenham sido recebidos  
           AND   ET.DtExp_RecepcaoDevolucaoCliente  IS NULL   -- IMPEDE REENVIO    
     
        
OPEN tmpCurIbex                                            
                                          
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,                                            
    @RemCGC,                                           
    @Documento,                                            
    @PedidoID,                                           
    @Desistencia,                                            
    @CodigoStatus,         
    @NomeRecep,                                   
    @RGRecep,                                           
    @DataEntrega,                                            
    @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                            
    @Tentativa ,                                           
    @Prod,                                            
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                          
/***********************************************************************************************************************                                            
-- Monta Linha de Detalhe -                                          
--*********************************************************************************************************************/                                
WHILE @@FETCH_STATUS = 0                                            
BEGIN       
  IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
  BEGIN                                            
    SET @CGCRet  = @RemCGC                                            
    SET @Seq  = @Seq + 1                                            
    SET @QtdeTot    = @QtdeTot + 1                                           
    SET @QtdeDev    = @QtdeDev + 1                                            
  END                            
  ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
  BEGIN                                            
    SET @CGCRet    = @DesCGC                                            
    SET @SeqCPP     = @SeqCPP + 1                                            
    SET @QtdeTotCPP = @QtdeTotCPP + 1                                            
    SET @QtdeDevCPP = @QtdeDevCPP + 1                                            
  END                                            
  /*********************************************************************************************************                                            
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************/                                            
  SET @Linha123 = '342'                         /*  -- Codigo Registro  */                                         
     + dbo.fmtCampo(@CGCRet, 14, 1)            /*  -- CNPJ Remetente     */                                       
     + dbo.fmtCampo(@SerieNF, 3, 3)             /*    -- S?rie da Nota Fiscal   */                                         
     + dbo.fmtCampo(@NF, 8, 0)                   /*   -- NF      */                                      
     + dbo.fmtCampo(@Codigo, 2,0)                             /* -- Codigo de OCO?ncia */                            
     + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)         /*   -- Dia Entrega  */                                          
     + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)       /*   -- M?s Entrega   */                                         
     + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega */                                           
     + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)   /*  -- Hora Entrega*/                                            
     + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)  /*  -- Minuto Entrega */                                          
     + SPACE(2)                                        
     + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status  */                                            
     + dbo.fmtCampo(@Documento, 9, 2)                                             
     + SPACE(62)                                     /* -- Filler    */                                          
     + dbo.fmtCampo(@numArEBCT, 13,1)                                          
     + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
  SET @Cont = @Cont+1                                          
                                  
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /**************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*************************************************************************************************************/                                          
 IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN                                        
    /****************************************************************************************************************                                            
    -- Insere na ExportaPedido                                            
    --***************************************************************************************************************/                                        
    IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                           
      END                                            
    END                                            
    ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaIDCP )                                            
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaIDCP, @PedidoID)                                            
      END                                            
    END                                            
                                          
    /*******************************************************************************************************************                                            
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--15 tmpLogStatus                  
 --Revisar                  
    UPDATE ControleExportaTracking SET /*DtExp_DevolucaoCliente = GETDATE(),*/          
                                 DtExp_RecepcaoDevolucaoCliente = GETDATE()          
    WHERE PedidoID = @PedidoID                         
                   
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                  
  END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                       
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                         
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli                  
END                           
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/***************************************************************************************************************                                            
-- Update DtExporta                                            
--***************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
                                          
TRUNCATE TABLE tmpClienteUpdate                     
                                          
#endregion '16.   Busca Remessas devolvidas e recebidas no CD do Cliente    -  (Recepcao de MDC)      '     
#region '17.a  Busca Remessas MDT - Em processo de devolução             -  (Confeccao de MDT)     '   
SET @CodigoSequoia = '88'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                            
  SELECT     PED.RemRazaoSocial ,                                           
             PED.DesRazaoSocial,                                            
             ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
             PED.Documento,                                            
             PED.PedidoID ,                                           
             PED.Desistencia,                                            
             dbo.fmtCampo(@CodigoSequoia,5,0) AS CodigoStatus  ,                                            
             dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)     AS Receptor      ,                                            
             dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)      AS RGReceptor    ,                           
             Isnull(MC.DtManif,'') AS DataEntrega   ,                                            
             dbo.fmtDataAAAAMMDDHHMM(ISNULL(MC.DtRecep,'')) ,                                            
             PD.CampoAlfa1 ,                                           
             PD.CampoAlfa2  ,                                            
             dbo.fn_AchaStatusCliente (PED.ClienteID,@CodigoSequoia),                                          
             PED.Tentativa  ,                                            
             PED.Produtoid  ,                                           
             PED.DesCGC,                                           
             PED.numArEBCT,                  
             SUBSTRING(ocSq.descricaocli,1,70)                      
  FROM       PedidoMestre            AS PED (NOLOCK)         
  --INNER JOIN FILTRA_NRT              AS  F           ON  F.NRT              = PED.PedidoId       
  INNER JOIN PedidoDetalhe           AS PD  (NOLOCK) ON PED.PedidoId        = PD.PedidoId                                            
  INNER JOIN TrackDevTransp          AS TC  (NOLOCK) ON PED.PedidoID        = TC.PedidoID                           
  INNER JOIN ManifestoDevTransp      AS MC  (NOLOCK) ON TC.ManifDevTranspID = MC.ManifDevTranspID                           
  INNER JOIN ControleExportaTracking AS ET  (NOLOCK) ON PED.PedidoID        = ET.PedidoID                                            
  INNER JOIN PedidoIDEtiqueta        AS PE  (NOLOCK) ON PED.PedidoID        = PE.PedidoID                     
  INNER JOIN Produto                 AS Pro (NOLOCK) ON PED.ProdutoID       = Pro.ProdutoID                                            
  LEFT  JOIN PedidoRedespacho        AS PR  (nolock) ON PED.PedidoID        = PR.PedidoID                                          
  INNER JOIN ocorrenciassequoia      AS ocSq         ON (ocSq.idCLiente     = @ClienteID and ocSq.statusseg = @CodigoSequoia)                  
  WHERE  1 = 1                                           
       AND   PED.ClienteID  = @ClienteID                          
       AND   ped.remUF = @REMUFID                        
       AND   ET.DtExp_RecepcaoCD IS NOT NULL         -- Recepcao Fisica                                                                                   
       AND   MC.DtManif IS NOT NULL                  -- Determina pedidos da etapa    
       AND   ET.DtExp_DevolucaoAgente IS NULL      -- Adicionado -- IMPEDE REENVIO  
      
      
OPEN tmpCurIbex                                            
                  
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,                                            
    @RemCGC,                                           
    @Documento,                                            
    @PedidoID,                                           
    @Desistencia,                                            
    @CodigoStatus,                                           
    @NomeRecep,                                   
    @RGRecep,                                           
    @DataEntrega,                                            
    @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                            
    @Tentativa ,                                           
    @Prod,                                            
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                          
/***********************************************************************************************************************                                            
-- Monta Linha de Detalhe -                                          
--*********************************************************************************************************************/                                            
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
  IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
  BEGIN                                            
    SET @CGCRet  = @RemCGC                                            
    SET @Seq  = @Seq + 1                                            
    SET @QtdeTot    = @QtdeTot + 1                                           
    SET @QtdeDev    = @QtdeDev + 1                                            
  END                                            
  ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
  BEGIN                                            
    SET @CGCRet    = @DesCGC                                            
    SET @SeqCPP     = @SeqCPP + 1                                            
    SET @QtdeTotCPP = @QtdeTotCPP + 1                                            
    SET @QtdeDevCPP = @QtdeDevCPP + 1                                            
  END                                            
  /*********************************************************************************************************                                            
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************/                                    
  SET @Linha123 = '342'                         /*  -- Codigo Registro  */                                         
     + dbo.fmtCampo(@CGCRet, 14, 1)            /*  -- CNPJ Remetente     */                                       
     + dbo.fmtCampo(@SerieNF, 3, 3)             /*    -- S?rie da Nota Fiscal   */                                         
+ dbo.fmtCampo(@NF, 8, 0)                   /*   -- NF     */                  
     + dbo.fmtCampo(@Codigo, 2,0)                             /* -- Codigo de OCO?ncia */                                           
     + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)         /*   -- Dia Entrega  */                                          
     + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)       /*   -- M?s Entrega   */                                         
     + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega */                                           
     + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)   /*  -- Hora Entrega*/                                            
     + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)  /*  -- Minuto Entrega */                                          
     + SPACE(2)                                              
     + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status  */                                            
     + dbo.fmtCampo(@Documento, 9, 2)                                             
     + SPACE(62)                                     /* -- Filler    */                                          
     + dbo.fmtCampo(@numArEBCT, 13,1)                                          
     + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
  SET @Cont = @Cont+1                                          
                                  
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /**************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*************************************************************************************************************/                                          
 IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN                                          
    /****************************************************************************************************************                                            
    -- Insere na ExportaPedido                                            
    --***************************************************************************************************************/                                        
    IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                           
      END                                            
    END                                            
    ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                      
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaIDCP )    
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaIDCP, @PedidoID)                                            
    END                                            
    END                
                                          
    /*******************************************************************************************************************                                            
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--15 tmpLogStatus                  
 --Revisar                  
    UPDATE ControleExportaTracking SET DtExp_DevolucaoAgente = GETDATE()                    
    WHERE PedidoID = @PedidoID                         
                   
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                  
  END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli                  
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
                                          
TRUNCATE TABLE tmpClienteUpdate                                          
                  
#endregion '17.a  Busca Remessas MDT - Em processo de devolução             -  (Confeccao de MDT)     '     
#region '17.b  Busca Remessas MDT - Em processo de devolução             -  (Recepcao  de MDT)     '  
SET @CodigoSequoia = '89'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                                            
  SELECT     PED.RemRazaoSocial ,                                           
             PED.DesRazaoSocial,                                            
             ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
             PED.Documento,                                            
             PED.PedidoID ,                                           
             PED.Desistencia,                                            
             dbo.fmtCampo(@CodigoSequoia,5,0) AS CodigoStatus  ,                                            
             dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)     AS Receptor      ,                                            
             dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)      AS RGReceptor    ,                                              
             Isnull(MC.DtRecep,'') AS DataEntrega   ,                                            
             dbo.fmtDataAAAAMMDDHHMM(ISNULL(MC.DtRecep,'')) ,                                            
             PD.CampoAlfa1 ,         
             PD.CampoAlfa2  ,                                            
             dbo.fn_AchaStatusCliente (PED.ClienteID,@CodigoSequoia),                                          
             PED.Tentativa  ,                                            
             PED.Produtoid  ,                                           
             PED.DesCGC,                                           
             PED.numArEBCT,                  
             SUBSTRING(ocSq.descricaocli,1,70)                    
  FROM       PedidoMestre            AS PED (NOLOCK)    
  --INNER JOIN  FILTRA_NRT             AS  F           ON F.NRT         = PED.PedidoId       
  INNER JOIN PedidoDetalhe           AS PD  (NOLOCK) ON PED.PedidoId  = PD.PedidoId                                            
  INNER JOIN TrackDevTransp          AS TC  (NOLOCK) ON PED.PedidoID  = TC.PedidoID                           
  INNER JOIN ManifestoDevTransp      AS MC  (NOLOCK) ON TC.ManifDevTranspID = MC.ManifDevTranspID                           
  INNER JOIN ControleExportaTracking AS ET  (NOLOCK) ON PED.PedidoID  = ET.PedidoID                
  INNER JOIN PedidoIDEtiqueta        AS PE  (NOLOCK) ON PED.PedidoID  = PE.PedidoID                     
  INNER JOIN Produto                 AS Pro (NOLOCK) ON PED.ProdutoID = Pro.ProdutoID                                            
  LEFT  JOIN PedidoRedespacho        AS PR  (NOLOCK) ON PED.PedidoID = PR.PedidoID                                          
  INNER JOIN ocorrenciassequoia      AS ocSq         ON (ocSq.idCLiente = @ClienteID and ocSq.statusseg = @CodigoSequoia)                  
  WHERE      1 = 1                                           
       AND   PED.ClienteID  = @ClienteID                          
       AND   ped.remUF      = @REMUFID                        
       AND   ET.DtExp_RecepcaoCD IS NOT NULL        --Recepcao Fisica                                                                                  
       AND   MC.DtRecep IS NOT NULL   
     AND   ET.DtExp_RecepcaoDevolucaoCD IS NULL   -- Acrescentado  IMPEDE REENVIO  --Edesoft N2 - 13/04/2021  
  
      
OPEN tmpCurIbex                                            
                  
FETCH NEXT FROM tmpCurIbex INTO                                    
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,                                            
    @RemCGC,                                           
    @Documento,                                            
    @PedidoID,                                           
@Desistencia,                                            
    @CodigoStatus,                                           
    @NomeRecep,                                   
    @RGRecep,                                           
    @DataEntrega,                                            
    @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                       
    @Tentativa ,                                           
    @Prod,                                            
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                          
/***********************************************************************************************************************                         
-- Monta Linha de Detalhe -                                          
--*********************************************************************************************************************/                                            
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
  IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
  BEGIN                                            
    SET @CGCRet  = @RemCGC                                            
    SET @Seq  = @Seq + 1                                            
    SET @QtdeTot    = @QtdeTot + 1                                           
    SET @QtdeDev    = @QtdeDev + 1                   
  END                                            
  ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
  BEGIN                                            
    SET @CGCRet    = @DesCGC                                            
    SET @SeqCPP     = @SeqCPP + 1                                 
    SET @QtdeTotCPP = @QtdeTotCPP + 1                                            
    SET @QtdeDevCPP = @QtdeDevCPP + 1                                            
  END                                            
  /*********************************************************************************************************                                         
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************/                                            
  SET @Linha123 = '342'                         /*  -- Codigo Registro  */                                         
     + dbo.fmtCampo(@CGCRet, 14, 1)            /*  -- CNPJ Remetente     */                                       
     + dbo.fmtCampo(@SerieNF, 3, 3)             /*    -- S?rie da Nota Fiscal   */                                         
     + dbo.fmtCampo(@NF, 8, 0)                   /*   -- NF      */                                      
     + dbo.fmtCampo(@Codigo, 2,0)                             /* -- Codigo de OCO?ncia */                                           
     + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)         /*   -- Dia Entrega  */                                          
     + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)       /*   -- M?s Entrega   */                                         
     + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega */                                           
     + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)   /*  -- Hora Entrega*/                                           
     + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)  /*  -- Minuto Entrega */                                          
     + SPACE(2)                                              
     + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status  */                               
     + dbo.fmtCampo(@Documento, 9, 2)                                             
     + SPACE(62)                                     /* -- Filler    */                       
     + dbo.fmtCampo(@numArEBCT, 13,1)                                          
     + dbo.fmtCampo(@Cont, 4, 0)                                          
                                          
  SET @Cont = @Cont+1                                          
                                  
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /**************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                
  --*************************************************************************************************************/                                          
 IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN                                          
    /****************************************************************************************************************                                            
    -- Insere na ExportaPedido                                            
    --***************************************************************************************************************/                                        
    IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
    BEGIN              
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )                                            
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                           
      END                                            
    END                                            
    ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaIDCP )                                            
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaIDCP, @PedidoID)                                            
      END                                            
    END                                            
                                          
    /*******************************************************************************************************************                                            
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--15 tmpLogStatus                  
 --Revisar                  
    UPDATE ControleExportaTracking SET DtExp_RecepcaoDevolucaoCD = GETDATE()                    
    WHERE PedidoID = @PedidoID                         
                  
                  
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                  
  END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                                            
      @RemCGC,                   
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                        
      @RGRecep,                                           
      @DataEntrega,                                            
      @dtMudancaSt,                                           
      @SerieNF,                                         
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli                  
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/***************************************************************************************************************                           
-- Update DtExporta                                            
--***************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
                                          
TRUNCATE TABLE tmpClienteUpdate                                          
                  
#endregion '17.b  Busca Remessas MDT - Em processo de devolução             -  (Recepcao  de MDT)     '  
#region '21.   Busca Remessas Entregue na Agência                        -  (Entregue na Agencia)  '   
SET @CodigoSequoia = '21'                  
                  
DECLARE tmpCurIbex INSENSITIVE CURSOR FOR                
  SELECT      PED.RemRazaoSocial ,                                           
              PED.DesRazaoSocial,                                            
              ISNULL(PR.CGC_Cli_DPC, PED.RemCGC) ,                                           
              PED.Documento,                                            
              PED.PedidoID ,                                           
              PED.Desistencia,                       
              dbo.fmtCampo(@CodigoSequoia,5,0) AS CodigoStatus  ,                                            
              dbo.fmtCampo(ISNULL(Receptor3, ISNULL(Receptor2, Receptor1)),30,1)     AS Receptor      ,                                            
              dbo.fmtCampo(ISNULL(RGRecep3, ISNULL(RGRecep2, RGRecep1)),20,1)      AS RGReceptor    ,                                              
              Isnull(tk.DtTracking,'') AS DataEntrega   ,                                            
              dbo.fmtDataAAAAMMDDHHMM(ISNULL(tk.DtTracking,'')) ,                                            
              PD.CampoAlfa1 ,                                           
              PD.CampoAlfa2  ,                                            
              dbo.fn_AchaStatusCliente (PED.ClienteID,@CodigoSequoia),                                          
              PED.Tentativa  ,                                            
              PED.Produtoid  ,                                           
              PED.DesCGC,                                           
              PED.numArEBCT,                  
              SUBSTRING(ocSq.descricaocli,1,70)                      
  FROM        PedidoMestre           AS PED (NOLOCK)  
  --INNER JOIN  FILTRA_NRT             AS  F           ON F.NRT           = PED.PedidoId       
  INNER JOIN  PedidoIDEtiqueta       AS PE  (NOLOCK) ON PED.PedidoID    = PE.PedidoID               
  INNER JOIN  PedidoDetalhe          AS PD  (NOLOCK) ON PED.PedidoId    = PD.PedidoId                    
  INNER JOIN  tracking               AS tk  (NOLOCK) ON PED.PedidoId    = tk.PedidoId and tk.StatusID = @CodigoSequoia                       
  LEFT  JOIN  PedidoRedespacho       AS PR  (nolock) ON PED.PedidoID    = PR.PedidoID             
  INNER JOIN  ocorrenciassequoia     AS ocSq         ON (ocSq.idCLiente = @ClienteID  and ocSq.statusseg = @CodigoSequoia)                      
  LEFT  JOIN  logstatusapi2          AS ls  (NOLOCK) ON ls.Tiposervico = 1            AND           
                                                        ls.Doc_envio   = 'OCO'        AND           
                                                        ls.Numerodoc   = 1            AND           
                                                        ls.SUCESSO     = 1            AND           
                                                        PED.pedidoid   = ls.nrt       AND           
                                                        ls.status      = @CodigoSequoia                   
 WHERE        1 = 1                                           
          AND PED.ClienteID  = @ClienteID                          
          AND PED.remUF = @REMUFID             
          AND PED.DtEmissao IS NOT NULL             -- Recepcao Fisica                 
          AND tk.DtTracking IS NOT NULL                  
          AND tk.StatusID = 21                      -- Determina pedidos da etapa    
          AND ls.status IS NULL                     -- IMPEDE REENVIO    
      
OPEN tmpCurIbex                                            
                  
FETCH NEXT FROM tmpCurIbex INTO                                            
    @RemRazaoSocial ,                                           
    @DesRazaoSocial,                                            
    @RemCGC,                                           
    @Documento,                                            
    @PedidoID,                                           
    @Desistencia,                                            
    @CodigoStatus,                                           
 @NomeRecep,                                   
    @RGRecep,                                           
    @DataEntrega,                                            
    @dtMudancaSt,                                           
    @SerieNF,                                            
    @NF,                                           
    @Codigo,                                            
    @Tentativa ,                                           
    @Prod,                                            
    @DesCGC,                                           
    @numArEBCT,                  
 @descricaocli                  
                                          
/***********************************************************************************************************************                                            
-- Monta Linha de Detalhe -                                          
--*********************************************************************************************************************/                                         
WHILE @@FETCH_STATUS = 0                                            
BEGIN                                            
  IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */              
  BEGIN                                            
    SET @CGCRet  = @RemCGC                                            
    SET @Seq  = @Seq + 1                                            
    SET @QtdeTot    = @QtdeTot + 1                                           
    SET @QtdeDev    = @QtdeDev + 1                                            
  END                                            
  ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
  BEGIN                                            
    SET @CGCRet    = @DesCGC                                            
    SET @SeqCPP     = @SeqCPP + 1                                            
    SET @QtdeTotCPP = @QtdeTotCPP + 1                                            
SET @QtdeDevCPP = @QtdeDevCPP + 1                                            
  END                                            
  /*********************************************************************************************************                                            
  --Gravacao do Registro '342'                                            
  --*******************************************************************************************************/                                            
  SET @Linha123 = '342'                         /*  -- Codigo Registro  */                                         
     + dbo.fmtCampo(@CGCRet, 14, 1)            /*  -- CNPJ Remetente     */                              
     + dbo.fmtCampo(@SerieNF, 3, 3)             /*    -- S?rie da Nota Fiscal   */                                         
     + dbo.fmtCampo(@NF, 8, 0)                   /*   -- NF      */                                      
     + dbo.fmtCampo(@Codigo, 2,0)                             /* -- Codigo de OCO?ncia */                                           
     + SUBSTRING(CAST((100 + DAY(@DataEntrega)) AS CHAR(3)), 2, 2)         /*   -- Dia Entrega  */                                  
     + SUBSTRING(CAST((100 + MONTH(@DataEntrega)) AS CHAR(3)), 2, 2)       /*   -- M?s Entrega   */                                         
     + SUBSTRING(CAST(YEAR(@DataEntrega) AS CHAR(4)), 1, 4)             /* -- Ano Entrega */                                           
     + SUBSTRING(CAST((100 + DATEPART(Hour, @DataEntrega)) AS CHAR(3)), 2, 2)   /*  -- Hora Entrega*/                                            
     + SUBSTRING(CAST((100 + DATEPART(Minute, @DataEntrega)) AS CHAR(3)), 2, 2)  /*  -- Minuto Entrega */   
     + SPACE(2)                                              
     + dbo.fmtCampo(@descricaocli,70,1)   /* -- Descricao Status  */                                            
     + dbo.fmtCampo(@Documento, 9, 2)                                             
     + SPACE(62)                                     /* -- Filler    */                                          
     + dbo.fmtCampo(@numArEBCT, 13,1)                                          
     + dbo.fmtCampo(@Cont, 4, 0)                                  
                                          
  SET @Cont = @Cont+1                                          
                                  
  INSERT INTO tmpExpCliente342 (Texto, seq) VALUES (@Linha123, @Cont)                                          
                                          
  /**************************************************************************************************************                                            
  --Grava na Tabela Temporaria os Registro Encontrados                                            
  --*************************************************************************************************************/                                          
 IF NOT EXISTS (SELECT 1 FROM tmpClienteUpdate WHERE PedidoID = @PedidoID)                                            
    INSERT INTO tmpClienteUpdate (PedidoID) VALUES (@PedidoID)                                          
                                          
  IF @Producao = 1                                          
  BEGIN                                          
    /****************************************************************************************************************                                            
    -- Insere na ExportaPedido                                         
    --***************************************************************************************************************/                                        
    IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*--(@ProdutoID,@ProdutoID_1) -- EPP */                                           
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaID )               
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaID, @PedidoID)                                           
      END                                            
    END                                            
    ELSE IF @Prod IN (select produtoid from produto (nolock) where clienteid = @ClienteID and receitaID in (11,31) ) /*-- (@ProdutoID_2,@ProdutoID_3) -- CPP */                                           
    BEGIN                                            
      IF NOT EXISTS (SELECT 1 FROM ExportaPedido (nolock) WHERE PedidoID = @PedidoID AND LoteExportaId = @LoteExportaIDCP )                                            
      BEGIN                                            
        INSERT INTO ExportaPedido (LoteExportaId,PedidoID) VALUES (@LoteExportaIDCP, @PedidoID)                                            
      END                                            
    END                                            
                                          
    /*******************************************************************************************************************                                            
    -- Update ControleExportaTracking                                            
    --*******************************************************************************************************************/                                            
--15 tmpLogStatus                  
 --Revisar                  
    INSERT INTO tmpLogStatus (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo, Tiposervico, Doc_envio, Numerodoc)                  
  VALUES (@PedidoID, CAST(@CodigoSequoia as int), @ArquivoName, NULL, GETDATE(), 1, @ClienteID, dbo.fmtCampo(@NF, 8, 0), NULL,dbo.fmtCampo(@SerieNF, 3, 3)                     
          ,NULL, 1, 'OCO', 1)                  
                  
  END                                          
                                          
  FETCH NEXT FROM tmpCurIbex INTO                                            
      @RemRazaoSocial ,                                           
      @DesRazaoSocial,                               
      @RemCGC,                                           
      @Documento,                                            
      @PedidoID,                                           
      @Desistencia,                                            
      @CodigoStatus,                                           
      @NomeRecep,                                            
      @RGRecep,                                           
      @DataEntrega,                                            
    @dtMudancaSt,                                           
      @SerieNF,                                            
      @NF,                                           
      @Codigo,                                            
      @Tentativa ,                                           
      @Prod,                                            
      @DesCGC,                                           
      @numArEBCT,                  
   @descricaocli                  
END                                             
CLOSE tmpCurIbex                                            
DEALLOCATE tmpCurIbex                                            
                                          
/***************************************************************************************************************                                            
-- Update DtExporta                                            
--***************************************************************************************************************/                                            
IF @Producao = 1                                          
BEGIN                                          
  UPDATE PedidoMestre SET DtExporta = @DtHoje WHERE PedidoID IN (SELECT PedidoID FROM tmpClienteUpdate)                                          
END                                          
                                          
TRUNCATE TABLE tmpClienteUpdate                                          
                  
#endregion '21.   Busca Remessas Entregue na Agência                        -  (Entregue na Agencia)  '   
  
#region 'Fim'   
     
#region 'Informacoes do Trailer'           
SET @Seq   = @Seq + 1                                            
SET @SeqCPP  = @SeqCPP + 1                                            
SET @Trailer   = '9' + SPACE(55)                                           
                                          
IF (SELECT COUNT(1) FROM tmpExpCliente342)>0                                             
   BEGIN     
     #region 'tmpExpCliente342)>0  '               
       /****************************************************************************************************                                            
       -- Atualiza Lote de exportacao                                            
       --***************************************************************************************************/                                            
       IF @Producao = 1                                          
          BEGIN      
             #region 'Atualiza Lote de exportacao'        
            UPDATE LoteExporta                                                                          
            SET  QtdeEntregue  = @QtdeEnt ,                                            
                 QtdeDev      = @QtdeDev ,                                            
                 Qtde       = @QtdeTot                                            
            WHERE  LoteExportaID  = @LoteExportaID   
            #endregion 'Atualiza Lote de exportacao'          
           
             #region 'Gerando Arquivo txt'                                      
              /************************************************************************************************************                                          
                 -- Gerando Arquivo txt                                            
                 --***********************************************************************************************************/                                          
                 SET @Comando = 'bcp " SELECT Texto FROM(SELECT Texto, seq FROM tmpExpCliente union SELECT Texto, seq FROM tmpExpCliente342) tmp ORDER BY seq "'                                            
                             + ' queryout ' + @_PathArquivos + @Arquivo                                          
                             + ' -f' + @_PathSistemas + 'Formato\' + @_PathFormato            
                             + ' -S' + @_Servidor                                          
                             + ' -U' + @_Usuario                                          
                             + ' -P' + @_Senha                         
                                   
                 EXEC master.dbo.xp_cmdshell @Comando                     
             #endregion 'Gerando Arquivo txt'                
             #region 'Após criar os registros na tabela LogStatusAPI'    
         /*Após criar os registros na tabela LogStatusAPI*/                              
            IF(SELECT COUNT(1) FROM tmpLogStatus)>0                        
            BEGIN                  
               INSERT INTO LogStatusAPI (nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo                  
                                        ,Tiposervico, Doc_envio, Numerodoc)                   
                 SELECT nrt, status, json, retorno, data, sucesso, clienteid, nf, chaveNF, serieNF, Protocolo,Tiposervico                  
                        ,Doc_envio, Numerodoc FROM tmpLogStatus                  
            END                  
            #endregion 'Após criar os registros na tabela LogStatusAPI'                                                     
          END                                          
              
       #region 'Imprimindo na tela '        
         /*******************************************************************************************************                                            
          --Imprimindo na tela                                            
          --******************************************************************************************************/                                            
          PRINT '>>>> Lote de Exportação EPP: ' + CAST(@LoteExportaID AS VARCHAR(10))                                            
          PRINT '>>>> Total de linhas: ' + CAST(@Seq AS CHAR(10)) /*-- Somente as linhas internas  */                                          
          PRINT '>>>> Gerado arquivo: ' + @Arquivo                                            
          PRINT '***** Fim da Exportação *****'                                            
          PRINT 'FIM: ' + CONVERT(CHAR(30), GETDATE(), 113)    
        #endregion 'Imprimindo na tela '        
     #endregion 'tmpExpCliente342)>0  '                                              
   END                                          
ELSE                                           
   BEGIN        
     #region 'Else tmpExpCliente342)>0  '                                        
          IF @Producao = 0                                          
          BEGIN                                          
          DELETE LoteExporta WHERE loteexportaid = @LoteExportaID                                      
            DELETE TB_SequenciaIbex WHERE sequencia = @Sequencia                                          
          END                     
          PRINT '>>>> Lote de Exportação EPP: <NÃO EXISTEM DADOS PARA GERAÇÃO DE ARQUIVO>'                                            
          PRINT '>>>> Total de linhas: 0'  /*-- Somente as linhas internas */                                           
          PRINT '>>>> Gerado arquivo: <NÃO EXISTEM DADOS PARA GERAÇÃO DE ARQUIVO>'                                            
          PRINT '***** Fim da Exportação *****'                                            
          PRINT 'FIM: ' + CONVERT(CHAR(30), GETDATE(), 113)    
     #endregion 'Else tmpExpCliente342)>0  '     
   END                                            
                                       
                          
  IF @Producao = 0                                          
    BEGIN                       
     SELECT Texto FROM(SELECT Texto, seq FROM mpExpCliente  union SELECT Texto, seq FROM tmpExpCliente342) tmp                                          
     ORDER BY seq                                          
    END   
#endregion 'Informacoes do Trailer'   
#region 'Excluindo as temporarias '              
DROP TABLE tmpClienteUpdate                            
DROP TABLE tmpExpCliente                                            
DROP TABLE tmpExpCliente342       
#endregion 'Excluindo as temporarias '                            
  
#endregion 'Fim' 