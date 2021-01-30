using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class ApplicantUpdateExample : IExamplesProvider<IApplicant>
    {
        public IApplicant GetExamples()
        {
            IApplicant ap1 = ApplicantFactory.GetApplicantObject();
            ap1.ID = 8;
            ap1.Name = "Massimiliano";
            ap1.FamilyName = "Allegri";
            ap1.Address = "54C, Via Panchitachi, Firenze, 50127";
            ap1.Hired = true;
            ap1.Age = 46;
            ap1.CountryOfOrigin = "Netherlands";
            ap1.EMailAddress = "lucas.rossi@example.com";
            return ap1;
        }
    }
}
