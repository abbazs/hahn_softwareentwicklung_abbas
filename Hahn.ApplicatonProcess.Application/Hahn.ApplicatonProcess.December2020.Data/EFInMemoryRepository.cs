using Hahn.ApplicatonProcess.December2020.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class EFInMemoryRepository : IApplicantRepository
    {
        private DbContextOptionsBuilder builder { get; set; }
        private DbContextOptions options { get; set; }
        public EFInMemoryRepository()
        {
            builder = new DbContextOptionsBuilder<ApplicantDBContext>();
            builder.UseInMemoryDatabase("applicantdb");
            options = builder.Options;
            AddExampleData();
        }
        public IApplicant AddApplicant(IApplicant applicant)
        {
            using(var context = new ApplicantDBContext(options))
            {
                context.Add(applicant);
                context.SaveChanges();
            }
            return applicant;
        }

        public bool DeleteApplicant(IApplicant applicant)
        {
            try
            {
                using (var context = new ApplicantDBContext(options))
                {
                    context.Remove(applicant);
                    context.SaveChanges();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteApplicant(int id)
        {
            IApplicant applicant = null;
            using (var context = new ApplicantDBContext(options))
            {
                applicant = context.Applicants.Where(x => x.ID == id).FirstOrDefault();
            }

            if (applicant != null)
            {
                return DeleteApplicant(applicant);
            }
            else
            {
                return false;
            }
        }

        public IApplicant GetApplicant(int id)
        {
            IApplicant applicant = null;
            using (var context = new ApplicantDBContext(options))
            {
                applicant = context.Applicants.Where(x => x.ID == id).FirstOrDefault();
            }

            return applicant;
        }

        public List<IApplicant> GetApplicants()
        {
            using (var context = new ApplicantDBContext(options))
            {
                return context.Applicants.ToList<IApplicant>();
            }
        }

        public IApplicant UpdateApplicant(IApplicant applicant)
        {
            try
            {
                using (var context = new ApplicantDBContext(options))
                {
                    context.Update(applicant);
                    context.SaveChanges();
                }

                return applicant;
            }
            catch
            {
                throw;
            }
        }

        private void AddExampleData()
        {
            List<IApplicant> examples = new();

            IApplicant ap1 = ApplicantFactory.GetApplicantObject();
            ap1.Name = "Lucas";
            ap1.FamilyName = "Rossi";
            ap1.Address = "54C, Via Panchitachi, Firenze, 50127";
            ap1.Hired = true;
            ap1.Age = 22;
            ap1.CountryOfOrigin = "Netherlands";
            ap1.EMailAddress = "lucas.rossi@example.com";
            examples.Add(ap1);

            IApplicant ap2 = ApplicantFactory.GetApplicantObject();
            ap2.Name = "Valeria";
            ap2.FamilyName = "Bichelli";
            ap2.Address = "52C, Via Panchitachi, Firenze, 50127";
            ap2.Hired = true;
            ap2.Age = 36;
            ap2.CountryOfOrigin = "Italy";
            ap2.EMailAddress = "valeria@example.com";
            examples.Add(ap2);

            IApplicant ap3 = ApplicantFactory.GetApplicantObject();
            ap3.Name = "Franco";
            ap3.FamilyName = "Sarri";
            ap3.Address = "55C, Via Panchitachi, Firenze, 50127";
            ap3.Hired = true;
            ap3.Age = 42;
            ap3.CountryOfOrigin = "Italy";
            ap3.EMailAddress = "franco@example.com";
            examples.Add(ap3);

            IApplicant ap4 = ApplicantFactory.GetApplicantObject();
            ap4.Name = "Danilo";
            ap4.FamilyName = "Ducchi";
            ap4.Address = "56C, Via Panchitachi, Firenze, 50127";
            ap4.Hired = true;
            ap4.Age = 26;
            ap4.CountryOfOrigin = "Italy";
            ap4.EMailAddress = "danilo@example.com";
            examples.Add(ap4);

            IApplicant ap5 = ApplicantFactory.GetApplicantObject();
            ap5.Name = "Andrea";
            ap5.FamilyName = "Tacconelli";
            ap5.Address = "59C, Via Panchitachi, Firenze, 50127";
            ap5.Hired = true;
            ap5.Age = 31;
            ap5.CountryOfOrigin = "Italy";
            ap5.EMailAddress = "remo@example.com";
            examples.Add(ap5);

            using (var cnn = new ApplicantDBContext(options))
            {
                cnn.AddRange(examples);
                cnn.SaveChanges();
            }
        }
    }
}
