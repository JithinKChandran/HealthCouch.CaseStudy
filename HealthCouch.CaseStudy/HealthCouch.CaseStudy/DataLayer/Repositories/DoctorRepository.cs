using System;

using System.Collections.Generic;
using System.Data.SQLite;
using HealthCouch.CaseStudy.Database;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.DataLayer.Repositories
{
    public class DoctorRepository
    {
        private readonly DataContext _dataContext;

        public DoctorRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("SELECT * FROM Doctors", connection))
            using (var reader = command.ExecuteReader())
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

        public List<Doctor> SearchDoctors(string searchDoctorId, string searchDoctorName, string searchSpeciality)
        {
            List<Doctor> doctors = new List<Doctor>();
            using (var connection = _dataContext.GetConnection())
            {
                string query = "SELECT * FROM Doctors WHERE ";
                bool isFirstCondition = true;

                if (!string.IsNullOrEmpty(searchDoctorId))
                {
                    query += (isFirstCondition ? "" : " OR ") + " DoctorId LIKE @SearchDoctorId";
                    isFirstCondition = false;
                }

                if (!string.IsNullOrEmpty(searchDoctorName))
                {
                    query += (isFirstCondition ? "" : " OR ") + " DoctorName LIKE @SearchDoctorName";
                    isFirstCondition = false;
                }

                if (!string.IsNullOrEmpty(searchSpeciality))
                {
                    query += (isFirstCondition ? "" : " OR ") + " Speciality LIKE @SearchSpeciality";
                    isFirstCondition = false;
                }

                if (isFirstCondition) // If no search criteria is provided, return all doctors
                {
                    query = "SELECT * FROM Doctors";
                }

                using (var command = new SQLiteCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(searchDoctorId))
                    {
                        command.Parameters.AddWithValue("@SearchDoctorId", "%" + searchDoctorId + "%");
                    }

                    if (!string.IsNullOrEmpty(searchDoctorName))
                    {
                        command.Parameters.AddWithValue("@SearchDoctorName", "%" + searchDoctorName + "%");
                    }

                    if (!string.IsNullOrEmpty(searchSpeciality))
                    {
                        command.Parameters.AddWithValue("@SearchSpeciality", "%" + searchSpeciality + "%");
                    }

                    using (var reader = command.ExecuteReader())
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
            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("SELECT DISTINCT Speciality FROM Doctors", connection))
            using (var reader = command.ExecuteReader())
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
            using (var connection = _dataContext.GetConnection())
            using (var command = new SQLiteCommand("SELECT DoctorName FROM Doctors WHERE Speciality = @Speciality", connection))
            {
                command.Parameters.AddWithValue("@Speciality", speciality);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        doctorNames.Add(reader.GetString(0));
                    }
                }
            }
            return doctorNames;
        }
    }
}