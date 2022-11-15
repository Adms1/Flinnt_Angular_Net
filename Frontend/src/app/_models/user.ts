import { UserInstitute } from "./user-Institute";
import { UserProfile } from "./user-profile";

export class LoginUser {
    userName: string;
    password: string;
}

export class User{
    userTypeId : number;
    userId : number;
    loginId : string;
    userInstitutes : Array<UserInstitute>;
    userProfiles: UserProfile;
}