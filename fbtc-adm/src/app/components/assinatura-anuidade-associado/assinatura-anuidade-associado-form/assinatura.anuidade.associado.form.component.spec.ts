import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinaturaAnuidadeAssociadoFormComponent } from './assinatura.anuidade.associado.form.component';

describe('AssinaturaAnuidadeAssociadoFormComponent', () => {
  let component: AssinaturaAnuidadeAssociadoFormComponent;
  let fixture: ComponentFixture<AssinaturaAnuidadeAssociadoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssinaturaAnuidadeAssociadoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssinaturaAnuidadeAssociadoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
