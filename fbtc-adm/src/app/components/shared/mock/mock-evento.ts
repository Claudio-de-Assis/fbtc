import { Evento } from '../model/Evento';

export const EVENTOS: Evento[] = [
    {EventoId: 1, Titulo: 'Tratamento Cognitivo Comportamental para o Transtorno de Ansiedade',
        Descricao: 'Workshop internacional do dia....', Codigo: '6CSNKQQN2', DtInicio: new Date('2018-04-19'),
        DtTermino: new Date('2018-04-22'), DtTerminoInscricao: new Date('2018-04-22'), TipoEvento: '1',
         AceitaIsencaoAta: true, Ativo: true, NomeFoto: 'NO-FOTO.PNG' },

    {EventoId: 2, Titulo: 'Tratamento Cognitivo Comportamental',
        Descricao: 'Workshop Tratamento Cognitivo Comportamental....', Codigo: 'XPTOKQQN2', DtInicio: new Date('2017-03-03'),
        DtTermino: new Date('2017-03-05'), DtTerminoInscricao: new Date('2017-03-05'), TipoEvento: '2',
        AceitaIsencaoAta: true, Ativo: true, NomeFoto: 'NO-FOTO.PNG' },

    {EventoId: 3, Titulo: 'Tratamento para o Transtorno de Ansiedade',
        Descricao: 'Workshop Tratamento para o Transtorno de Ansiedade....', Codigo: 'ERDGRDFG3', DtInicio: new Date('2016-02-03'),
        DtTermino: new Date('2016-02-03'), DtTerminoInscricao: new Date('2016-02-03'),
        TipoEvento: '3', AceitaIsencaoAta: true, Ativo: true, NomeFoto: 'NO-FOTO.PNG' },

    {EventoId: 4, Titulo: 'Tratamento de Ansiedade',
        Descricao: 'Workshop Tratamento de Ansiedade....', Codigo: 'EREWERDRT1', DtInicio: new Date('2015-12-01'),
        DtTermino: new Date('2015-12-03'), DtTerminoInscricao: new Date('2015-12-03'), TipoEvento: '4',
        AceitaIsencaoAta: true, Ativo: true, NomeFoto: 'NO-FOTO.PNG' },

    {EventoId: 5, Titulo: 'Tratamento Cognitivo',
        Descricao: 'Workshop Tratamento Cognitivo....', Codigo: 'SXDCDFCV9', DtInicio: new Date('2014-04-19'),
        DtTermino: new Date('2014-04-22'), DtTerminoInscricao: new Date('2014-04-22'), TipoEvento: '5',
        AceitaIsencaoAta: true, Ativo: true, NomeFoto: 'NO-FOTO.PNG' },
];

/*
    Workshop internacional e Congresso: 1
    Workshop Internacional: 2
    Workshop Nacional: 3
    Congresso: 4
    Certificação: 5
*/
