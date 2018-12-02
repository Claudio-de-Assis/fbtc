import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinaturaAnuidadeFormComponent } from './assinatura.anuidade.form.component';

describe('AssinaturaAnuidadeFormComponent', () => {
  let component: AssinaturaAnuidadeFormComponent;
  let fixture: ComponentFixture<AssinaturaAnuidadeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssinaturaAnuidadeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssinaturaAnuidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
