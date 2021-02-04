import { ValidationRules } from "aurelia-validation";
import { HttpClient, json } from "aurelia-fetch-client";

export class Applicant {
  public ID: number;
  public Name: string;
  public FamilyName: string;
  public Address: string;
  public CountryOfOrigin: string;
  public EMailAddress: string;
  public Age: number;
  public Hired: boolean;
}

ValidationRules.customRule(
  "country_rule",
  (value: String) => {
    var ret = getCountryStatus(value);
    console.log(ret);
    return ret;
  },
  "Given coutry is not valid."
);

ValidationRules.ensure("Name")
  .required()
  .minLength(5)
  .withMessage("At least 5 characters are required.")
  .ensure("FamilyName")
  .required()
  .minLength(5)
  .withMessage("At least 5 characters are required.")
  .ensure("Address")
  .required()
  .minLength(10)
  .withMessage("At least 10 characters are required.")
  .ensure("EMailAddress")
  .required()
  .email()
  .withMessage("Please provide a valid email address.")
  .ensure("Age")
  .required()
  .between(20, 60)
  .withMessage("Age must be between 20 and 60.")
  .ensure("CountryOfOrigin")
  .required()
  .satisfiesRule("country_rule")
  .withMessage("Given country must be a valid country")
  .on(Applicant);

function getCountryStatus(value: String) {
  var http = new HttpClient();
  return http
    .fetch(`https://restcountries.eu/rest/v2/name/${value}?fullText=true`)
    .then((response) => {
      return response.json();
    })
    .then((data) => {
      console.log(data);
      if (data.hasOwnProperty("status")) {
        console.log("Has status");
        if (data.status != 200) {
          return false;
        } else {
          return true;
        }
      } else {
        console.log("Has no status");
        return true;
      }
    })
    .catch((error) => {
      console.log(error);
      return false;
    });
}
