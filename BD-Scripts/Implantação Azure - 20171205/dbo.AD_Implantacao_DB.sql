
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Objeto: Table [dbo].[AD_Anuidade] Data do Script: 05/12/2017 09:23:01 ******/
CREATE TABLE [dbo].[AD_Anuidade] (
    [AnuidadeId] INT      IDENTITY (1, 1) NOT NULL,
    [DtCadastro] DATETIME NOT NULL,
    [Codigo]     INT      NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Anuidade_AnuidadeId]
    ON [dbo].[AD_Anuidade]([AnuidadeId] ASC);
	
/****** Objeto: Table [dbo].[AD_Associado] Data do Script: 05/12/2017 09:25:50 *********************************************/
CREATE TABLE [dbo].[AD_Associado] (
    [AssociadoId]             INT            IDENTITY (1, 1) NOT NULL,
    [PessoaId]                INT            NOT NULL,
    [AtcId]                   INT            NULL,
    [TipoPublicoId]           INT            NOT NULL,
    [NrMatricula]             VARCHAR (15)   NULL,
    [CRP]                     NVARCHAR (15)  NULL,
    [CRM]                     NVARCHAR (15)  NULL,
    [NomeInstFormacao]        VARCHAR (100)  NULL,
    [Certificado]             BIT            NOT NULL,
    [DtCertificacao]          DATETIME       NULL,
    [DivulgarContato]         BIT            NULL,
    [TipoFormaContato]        CHAR (1)       NULL,
    [IntegraDiretoria]        BIT            NOT NULL,
    [IntegraConfi]            BIT            NOT NULL,
    [NrTelDivulgacao]         NVARCHAR (15)  NULL,
    [ComprovanteAfiliacaoAtc] NVARCHAR (100) NULL,
    [TipoProfissao]           CHAR (1)       NULL,
    [TipoTitulacao]           CHAR (1)       NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Associado_AssociadoId]
    ON [dbo].[AD_Associado]([AssociadoId] ASC);


/****** Objeto: Table [dbo].[AD_Associado_Isento] Data do Script: 05/12/2017 09:23:39 ****************************************/
CREATE TABLE [dbo].[AD_Associado_Isento] (
    [AssociadoIsentoId] INT      IDENTITY (1, 1) NOT NULL,
    [IsencaoId]         INT      NOT NULL,
    [AssociadoId]       INT      NOT NULL,
    [DtCadastro]        DATETIME NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Associado_Isento_AssociadoIsentoId]
    ON [dbo].[AD_Associado_Isento]([AssociadoIsentoId] ASC);

	
/****** Objeto: Table [dbo].[AD_ATC] Data do Script: 05/12/2017 09:23:51 ******/
CREATE TABLE [dbo].[AD_ATC] (
    [AtcId]         INT           IDENTITY (1, 1) NOT NULL,
    [Nome]          VARCHAR (100) NOT NULL,
    [UF]            CHAR (2)      NOT NULL,
    [NomePres]      VARCHAR (100) NOT NULL,
    [NomeVPres]     VARCHAR (100) NULL,
    [NomePSec]      VARCHAR (100) NULL,
    [NomeSSec]      VARCHAR (100) NULL,
    [NomePTes]      VARCHAR (100) NULL,
    [NomeSTes]      VARCHAR (100) NULL,
    [Site]          VARCHAR (100) NULL,
    [SiteDiretoria] VARCHAR (100) NULL,
    [Ativo]         BIT           NOT NULL,
    [Codigo]        INT           NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_ATC_AtcId]
    ON [dbo].[AD_ATC]([AtcId] ASC);

/****** Objeto: Table [dbo].[AD_Cartao_Associado] Data do Script: 05/12/2017 09:23:59 ******/
CREATE TABLE [dbo].[AD_Cartao_Associado] (
    [CartaoAssociadoId] INT      IDENTITY (1, 1) NOT NULL,
    [AssociadoId]       INT      NOT NULL,
    [DtEmissao]         DATETIME NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Cartao_Associado_CartaoAssociadoId]
    ON [dbo].[AD_Cartao_Associado]([CartaoAssociadoId] ASC);


/****** Objeto: Table [dbo].[AD_Colaborador] Data do Script: 05/12/2017 09:24:07 ******/
CREATE TABLE [dbo].[AD_Colaborador] (
    [ColaboradorId] INT      IDENTITY (1, 1) NOT NULL,
    [TipoPerfil]    CHAR (1) NOT NULL,
    [PessoaId]      INT      NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Colaborador_ColaboradorId]
    ON [dbo].[AD_Colaborador]([ColaboradorId] ASC);
	

