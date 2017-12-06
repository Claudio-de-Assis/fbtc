import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';

import { AssociadoService } from '../../shared/services/associado.service';
import { CepCorreiosService } from './../../shared/services/cep-correios.service';
import { TipoPublicoService } from '../../shared/services/tipo-publico.service';
import { Associado } from '../../shared/model/associado';
// import { EnderecoCep } from '../../shared/model/endereco-cep';
import { debug } from 'util';
import { TipoPublico } from '../../shared/model/tipo-publico';

@Component({
    selector: 'app-associado-form',
    templateUrl: './associado.form.component.html',
    styleUrls: ['./associado.form.component.css'],
    providers: [AssociadoService, CepCorreiosService]
})
/** AssociadoForm component*/
export class AssociadoFormComponent implements OnInit {

    @Input() associado: Associado;
   /* @Input() cepCorreios: CepCorreios = {
        bairro: '',
        cidade: '',
        logradouro: '',
        estado_info: {area_km2: '', codigo_ibge: '', nome: '' },
        cep: '',
        cidade_info: {area_km2: '', codigo_ibge: ''},
        estado: '' };*/

    tiposPublicos: TipoPublico[];

    optSexo = [
        {name: 'Masculino', value: 'M'},
        {name: 'Feminino', value: 'F'}
    ];

    // retirar essa option:
    optATC = [
        {name: 'Rio de Janeiro', value: '1'},
        {name: 'Minas Gerais', value: '2'},
        {name: 'Alagoas', value: '3'}
    ];

    optTipoFormaContato = [
        {name: 'E-Mail', value: 1},
        {name: 'Celular', value: 2},
        {name: 'Endereço', value: 3},
        {name: 'Todos', value: 4}
    ];

    optTipoProfissao = [
        {name: 'Psicólogo', value: '7'},
        {name: 'Médico', value: '8'}
    ];

    optTipoTitulacao = [
        {name: 'Graduado', value: '1'},
        {name: 'Especialista', value: '2'},
        {name: 'Mestre', value: '3'},
        {name: 'Doutor', value: '4'},
        {name: 'Pós-Doutor', value: '5'}
    ];

    optBoolean = [
        {name: 'Sim', value: 'true'},
        {name: 'Não', value: 'false'}
    ];

    title = 'Dados do Associado - Edição';

    private selectedId: any;

    /** AssociadoFrm ctor */
    constructor(
        private service: AssociadoService,
        private serviceTP: TipoPublicoService,
        private router: Router,
        private route: ActivatedRoute,
        private serviceCEP: CepCorreiosService
    ) { }

    getAssociadoById(id: number): void {

        this.service.getById(id)
            .subscribe(associado => this.associado = associado);
    }

    setAssociado(): void {

        this.service.setAssociado()
            .subscribe(associado => this.associado = associado);
    }

    /** Called by Angular after AssociadoForm component initialized */
    ngOnInit(): void {

        this.getTiposPublicos();

        const id = +this.route.snapshot.paramMap.get('id');
        if (id > 0) {
            this.getAssociadoById(id);
        } else {
            this.setAssociado();
        }
    }

    gotoAssociados() {
        let associadoId = this.associado ? this.associado.associadoId : null;
        // Pass along the Associado id if available
        // so that the AssociadoList component can select that Associado.
        // Include a junk 'foo' property for fun.
        this.router.navigate(['/Associado', { id: associadoId, foo: 'foo' }]);
    }

    save() {

        this.service.addAssociado(this.associado)
            .subscribe(() => this.gotoAssociados());
    }

    excluir() {

        this.gotoAssociados();
    }

    getTiposPublicos(): void {

        this.serviceTP.getTiposPublicos().subscribe(tiposPublicos => this.tiposPublicos = tiposPublicos);
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
}
