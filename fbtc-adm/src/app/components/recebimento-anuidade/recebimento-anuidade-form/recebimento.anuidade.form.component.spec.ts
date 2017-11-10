import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecebimentoAnuidadeFormComponent } from './recebimento.anuidade.form.component';

describe('RecebimentoAnuidadeFormComponent', () => {
  let component: RecebimentoAnuidadeFormComponent;
  let fixture: ComponentFixture<RecebimentoAnuidadeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecebimentoAnuidadeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecebimentoAnuidadeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
