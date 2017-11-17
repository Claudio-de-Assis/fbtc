import { TipoPublico } from '../model/tipo.publico';

export const TIPOSPUBLICOS: TipoPublico[] = [
    {TipoPublicoId: 1, Nome: 'Profissional - Associado',
        DescricaoValor: 'Valores para profissionais associados...', Ativo: true},
    {TipoPublicoId: 2, Nome: 'Profissional - Não Associado',
        DescricaoValor: 'Valores para profissionais não associados...', Ativo: true},
    {TipoPublicoId: 3, Nome: 'Estudante de Pós - Associado',
        DescricaoValor: 'Valores para estudantes de Pós associados...', Ativo: true},
    {TipoPublicoId: 4, Nome: 'Estudante de Pós - Não Associado',
        DescricaoValor: 'Valores para estudantes de Pós não associados...', Ativo: true},
    {TipoPublicoId: 5, Nome: 'Estudante - Associado',
        DescricaoValor: 'Valores para estudantes associados...', Ativo: true},
    {TipoPublicoId: 6, Nome: 'Estudante - Não Associado',
        DescricaoValor: 'Valores para estudantes não associados...', Ativo: true},
];
