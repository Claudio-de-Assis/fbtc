USE [FBTC-Prod]
GO

/****** Objeto: Table [dbo].[AD_Pagamento] Data do Script: 22/11/2017 15:43:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AD_Pagamento] (
    [PagamentoId]          INT            IDENTITY (1, 1) NOT NULL,
    [AssociadoId]          INT            NULL,
    [ValorEventoPublicoId] INT            NULL,
    [ValorAnuidadePublicoId] INT          NULL,
    [AssociadoIsentoId]    INT            NULL,
    [ObjetivoPagto]        CHAR (1)       NOT NULL,
    [DtCadastro]           DATETIME       NOT NULL,
    [DtVencimento]         DATETIME       NOT NULL,
    [DtPagamento]          DATETIME       NULL,
    [DtNotificacao]        DATETIME       NULL,
    [StatusPagto]          CHAR (1)       NOT NULL,
    [FormaPagto]           CHAR (1)       NOT NULL,
    [NrDocCobranca]        VARCHAR (100)  NULL,
    [ValorPago]            NUMERIC (6, 2) NULL,
    [Observacao]           VARCHAR (500)  NULL,
    [TokenPagto]           VARCHAR (100)  NULL,
    [Ativo]                BIT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AD_Pagamento_PagamentoId]
    ON [dbo].[AD_Pagamento]([PagamentoId] ASC);

GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [PK_AD_Pagamento] PRIMARY KEY CLUSTERED ([PagamentoId] ASC);
	
GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [FK_AD_Pagamento_AD_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [FK_AD_Pagamento_AD_Valor_Anuidade_Publico] FOREIGN KEY ([ValorAnuidadePublicoId]) REFERENCES [dbo].[AD_Valor_Anuidade_Publico] ([ValorAnuidadePublicoId]);


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [FK_AD_Pagamento_AD_Associado_Isento] FOREIGN KEY ([AssociadoIsentoId]) REFERENCES [dbo].[AD_Associado_Isento] ([AssociadoIsentoId]);


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [FK_AD_Pagamento_AD_Valor_Evento_Publico] FOREIGN KEY ([ValorEventoPublicoId]) REFERENCES [dbo].[AD_Valor_Evento_Publico] ([ValorEventoPublicoId]);


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [CK_AD_Pagamento_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [CK_AD_Pagamento_FormaPagto] CHECK ([FormaPagto]='1' OR [FormaPagto]='2' OR [FormaPagto]='3');


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [CK_AD_Pagamento_ObjetivoPagto] CHECK ([ObjetivoPagto]='1' OR [ObjetivoPagto]='2');


GO
ALTER TABLE [dbo].[AD_Pagamento]
    ADD CONSTRAINT [CK_AD_Pagamento_StatusPagto] CHECK ([StatusPagto]='0' OR [StatusPagto]='1' OR [StatusPagto]='2');


