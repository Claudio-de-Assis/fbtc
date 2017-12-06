USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Valor_Evento_Publico] Data do Script: 05/12/2017 09:24:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Valor_Evento_Publico] (
    [ValorEventoPublicoId] INT            IDENTITY (1, 1) NOT NULL,
    [Valor]                NUMERIC (6, 2) NOT NULL,
    [EventoId]             INT            NOT NULL,
    [TipoPublicoId]        INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Valor_Evento_Publico_ValorEventoPublicoId]
    ON [dbo].[AD_Valor_Evento_Publico]([ValorEventoPublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [PK_AD_Valor_Evento_Publico] PRIMARY KEY CLUSTERED ([ValorEventoPublicoId] ASC);


GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Evento_Publico_AD_Evento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[AD_Evento] ([EventoId]);


GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Evento_Publico_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);


