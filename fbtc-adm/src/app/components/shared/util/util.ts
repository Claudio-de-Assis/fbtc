export class Util {


    public static lstAno = [2019, 2018, 2017, 2016, 2015];

    // public static lstPerfil = ['Gestor do Site', 'Secretaria', 'Financeiro'];

    public static optTipoPerfil = [
        {name: 'Gestor', value: '1'},
        {name: 'Secretário', value: '2'},
        {name: 'Financeiro', value: '3'},
        {name: 'Cliente Externo', value: '4'}
    ];

    public static lstMes = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
    // public static lstStatus = ['Adimplente', 'Inadimplente'];

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

    public static optTiposEventos = [
        {name: 'Workshop Internacional', value: '2'},
        {name: 'Congresso Brasileiro', value: '4'},
    ];

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
        {name: '', value: '0'},
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
        {name: 'Evento', value: '1'},
        {name: 'Anuidade', value: '2'}
    ];

    public static optTipoEndereco = [
        {name: '', value: ''},
        {name: 'Residencial', value: '1'},
        {name: 'Comercial', value: '2'},
        {name: 'Outro', value: '3'}
    ];
}
