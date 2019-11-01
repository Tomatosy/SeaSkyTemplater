import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotfindComponent } from './content/notfind/notfind.component';
import { NopermissionComponent } from './content/nopermission/nopermission.component';
import { ContentComponent } from './content/content.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '', component: ContentComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'Home', component: HomeComponent },
      { path: 'BaseData', loadChildren: '../baseData/baseData.module#BaseDataModule' },
      { path: 'nopermission', component: NopermissionComponent },
      { path: '**', component: NotfindComponent }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FrameRoutingModule { }
