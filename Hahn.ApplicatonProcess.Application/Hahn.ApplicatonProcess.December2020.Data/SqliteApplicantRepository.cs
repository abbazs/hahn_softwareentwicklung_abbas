using Dapper;
using Hahn.ApplicatonProcess.December2020.Domain;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class SqliteApplicantRepository : IApplicantRepository
    {
        public string ApplicantDBFile
        {
            get { return Environment.CurrentDirectory + "\\ApplicantDb.sqlite"; }
        }

        public SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + ApplicantDBFile);
        }

        public IApplicant AddApplicant(IApplicant applicant)
        {
            if(!File.Exists(ApplicantDBFile))
            {
                CreateDatabase();
            }

            using(var cnn = SimpleDbConnection())
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
            if (!File.Exists(ApplicantDBFile))
            {
                CreateDatabase();
            }

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Query<int>(@"DELETE FROM Applicant WHERE ID = @id");
            }
            return true;
        }

        public IApplicant GetApplicant(int id)
        {
            if (!File.Exists(ApplicantDBFile))
            {
                CreateDatabase();
            }
            IApplicant ret = null;
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                ret = cnn.Query<IApplicant>(@"SELECT * FROM Applicant WHERE ID = @id").FirstOrDefault();
            }
            return ret;
        }

        public List<IApplicant> GetApplicants()
        {
            if (!File.Exists(ApplicantDBFile))
            {
                CreateDatabase();
            }
            IEnumerable<IApplicant> ret = null;
            using (var cnn = SimpleDbConnection())
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
            using (var cnn = SimpleDbConnection())
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
                        Hired BOOLEAN NOT NULL CHECK (Hired IN (0,1))
                    )";
                
                SqliteCommand createTable = new SqliteCommand(create_table, cnn);
                createTable.ExecuteReader();
            }

            AddExampleData();
        }

        private void AddExampleData()
        {
            List<IApplicant> examples = new();

            IApplicant ap1 = ApplicantFactory.GetApplicantObject();
            ap1.Name = "Lucas";
            ap1.FamilyName = "Rossi";
            ap1.Hired = true;
            ap1.Age = 22;
            ap1.CountryOfOrigin = "Netherlands";
            ap1.EMailAddress = "lucas.rossi@example.com";
            examples.Add(ap1);            
            
            IApplicant ap2 = ApplicantFactory.GetApplicantObject();
            ap2.Name = "Valeria";
            ap2.FamilyName = "Bichelli";
            ap2.Hired = true;
            ap2.Age = 36;
            ap2.CountryOfOrigin = "Italy";
            ap2.EMailAddress = "valeria@example.com";
            examples.Add(ap2);

            IApplicant ap3 = ApplicantFactory.GetApplicantObject();
            ap3.Name = "Franco";
            ap3.FamilyName = "Sarri";
            ap3.Hired = true;
            ap3.Age = 42;
            ap3.CountryOfOrigin = "Italy";
            ap3.EMailAddress = "franco@example.com";
            examples.Add(ap3);            
            
            IApplicant ap4 = ApplicantFactory.GetApplicantObject();
            ap4.Name = "Danilo";
            ap4.FamilyName = "Ducchi";
            ap4.Hired = true;
            ap4.Age = 26;
            ap4.CountryOfOrigin = "Italy";
            ap4.EMailAddress = "danilo@example.com";
            examples.Add(ap4);            
            
            IApplicant ap5 = ApplicantFactory.GetApplicantObject();
            ap5.Name = "Remo";
            ap5.FamilyName = "Tacconelli";
            ap5.Hired = true;
            ap5.Age = 31;
            ap5.CountryOfOrigin = "Italy";
            ap5.EMailAddress = "remo@example.com";
            examples.Add(ap5);

            foreach (IApplicant ap in examples)
            {
                using (var cnn = SimpleDbConnection())
                {
                    cnn.Open();
                    ap.ID = cnn.Query<int>(
                        @"INSERT INTO Applicant 
                    (Name, FamilyName, Address, CountryOfOrigin, EMailAddress, Age, Hired)
                    VALUES
                    (@Name, @FamilyName, @Address, @CountryOfOrigin, @EMailAddress, @Age, @Hired)
                    select last_insert_rowid()", ap).First();
                }
            }
        }
    }
}
