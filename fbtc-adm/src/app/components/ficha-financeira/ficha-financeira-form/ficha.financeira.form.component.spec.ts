import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FichaFinanceiraFormComponent } from './ficha.financeira.form.component';

describe('FichaFinanceiraFormComponent', () => {
  let component: FichaFinanceiraFormComponent;
  let fixture: ComponentFixture<FichaFinanceiraFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FichaFinanceiraFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FichaFinanceiraFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
