using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public static class ApplicantFactory
    {
        public static IApplicant GetApplicantObject()
        {
            return new Applicant();
        }

        public static ValidationResult ValidateApplicant(IApplicant applicant)
        {
            ApplicantValidator av = new ApplicantValidator();
            return av.Validate(applicant);
        }
    }
}
