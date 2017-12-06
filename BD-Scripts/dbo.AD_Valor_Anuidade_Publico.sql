USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Valor_Anuidade_Publico] Data do Script: 05/12/2017 09:24:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Valor_Anuidade_Publico] (
    [ValorAnuidadePublicoId] INT            IDENTITY (1, 1) NOT NULL,
    [Valor]                  NUMERIC (6, 2) NOT NULL,
    [AnuidadeId]             INT            NOT NULL,
    [TipoPublicoId]          INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Valor_Anuidade_Publico_ValorAnuidadePublicoId]
    ON [dbo].[AD_Valor_Anuidade_Publico]([ValorAnuidadePublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [PK_AD_Valor_Anuidade_Publico] PRIMARY KEY CLUSTERED ([ValorAnuidadePublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Anuidade_Publico_AD_Anuidade] FOREIGN KEY ([AnuidadeId]) REFERENCES [dbo].[AD_Anuidade] ([AnuidadeId]);


GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Anuidade_Publico_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);


