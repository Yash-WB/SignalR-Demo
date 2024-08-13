import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntityListFormComponent } from './entity-list-form.component';

describe('EntityListFormComponent', () => {
  let component: EntityListFormComponent;
  let fixture: ComponentFixture<EntityListFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EntityListFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EntityListFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
