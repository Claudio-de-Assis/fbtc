USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Colaborador] Data do Script: 22/11/2017 16:58:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Colaborador] (
    [ColaboradorId] INT      IDENTITY (1, 1) NOT NULL,
    [TipoPerfil]    CHAR (1) NOT NULL,
    [PessoaId]      INT      NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Colaborador_ColaboradorId]
    ON [dbo].[AD_Colaborador]([ColaboradorId] ASC);


GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [PK_AD_Colaborador] PRIMARY KEY CLUSTERED ([ColaboradorId] ASC);


GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [FK_AD_Colaborador_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [CK_AD_Colaborador_TipoPerfil] CHECK ([TipoPerfil]='1' OR [TipoPerfil]='2' OR [TipoPerfil]='3');


