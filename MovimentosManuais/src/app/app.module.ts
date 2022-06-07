import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CadastroComponent } from './screens/cadastro/cadastro.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxUiLoaderConfig, NgxUiLoaderModule, PB_DIRECTION, POSITION, SPINNER } from 'ngx-ui-loader';
import { NgxMaskModule } from 'ngx-mask';
import { AppConfigService } from './common/services/app-config.service';
import { CurrencyMaskConfig, CURRENCY_MASK_CONFIG, CurrencyMaskModule } from 'ng2-currency-mask';
import { OnlynumberDirective } from './common/directives/onlyNumbers.directive';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  fgsColor: '#64D1C6',                  //Cor do item ao centro da tela.
  pbColor: '#64D1C6',                   //Progress bar no topo
  bgsPosition: POSITION.bottomCenter,
  bgsSize: 120,
  bgsType: SPINNER.squareJellyBox,      //SPINNER.rectangleBounce,  //Type muda o formato do loader
  fgsType: SPINNER.threeStrings,
  pbDirection: PB_DIRECTION.leftToRight,
  pbThickness: 10,
};

export function appInit(appConfigService: AppConfigService) {
  return () => appConfigService.getConfig();
}

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: "right",
  allowNegative: true,
  decimal: ",",
  precision: 2,
  prefix: "R$ ",
  suffix: "",
  thousands: "."
};

@NgModule({
  declarations: [
    AppComponent,
    CadastroComponent,
    OnlynumberDirective
  ],
  exports: [
    OnlynumberDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    MaterialModule,
    MatSnackBarModule,
    FormsModule,
    ReactiveFormsModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxMaskModule.forRoot({ showMaskTyped: true }),
    CurrencyMaskModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: appInit,
      multi: true,
      deps: [AppConfigService]
    },
    {
      provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
