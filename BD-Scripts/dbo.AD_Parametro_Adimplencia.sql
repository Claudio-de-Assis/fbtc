USE [FBTC-Prod]
GO

/****** Objeto: Table [dbo].[AD_Parametro_Adimplencia] Data do Script: 22/11/2017 15:49:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Parametro_Adimplencia] (
    [ParametroAdimplenciaId]  INT IDENTITY (1, 1) NOT NULL,
    [NotificacaoVencAnuidade] BIT NOT NULL,
    [NotificacaoVencEvento]   BIT NOT NULL,
    [EnviarBoletoEMail]       BIT NOT NULL,
    [QtdDias]                 INT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Parametro_Adimplencia_ParametroAdimplenciaId]
    ON [dbo].[AD_Parametro_Adimplencia]([ParametroAdimplenciaId] ASC);
	
GO
ALTER TABLE [dbo].[AD_Parametro_Adimplencia]
    ADD CONSTRAINT [PK_AD_Parametro_Adimplencia] PRIMARY KEY CLUSTERED ([ParametroAdimplenciaId] ASC);


