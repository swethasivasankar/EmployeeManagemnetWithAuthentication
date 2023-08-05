import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { JwtInterceptor } from "./_helpers/jwt.interceptor";
import { ErrorInterceptor } from "./_helpers/error.interceptor";
import { HomeComponent } from "./home/home.component";
import { AlertComponent } from "./_components/alert.component";
import { AppRoutingModule } from "./app-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { EmployeeComponent } from './employee/employee.component';
import { EmployerComponent } from './employer/employer.component';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        HttpClientModule,
    
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { };