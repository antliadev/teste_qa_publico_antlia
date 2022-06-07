import { Component, DEFAULT_CURRENCY_CODE, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import * as $ from 'jquery';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MovimentoManualViewModel } from 'src/app/common/models/movimentoManualViewModel.model';
import { MovimentoViewModel } from 'src/app/common/models/movimentoViewModel.model';
import { ResultValuePair } from 'src/app/common/models/resultValuePair.model';
import { MovimentoService } from 'src/app/common/services/movimento.service';

@Component({
    selector: 'app-cadastro',
    templateUrl: './cadastro.component.html',
    styleUrls: ['./cadastro.component.css'],
    providers: [
        {
            provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL'
        }
    ]
})
export class CadastroComponent implements OnInit {

    lstProdutos: ResultValuePair[] = [];
    lstCosif: ResultValuePair[] = [];
    lstMovimento: MovimentoViewModel[] = [];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    displayedColumns = ['mes', 'ano', 'numLancamento', 'desProduto', 'codClassificacao', 'valor']
    dataSource: MatTableDataSource<MovimentoViewModel>;

    disabledFields: boolean = true;
    disabledNew: boolean = false;
    disabledAdd: boolean = true;
    disabledClear: boolean = true;

    fields: MovimentoManualViewModel = new MovimentoManualViewModel;

    valor: string = '';

    constructor(public movimentoService: MovimentoService, private ngxLoader: NgxUiLoaderService) { }

    ngOnInit(): void {
        this.ngxLoader.start();

        this.listaTodosProdutos();
        this.listaTodosProdutoCosif();
        this.listaMovimentosManuais();

        this.ngxLoader.stop();
    }

    listaTodosProdutos() {
        this.movimentoService.listaTodosProdutos().subscribe((data: any[]) => {
            if (JSON.stringify(data['data']) != 'null' && JSON.stringify(data) != '{}' && JSON.stringify(data) != '[]' && typeof JSON.stringify(data) != 'undefined') {
                this.lstProdutos = [];
                this.lstProdutos.push({ codigo: 0, descricao: 'Selecione' });

                for (let index = 0; index < data['data'].length; index++) {
                    const element = data['data'][index];

                    this.lstProdutos.push(element);
                }

                this.fields.cOD_PRODUTO = 0;
            }
        });
    }

    listaTodosProdutoCosif() {
        this.movimentoService.listaTodosProdutoCosif().subscribe((data: any[]) => {
            if (JSON.stringify(data['data']) != 'null' && JSON.stringify(data) != '{}' && JSON.stringify(data) != '[]' && typeof JSON.stringify(data) != 'undefined') {
                this.lstCosif = [];
                this.lstCosif.push({ codigo: 0, descricao: 'Selecione' });

                for (let index = 0; index < data['data'].length; index++) {
                    const element = data['data'][index];

                    this.lstCosif.push(element);
                }

                this.fields.cOD_COSIF = 0;
            }
        });
    }

    listaProdutoCosifPorProduto(produto: number) {
        this.movimentoService.listaProdutoCosifPorProduto(produto).subscribe((data: any[]) => {
            if (JSON.stringify(data['data']) != 'null' && JSON.stringify(data) != '{}' && JSON.stringify(data) != '[]' && typeof JSON.stringify(data) != 'undefined') {
                this.lstCosif = [];
                this.lstCosif.push({ codigo: 0, descricao: 'Selecione' });

                for (let index = 0; index < data['data'].length; index++) {
                    const element = data['data'][index];

                    this.lstCosif.push(element);
                }

                this.fields.cOD_COSIF = 0;
            }
        });
    }

    listaMovimentosManuais() {
        this.movimentoService.listaMovimentosManuais().subscribe((data: any[]) => {
            if (JSON.stringify(data['data']) != 'null' && JSON.stringify(data) != '{}' && JSON.stringify(data) != '[]' && typeof JSON.stringify(data) != 'undefined') {
                this.lstMovimento = data['data'];
                this.dataSource = new MatTableDataSource(this.lstMovimento);

                this.dataSource.paginator = this.paginator;
            }
        });
    }

    changeProduto(produto: any) {
        this.fields.cOD_PRODUTO = produto.value;

        //this.listaProdutoCosifPorProduto(this.fields.cOD_PRODUTO);
    }

    changeCosif(cosif: any) {
        this.fields.cOD_COSIF = cosif.value;
    }

