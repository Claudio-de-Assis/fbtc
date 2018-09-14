import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FichaFinanceiraListComponent } from './ficha.financeira.list.component';

describe('FichaFinanceiraListComponent', () => {
  let component: FichaFinanceiraListComponent;
  let fixture: ComponentFixture<FichaFinanceiraListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FichaFinanceiraListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FichaFinanceiraListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
