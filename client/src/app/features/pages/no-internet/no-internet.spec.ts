import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoInternet } from './no-internet';

describe('NoInternet', () => {
  let component: NoInternet;
  let fixture: ComponentFixture<NoInternet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoInternet]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NoInternet);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
