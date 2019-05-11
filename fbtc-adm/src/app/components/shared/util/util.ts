export class Util {

    public static lstAno = [2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019];

    public static optTipoPerfil = [
        {name: 'Gestor', value: '1'},
        {name: 'Secretário', value: '2'},
        {name: 'Financeiro', value: '3'},
        {name: 'Cliente Externo', value: '4'}
    ];

    public static lstMes = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];

    public static lstStatus = ['Adimplente', 'Inadimplente'];

    public static optBooleanFull = [
        {name: 'Todos', value: null},
        {name: 'Sim', value: true},
        {name: 'Não', value: false}
    ];

    public static optBoolean = [
        {name: 'Sim', value: true},
        {name: 'Não', value: false}
    ];

    public static optStatusAdimplencia = [
        {name: '', value: '99'},
        {name: 'Isento', value: '0'},
        {name: 'Aguardando pagamento', value: '1'},
        {name: 'Em análise', value: '2'},
        {name: 'Paga', value: '3'},
        {name: 'Disponível', value: '4'},
        {name: 'Em disputa', value: '5'},
        {name: 'Devolvida', value: '6'},
        {name: 'Cancelada', value: '7'},
        {name: 'Debitado', value: '8'},
        {name: 'Retenção temporária', value: '9'}
    ];

    /*
    public static optTiposEventos = [
        {name: 'Workshop Internacional', value: '2'},
        {name: 'Congresso Brasileiro', value: '4'},
    ];*/

    public static optSexo = [
        {name: 'Masculino', value: 'M'},
        {name: 'Feminino', value: 'F'}
    ];

    public static optTipoFormaContato = [
        {name: 'E-Mail', value: '1'},
        {name: 'Celular', value: '2'},
        {name: 'Endereço', value: '3'},
        {name: 'Todos', value: '4'}
    ];

    public static optTipoProfissao = [
        {name: 'Outros', value: '1'},
        {name: 'Psicólogo', value: '7'},
        {name: 'Médico', value: '8'}
    ];

    public static optTipoTitulacao = [
        {name: 'Graduado', value: '1'},
        {name: 'Especialista', value: '2'},
        {name: 'Mestre', value: '3'},
        {name: 'Doutor', value: '4'},
        {name: 'Pós-Doutor', value: '5'}
    ];

    public static optStatusPS = [
        {name: '', value: '99'},
        {name: 'Isento', value: '0'},
        {name: 'Aguardando pagamento', value: '1'},
        {name: 'Em Análise', value: '2'},
        {name: 'Paga', value: '3'},
        {name: 'Disponível', value: '4'},
        {name: 'Em Disputa', value: '5'},
        {name: 'Devolvida', value: '6'},
        {name: 'Cancelada', value: '7'},
        {name: 'Debitado', value: '8'},
        {name: 'Retenção Temporária', value: '9'}
    ];

    public static optObjetivoPagamento = [
       // {name: 'Evento', value: '1'},
        {name: 'Anuidade', value: '2'}
    ];

    public static optTipoEndereco = [
        {name: '', value: ''},
        {name: 'Residencial', value: '1'},
        {name: 'Comercial', value: '2'},
        {name: 'Outro', value: '3'}
    ];


    public static StringSanity(strEnt: string): string {

        let strRet = strEnt;

        if (strRet.length > 0) {
            strRet = strRet.trim();
             strRet = strRet.replace(/[^A-Za-z0-9_' 'áéíóúÁÉÍÓÚàÀãÃâÂêÊõÕôÔüÜçÇ/'/-]/g, '');
        }
        return strRet;
    }

    public static TelefoneSanity(strEnt: string): string {

        let strRet = strEnt;

        if (strRet.length > 0) {
            strRet = strRet.trim();
             strRet = strRet.replace(/[^0-9]/g, '');
        }
        return strRet;
    }

    public static NomeMes(mes: number): string {

        if (mes === 1) {
            return 'janeiro';
        } else if (mes === 2) {
            return 'fevereiro';
        } else if (mes === 3) {
            return 'março';
        } else if (mes === 4) {
            return 'abril';
        } else if (mes === 5) {
            return 'maio';
        } else if (mes === 6) {
            return 'junho';
        } else if (mes === 7) {
            return 'julho';
        } else if (mes === 8) {
            return 'agosto';
        } else if (mes === 9) {
            return 'setembro';
        } else if (mes === 10) {
            return 'outubro';
        } else if (mes === 11) {
            return 'novembro';
        } else if (mes === 12) {
            return 'dezembro';
        } else {
            return '';
        }
    }
}
