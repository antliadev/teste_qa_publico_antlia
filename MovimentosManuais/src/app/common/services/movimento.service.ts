import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatSnackBar, MatSnackBarConfig } from "@angular/material/snack-bar";
import { EMPTY, Observable } from "rxjs";
import { ResultViewModel } from "../models/resultViewModel.model";
import { AppConfigService } from "./app-config.service";
import { catchError, map, retry } from "rxjs/operators";

@Injectable({
    providedIn: 'root'
})

export class MovimentoService {

    constructor(public config: AppConfigService, private http: HttpClient, protected snackBar: MatSnackBar) { }

    listaMovimentosManuais(): Observable<ResultViewModel[]>  {
        return this.http.get<ResultViewModel[]>(this.config.baseUrl + "/Movimentos/ListaTodosMovimentosManuais").pipe(
            retry(3),
            map((obj) => obj),
            catchError((e) => this.errorHandler(e))
        );
    }

    listaTodosProdutos(): Observable<ResultViewModel[]>  {
        return this.http.get<ResultViewModel[]>(this.config.baseUrl + "/Movimentos/ListaTodosProdutos").pipe(
            retry(3),
            map((obj) => obj),
            catchError((e) => this.errorHandler(e))
        );
    }

    listaTodosProdutoCosif(): Observable<ResultViewModel[]>  {
        return this.http.get<ResultViewModel[]>(this.config.baseUrl + "/Movimentos/ListaTodosProdutoCosif").pipe(
            retry(3),
            map((obj) => obj),
            catchError((e) => this.errorHandler(e))
        );
    }

    listaProdutoCosifPorProduto(produto: any): Observable<ResultViewModel[]>  {
        return this.http.get<ResultViewModel[]>(this.config.baseUrl + "/Movimentos/ListaProdutoCosifPorProduto/" + produto).pipe(
            retry(3),
            map((obj) => obj),
            catchError((e) => this.errorHandler(e))
        );
    }

    InsereNovoMovimentoManual(data: JSON): Observable<ResultViewModel[]>  {
        return this.http.post<ResultViewModel[]>(this.config.baseUrl + "/Movimentos/InsereNovoMovimentoManual", data).pipe(
            retry(3),
            map((obj) => obj),
            catchError((e) => this.errorHandler(e))
        );
    }

    showMessage(msg: string, duration: number, isError: boolean = false): void {
        let config = new MatSnackBarConfig();

        //config.panelClass = isError == true ? ['alert-red'] : ['alert-sucess'];
        config.duration = duration;
        config.horizontalPosition = "center";
        config.verticalPosition = "top";

        this.snackBar.open(msg, "X", config);
    }

    errorHandler(e: any): Observable<any> {
        this.showMessage("Ocorreu um erro! " + e, 5000, true);
        return EMPTY;
    }
}