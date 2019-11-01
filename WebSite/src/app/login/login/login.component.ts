import { Component, OnInit } from '@angular/core';
// import {
//   UserInputModel,
//   UserOutputModel
// } from 'src/app/shared/model/baseData/User.model';
// import { BaseDataService } from 'src/app/shared/services/database.service';
import { Router } from '@angular/router';
import { CacheService } from 'src/app/shared/services/cache.service';
import { PageModel } from 'src/app/shared/model/page.model';
import { ToastrService } from 'ngx-toastr';
import { Md5 } from '../../../../node_modules/ts-md5/dist/md5';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  // UserInputModel: UserInputModel = new UserInputModel();
  // UserOutputModel: PageModel<UserOutputModel> = new PageModel<UserOutputModel>();
  constructor(
    private router: Router,
    // private baseDataService: BaseDataService,
    private toastrService: ToastrService,
    private cacheService: CacheService) { }

  ngOnInit() {
    // $('#RememberPassword').iCheck({
    //   handle: 'checkbox',
    //   checkboxClass: 'icheckbox_flat-green'
    // });
  }
  onLogin() {
    // if (!$.trim(this.UserInputModel.userName)) {
    //   this.toastrService.error('请输入名称', 'Error!', {
    //     positionClass: 'toast-top-center',
    //     timeOut: 3000, closeButton: true
    //   });
    //   return;
    // }
    // if (!$.trim(this.UserInputModel.loginPwd)) {
    //   this.toastrService.error('请输入密码', 'Error!', {
    //     positionClass: 'toast-top-center',
    //     timeOut: 3000, closeButton: true
    //   });
    //   return;
    // }
    // this.login();
  }

  // private encryption(pwd: string) {
  //   if (this.UserInputModel.personnelMaintenancePwd === '') {
  //     return;
  //   }
  //   let pwd_Md5;
  //   pwd_Md5 = Md5.hashStr(pwd);
  //   let pwd_base64;
  //   return pwd_base64 = new Buffer(pwd_Md5).toString('base64');
  // }

  // private login() {
  //   this.baseDataService.Login(this.UserInputModel)
  //     .subscribe((params) => {
  //       if (!params.data || params.data.length === 0) {
  //         this.toastrService.error('用户名或密码错误！', 'Error!', {
  //           positionClass: 'toast-top-center',
  //           timeOut: 3000, closeButton: true
  //         });
  //         return;
  //       }
  //       this.cacheService.setLocalCaches('user', params.data[0]);
  //       this.router.navigateByUrl('/Main/Home');
  //     });
  // }
}
