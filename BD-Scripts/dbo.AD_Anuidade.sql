USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Anuidade] Data do Script: 05/12/2017 09:23:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Anuidade] (
    [AnuidadeId] INT      IDENTITY (1, 1) NOT NULL,
    [DtCadastro] DATETIME NOT NULL,
    [Codigo]     INT      NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Anuidade_AnuidadeId]
    ON [dbo].[AD_Anuidade]([AnuidadeId] ASC);


GO
ALTER TABLE [dbo].[AD_Anuidade]
    ADD CONSTRAINT [PK_AD_Anuidade] PRIMARY KEY CLUSTERED ([AnuidadeId] ASC);


