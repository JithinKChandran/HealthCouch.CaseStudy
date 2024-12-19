using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            CreatePatientTable();
        }

        private void CreatePatientTable()
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Patients (
                    PatientID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Address TEXT,
                    Age INTEGER NOT NULL,
                    Gender TEXT NOT NULL,
                    ContactNumber TEXT NOT NULL,
                    EmergencyContact TEXT,
                    BloodGroup TEXT,
                    Symptoms TEXT,
                    DoctorSpeciality TEXT NOT NULL,
                    DoctorName TEXT NOT NULL,
                    AppointmentDate TEXT NOT NULL,
                    TimeSlot TEXT NOT NULL
                );";

            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Add(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("INSERT INTO Patients (Name, Address, Age, Gender, ContactNumber, EmergencyContact, BloodGroup, Symptoms, DoctorSpeciality, DoctorName, AppointmentDate, TimeSlot) " +
                                                   "VALUES (@Name, @Address, @Age, @Gender, @ContactNumber, @EmergencyContact, @BloodGroup, @Symptoms, @DoctorSpeciality, @DoctorName, @AppointmentDate, @TimeSlot)", connection))
            {
                command.Parameters.AddWithValue("@Name", patient.Name);
                command.Parameters.AddWithValue("@Address", patient.Address);
                command.Parameters.AddWithValue("@Age", patient.Age);
                command.Parameters.AddWithValue("@Gender", patient.Gender);
                command.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);
                command.Parameters.AddWithValue("@EmergencyContact", patient.EmergencyContact);
                command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                command.Parameters.AddWithValue("@Symptoms", patient.Symptoms);
                command.Parameters.AddWithValue("@DoctorSpeciality", patient.DoctorSpeciality);
                command.Parameters.AddWithValue("@DoctorName", patient.DoctorName);
                command.Parameters.AddWithValue("@AppointmentDate", patient.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@TimeSlot", patient.TimeSlot);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("UPDATE Patients SET Name = @Name, Address = @Address, Age = @Age, Gender = @Gender, " +
                                                   "ContactNumber = @ContactNumber, EmergencyContact = @EmergencyContact, BloodGroup = @BloodGroup, Symptoms = @Symptoms, " +
                                                   "DoctorSpeciality = @DoctorSpeciality, DoctorName = @DoctorName, AppointmentDate = @AppointmentDate, TimeSlot = @TimeSlot " +
                                                   "WHERE PatientID = @PatientID", connection))
            {
                command.Parameters.AddWithValue("@Name", patient.Name);
                command.Parameters.AddWithValue("@Address", patient.Address);
                command.Parameters.AddWithValue("@Age", patient.Age);
                command.Parameters.AddWithValue("@Gender", patient.Gender);
                command.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);
                command.Parameters.AddWithValue("@EmergencyContact", patient.EmergencyContact);
                command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                command.Parameters.AddWithValue("@Symptoms", patient.Symptoms);
                command.Parameters.AddWithValue("@DoctorSpeciality", patient.DoctorSpeciality);
                command.Parameters.AddWithValue("@DoctorName", patient.DoctorName);
                command.Parameters.AddWithValue("@AppointmentDate", patient.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@TimeSlot", patient.TimeSlot);
                command.Parameters.AddWithValue("@PatientID", patient.PatientID);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int patientID)
        {
            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("DELETE FROM Patients WHERE PatientID = @PatientID", connection))
            {
                command.Parameters.AddWithValue("@PatientID", patientID);
                command.ExecuteNonQuery();
            }
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("SELECT * FROM Patients", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    patients.Add(new Patient
                    {
                        PatientID = Convert.ToInt32(reader["PatientID"]),
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Gender = reader["Gender"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        EmergencyContact = reader["EmergencyContact"].ToString(),
                        BloodGroup = reader["BloodGroup"].ToString(),
                        Symptoms = reader["Symptoms"].ToString(),
                        DoctorSpeciality = reader["DoctorSpeciality"].ToString(),
                        DoctorName = reader["DoctorName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        TimeSlot = reader["TimeSlot"].ToString()
                    });
                }
            }

            return patients;
        }

        public List<Patient> SearchPatients(string searchCriteria, string searchValue)
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand($"SELECT * FROM Patients WHERE {searchCriteria} LIKE @SearchValue", connection))
            {
                command.Parameters.AddWithValue("@SearchValue", $"%{searchValue}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientID = Convert.ToInt32(reader["PatientID"]),
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Gender = reader["Gender"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString(),
                            EmergencyContact = reader["EmergencyContact"].ToString(),
                            BloodGroup = reader["BloodGroup"].ToString(),
                            Symptoms = reader["Symptoms"].ToString(),
                            DoctorSpeciality = reader["DoctorSpeciality"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            TimeSlot = reader["TimeSlot"].ToString()
                        });
                    }
                }
            }

            return patients;
        }
    }
}
