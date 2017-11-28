USE [FBTC-Prod]
GO

/****** Objeto: Table [dbo].[AD_ATC] Data do Script: 22/11/2017 15:36:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_ATC] (
    [AtcId]         INT           IDENTITY (1, 1) NOT NULL,
    [Nome]          VARCHAR (100) NOT NULL,
    [UF]            CHAR (2)      NOT NULL,
    [NomePres]      VARCHAR (100) NOT NULL,
    [NomeVPres]     VARCHAR (100) NULL,
    [NomePSec]      VARCHAR (100) NULL,
    [NomeSSec]      VARCHAR (100) NULL,
    [NomePTes]      VARCHAR (100) NULL,
    [NomeSTes]      VARCHAR (100) NULL,
    [Site]          VARCHAR (100) NULL,
    [SiteDiretoria] VARCHAR (100) NULL,
    [Ativo]         BIT           NOT NULL,
	[Codigo]		INT,	
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_ATC_AtcId]
    ON [dbo].[AD_ATC]([AtcId] ASC);

GO
ALTER TABLE [dbo].[AD_ATC]
    ADD CONSTRAINT [PK_AD_ATC] PRIMARY KEY CLUSTERED ([AtcId] ASC);	

GO
ALTER TABLE [dbo].[AD_ATC]
    ADD CONSTRAINT [CK_AD_ATC_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


