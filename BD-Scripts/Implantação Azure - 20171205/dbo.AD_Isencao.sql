USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Isencao] Data do Script: 05/12/2017 09:24:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Isencao] (
    [IsencaoId]   INT           IDENTITY (1, 1) NOT NULL,
    [AnuidadeId]  INT           NULL,
    [EventoId]    INT           NULL,
    [Descricao]   VARCHAR (100) NOT NULL,
    [DtAta]       DATETIME      NOT NULL,
    [AnoEvento]   INT           NOT NULL,
    [TipoIsencao] CHAR (1)      NOT NULL,
    [Ativo]       BIT           NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Isencao_IsencaoId]
    ON [dbo].[AD_Isencao]([IsencaoId] ASC);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [PK_AD_Isencao] PRIMARY KEY CLUSTERED ([IsencaoId] ASC);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [FK_AD_Isencao_AD_Anuidade] FOREIGN KEY ([AnuidadeId]) REFERENCES [dbo].[AD_Anuidade] ([AnuidadeId]);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [FK_AD_Isencao_AD_Evento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[AD_Evento] ([EventoId]);


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [CK_AD_Isencao_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Isencao]
    ADD CONSTRAINT [CK_AD_Isencao_TipoIsencao] CHECK ([TipoIsencao]='1' OR [TipoIsencao]='2');


