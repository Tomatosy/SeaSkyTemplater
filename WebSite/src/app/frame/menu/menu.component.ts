import { Component, OnInit, AfterViewInit, Output } from '@angular/core';
import { CommonService } from 'src/app/shared/services/common.service';
import { Router, NavigationEnd } from '@angular/router';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
})
export class MenuComponent implements OnInit, AfterViewInit {

  menuList: any;

  constructor(
    private commonService: CommonService,
    private router: Router,
    private loginService: LoginService,
  ) {
  }

  ngOnInit() {
    this.getListMenu();
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.setBreadcrumb();
      }
    });
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      $('.sidebar-menu').tree();
    }, 1000);
  }

  getBreadcrumb(tree, url: string, breadcrumb: Array<any>) {
    if (!tree) {
      return null;
    }
    for (let i = 0; i < tree.length; i++) {
      const element = tree[i];
      if (element.routingUrl !== '' && url.includes(element.routingUrl)) {
        breadcrumb.unshift({ name: element.menuName, url: element.routingUrl });
        return breadcrumb;
      }
      if (element.redirectUrl && element.redirectUrl.length > 0 && decodeURIComponent(url).includes(element.redirectUrl)) {
        breadcrumb.unshift({ name: element.menuName, url: '' });
        return breadcrumb;
      }
      if (element.children && element.children.length > 0) {
        this.getBreadcrumb(element.children, url, breadcrumb);
      }
      if (breadcrumb && breadcrumb.length > 0) {
        breadcrumb.unshift({ name: element.menuName, url: element.routingUrl });
        return breadcrumb;
      }
    }
    return breadcrumb;
  }

  onClick(item) {
    if (item.routingUrl) {
      const breadcrumb = this.getBreadcrumb(this.menuList, item.routingUrl, new Array());
      this.commonService.eventEmit.emit(breadcrumb);
    } else {
      const breadcrumb = this.getBreadcrumb(this.menuList, item.redirectUrl, new Array());
      this.commonService.eventEmit.emit(breadcrumb);
    }
  }

  private setBreadcrumb() {
    setTimeout(() => {
      const url = this.router.url;
      if (url !== '/Main') {
        const breadcrumb = this.getBreadcrumb(this.menuList, url, new Array());
        this.commonService.eventEmit.emit(breadcrumb);
      }
    });
  }

  private getListMenu() {
    this.loginService.ListAllMenuTree().subscribe((params) => {
      this.menuList = params;
      this.setBreadcrumb();
      $('.sidebar-menu').tree();
    });
  }
}
