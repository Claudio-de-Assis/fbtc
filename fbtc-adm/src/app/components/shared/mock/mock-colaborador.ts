import { Colaborador } from '../model/Colaborador';


export const COLABORADORES: Colaborador[] = [
    { ColaboradorId: 1, pessoaId: 7, TipoPerfil: '1', nome: 'Joaquim Silva', eMail: 'joaquim@teste.com', nomeFoto: '', sexo: 'M',
       dtNascimento: new Date('04-01-1960'), nrCelular: '21 999665522', passwordHash: 'asksndnak',
        dtCadastro: new Date('01-01-2000'), ativo: true},
    { ColaboradorId: 2, pessoaId: 8, TipoPerfil: '2', nome: 'Mario Solto', eMail: 'mario@teste.com', nomeFoto: '', sexo: 'M',
    dtNascimento: new Date('04-01-1960'), nrCelular: '21 888552299', passwordHash: 'asksndnak',
        dtCadastro: new Date('01-01-2000'), ativo: true},
    { ColaboradorId: 3, pessoaId: 9, TipoPerfil: '3', nome: 'Victor da Silva', eMail: 'Victor@teste.com', nomeFoto: '', sexo: 'M',
    dtNascimento: new Date('04-01-1960'), nrCelular: '21 888552211', passwordHash: 'asksndnak',
        dtCadastro: new Date('01-01-2000'), ativo: true},
    { ColaboradorId: 4, pessoaId: 10, TipoPerfil: '1', nome: 'Jandira de Souza', eMail: 'Jandira@teste.com', nomeFoto: '', sexo: 'F',
    dtNascimento: new Date('04-01-1960'), nrCelular: '21 888552777', passwordHash: 'asksndnak',
        dtCadastro: new Date('01-01-2000'), ativo: true}
];
