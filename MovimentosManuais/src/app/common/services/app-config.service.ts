import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  public baseUrl: string;
  public baseUrlManagement: string;
  public gameUrl: string;
  public contentsUrl: string;

  public crudGameUrl: string;
  public managementUrl: string;

  public scormUrl: string;

  public dataSourceHeaders = new BehaviorSubject<any>({});
  dataHeaders = this.dataSourceHeaders.asObservable();

  constructor(private http: HttpClient) { }

  async getConfig() {
    return await this.http.get('/assets/settings/config.json').toPromise().then(data => {
      if (isDevMode()) {
        this.baseUrl = data['apiUrlLocal'];
      } else {
        this.baseUrl = data['apiUrlProd'];
      }
    });
  }

  async setTokenHeaders(token: any) {
    await this.dataSourceHeaders.next(token);
  }
}
