import { TipoPublico } from '../model/tipo-publico';

export const TIPOSPUBLICOS: TipoPublico[] = [
    {tipoPublicoId: 1, nome: 'Profissional - Associado',
        descricaoValor: 'Valores para profissionais associados...', ativo: true},
    {tipoPublicoId: 2, nome: 'Profissional - Não Associado',
        descricaoValor: 'Valores para profissionais não associados...', ativo: true},
    {tipoPublicoId: 3, nome: 'Estudante de Pós - Associado',
        descricaoValor: 'Valores para estudantes de Pós associados...', ativo: true},
    {tipoPublicoId: 4, nome: 'Estudante de Pós - Não Associado',
        descricaoValor: 'Valores para estudantes de Pós não associados...', ativo: true},
    {tipoPublicoId: 5, nome: 'Estudante - Associado',
        descricaoValor: 'Valores para estudantes associados...', ativo: true},
    {tipoPublicoId: 6, nome: 'Estudante - Não Associado',
        descricaoValor: 'Valores para estudantes não associados...', ativo: true},
];
