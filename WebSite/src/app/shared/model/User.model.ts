import { BasePageModel } from './base-page.model';

export class UserModel extends BasePageModel {

	public userID: string;
	public userName: string;
	public userNo: string;
	public loginNo: string;
	public loginPwd: string;
	public userType: string;
	public email: string;
	public telephone: string;
	public lockState: boolean;
	public useState: boolean;
	public extend1: string;
	public extend2: string;
	public extend3: string;
	public extend4: string;
	public extend5: string;
	public extend6: string;
	public extend7: string;
	public extend8: string;
	public extend9: string;
	public extend10: string;

}

export class UserInputModel extends UserModel {

}

export class UserOutputModel extends UserModel {

}
