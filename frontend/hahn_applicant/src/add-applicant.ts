import { Applicant } from './Applicant';
import { HttpClient, json } from "aurelia-fetch-client";
import { inject } from "aurelia-framework";

@inject(HttpClient)
export class AddApplicant {
  public applicant;

  constructor(private http: HttpClient) {
    this.http.configure(config => {
      config
        .withDefaults({
          credentials: 'same-origin',
          mode: 'cors',
          headers: {
            'Accept': 'application/json',
            'X-Requested-With': 'Fetch'
          }
        });
    });
  }

  created() {
    this.reset();
  }
  add(){
    console.log(this.applicant);
    // this.http.fetch("http://localhost:5000/api/Applicant")
  }
  reset(){
    this.applicant = new Applicant();
  }
}
