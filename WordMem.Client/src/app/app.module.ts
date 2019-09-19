import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { AppComponent } from './app.component';
import { appRoutingModule } from './app.routing';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { LoginComponent } from './login';;
import { WordsComponent } from './words/words.component';
import { TestapiComponent } from './testapi/testapi.component';
import { HomeComponent } from './_shared/home/home.component';
import { NotfoundComponent } from './_shared/notfound/notfound.component';
import { NavbarComponent } from './_shared/navbar/navbar.component';
import { FooterComponent } from './_shared/footer/footer.component';
import { AlertifyService} from './_services/alertify.service';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        appRoutingModule
    ],
    declarations: [
        AppComponent,
        LoginComponent
,
        WordsComponent ,
        TestapiComponent ,
        HomeComponent ,
        NotfoundComponent ,
        NavbarComponent,
        FooterComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        AlertifyService

    ],
    bootstrap: [AppComponent]
})
export class AppModule { }