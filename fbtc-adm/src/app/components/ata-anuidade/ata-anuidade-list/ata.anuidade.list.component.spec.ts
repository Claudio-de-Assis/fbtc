import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtaAnuidadeListComponent } from './ata.anuidade.list.component';

describe('AtaAnuidadeListComponent', () => {
  let component: AtaAnuidadeListComponent;
  let fixture: ComponentFixture<AtaAnuidadeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtaAnuidadeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtaAnuidadeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
