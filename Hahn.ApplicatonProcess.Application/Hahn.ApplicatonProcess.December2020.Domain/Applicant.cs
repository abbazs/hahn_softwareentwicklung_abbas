using System;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class Applicant : IApplicant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EMailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
    }
}
