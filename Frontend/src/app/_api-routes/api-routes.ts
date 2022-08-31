import { environment } from "src/environments/environment";

export class API {
    public static INSTITUTE_ROUTES = {
      saveInstitute: `/${environment.API_Version}/institute//create-institute`
    };
}