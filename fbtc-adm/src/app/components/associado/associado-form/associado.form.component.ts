import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { CepCorreiosService } from './../../shared/services/cep-correios.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { AtcService } from './../../shared/services/atc.service';

import { Associado } from '../../shared/model/associado';
import { TipoPublico } from '../../shared/model/tipo-publico';
import { Atc } from './../../shared/model/atc';

import { debug } from 'util';
import { Util } from './../../shared/util/util';

@Component({
    selector: 'app-associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css'],
    providers: [AssociadoService, CepCorreiosService]
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit {

    @Input() associado: Associado = { associadoId: 0, atcId: null, tipoPublicoId: null, nrMatricula: '', crp: '',
            crm: '', nomeInstFormacao: '', certificado: false, dtCertificacao: null, divulgarContato: false,
            tipoFormaContato: '', integraDiretoria: false, integraConfi: false, nrTelDivulgacao: '',
            comprovanteAfiliacaoAtc: '', tipoProfissao: '', tipoTitulacao: '',
            pessoaId: 0, nome: '', cpf: '', rg: '', eMail: '', nomeFoto: '',
            sexo: '', dtNascimento: null, nrCelular: '', passwordHash: '',
            dtCadastro: null, ativo: true,
            enderecoPessoa: { enderecoId: 0, pessoaId: 0, numero: '', complemento: '', tipoEndereco: '',
                bairro: '', cidade: '', logradouro: '', estado_info: { area_km2: '', codigo_ibge: '', nome: '' },
                cep: '', cidade_info: { area_km2: '', codigo_ibge: ''}, estado: ''}
    };

    title = 'Usuário'; // Associado
    badge = '';

    _util = Util;

    private selectedId: any;

    tiposPublicos: TipoPublico[];
    atcs: Atc[];

    submitted = false;

    /** AssociadoFrm ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceCEP: CepCorreiosService,
        private serviceAtc: AtcService
    ) { }

    getAssociadoById(id: number): void {

        this.service.getById(id)
            .subscribe(associado => this.associado = associado);
    }

    setAssociado(): void {

        this.service.setAssociado()
            .subscribe(associado => this.associado = associado);
    }

    gotoAssociados() {

        let associadoId = this.associado ? this.associado.associadoId : null;
        this.router.navigate(['/Associado', { id: associadoId, foo: 'foo' }]);
    }

    save() {

        this.service.addAssociado(this.associado)
        .subscribe(() =>  this.gotoShowPopUp());
    }

    gotoShowPopUp() {

      // Colocar a chamada para a implementação do PopUp modal de aviso:
      alert('Registro salvo com sucesso!');
    }

    excluir() {

        this.gotoAssociados();
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
    }

    getAtcs(): void {

        this.serviceAtc.getAtcs().subscribe(atcs => this.atcs = atcs);
    }

    getEnderecoByCep(): void {

        this.associado.enderecoPessoa.logradouro = '';
        this.associado.enderecoPessoa.numero = '';
        this.associado.enderecoPessoa.complemento = '';
        this.associado.enderecoPessoa.bairro = '';
        this.associado.enderecoPessoa.cidade = '';
        this.associado.enderecoPessoa.estado = '';

        this.serviceCEP.getByCep(this.associado.enderecoPessoa.cep)
            .subscribe(endereco => this.associado.enderecoPessoa = endereco);
    }

    onSubmit() {

        this.submitted = true;
        this.save();
    }

    /** Called by Angular after AssociadoForm component initialized */
    ngOnInit(): void {

        this.getAtcs();

        this.getTiposPublicos();

        const id = +this.route.snapshot.paramMap.get('id');
        if (id > 0) {
            this.badge = 'Edição';
            this.getAssociadoById(id);
        } else {
            this.badge = 'Novo';
            // this.setAssociado();
        }
    }
}
