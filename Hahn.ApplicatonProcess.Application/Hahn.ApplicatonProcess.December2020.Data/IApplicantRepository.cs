using Hahn.ApplicatonProcess.December2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public interface IApplicantRepository
    {
        IApplicant AddApplicant(IApplicant applicant);
        bool UpdateApplicant(IApplicant applicant);
        bool DeleteApplicant(IApplicant applicant);
        bool DeleteApplicant(int id);
        List<IApplicant> GetApplicants();
        IApplicant GetApplicant(int id);
    }
}
