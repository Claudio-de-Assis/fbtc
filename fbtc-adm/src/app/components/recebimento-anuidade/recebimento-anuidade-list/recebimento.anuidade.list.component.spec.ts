import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecebimentoAnuidadeListComponent } from './recebimento.anuidade.list.component';

describe('RecebimentoAnuidadeListComponent', () => {
  let component: RecebimentoAnuidadeListComponent;
  let fixture: ComponentFixture<RecebimentoAnuidadeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecebimentoAnuidadeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecebimentoAnuidadeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
