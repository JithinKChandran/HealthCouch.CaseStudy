using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Creating doctor table.
        public void CreateDoctorTable()
        {
            string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Doctors' AND xtype='U')
                BEGIN
                CREATE TABLE Doctors (
                DoctorId INT PRIMARY KEY IDENTITY,
                DoctorName VARCHAR(100) NOT NUL,
                Speciality VARCHAR(100) NOT NULL
                );
                END";

            var connection = _dataContext.GetConnection();
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        // Add a new doctor to the repository.
        public void Add(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Doctors (DoctorName, Speciality)
                    VALUES (@DoctorName, @Speciality)";

                // Adding parameters to SQL command
                command.Parameters.AddWithValue("@DoctorName", doctor.DoctorName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Speciality", doctor.Speciality ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        // Get all doctors from the repository.
        public List<Doctor> GetDoctors()
        {
            var doctors = new List<Doctor>();
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Doctors";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var doctor = new Doctor
                        {
                            DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                            DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                            Speciality = reader.GetString(reader.GetOrdinal("Speciality"))
                        };
                        doctors.Add(doctor);
                    }
                }
            }
            return doctors;
        }

        // Update an existing doctor's information.
        public void Update(Doctor updatedDoctor)
        {
            if (updatedDoctor == null)
                throw new ArgumentNullException(nameof(updatedDoctor));

            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Doctors
                    SET DoctorName = @DoctorName,
                        Speciality = @Speciality
                    WHERE DoctorId = @DoctorId";

                // Adding parameters to SQL command.
                command.Parameters.AddWithValue("@DoctorId", updatedDoctor.DoctorId);
                command.Parameters.AddWithValue("@DoctorName", updatedDoctor.DoctorName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Speciality", updatedDoctor.Speciality ?? (object)DBNull.Value);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new KeyNotFoundException("Doctor not found.");
                }
            }
        }

        // Remove a doctor from the repository.
        public void Remove(int doctorId)
        {
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";
                command.Parameters.AddWithValue("@DoctorId", doctorId);

                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new KeyNotFoundException("Doctor not found.");
                }
            }
        }

        // Find a doctor by ID.
        public Doctor FindById(int doctorId)
        {
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Doctors WHERE DoctorId = @DoctorId";
                command.Parameters.AddWithValue("@DoctorId", doctorId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Doctor
                        {
                            DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                            DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                            Speciality = reader.GetString(reader.GetOrdinal("Speciality"))
                        };
                    }
                }
            }
            return null;
        }

        // Find doctors by speciality.
        public List<Doctor> FindBySpeciality(string speciality)
        {
            var doctors = new List<Doctor>();
            using (var connection = _dataContext.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Doctors WHERE Speciality LIKE @Speciality";
                command.Parameters.AddWithValue("@Speciality", "%" + speciality + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var doctor = new Doctor
                        {
                            DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                            DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                            Speciality = reader.GetString(reader.GetOrdinal("Speciality"))
                        };
                        doctors.Add(doctor);
                    }
                }
            }
            return doctors;
        }
    }
}
