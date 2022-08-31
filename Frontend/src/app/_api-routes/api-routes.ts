import { environment } from "src/environments/environment";

export class API {
    public static INSTITUTE_ROUTES = {
      saveInstitute: `/institute/${environment.API_Version}/create-institute`
    };
}