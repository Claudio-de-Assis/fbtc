USE [FBTC-Prod]
GO

/****** Objeto: Table [dbo].[AD_Associado_Isento] Data do Script: 22/11/2017 15:32:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Associado_Isento] (
    [AssociadoIsentoId] INT      IDENTITY (1, 1) NOT NULL,
    [IsencaoId]         INT      NOT NULL,
    [AssociadoId]       INT      NOT NULL,
    [DtCadastro]        DATETIME NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Associado_Isento_AssociadoIsentoId]
    ON [dbo].[AD_Associado_Isento]([AssociadoIsentoId] ASC);

GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [PK_AD_Associado_Isento] PRIMARY KEY CLUSTERED ([AssociadoIsentoId] ASC);	

GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [FK_AD_Associado_Isento_AD_Isencao] FOREIGN KEY ([IsencaoId]) REFERENCES [dbo].[AD_Isencao] ([IsencaoId]);


GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [FK_AD_Associado_Isento_AD_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);


