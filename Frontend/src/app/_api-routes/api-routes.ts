import { environment } from "src/environments/environment";

export class API {
  public static LOGIN_ROUTES = {
    login: `/${environment.API_Version}/authentication/login`
  };

  public static INSTITUTE_ROUTES = {
    saveInstitute: `/${environment.API_Version}/institute/create`
  };

  public static COUNTRY_ROUTES = {
    getCountries: `/${environment.API_Version}/country/list`
  }

  public static STATE_ROUTES = {
    getStateByCountryId: `/${environment.API_Version}/state/get-by-countryId`
  }
}