import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { UserModalComponent } from './user/modals/user-modal.component';

const routes: Routes = [
  { path: 'User', component: UserComponent },
  { path: 'UserModal', component: UserModalComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BaseDataRoutingModule { }
