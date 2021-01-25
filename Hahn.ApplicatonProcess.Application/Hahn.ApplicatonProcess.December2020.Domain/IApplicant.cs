namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public interface IApplicant
    {
        /// <summary>
        /// Address of the applicant at least 10 characters
        /// </summary>
        string Address { get; set; }
        /// <summary>
        /// Age of applicant, between 20 to 60
        /// </summary>
        int Age { get; set; }
        /// <summary>
        /// A valid country
        /// </summary>
        string CountryOfOrigin { get; set; }
        /// <summary>
        /// A valid email address
        /// </summary>
        string EMailAddress { get; set; }
        /// <summary>
        /// Family name of the applicant at least 5 characters
        /// </summary>
        string FamilyName { get; set; }
        /// <summary>
        /// True or false - Is the applicant hired?
        /// </summary>
        bool Hired { get; set; }
        /// <summary>
        /// ID of the applicant also the primary key
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// Name of the applicant
        /// </summary>
        string Name { get; set; }
    }
}