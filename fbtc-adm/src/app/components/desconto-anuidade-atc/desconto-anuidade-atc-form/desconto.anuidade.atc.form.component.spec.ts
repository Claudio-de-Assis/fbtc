import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DescontoAnuidadeAtcFormComponent } from './desconto.anuidade.atc.form.component';

describe('DescontoAnuidadeAtcFormComponent', () => {
  let component: DescontoAnuidadeAtcFormComponent;
  let fixture: ComponentFixture<DescontoAnuidadeAtcFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DescontoAnuidadeAtcFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DescontoAnuidadeAtcFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
