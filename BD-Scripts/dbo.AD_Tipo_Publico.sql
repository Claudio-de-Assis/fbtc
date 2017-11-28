USE [FBTC-Prod]
GO

/****** Objeto: Table [dbo].[AD_Tipo_Publico] Data do Script: 22/11/2017 15:56:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Tipo_Publico] (
    [TipoPublicoId] INT          IDENTITY (1, 1) NOT NULL,
    [Nome]          VARCHAR (60) NOT NULL,
    [DescricaoValor] VARCHAR (200) NOT NULL,
    [Ativo]         BIT          NOT NULL,
    [Ordem]         SMALLINT     NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Tipo_Publico_TipoPublicoId]
    ON [dbo].[AD_Tipo_Publico]([TipoPublicoId] ASC);

GO
ALTER TABLE [dbo].[AD_Tipo_Publico]
    ADD CONSTRAINT [PK_AD_Tipo_Publico] PRIMARY KEY CLUSTERED ([TipoPublicoId] ASC);	

GO
ALTER TABLE [dbo].[AD_Tipo_Publico]
    ADD CONSTRAINT [CK_AD_Tipo_Publico_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


