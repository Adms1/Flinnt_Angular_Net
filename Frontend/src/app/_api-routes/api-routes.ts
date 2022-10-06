import { environment } from "src/environments/environment";

export class API {
  public static LOGIN_ROUTES = {
    login: `/${environment.API_Version}/authentication/login`
  };

  public static INSTITUTE_ROUTES = {
    saveInstitute: `/${environment.API_Version}/institute/create`,
    accountVerify: `/${environment.API_Version}/institute/account-verify`
  };

  public static INSTITUTE_CONFIG_ROUTE = {
    getInstituteType: `/${environment.API_Version}/institute/configure/type/list`,
    getInstituteGroupStructure: `/${environment.API_Version}/institute/configure/group-structure/list`,
    getInstituteBoard: `/${environment.API_Version}/institute/configure/board/list`,
    getInstituteMedium: `/${environment.API_Version}/institute/configure/medium/list`,
    getInstituteStandard: `/${environment.API_Version}/institute/configure/standard/list`,
    getInstituteDivision: `/${environment.API_Version}/institute/configure/division/list`
  }

  public static COUNTRY_ROUTES = {
    getCountries: `/${environment.API_Version}/country/list`
  }

  public static STATE_ROUTES = {
    getStateByCountryId: `/${environment.API_Version}/state/get-by-countryId`
  }
}