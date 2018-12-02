import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DescontoAnuidadeAtcListComponent } from './desconto.anuidade.atc.list.component';

describe('DescontoAnuidadeAtcListComponent', () => {
  let component: DescontoAnuidadeAtcListComponent;
  let fixture: ComponentFixture<DescontoAnuidadeAtcListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DescontoAnuidadeAtcListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DescontoAnuidadeAtcListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
