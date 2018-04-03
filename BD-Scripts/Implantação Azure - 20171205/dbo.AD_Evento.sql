USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Evento] Data do Script: 05/12/2017 09:24:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


