import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PessoaComponent } from './components/pessoa/pessoa.component';

const routes: Routes = [
  { path: 'pessoa', component: PessoaComponent } ,
  { path: 'app', component: Component } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
