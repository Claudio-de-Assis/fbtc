USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Endereco] Data do Script: 05/12/2017 09:51:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [PK_AD_Endereco] PRIMARY KEY CLUSTERED ([EnderecoId] ASC);


GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [FK_AD_Endereco_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [CK_AD_Endereco_TipoEndereco] CHECK ([TipoEndereco]='1' OR [TipoEndereco]='2' OR [TipoEndereco]='3');


