import { HttpClient, json } from "aurelia-fetch-client";
import { inject } from "aurelia-framework";

@inject(HttpClient)
export class ApplicantList {
  public applicants;
  public selectedId: number;

  constructor(private http: HttpClient) {
    this.http.configure((config) => {
      config.withDefaults({
        credentials: "same-origin",
        mode: "cors",
        headers: {
          Accept: "application/json",
          "X-Requested-With": "Fetch",
        },
      });
    });
  }

  created() {
    this.http
      .fetch("http://localhost:5000/api/Applicant")
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        this.applicants = data;
      })
      .catch((error) => {
        console.log(error);
      });
  }

  select(applicant) {
    this.selectedId = applicant.id;
    return true;
  }
}
