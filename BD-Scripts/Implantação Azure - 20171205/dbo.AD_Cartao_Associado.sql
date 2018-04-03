USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Cartao_Associado] Data do Script: 05/12/2017 09:23:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Cartao_Associado] (
    [CartaoAssociadoId] INT      IDENTITY (1, 1) NOT NULL,
    [AssociadoId]       INT      NOT NULL,
    [DtEmissao]         DATETIME NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Cartao_Associado_CartaoAssociadoId]
    ON [dbo].[AD_Cartao_Associado]([CartaoAssociadoId] ASC);


GO
ALTER TABLE [dbo].[AD_Cartao_Associado]
    ADD CONSTRAINT [PK_AD_Cartao_Associado] PRIMARY KEY CLUSTERED ([CartaoAssociadoId] ASC);


GO
ALTER TABLE [dbo].[AD_Cartao_Associado]
    ADD CONSTRAINT [FK_AD_Cartao_Associado_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);


