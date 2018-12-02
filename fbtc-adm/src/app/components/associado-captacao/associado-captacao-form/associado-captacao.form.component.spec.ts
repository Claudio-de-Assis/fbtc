import { AssociadoCaptacaoFormComponent } from './associado-captacao.form.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('AssociadoCaptacaoFormComponent', () => {
  let component: AssociadoCaptacaoFormComponent;
  let fixture: ComponentFixture<AssociadoCaptacaoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoCaptacaoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoCaptacaoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
