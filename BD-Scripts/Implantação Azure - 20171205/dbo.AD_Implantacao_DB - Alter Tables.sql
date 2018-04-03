/****** Alteração das tabelas para criação das CONSTRAINTs e etc *********************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************************************************************************************************/	
/******** Objetivo: Criar os relacionamentos entre as tabelas  *****************************************************************/	


/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	



GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [FK_AD_Associado_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [FK_AD_Associado_AD_Atc] FOREIGN KEY ([AtcId]) REFERENCES [dbo].[AD_ATC] ([AtcId]);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [FK_AD_Associado_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_Certificado] CHECK ([Certificado]='0' OR [Certificado]='1');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_DivulgarContato] CHECK ([DivulgarContato]='0' OR [DivulgarContato]='1' OR [DivulgarContato]='');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_TipoFormaContato] CHECK ([TipoFormaContato] IS NULL OR [TipoFormaContato]='1' OR [TipoFormaContato]='2' OR [TipoFormaContato]='3' OR [TipoFormaContato]='4' OR [TipoFormaContato]='0' OR [TipoFormaContato]='');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_IntegraConfi] CHECK ([IntegraConfi]='0' OR [IntegraConfi]='1');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_TipoProfissao] CHECK ([TipoProfissao]='7' OR [TipoProfissao]='8' OR [TipoProfissao]='0' OR [TipoProfissao]='');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_IntegraDiretoria] CHECK ([IntegraDiretoria]='0' OR [IntegraDiretoria]='1');


GO
ALTER TABLE [dbo].[AD_Associado]
    ADD CONSTRAINT [CK_AD_Associado_TipoTitulacao] CHECK ([TipoTitulacao]='1' OR [TipoTitulacao]='2' OR [TipoTitulacao]='3' OR [TipoTitulacao]='4' OR [TipoTitulacao]='5' OR [TipoTitulacao]='0' OR [TipoTitulacao]='');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	


GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [FK_AD_Associado_Isento_AD_Isencao] FOREIGN KEY ([IsencaoId]) REFERENCES [dbo].[AD_Isencao] ([IsencaoId]);


GO
ALTER TABLE [dbo].[AD_Associado_Isento]
    ADD CONSTRAINT [FK_AD_Associado_Isento_AD_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);
	
/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_ATC]
    ADD CONSTRAINT [CK_AD_ATC_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');
	
/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Cartao_Associado]
    ADD CONSTRAINT [FK_AD_Cartao_Associado_Associado] FOREIGN KEY ([AssociadoId]) REFERENCES [dbo].[AD_Associado] ([AssociadoId]);

	
/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [FK_AD_Colaborador_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Colaborador]
    ADD CONSTRAINT [CK_AD_Colaborador_TipoPerfil] CHECK ([TipoPerfil]='1' OR [TipoPerfil]='2' OR [TipoPerfil]='3');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [FK_AD_Endereco_AD_Pessoa] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[AD_Pessoa] ([PessoaId]);


GO
ALTER TABLE [dbo].[AD_Endereco]
    ADD CONSTRAINT [CK_AD_Endereco_TipoEndereco] CHECK ([TipoEndereco]='1' OR [TipoEndereco]='2' OR [TipoEndereco]='3');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [CK_AD_Evento_AceitaIsencaoAta] CHECK ([AceitaIsencaoAta]='0' OR [AceitaIsencaoAta]='1');


GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [CK_AD_Evento_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');


GO
ALTER TABLE [dbo].[AD_Evento]
    ADD CONSTRAINT [CK_AD_Evento_TipoEvento] CHECK ([TipoEvento]='1' OR [TipoEvento]='2' OR [TipoEvento]='3' OR [TipoEvento]='4' OR [TipoEvento]='5');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

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

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [CK_AD_Pessoa_Ativo] CHECK ([Ativo]='1' OR [Ativo]='0');


GO
ALTER TABLE [dbo].[AD_Pessoa]
    ADD CONSTRAINT [CK_AD_Pessoa_Sexo] CHECK ([Sexo]='M' OR [Sexo]='F');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

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

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Tipo_Publico]
    ADD CONSTRAINT [CK_AD_Tipo_Publico_Ativo] CHECK ([Ativo]='0' OR [Ativo]='1');

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Anuidade_Publico_AD_Anuidade] FOREIGN KEY ([AnuidadeId]) REFERENCES [dbo].[AD_Anuidade] ([AnuidadeId]);


GO
ALTER TABLE [dbo].[AD_Valor_Anuidade_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Anuidade_Publico_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	

GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Evento_Publico_AD_Evento] FOREIGN KEY ([EventoId]) REFERENCES [dbo].[AD_Evento] ([EventoId]);


GO
ALTER TABLE [dbo].[AD_Valor_Evento_Publico]
    ADD CONSTRAINT [FK_AD_Valor_Evento_Publico_AD_Tipo_Publico] FOREIGN KEY ([TipoPublicoId]) REFERENCES [dbo].[AD_Tipo_Publico] ([TipoPublicoId]);

/**************************************************************************************************************************************/	
/**************************************************************************************************************************************/	
