import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EntityService, MyEntity } from '../../services/entity.service';

@Component({
  selector: 'app-entity-list-form',
  templateUrl: './entity-list-form.component.html',
  styleUrls: ['./entity-list-form.component.css']
})
export class EntityListFormComponent implements OnInit {
  entities: MyEntity[] = [];
  entityForm: FormGroup;

  constructor(
    private entityService: EntityService,
    private fb: FormBuilder
  ) {
    this.entityForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.entityService.entities$.subscribe(data => {
      this.entities = data;
    });
  }

  onSubmit(): void {
    if (this.entityForm.valid) {
      this.entityService.addEntity(this.entityForm.value).subscribe(newEntity => {
        this.entityForm.reset();
      });
    }
  }
}
