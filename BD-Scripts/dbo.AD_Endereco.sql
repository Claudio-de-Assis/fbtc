USE [FBTC-Prod]
GO

/****** Objeto: Table [dbo].[AD_Endereco] Data do Script: 22/11/2017 15:41:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Endereco] (
    [EnderecoId]  INT           IDENTITY (1, 1) NOT NULL,
    [PessoaId]    INT           NOT NULL,
    [Logradouro]  VARCHAR (100) NULL,
    [Numero]      VARCHAR (10)  NULL,
    [Complemento] VARCHAR (100) NULL,
    [Bairro]      VARCHAR (100) NULL,
    [Cidade]      VARCHAR (100) NULL,
    [SiglaUF]     CHAR (2)      NULL,
    [CEP]         VARCHAR (10)  NULL,
	[TipoEndereco] CHAR (1)
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Endereco_EnderecoId]
    ON [dbo].[AD_Endereco]([EnderecoId] ASC);

GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [PK_AD_Endereco] PRIMARY KEY CLUSTERED ([EnderecoId] ASC);
	
	
GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [FK_AD_Endereco_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);
	
GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [CK_AD_Endereco_TipoEndereco] CHECK ([TipoEndereco]='1' OR [TipoEndereco]='2' OR [TipoEndereco]='3');


