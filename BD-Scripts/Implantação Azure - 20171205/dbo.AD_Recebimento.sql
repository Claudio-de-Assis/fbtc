USE [FBTC-Dsv]
GO

/****** Objeto: Table [dbo].[AD_Recebimento] Data do Script: 05/12/2017 09:24:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Recebimento] (
    [RecebimentoId]          INT            IDENTITY (1, 1) NOT NULL,
    [AssociadoId]            INT            NULL,
    [ValorEventoPublicoId]   INT            NULL,
    [ValorAnuidadePublicoId] INT            NULL,
    [AssociadoIsentoId]      INT            NULL,
    [ObjetivoPagamento]      CHAR (1)       NOT NULL,
    [DtCadastro]             DATETIME       NOT NULL,
    [DtVencimento]           DATETIME       NULL,
    [DtPagamento]            DATETIME       NULL,
    [DtNotificacao]          DATETIME       NULL,
    [StatusPagamento]        CHAR (1)       NULL,
    [FormaPagamento]         CHAR (1)       NULL,
    [NrDocCobranca]          VARCHAR (100)  NULL,
    [ValorPago]              NUMERIC (6, 2) NULL,
    [Observacao]             VARCHAR (500)  NULL,
    [TokenPagamento]         VARCHAR (100)  NULL,
    [Ativo]                  BIT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Recebimento_RecebimentoId]
    ON [dbo].[AD_Recebimento]([RecebimentoId] ASC);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [PK_AD_Recebimento] PRIMARY KEY CLUSTERED ([RecebimentoId] ASC);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Valor_Anuidade_Publico] FOREIGN KEY ([ValorAnuidadePublicoId]) REFERENCES [dbo].[AD_Valor_Anuidade_Publico] ([ValorAnuidadePublicoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Associado_Isento] FOREIGN KEY ([AssociadoIsentoId]) REFERENCES [dbo].[AD_Associado_Isento] ([AssociadoIsentoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [FK_AD_Recebimento_AD_Valor_Evento_Publico] FOREIGN KEY ([ValorEventoPublicoId]) REFERENCES [dbo].[AD_Valor_Evento_Publico] ([ValorEventoPublicoId]);


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_ObjetivoPagto] CHECK ([ObjetivoPagamento]='1' OR [ObjetivoPagamento]='2');


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_FormaPagto] CHECK ([FormaPagamento]='1' OR [FormaPagamento]='2' OR [FormaPagamento]='3');


GO
ALTER TABLE [dbo].[AD_Recebimento]
    ADD CONSTRAINT [CK_AD_Recebimento_StatusPagto] CHECK ([StatusPagamento]='0' OR [StatusPagamento]='1' OR [StatusPagamento]='2');


