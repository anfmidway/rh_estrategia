import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
import { Pessoa } from '../../models/Pessoa';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { PessoaService } from '../../services/pessoa.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
  
import { ActivatedRoute } from '@angular/router';
   
@Component({
  selector: 'app-pessoas',
  templateUrl: './pessoas.component.html',
  styleUrls: ['./pessoas.component.css']
})
export class pessoasComponent implements OnInit, OnDestroy {

  public modalRef: BsModalRef;
  public pessoaForm: FormGroup;
  public titulo = 'pessoas';
  public pessoaSelecionado: pessoa;
  public textSimple: string;
  

  private unsubscriber = new Subject();

  public pessoas: Pessoa[];
  public pessoa: Pessoa;
  public msnDeletePessoa: string;
  public modeSave = 'post';

   
  closeModal() {
    this.modalRef.hide();
  }
 

  constructor(
    private pessoaService: pessoaService,
    private route: ActivatedRoute,
     
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.criarForm();
  }

  ngOnInit() {
    this.carregarpessoas();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

  criarForm() {
    this.pessoaForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      telefone: ['', Validators.required]
    });
  }

  savepessoa() {
    if (this.pessoaForm.valid) {
      this.spinner.show();

      if (this.modeSave === 'post') {
        this.pessoa = {...this.pessoaForm.value};
      } else {
        this.pessoa = {id: this.pessoaSelecionado.id, ...this.pessoaForm.value};
      }

      this.pessoaService[this.modeSave](this.pessoa)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.carregarpessoas();
            this.toastr.success('pessoa salvo com sucesso!');
          }, (error: any) => {
            this.toastr.error(`Erro: pessoa não pode ser salvo!`);
            console.error(error);
          }, () => this.spinner.hide()
        );

    }
  }

  carregarpessoas() {
    const id = +this.route.snapshot.paramMap.get('id');

    this.spinner.show();
    this.pessoaService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((pessoas: pessoa[]) => {
        this.pessoas = pessoas;

        if (id > 0) {
          this.pessoaSelect(this.pessoas.find(pessoa => pessoa.id === id));
        }

        this.toastr.success('pessoas foram carregado com Sucesso!');
      }, (error: any) => {
        this.toastr.error('pessoas não carregados!');
        console.log(error);
      }, () => this.spinner.hide()
    );
  }

  pessoaSelect(pessoa: pessoa) {
    this.modeSave = 'put';
    this.pessoaSelecionado = pessoa;
    this.pessoaForm.patchValue(pessoa);
  }

  voltar() {
    this.pessoaSelecionado = null;
  }

}