/****** Objeto: Table [dbo].[AD_Endereco] Data do Script: 05/12/2017 09:51:30 ******/
CREATE TABLE [dbo].[AD_Endereco] (
    [EnderecoId]   INT           IDENTITY (1, 1) NOT NULL,
    [PessoaId]     INT           NOT NULL,
    [Logradouro]   VARCHAR (100) NOT NULL,
    [Numero]       VARCHAR (10)  NOT NULL,
    [Complemento]  VARCHAR (100) NULL,
    [Bairro]       VARCHAR (100) NOT NULL,
    [Cidade]       VARCHAR (100) NOT NULL,
    [SiglaUF]      CHAR (2)      NOT NULL,
    [CEP]          VARCHAR (10)  NOT NULL,
    [TipoEndereco] CHAR (1)      NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Endereco_EnderecoId]
    ON [dbo].[AD_Endereco]([EnderecoId] ASC);


/****** Objeto: Table [dbo].[AD_Evento] Data do Script: 05/12/2017 09:24:20 ******/
CREATE TABLE [dbo].[AD_Evento] (
    [EventoId]           INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]             VARCHAR (100) NOT NULL,
    [Descricao]          VARCHAR (MAX) NOT NULL,
    [Codigo]             VARCHAR (60)  NULL,
    [DtInicio]           DATETIME      NOT NULL,
    [DtTermino]          DATETIME      NOT NULL,
    [DtTerminoInscricao] DATETIME      NULL,
    [TipoEvento]         CHAR (1)      NOT NULL,
    [AceitaIsencaoAta]   BIT           NOT NULL,
    [Ativo]              BIT           NOT NULL,
    [NomeFoto]           VARCHAR (32)  NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Evento_EventoId]
    ON [dbo].[AD_Evento]([EventoId] ASC);
	
	
/****** Objeto: Table [dbo].[AD_Isencao] Data do Script: 05/12/2017 09:24:25 ******/
CREATE TABLE [dbo].[AD_Isencao] (
    [IsencaoId]   INT           IDENTITY (1, 1) NOT NULL,
    [AnuidadeId]  INT           NULL,
    [EventoId]    INT           NULL,
    [Descricao]   VARCHAR (100) NOT NULL,
    [DtAta]       DATETIME      NOT NULL,
    [AnoEvento]   INT           NOT NULL,
    [TipoIsencao] CHAR (1)      NOT NULL,
    [Ativo]       BIT           NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Isencao_IsencaoId]
    ON [dbo].[AD_Isencao]([IsencaoId] ASC);
	
/****** Objeto: Table [dbo].[AD_Parametro_Adimplencia] Data do Script: 05/12/2017 09:24:30 ******/
CREATE TABLE [dbo].[AD_Parametro_Adimplencia] (
    [ParametroAdimplenciaId]  INT IDENTITY (1, 1) NOT NULL,
    [NotificacaoVencAnuidade] BIT NOT NULL,
    [NotificacaoVencEvento]   BIT NOT NULL,
    [EnviarBoletoEMail]       BIT NOT NULL,
    [QtdDias]                 INT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Parametro_Adimplencia_ParametroAdimplenciaId]
    ON [dbo].[AD_Parametro_Adimplencia]([ParametroAdimplenciaId] ASC);

/****** Objeto: Table [dbo].[AD_Pessoa] Data do Script: 05/12/2017 09:24:35 ******/
CREATE TABLE [dbo].[AD_Pessoa] (
    [PessoaId]     INT           IDENTITY (1, 1) NOT NULL,
    [Nome]         VARCHAR (100) NOT NULL,
    [CPF]          VARCHAR (15)  NULL,
    [RG]           VARCHAR (15)  NULL,
    [EMail]        VARCHAR (100) NOT NULL,
    [NomeFoto]     VARCHAR (32)  NULL,
    [Sexo]         CHAR (1)      NULL,
    [DtNascimento] DATETIME      NULL,
    [NrCelular]    VARCHAR (15)  NOT NULL,
    [PassWordHash] VARCHAR (100) NULL,
    [DtCadastro]   DATETIME      NOT NULL,
    [Ativo]        BIT           NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Pessoa_PessoaId]
    ON [dbo].[AD_Pessoa]([PessoaId] ASC);

/****** Objeto: Table [dbo].[AD_Recebimento] Data do Script: 05/12/2017 09:24:40 ******/
CREATE TABLE [dbo].[AD_Recebimento] (
    [RecebimentoId]          INT            IDENTITY (1, 1) NOT NULL,
    [AssociadoId]            INT            NULL,
    [ValorEventoPublicoId]   INT            NULL,
    [ValorAnuidadePublicoId] INT            NULL,
    [AssociadoIsentoId]      INT            NULL,
    [ObjetivoPagamento]      CHAR (1)       NOT NULL,
    [DtCadastro]             DATETIME       NOT NULL,
    [DtVencimento]           DATETIME       NULL,
    [DtPagamento]            DATETIME       NULL,
    [DtNotificacao]          DATETIME       NULL,
    [StatusPagamento]        CHAR (1)       NULL,
    [FormaPagamento]         CHAR (1)       NULL,
    [NrDocCobranca]          VARCHAR (100)  NULL,
    [ValorPago]              NUMERIC (6, 2) NULL,
    [Observacao]             VARCHAR (500)  NULL,
    [TokenPagamento]         VARCHAR (100)  NULL,
    [Ativo]                  BIT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Recebimento_RecebimentoId]
    ON [dbo].[AD_Recebimento]([RecebimentoId] ASC);
	
	
