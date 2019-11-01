import { BasePageModel } from './base-page.model';

export class MenuModel extends BasePageModel {

  public menuID: string;
  public applicationID: string;
  public menuNo: string;
  public menuName: string;
  public menuIcon: string;
  public parentID: string;
  public routingUrl: string;
  public redirectUrl: string;
  public urlParameter: string;
  public isNewWindow: boolean;
  public isSystem: boolean;
  public isUse: boolean;
}

export class MenuInputModel extends MenuModel {

}

export class MenuOutputModel extends MenuModel {
  public isRoleChecked: boolean;
  public children: Array<MenuOutputModel>;
}

