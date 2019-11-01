import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { ResultModel } from '../../shared/model/result.model';
import { Observable } from 'rxjs';
import { PageModel } from '../model/page.model';
import { MenuInputModel, MenuOutputModel } from '../model/Menu.model';
import { UserInputModel, UserOutputModel } from '../model/User.model';


@Injectable({
    providedIn: 'root'
})
export class BaseDataService {

    constructor(
        private serviceBase: ServiceBase
    ) {
    }
    // 菜单
    ListPageMenu(model: MenuInputModel): Observable<PageModel<MenuOutputModel>> {
        return this.serviceBase.invokeService('Menu/ListPageMenu', model);
    }
    ModifyMenu(model: MenuInputModel): Observable<MenuOutputModel> {
        return this.serviceBase.invokeService('Menu/ModifyMenu', model);
    }
    DeleteMenu(model: []): Observable<number> {
        return this.serviceBase.invokeService('Menu/DeleteMenu', model);
    }
    GetMenu(inputID: any): Observable<MenuOutputModel> {
        return this.serviceBase.invokeService('Menu/GetMenu?ID=' + inputID, null);
    }
    // 用户
    ListPageUser(model: UserInputModel):
        Observable<PageModel<UserOutputModel>> {
        return this.serviceBase.invokeService('User/ListPageUser', model);
    }
    ModifyUser(model: UserInputModel): Observable<UserOutputModel> {
        return this.serviceBase.invokeService('User/ModifyUser', model);
    }
    DeleteUser(model: []): Observable<number> {
        return this.serviceBase.invokeService('User/DeleteUser', model);
    }
    GetUser(inputID: any): Observable<UserOutputModel> {
        return this.serviceBase.invokeService('User/GetUser?ID=' + inputID, null);
    }

}