/****** Objeto: Table [dbo].[AD_Tipo_Publico] Data do Script: 05/12/2017 09:24:46 ******/
CREATE TABLE [dbo].[AD_Tipo_Publico] (
    [TipoPublicoId]  INT           IDENTITY (1, 1) NOT NULL,
    [Nome]           VARCHAR (60)  NOT NULL,
    [DescricaoValor] VARCHAR (200) NOT NULL,
    [Ativo]          BIT           NOT NULL,
    [Ordem]          SMALLINT      NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Tipo_Publico_TipoPublicoId]
    ON [dbo].[AD_Tipo_Publico]([TipoPublicoId] ASC);

/****** Objeto: Table [dbo].[AD_Valor_Anuidade_Publico] Data do Script: 05/12/2017 09:24:52 ******/
CREATE TABLE [dbo].[AD_Valor_Anuidade_Publico] (
    [ValorAnuidadePublicoId] INT            IDENTITY (1, 1) NOT NULL,
    [Valor]                  NUMERIC (6, 2) NOT NULL,
    [AnuidadeId]             INT            NOT NULL,
    [TipoPublicoId]          INT            NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Valor_Anuidade_Publico_ValorAnuidadePublicoId]
    ON [dbo].[AD_Valor_Anuidade_Publico]([ValorAnuidadePublicoId] ASC);


/****** Objeto: Table [dbo].[AD_Valor_Evento_Publico] Data do Script: 05/12/2017 09:24:57 ******/
CREATE TABLE [dbo].[AD_Valor_Evento_Publico] (
    [ValorEventoPublicoId] INT            IDENTITY (1, 1) NOT NULL,
    [Valor]                NUMERIC (6, 2) NOT NULL,
    [EventoId]             INT            NOT NULL,
    [TipoPublicoId]        INT            NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_AD_Valor_Evento_Publico_ValorEventoPublicoId]
    ON [dbo].[AD_Valor_Evento_Publico]([ValorEventoPublicoId] ASC);
	

/*******************************************************************************************************************************/		
/*******************************************************************************************************************************/	
/******** Objetivo: Criar os relacionamentos entre as tabelas  *****************************************************************/	

GO
ALTER TABLE [dbo].[AD_Anuidade]
    ADD CONSTRAINT [PK_AD_Anuidade] PRIMARY KEY CLUSTERED ([AnuidadeId] ASC);

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [PK_AD_Associado] PRIMARY KEY CLUSTERED ([AssociadoId] ASC);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [FK_AD_Associado_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [FK_AD_Associado_AD_Atc] FOREIGN KEY ([AtcId]) REFERENCES [dbo].[AD_ATC] ([AtcId]);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [FK_AD_Associado_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_Certificado] CHECK ([Certificado]='0' OR [Certificado]='1');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_DivulgarContato] CHECK ([DivulgarContato]='0' OR [DivulgarContato]='1' OR [DivulgarContato]='');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_TipoFormaContato] CHECK ([TipoFormaContato] IS NULL OR [TipoFormaContato]='1' OR [TipoFormaContato]='2' OR [TipoFormaContato]='3' OR [TipoFormaContato]='4' OR [TipoFormaContato]='0' OR [TipoFormaContato]='');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_IntegraConfi] CHECK ([IntegraConfi]='0' OR [IntegraConfi]='1');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_TipoProfissao] CHECK ([TipoProfissao]='7' OR [TipoProfissao]='8' OR [TipoProfissao]='0' OR [TipoProfissao]='');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_IntegraDiretoria] CHECK ([IntegraDiretoria]='0' OR [IntegraDiretoria]='1');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_TipoTitulacao] CHECK ([TipoTitulacao]='1' OR [TipoTitulacao]='2' OR [TipoTitulacao]='3' OR [TipoTitulacao]='4' OR [TipoTitulacao]='5' OR [TipoTitulacao]='0' OR [TipoTitulacao]='');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [PK_AD_Associado_Isento] PRIMARY KEY CLUSTERED ([AssociadoIsentoId] ASC);


GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [FK_AD_Associado_Isento_AD_Isencao] FOREIGN KEY ([IsencaoId]) REFERENCES [dbo].[AD_Isencao] ([IsencaoId]);


GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [FK_AD_Associado_Isento_AD_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);
	
/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_ATC]
    ADD CONSTRAINT [PK_AD_ATC] PRIMARY KEY CLUSTERED ([AtcId] ASC);


GO
ALTER TABLE [dbo].[AD_ATC]
    ADD CONSTRAINT [CK_AD_ATC_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');
	
/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Cartao_Associado]
    ADD CONSTRAINT [PK_AD_Cartao_Associado] PRIMARY KEY CLUSTERED ([CartaoAssociadoId] ASC);


GO
ALTER TABLE [dbo].[AD_Cartao_Associado]
    ADD CONSTRAINT [FK_AD_Cartao_Associado_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);
	
	
/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [PK_AD_Colaborador] PRIMARY KEY CLUSTERED ([ColaboradorId] ASC);


GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [FK_AD_Colaborador_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [CK_AD_Colaborador_TipoPerfil] CHECK ([TipoPerfil]='1' OR [TipoPerfil]='2' OR [TipoPerfil]='3');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [PK_AD_Endereco] PRIMARY KEY CLUSTERED ([EnderecoId] ASC);


GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [FK_AD_Endereco_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [CK_AD_Endereco_TipoEndereco] CHECK ([TipoEndereco]='1' OR [TipoEndereco]='2' OR [TipoEndereco]='3');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [PK_AD_Evento] PRIMARY KEY CLUSTERED ([EventoId] ASC);


GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [CK_AD_Evento_AceitaIsencaoAta] CHECK ([AceitaIsencaoAta]='0' OR [AceitaIsencaoAta]='1');


GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [CK_AD_Evento_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [CK_AD_Evento_TipoEvento] CHECK ([TipoEvento]='1' OR [TipoEvento]='2' OR [TipoEvento]='3' OR [TipoEvento]='4' OR [TipoEvento]='5');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [PK_AD_Isencao] PRIMARY KEY CLUSTERED ([IsencaoId] ASC);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [FK_AD_Isencao_AD_Anuidade] FOREIGN KEY ([AnuidadeId]) REFERENCES [dbo].[AD_Anuidade] ([AnuidadeId]);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [FK_AD_Isencao_AD_Evento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[AD_Evento] ([EventoId]);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [CK_AD_Isencao_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [CK_AD_Isencao_TipoIsencao] CHECK ([TipoIsencao]='1' OR [TipoIsencao]='2');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Parametro_Adimplencia]
    ADD CONSTRAINT [PK_AD_Parametro_Adimplencia] PRIMARY KEY CLUSTERED ([ParametroAdimplenciaId] ASC);

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [PK_AD_Pessoa] PRIMARY KEY CLUSTERED ([PessoaId] ASC);


GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [CK_AD_Pessoa_Ativo] CHECK ([Ativo]='1' OR [Ativo]='0');


GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [CK_AD_Pessoa_Sexo] CHECK ([Sexo]='M' OR [Sexo]='F');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [PK_AD_Recebimento] PRIMARY KEY CLUSTERED ([RecebimentoId] ASC);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Valor_Anuidade_Publico] FOREIGN KEY ([ValorAnuidadePublicoId]) REFERENCES [dbo].[AD_Valor_Anuidade_Publico] ([ValorAnuidadePublicoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Associado_Isento] FOREIGN KEY ([AssociadoIsentoId]) REFERENCES [dbo].[AD_Associado_Isento] ([AssociadoIsentoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Valor_Evento_Publico] FOREIGN KEY ([ValorEventoPublicoId]) REFERENCES [dbo].[AD_Valor_Evento_Publico] ([ValorEventoPublicoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_ObjetivoPagto] CHECK ([ObjetivoPagamento]='1' OR [ObjetivoPagamento]='2');


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_FormaPagto] CHECK ([FormaPagamento]='1' OR [FormaPagamento]='2' OR [FormaPagamento]='3');


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_StatusPagto] CHECK ([StatusPagamento]='0' OR [StatusPagamento]='1' OR [StatusPagamento]='2');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Tipo_Publico]
    ADD CONSTRAINT [PK_AD_Tipo_Publico] PRIMARY KEY CLUSTERED ([TipoPublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Tipo_Publico]
    ADD CONSTRAINT [CK_AD_Tipo_Publico_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [PK_AD_Valor_Anuidade_Publico] PRIMARY KEY CLUSTERED ([ValorAnuidadePublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Anuidade_Publico_AD_Anuidade] FOREIGN KEY ([AnuidadeId]) REFERENCES [dbo].[AD_Anuidade] ([AnuidadeId]);


GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Anuidade_Publico_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [PK_AD_Valor_Evento_Publico] PRIMARY KEY CLUSTERED ([ValorEventoPublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Evento_Publico_AD_Evento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[AD_Evento] ([EventoId]);


GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Evento_Publico_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
