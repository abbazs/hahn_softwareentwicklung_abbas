using FluentValidation.Results;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    /// <summary>
    /// API for Applicant - to GET, POST, PUT, DELETE
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantRepository repository;

        public ApplicantController(IApplicantRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Gets all the list of Applicants available in the system
        /// </summary>
        /// <returns>List of applicants</returns>
        // GET: api/<ApplicantController>
        [HttpGet]
        public ActionResult<IEnumerable<IApplicant>> Get()
        {
            return repository.GetApplicants();
        }

        /// <summary>
        /// Gets the Applicant object for a given id.
        /// </summary>
        /// <param name="id">ID of a applicant</param>
        /// <returns>Applicant object for the given id.</returns>
        // GET api/<ApplicantController>/5
        [HttpGet("{id}")]
        public ActionResult<IApplicant> Get(int id)
        {
            return Ok(repository.GetApplicant(id));
        }

        /// <summary>
        /// Creates a new Applicant object with given inputs. 
        /// Before creating the Applicant data is validated.
        /// </summary>
        /// <param name="value">A Applicant model</param>
        /// <returns>Returns validation result object</returns>
        // POST api/<ApplicantController>
        [SwaggerRequestExample(typeof(Applicant), typeof(ApplicantExample))]
        [HttpPost]
        public ActionResult<ValidationResult> Post([FromBody] Applicant value)
        {
            ValidationResult vr = ApplicantFactory.ValidateApplicant(value);
            if (vr.IsValid)
            {
                IApplicant applicant = repository.AddApplicant(value);
            }
            return vr;
        }

        /// <summary>
        /// Update an existing Applicant object/record
        /// </summary>
        /// <param name="value">Object of the Applicant</param>
        /// <returns>Returns validation result object</returns>
        // PUT api/<ApplicantController>/5
        [HttpPut("{id}")]
        public ActionResult<ValidationResult> Put([FromBody] Applicant value)
        {
            ValidationResult vr = ApplicantFactory.ValidateApplicant(value);
            if (vr.IsValid)
            {
                repository.UpdateApplicant(value);
            }
            return vr;
        }

        /// <summary>
        /// Delets an Applicant object/record
        /// </summary>
        /// <param name="id">Applicant id</param>
        // DELETE api/<ApplicantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.DeleteApplicant(id);
        }
    }
}
