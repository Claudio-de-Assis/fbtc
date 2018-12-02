import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinaturaAnuidadeAssociadoListComponent } from './assinatura.anuidade.associado.list.component';

describe('AssinaturaAnuidadeAssociadoListComponent', () => {
  let component: AssinaturaAnuidadeAssociadoListComponent;
  let fixture: ComponentFixture<AssinaturaAnuidadeAssociadoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssinaturaAnuidadeAssociadoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssinaturaAnuidadeAssociadoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
