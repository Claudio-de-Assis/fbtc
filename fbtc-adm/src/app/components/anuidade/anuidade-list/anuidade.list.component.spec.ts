import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnuidadeListComponent } from './anuidade.list.component';

describe('Anuidade.ListComponent', () => {
  let component: AnuidadeListComponent;
  let fixture: ComponentFixture<AnuidadeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnuidadeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnuidadeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
