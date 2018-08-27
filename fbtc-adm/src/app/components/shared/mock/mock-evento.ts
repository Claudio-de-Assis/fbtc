import { Evento } from '../model/evento';

export const EVENTOS: Evento[] = [
    {eventoId: 1, titulo: 'Tratamento Cognitivo Comportamental para o Transtorno de Ansiedade',
        descricao: 'Workshop internacional do dia....', codigo: '6CSNKQQN2', dtInicio: new Date('2018-04-19'),
        dtTermino: new Date('2018-04-22'), dtTerminoInscricao: new Date('2018-04-22'), tipoEvento: '1',
         aceitaIsencaoAta: true, ativo: true, nomeFoto: 'NO-FOTO.PNG' },

    {eventoId: 2, titulo: 'Tratamento Cognitivo Comportamental',
        descricao: 'Workshop Tratamento Cognitivo Comportamental....', codigo: 'XPTOKQQN2', dtInicio: new Date('2017-03-03'),
        dtTermino: new Date('2017-03-05'), dtTerminoInscricao: new Date('2017-03-05'), tipoEvento: '2',
        aceitaIsencaoAta: true, ativo: true, nomeFoto: 'NO-FOTO.PNG' },

    {eventoId: 3, titulo: 'Tratamento para o Transtorno de Ansiedade',
        descricao: 'Workshop Tratamento para o Transtorno de Ansiedade....', codigo: 'ERDGRDFG3', dtInicio: new Date('2016-02-03'),
        dtTermino: new Date('2016-02-03'), dtTerminoInscricao: new Date('2016-02-03'),
        tipoEvento: '3', aceitaIsencaoAta: true, ativo: true, nomeFoto: 'NO-FOTO.PNG' },

    {eventoId: 4, titulo: 'Tratamento de Ansiedade',
        descricao: 'Workshop Tratamento de Ansiedade....', codigo: 'EREWERDRT1', dtInicio: new Date('2015-12-01'),
        dtTermino: new Date('2015-12-03'), dtTerminoInscricao: new Date('2015-12-03'), tipoEvento: '4',
        aceitaIsencaoAta: true, ativo: true, nomeFoto: 'NO-FOTO.PNG' },

    {eventoId: 5, titulo: 'Tratamento Cognitivo',
        descricao: 'Workshop Tratamento Cognitivo....', codigo: 'SXDCDFCV9', dtInicio: new Date('2014-04-19'),
        dtTermino: new Date('2014-04-22'), dtTerminoInscricao: new Date('2014-04-22'), tipoEvento: '5',
        aceitaIsencaoAta: true, ativo: true, nomeFoto: 'NO-FOTO.PNG' },
];

