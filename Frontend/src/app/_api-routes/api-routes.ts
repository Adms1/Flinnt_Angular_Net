import { environment } from "src/environments/environment";

export class API {
  public static LOGIN_ROUTES = {
    login: `/${environment.API_Version}/authentication/login`
  };

  public static INSTITUTE_ROUTES = {
    getInstitute: `/${environment.API_Version}/institute`,
    saveInstitute: `/${environment.API_Version}/institute/create`,
    updateInstitute: `/${environment.API_Version}/institute/update`,
    accountVerify: `/${environment.API_Version}/institute/account-verify`
  };

  public static INSTITUTE_CONFIG_ROUTE = {
    getInstituteConfigureSession: `/${environment.API_Version}/institute/configure/session`,
    getInstituteType: `/${environment.API_Version}/institute/configure/type/list`,
    getInstituteGroupStructure: `/${environment.API_Version}/institute/configure/group-structure/list`,
    getInstituteBoard: `/${environment.API_Version}/institute/configure/board/list`,
    getInstituteMedium: `/${environment.API_Version}/institute/configure/medium/list`,
    getInstituteStandard: `/${environment.API_Version}/institute/configure/standard/list`,
    getInstituteDivision: `/${environment.API_Version}/institute/configure/division`,
    getDivisionByInstituteGroupId: `/${environment.API_Version}/institute/configure/division`,
    getInstituteGroup: `/${environment.API_Version}/institute/configure/group`,
    getSpecificInstituteGroup: `/${environment.API_Version}/institute/configure/group/get`,
    saveInstituteGroup:`/${environment.API_Version}/institute/configure/group/create`,
    saveInstituteDivision:`/${environment.API_Version}/institute/configure/division/create`,
    deleteInstituteDivision:`/${environment.API_Version}/institute/configure/division`,
    saveInstituteConfigureSession:`/${environment.API_Version}/institute/configure/session/create`
  }

  public static COUNTRY_ROUTES = {
    getCountries: `/${environment.API_Version}/country/list`
  }

  public static STATE_ROUTES = {
    getStateByCountryId: `/${environment.API_Version}/state/get-by-countryId`
  }

  public static PARENT_ROUTES = {
    dataFilter: `/${environment.API_Version}/parent/DataFilter`,
    saveParent: `/${environment.API_Version}/parent/create`,
    importParent: `/${environment.API_Version}/parent/validate-parent-import`
  };

  public static STUDENT_ROUTES = {
    saveStudent: `/${environment.API_Version}/student/create`,
    importStudent: `/${environment.API_Version}/student/validate-student-import`
  };

  public static USER_ROUTES = {
    getUserByEmailId: `/${environment.API_Version}/user/get/emailid`,
    getUserById: `/${environment.API_Version}/user/get/userid`,
  };
}