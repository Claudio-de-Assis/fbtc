import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociadoIsencaoListComponent } from './associado-isencao-list.component';

describe('AssociadoIsencaoListComponent', () => {
  let component: AssociadoIsencaoListComponent;
  let fixture: ComponentFixture<AssociadoIsencaoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssociadoIsencaoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociadoIsencaoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
