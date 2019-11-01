import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { Observable } from 'rxjs';
import { PageModel } from '../model/page.model';
import { MenuInputModel, MenuOutputModel } from '../model/Menu.model';
@Injectable({
    providedIn: 'root'
})
export class LoginService {

    constructor(
        private serviceBase: ServiceBase
    ) {

    }

    // 菜单
    ListPageMenu(model: MenuInputModel):
        Observable<PageModel<MenuOutputModel>> {
        return this.serviceBase.invokeService('Menu/ListPageMenu', model);
    }
    ModifyMenu(model: MenuInputModel): Observable<MenuOutputModel> {
        return this.serviceBase.invokeService('Menu/ModifyMenu', model);
    }
    DeleteMenu(model: []): Observable<number> {
        return this.serviceBase.invokeService('Menu/DeleteMenu', model);
    }
    GetMenu(menuID: any): Observable<MenuOutputModel> {
        return this.serviceBase.invokeService('Menu/GetMenu?ID=' + menuID, null);
    }
    ListAllMenuTree():
        Observable<Array<MenuOutputModel>> {
        return this.serviceBase.invokeService('Menu/ListAllMenuTree', null);
    }
}