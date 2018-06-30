import { EnderecoCep } from './endereco-cep';

export class Endereco extends EnderecoCep {
    enderecoId: number;
    pessoaId: number;
    numero: string;
    complemento: string;
    tipoEndereco: string;
    ordemEndereco: string;
}
