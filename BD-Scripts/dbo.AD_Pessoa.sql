USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Pessoa] Data do Script: 05/12/2017 09:24:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [PK_AD_Pessoa] PRIMARY KEY CLUSTERED ([PessoaId] ASC);


GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [CK_AD_Pessoa_Ativo] CHECK ([Ativo]='1' OR [Ativo]='0');


GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [CK_AD_Pessoa_Sexo] CHECK ([Sexo]='M' OR [Sexo]='F');


