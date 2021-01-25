using Dapper;
using Hahn.ApplicatonProcess.December2020.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class MySqlApplicantRepository : IApplicantRepository
    {
        public string ConnectionString { get; init; }

        public IDbConnection DBConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public MySqlApplicantRepository(string connectionstring)
        {
            ConnectionString = connectionstring;
            CreateDatabase();
        }

        public IApplicant AddApplicant(IApplicant applicant)
        {
            using (var cnn = DBConnection())
            {
                cnn.Open();
                applicant.ID = cnn.Query<int>(
                    @"INSERT INTO Applicant
                    (Name, FamilyName, Address, CountryOfOrigin, EMailAddress, Age, Hired)
                    VALUES
                    (@Name, @FamilyName, @Address, @CountryOfOrigin, @EMailAddress, @Age, @Hired)
                    select last_insert_rowid()", applicant).First();
            }
            return applicant;
        }

        public bool DeleteApplicant(IApplicant applicant)
        {
            return DeleteApplicant(applicant.ID);
        }

        public bool DeleteApplicant(int id)
        {
            using (var cnn = DBConnection())
            {
                cnn.Open();
                cnn.Query<int>(@"DELETE FROM Applicant WHERE ID = @id");
            }
            return true;
        }

        public IApplicant GetApplicant(int id)
        {
            IApplicant ret = null;
            using (var cnn = DBConnection())
            {
                cnn.Open();
                ret = cnn.Query<IApplicant>(@"SELECT * FROM Applicant WHERE ID = @id").FirstOrDefault();
            }
            return ret;
        }

        public List<IApplicant> GetApplicants()
        {
            IEnumerable<IApplicant> ret = null;
            using (var cnn = DBConnection())
            {
                cnn.Open();
                ret = cnn.Query<Applicant>(@"SELECT * FROM Applicant");
            }
            return ret?.ToList();
        }

        public bool UpdateApplicant(IApplicant applicant)
        {
            throw new NotImplementedException();
        }

        private void CreateDatabase()
        {
            using (var cnn = DBConnection())
            {
                cnn.Open();
                string create_table = @"CREATE TABLE IF NOT EXISTS Applicant
                     (
                        ID INTEGER PRIMARY KEY,
                        Name VARCHAR(100) NOT NULL,
                        FamilyName VARCHAR(100) NOT NULL,
                        Address NVARCHAR(1024) NOT NULL,
                        CountryOfOrigin VARCHAR(100) NOT NULL,
                        EMailAddress VARCHAR(200) NOT NULL,
                        Age INTEGER NOT NULL,
                        Hired BOOLEAN NOT NULL
                    )";
                cnn.Execute(create_table);
            }

            AddExampleData();
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
            ap5.Name = "Remo";
            ap5.FamilyName = "Tacconelli";
            ap5.Address = "59C, Via Panchitachi, Firenze, 50127";
            ap5.Hired = true;
            ap5.Age = 31;
            ap5.CountryOfOrigin = "Italy";
            ap5.EMailAddress = "remo@example.com";
            examples.Add(ap5);

            for (int i = 1; i <= examples.Count; i++)
            {
                IApplicant ap = examples[i - 1];
                ap.ID = i;
                using (var cnn = DBConnection())
                {
                    cnn.Open();
                    string insert = @"INSERT INTO Applicant
                    (ID, Name, FamilyName, Address, CountryOfOrigin, EMailAddress, Age, Hired)
                    VALUES
                    (@ID, @Name, @FamilyName, @Address, @CountryOfOrigin, @EMailAddress, @Age, @Hired)
                    ON DUPLICATE KEY UPDATE ID=ID
                    ";
                    cnn.Execute(insert, ap);
                }
            }
        }
    }
}