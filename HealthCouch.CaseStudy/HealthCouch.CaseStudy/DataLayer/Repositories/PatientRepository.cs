using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using HealthCouch.CaseStudy.Database;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.DataLayer.Repositories
{
    public class PatientRepository
    {
        private readonly DataContext _dataContext;

        public PatientRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        // Creating patient table.
        public void CreatePatientTable()
        {
            string createTableQuery = @"
                CREATE TABLE Patients (
                PatientId INT PRIMARY KEY IDENTITY,
                PatinetName VARCHAR(100) NOT NUL,
                Age INT NOT NUL,
                ContactNumber VARCHAR(20) NOT NUL,
                Gender VARCHAR(15) NOT NUL,
                BloodGroup VARCHAR(10) NOT NUL,
                Symptoms VARCHAR(300) NOT NULL
                );";

            var connection = _dataContext.GetConnection();
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        // Add a new patient to the repository.
        public void Add(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Patients (PatientName, Age, ContactNumber, Gender, BloodGroup, Symptoms)
                    VALUES (@PatientName, @Age, @ContactNumber, @Gender, @BloodGroup, @Symptoms)";
                command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                command.Parameters.AddWithValue("@Age", patient.Age);
                command.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);
                command.Parameters.AddWithValue("@Gender", patient.Gender);
                command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                command.Parameters.AddWithValue("@Symptoms", patient.Symptoms);

                command.ExecuteNonQuery();
            }
        }

        // Get all patients from the repository.
        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Patients";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var patient = new Patient
                        {
                            PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                            PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age")),
                            ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            BloodGroup = reader.GetString(reader.GetOrdinal("BloodGroup")),
                            Symptoms = reader.GetString(reader.GetOrdinal("Symptoms"))
                        };
                        patients.Add(patient);
                    }
                }
            }
            return patients;
        }

        // Update an existing patient's information.
        public void Update(Patient updatedPatient)
        {
            if (updatedPatient == null)
                throw new ArgumentNullException(nameof(updatedPatient));

            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Patients
                    SET PatientName = @PatientName,
                        Age = @Age,
                        ContactNumber = @ContactNumber,
                        Gender = @Gender,
                        BloodGroup = @BloodGroup,
                        Symptoms = @Symptoms
                    WHERE PatientId = @PatientId";

                command.Parameters.AddWithValue("@PatientId", updatedPatient.PatientId);
                command.Parameters.AddWithValue("@PatientName", updatedPatient.PatientName);
                command.Parameters.AddWithValue("@Age", updatedPatient.Age);
                command.Parameters.AddWithValue("@ContactNumber", updatedPatient.ContactNumber);
                command.Parameters.AddWithValue("@Gender", updatedPatient.Gender);
                command.Parameters.AddWithValue("@BloodGroup", updatedPatient.BloodGroup);
                command.Parameters.AddWithValue("@Symptoms", updatedPatient.Symptoms);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new KeyNotFoundException("Patient not found.");
                }
            }
        }

        // Remove a patient from the repository.
        public void Remove(int patientId)
        {
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Patients WHERE PatientId = @PatientId";
                command.Parameters.AddWithValue("@PatientId", patientId);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new KeyNotFoundException("Patient not found.");
                }
            }
        }

        // Find a patient by ID.
        public Patient FindById(int patientId)
        {
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Patients WHERE PatientId = @PatientId";
                command.Parameters.AddWithValue("@PatientId", patientId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Patient
                        {
                            PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                            PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age")),
                            ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            BloodGroup = reader.GetString(reader.GetOrdinal("BloodGroup")),
                            Symptoms = reader.GetString(reader.GetOrdinal("Symptoms"))
                        };
                    }
                }
            }
            return null;
        }

        // Find patients by name.
        public List<Patient> FindByName(string patientName)
        {
            var patients = new List<Patient>();
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Patients WHERE PatientName LIKE @PatientName";
                command.Parameters.AddWithValue("@PatientName", "%" + patientName + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var patient = new Patient
                        {
                            PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                            PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age")),
                            ContactNumber = reader.GetString(reader.GetOrdinal("ContactNumber")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            BloodGroup = reader.GetString(reader.GetOrdinal("BloodGroup")),
                            Symptoms = reader.GetString(reader.GetOrdinal("Symptoms"))
                        };
                        patients.Add(patient);
                    }
                }
            }
            return patients;
        }
    }
}
