import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BaseDataRoutingModule } from './baseData-routing.module';
import { UserComponent } from './user/user.component';
import { UserModalComponent } from './user/modals/user-modal.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    UserComponent,
    UserModalComponent
  ],
  imports: [
    CommonModule,
    BaseDataRoutingModule,
    FormsModule,
    SharedModule.forRoot()
  ]
})
export class BaseDataModule { }
