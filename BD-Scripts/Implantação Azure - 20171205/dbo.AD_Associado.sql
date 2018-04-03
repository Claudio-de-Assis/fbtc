USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Associado] Data do Script: 05/12/2017 09:25:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


