import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { MenuComponent } from './menu/menu.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { ContentComponent } from './content/content.component';
import { NotfindComponent } from './content/notfind/notfind.component';
import { NopermissionComponent } from './content/nopermission/nopermission.component';
import { FrameRoutingModule } from './frame-routing.module';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    MenuComponent,
    SidebarComponent,
    ContentComponent,
    NotfindComponent,
    NopermissionComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    FrameRoutingModule,
    FormsModule,
    SharedModule.forRoot()
  ]
})
export class FrameModule { }