    replaceAll(text: string): string {
        return text.replace(/,/g, '#').replace(/\./g, ',').replace(/#/g, '.');
    }

    formatNumberLength(number: string, length: number): string {
        return ("0".repeat(length) + number).slice(length * (-1));
    }

    limpar() {
        this.limparCampos();
    }

    novo() {
        this.limparCampos();

        this.disabledFields = false;
        this.disabledNew = true;
        this.disabledClear = false;
        this.disabledAdd = false;
    }

    incluir() {
        this.ngxLoader.start();

        if (!this.validarCampos()) {
            this.ngxLoader.stop();
            return;
        }

        var numLancamento: number = 1;

        // if (this.lstMovimento.filter(x => x.mes == this.fields.dAT_MES && x.ano == this.fields.dAT_ANO).length > 0) {
        //     numLancamento = Math.max.apply(Math, this.lstMovimento.filter(x => x.mes == this.fields.dAT_MES && x.ano == this.fields.dAT_ANO).map(function (o) { return o.numLancamento; })) + 1;
        // }

        if (this.lstMovimento.length > 0) {
            numLancamento = Math.max.apply(Math, this.lstMovimento.map(function (o) { return o.numLancamento; })) + 1;
        }

        this.fields.nUM_LANCAMENTO = numLancamento;
        this.fields.cOD_USUARIO = '001';

        this.fields.vAL_VALOR = Number(this.valor);

        var data = JSON.parse(JSON.stringify(this.fields));

        this.movimentoService.InsereNovoMovimentoManual(data).subscribe((data: any[]) => {
            if (JSON.stringify(data['data']) != 'null' && JSON.stringify(data) != '{}' && JSON.stringify(data) != '[]' && typeof JSON.stringify(data) != 'undefined') {

                this.movimentoService.showMessage('Movimento Manual inserido com Sucesso', 5000, false);

                this.lstMovimento = data['data'];
                this.dataSource = new MatTableDataSource(this.lstMovimento);

                this.dataSource.paginator = this.paginator;

                this.disabledFields = true;

                this.disabledNew = false;
                this.disabledClear = true;
                this.disabledAdd = true;

                this.limparCampos();

                this.ngxLoader.stop();
            }
        });
    }

    limparCampos() {
        this.fields = new MovimentoManualViewModel;
        this.fields.cOD_COSIF = 0;
        this.fields.cOD_PRODUTO = 0;
        //this.valor = '';

        $("#lblMes").removeClass("lblError");
        $("#lblAno").removeClass("lblError");
        $("#lblProduto").removeClass("lblError");
        $("#lblCosif").removeClass("lblError");
        $("#lblValor").removeClass("lblError");
        $("#lblObservacao").removeClass("lblError");
    }

    validarCampos(): boolean {
        var msg: string = '';
        var valid: boolean = true;

        $("#lblMes").removeClass("lblError");
        $("#lblAno").removeClass("lblError");
        $("#lblProduto").removeClass("lblError");
        $("#lblCosif").removeClass("lblError");
        $("#lblValor").removeClass("lblError");
        $("#lblObservacao").removeClass("lblError");

        if (this.fields.dAT_MES == null || this.fields.dAT_MES == undefined || this.fields.dAT_MES.toString().trim().length == 0) {
            msg += "● O Mês deve ser informado;\n\n";

            $("#lblMes").addClass("lblError");

            valid = false;
        }
        // else if (this.fields.dAT_MES < 1 || this.fields.dAT_MES > 12) {
        //     msg += "● O Mês informado é inválido;\n\n";

        //     $("#lblMes").addClass("lblError");

        //     valid = false;
        // }

        if (this.fields.dAT_ANO == null || this.fields.dAT_ANO == undefined || this.fields.dAT_ANO.toString().trim().length == 0) {
            msg += "● O Ano deve ser informado;\n\n";

            $("#lblAno").addClass("lblError");

            valid = false;
        }
        // else {
        //     var anosAnteriores = new Date().getFullYear() - 10;
        //     var anoPosteriores = new Date().getFullYear() + 10;

        //     if (this.fields.dAT_ANO < anosAnteriores || this.fields.dAT_ANO > anoPosteriores) {
        //      msg += "● O Ano informado é inválido;\n\n";

        //      $("#lblAno").addClass("lblError");

        //      valid = false;
        //     }
        //  }

        if (this.fields.cOD_PRODUTO == null || this.fields.cOD_PRODUTO == undefined || this.fields.cOD_PRODUTO.toString().trim().length == 0 || this.fields.cOD_PRODUTO == 0) {
            msg += "● O Produto deve ser selecionado;\n\n";

            $("#lblProduto").addClass("lblError");

            valid = false;
        }

        if (this.fields.cOD_COSIF == null || this.fields.cOD_COSIF == undefined || this.fields.cOD_COSIF.toString().trim().length == 0 || this.fields.cOD_COSIF == 0) {
            msg += "● O Cosif deve ser selecionado;\n\n";

            $("#lblCosif").addClass("lblError");

            valid = false;
        }

        if (this.valor == null || this.valor == undefined || this.valor.toString().trim().length == 0) {
            msg += "● O Valor deve ser informado;\n\n";

            $("#lblValor").addClass("lblError");

            valid = false;
        }

        // if (this.fields.dES_DESCRICAO == null || this.fields.dES_DESCRICAO == undefined || this.fields.dES_DESCRICAO.toString().trim().length == 0) {
        //     msg += "● A Descrição deve ser informada;\n\n";

        //     $("#lblObservacao").addClass("lblError");

        //     valid = false;
        // }

        if (!valid) {
            msg = "Não é possível realizar a inclusão, pois o(s) erro(s) deve(m) ser corrigido(s):\n\n" + msg;

            this.movimentoService.showMessage(msg, 5000, false);
        }

        return valid;
    }
}
