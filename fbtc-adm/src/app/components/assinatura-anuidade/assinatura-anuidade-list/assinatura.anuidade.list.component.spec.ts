import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssinaturaAnuidadeListComponent } from './assinatura.anuidade.list.component';

describe('AssinaturaAnuidadeListComponent', () => {
  let component: AssinaturaAnuidadeListComponent;
  let fixture: ComponentFixture<AssinaturaAnuidadeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssinaturaAnuidadeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssinaturaAnuidadeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
