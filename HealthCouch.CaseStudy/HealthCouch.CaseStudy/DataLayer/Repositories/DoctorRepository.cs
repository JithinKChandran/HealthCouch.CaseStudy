using System;
using System.Collections.Generic;
using System.Data.SQLite;
using HealthCouch.CaseStudy.Database;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.DataLayer.Repositories
{
    public class DoctorRepository
    {
        private readonly DataContext _dataContext = new DataContext();

        public DoctorRepository()
        {
            CreateDoctorsTable();
        }

        private void CreateDoctorsTable()
        {
            string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Doctors (
                        DoctorId INTEGER PRIMARY KEY AUTOINCREMENT, 
                        DoctorName TEXT NOT NULL,
                        Speciality TEXT NOT NULL
                    );";
            var connection = _dataContext.GetConnection();
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            var connection = _dataContext.GetConnection();
            var command = new SQLiteCommand("SELECT * FROM Doctors", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            {
                while (reader.Read())
                {
                    doctors.Add(new Doctor
                    {
                        DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                        DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                        Speciality = reader.GetString(reader.GetOrdinal("Speciality"))
                    });
                }
            }
            return doctors;
        }

        public List<Doctor> SearchDoctors(string searchDoctorName, string searchSpeciality)
        {
            List<Doctor> doctors = new List<Doctor>();
            var connection = _dataContext.GetConnection();
            {
                string query = "SELECT * FROM Doctors WHERE 1=1";
                if (!string.IsNullOrEmpty(searchDoctorName))
                {
                    query += " AND DoctorName LIKE @SearchDoctorName";
                }
                if (!string.IsNullOrEmpty(searchSpeciality))
                {
                    query += " AND Speciality LIKE @SearchSpeciality";
                }

                var command = new SQLiteCommand(query, connection);
                {
                    if (!string.IsNullOrEmpty(searchDoctorName))
                    {
                        command.Parameters.AddWithValue("@SearchDoctorName", "%" + searchDoctorName + "%");
                    }
                    if (!string.IsNullOrEmpty(searchSpeciality))
                    {
                        command.Parameters.AddWithValue("@SearchSpeciality", "%" + searchSpeciality + "%");
                    }

                    SQLiteDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor
                            {
                                DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                                Speciality = reader.GetString(reader.GetOrdinal("Speciality"))
                            });
                        }
                    }
                }
            }
            return doctors;
        }

        public List<string> GetAllDoctorSpecialities()
        {
            List<string> specialities = new List<string>();
            var connection = _dataContext.GetConnection();
            var command = new SQLiteCommand("SELECT DISTINCT Speciality FROM Doctors", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            {
                while (reader.Read())
                {
                    specialities.Add(reader.GetString(0));
                }
            }
            return specialities;
        }

        public List<string> GetDoctorNamesBySpeciality(string speciality)
        {
            List<string> doctorNames = new List<string>();
            var connection = _dataContext.GetConnection();
            var command = new SQLiteCommand("SELECT DoctorName FROM Doctors WHERE Speciality = @Speciality", connection);
                command.Parameters.AddWithValue("@Speciality", speciality);

            SQLiteDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        doctorNames.Add(reader.GetString(0));
                    }
                }
            return doctorNames;
        }

        public void AddDoctor(Doctor doctor)
        {
                string query = "INSERT INTO Doctors (DoctorName, Speciality) VALUES (@DoctorName, @Speciality)";
                var connection = _dataContext.GetConnection();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
                    command.Parameters.AddWithValue("@Speciality", doctor.Speciality);
                    command.ExecuteNonQuery();
            
        }
    }
}
