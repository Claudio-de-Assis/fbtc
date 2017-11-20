import { Colaborador } from '../model/Colaborador';

export const COLABORADORES: Colaborador[] = [
    { ColaboradorId: 1, PessoaId: 7, TipoPerfil: '1', Nome: 'Joaquim Silva', EMail: 'joaquim@teste.com', NomeFoto: '', Sexo: 'M',
       DtNascimento: new Date('04-01-1960'), NrCelular: '21 999665522', PasswordHash: 'asksndnak',
        DtCadastro: new Date('01-01-2000'), Ativo: true},
    { ColaboradorId: 2, PessoaId: 8, TipoPerfil: '2', Nome: 'Mario Solto', EMail: 'mario@teste.com', NomeFoto: '', Sexo: 'M',
    DtNascimento: new Date('04-01-1960'), NrCelular: '21 888552299', PasswordHash: 'asksndnak',
        DtCadastro: new Date('01-01-2000'), Ativo: true},
    { ColaboradorId: 3, PessoaId: 9, TipoPerfil: '3', Nome: 'Victor da Silva', EMail: 'Victor@teste.com', NomeFoto: '', Sexo: 'M',
    DtNascimento: new Date('04-01-1960'), NrCelular: '21 888552211', PasswordHash: 'asksndnak',
        DtCadastro: new Date('01-01-2000'), Ativo: true},
    { ColaboradorId: 4, PessoaId: 10, TipoPerfil: '1', Nome: 'Jandira de Souza', EMail: 'Jandira@teste.com', NomeFoto: '', Sexo: 'F',
    DtNascimento: new Date('04-01-1960'), NrCelular: '21 888552777', PasswordHash: 'asksndnak',
        DtCadastro: new Date('01-01-2000'), Ativo: true}
];
