/****** Criação dos índices  *********************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Objeto: Table [dbo].[AD_Anuidade] Data do Script: 05/12/2017 09:23:01 ******/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Anuidade_AnuidadeId]
    ON [dbo].[AD_Anuidade]([AnuidadeId] ASC);
	
/****** Objeto: Table [dbo].[AD_Associado] Data do Script: 05/12/2017 09:25:50 *********************************************/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Associado_AssociadoId]
    ON [dbo].[AD_Associado]([AssociadoId] ASC);


/****** Objeto: Table [dbo].[AD_Associado_Isento] Data do Script: 05/12/2017 09:23:39 ****************************************/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Associado_Isento_AssociadoIsentoId]
    ON [dbo].[AD_Associado_Isento]([AssociadoIsentoId] ASC);

	
/****** Objeto: Table [dbo].[AD_ATC] Data do Script: 05/12/2017 09:23:51 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_ATC_AtcId]
    ON [dbo].[AD_ATC]([AtcId] ASC);
	
	
/****** Objeto: Table [dbo].[AD_Cartao_Associado] Data do Script: 05/12/2017 09:23:59 ******/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Cartao_Associado_CartaoAssociadoId]
    ON [dbo].[AD_Cartao_Associado]([CartaoAssociadoId] ASC);
	
/****** Objeto: Table [dbo].[AD_Colaborador] Data do Script: 05/12/2017 09:24:07 ******/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Colaborador_ColaboradorId]
    ON [dbo].[AD_Colaborador]([ColaboradorId] ASC);


/****** Objeto: Table [dbo].[AD_Endereco] Data do Script: 05/12/2017 09:51:30 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_Endereco_EnderecoId]
    ON [dbo].[AD_Endereco]([EnderecoId] ASC);


/****** Objeto: Table [dbo].[AD_Evento] Data do Script: 05/12/2017 09:24:20 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_Evento_EventoId]
    ON [dbo].[AD_Evento]([EventoId] ASC);
	
	
/****** Objeto: Table [dbo].[AD_Isencao] Data do Script: 05/12/2017 09:24:25 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_Isencao_IsencaoId]
    ON [dbo].[AD_Isencao]([IsencaoId] ASC);
	
/****** Objeto: Table [dbo].[AD_Parametro_Adimplencia] Data do Script: 05/12/2017 09:24:30 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_Parametro_Adimplencia_ParametroAdimplenciaId]
    ON [dbo].[AD_Parametro_Adimplencia]([ParametroAdimplenciaId] ASC);

/****** Objeto: Table [dbo].[AD_Pessoa] Data do Script: 05/12/2017 09:24:35 ******/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Pessoa_PessoaId]
    ON [dbo].[AD_Pessoa]([PessoaId] ASC);

/****** Objeto: Table [dbo].[AD_Recebimento] Data do Script: 05/12/2017 09:24:40 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_Recebimento_RecebimentoId]
    ON [dbo].[AD_Recebimento]([RecebimentoId] ASC);
	
	
/****** Objeto: Table [dbo].[AD_Tipo_Publico] Data do Script: 05/12/2017 09:24:46 ******/

GO
CREATE NONCLUSTERED INDEX [IX_AD_Tipo_Publico_TipoPublicoId]
    ON [dbo].[AD_Tipo_Publico]([TipoPublicoId] ASC);

/****** Objeto: Table [dbo].[AD_Valor_Anuidade_Publico] Data do Script: 05/12/2017 09:24:52 ******/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Valor_Anuidade_Publico_ValorAnuidadePublicoId]
    ON [dbo].[AD_Valor_Anuidade_Publico]([ValorAnuidadePublicoId] ASC);


/****** Objeto: Table [dbo].[AD_Valor_Evento_Publico] Data do Script: 05/12/2017 09:24:57 ******/
GO
CREATE NONCLUSTERED INDEX [IX_AD_Valor_Evento_Publico_ValorEventoPublicoId]
    ON [dbo].[AD_Valor_Evento_Publico]([ValorEventoPublicoId] ASC);
	
