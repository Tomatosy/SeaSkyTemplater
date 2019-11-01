import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/shared/services/login.service';
import { MenuInputModel, MenuOutputModel } from '../../shared/model/Menu.model';
import { TreeviewItem } from 'ngx-treeview';
import { CommonService } from 'src/app/shared/services/common.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  menuInfo: MenuOutputModel;
  menuInfoInputModel: MenuInputModel;
  addmenuInfoInputModel: MenuInputModel = new MenuInputModel();
  isCollapse = false;
  menuTree: TreeviewItem[];

  constructor(
    private loginService: LoginService,
    private commonService: CommonService) { }

  onCollapse() {
    this.isCollapse = !this.isCollapse;
  }

  ngOnInit() {
    this.listAllMenuTree();
  }

  // 下拉树点击事件
  dropdownValueChange(e: any) {
    this.menuInfo.parentID = e;
  }

  // 树点击事件
  async onTreeSelect(e: any) {
    this.menuInfo = new MenuOutputModel();
    this.menuInfo.menuName = e.text;
    this.menuInfo.parentID = undefined;
    this.getMenu(e.value);
  }

  // 删除菜单
  onDeleteMenu(menuID: string) {
    const delListID = new Array();
    delListID.push(menuID);
    this.delMenu(delListID);
  }

  // 修改菜单
  onMenuSave() {
    this.menuInfoInputModel = new MenuInputModel();
    this.menuInfoInputModel = Object.assign(this.menuInfoInputModel, this.menuInfo);
    this.loginService.ModifyMenu(this.menuInfoInputModel).subscribe((response) => {
      this.menuInfo.parentID = undefined;
      this.listAllMenuTree();
      this.getMenu(this.menuInfoInputModel.menuID);
      this.commonService.showSuccess('修改成功');
    });
  }

  // 添加弹出框
  onAddMenu(menuInfo: any) {
    this.addmenuInfoInputModel = new MenuInputModel();
    if (this.menuInfo) {
      this.addmenuInfoInputModel.parentID = this.menuInfo.menuID;
    }
    // this.addmenuInfoInputModel.isNewWindow = true;
    this.addmenuInfoInputModel.isSystem = true;
    this.addmenuInfoInputModel.isUse = true;
  }

  // 添加子菜单
  onAddMenuSave() {
    this.addmenuInfoInputModel.parentID = this.addmenuInfoInputModel.parentID ?
      this.addmenuInfoInputModel.parentID : '00000000-0000-0000-0000-000000000000';
    this.loginService.ModifyMenu(this.addmenuInfoInputModel).subscribe(async (response) => {

      $('#add').modal('hide');
      if (this.menuInfo) {
        this.menuInfo.parentID = undefined;
        this.getMenu(this.menuInfo.menuID);
      }
      this.listAllMenuTree();
      this.commonService.showSuccess('添加成功');
    });
  }

  get isRootMenu(): boolean {
    if (this.menuInfo) {
      return this.menuInfo.parentID === '00000000-0000-0000-0000-000000000000';
    } else {
      return true;
    }
  }

  // 递归后台数据填充到树
  private initTree(e: MenuOutputModel): TreeviewItem {
    const childs = new Array<TreeviewItem>();
    if (e.children) {
      e.children.forEach(element => {
        childs.push(this.initTree(element));
      });
      const item = new TreeviewItem({
        text: e.menuName, value: e.menuID, children: childs
      });
      return item;
    } else {
      const item = new TreeviewItem({
        text: e.menuName, value: e.menuID
      });
      return item;
    }
  }

  // 左侧页面树
  private listAllMenuTree() {
    this.menuTree = new Array<TreeviewItem>();
    this.loginService.ListAllMenuTree().subscribe((response) => {
      response.forEach(element => {
        this.menuTree.push(this.initTree(element));
      });
    });
  }

  // 树点击事件后获取单个菜单
  private getMenu(menuID: string) {
    this.loginService.GetMenu(menuID).subscribe((response: MenuOutputModel) => {
      this.menuInfo = response;
      this.menuInfo.parentID = response.parentID;
    });
  }

  private delMenu(delListID: any) {
    this.loginService.DeleteMenu(delListID).subscribe((response) => {
      this.listAllMenuTree();
      this.menuInfo = null;
      this.commonService.showSuccess('删除成功!');
    });
  }
}
