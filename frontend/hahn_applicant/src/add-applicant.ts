import { Applicant } from './Applicant';
import { HttpClient, json } from "aurelia-fetch-client";
import { inject } from "aurelia-dependency-injection";
import { bindable, observable } from "aurelia-framework";
import {
  ValidationRules,
  ValidationControllerFactory,
  ValidationController,
  validateTrigger,
} from "aurelia-validation";
@inject(HttpClient, ValidationControllerFactory)
export class AddApplicant {
  @bindable @observable applicant: Applicant;
  public controller : ValidationController;

  constructor(private http: HttpClient, private vcf: ValidationControllerFactory) {
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
    this.controller = this.vcf.createForCurrentScope();
    // this.controller.validateTrigger = validateTrigger.changeOrFocusout;
    this.reset();
  }

  created() {
    this.reset();
  }
  add(){
    this.controller.validate({object: this.applicant}).then(result =>{
      if(result.valid){
        console.log("Validation successfull");
      }
      else{
        console.log("Validation failed");
        console.log(result);
      }
      console.log(result);
    })
    console.log(this.applicant);
    // this.http.fetch("http://localhost:5000/api/Applicant")
  }
  reset(){
    this.applicant = new Applicant();
  }

  // nameChanged(newValue, oldValue){
  //   if(this.applicant){
  //     ValidationRules.ensure("Name")
  //     .required()
  //     .minLength(5)
  //     .withMessage("Name has to be atleast 5 characters long.")
  //     .on(this.applicant);
  //     console.log("Changed name " + newValue);
  //   }
  // }
}
