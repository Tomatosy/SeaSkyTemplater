import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: '', redirectTo: '/Login', pathMatch: 'full' },
  { path: 'Main', loadChildren: './frame/frame.module#FrameModule' },
  { path: 'Login', loadChildren: './login/login.module#LoginModule' },
  { path: 'Menu', loadChildren: './menu/menu.module#MenuModule' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
