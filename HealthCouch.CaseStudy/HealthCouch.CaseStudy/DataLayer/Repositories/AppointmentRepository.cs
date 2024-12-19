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
    public class AppointmentRepository
    {
        private readonly DataContext _dataContext;

        // Creating patient table.
        public void CreateAppointmentTable()
        {
            string createTableQuery = @"
                CREATE TABLE Appointments (
                Id INT PRIMARY KEY IDENTITY,
                PatinetName VARCHAR(100) NOT NULL,
                TimeSlot VARCHAR(100) NOT NULL,
                DoctorName VARCHAR(100) NOT NULL,
                Age INT NOT NULL,
                Gender VARCHAR(100) NOT NULL
                );";

            var connection = _dataContext.GetConnection();
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        // Add a new appointment to the database
        public void Add(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            using (var connection = _dataContext.GetConnection())
            {
                var command = new SQLiteCommand(
                    "INSERT INTO Appointments (PatientName, TimeSlot, DoctorName, Age, Gender) VALUES (@Id, @PatientName, @TimeSlot, @DoctorName, @Age, @Gender)",
                    connection);

                command.Parameters.AddWithValue("@PatientName", appointment.PatientName);
                command.Parameters.AddWithValue("@TimeSlot", appointment.TimeSlot);
                command.Parameters.AddWithValue("@DoctorName", appointment.DoctorName);
                command.Parameters.AddWithValue("@Age", appointment.Age);
                command.Parameters.AddWithValue("@Gender", appointment.Gender);

                command.ExecuteNonQuery();
            }
        }

        // Get all appointments from the database
        public List<Appointment> GetAll()
        {
            var appointments = new List<Appointment>();

            using (var connection = _dataContext.GetConnection())
            {
                var command = new SQLiteCommand("SELECT * FROM Appointments", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PatientName = reader["PatientName"].ToString(),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Gender = reader["Gender"].ToString(),
                        });
                    }
                }
            }

            return appointments;
        }

        // Update an existing appointment's information in the database
        public void Update(Appointment updatedAppointment)
        {
            if (updatedAppointment == null)
                throw new ArgumentNullException(nameof(updatedAppointment));

            using (var connection = _dataContext.GetConnection())
            {
                var command = new SQLiteCommand(
                    "UPDATE Appointments SET PatientName = @PatientName, TimeSlot = @TimeSlot, DoctorName = @DoctorName, Age = @Age, Gender = @Gender WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", updatedAppointment.Id);
                command.Parameters.AddWithValue("@PatientName", updatedAppointment.PatientName);
                command.Parameters.AddWithValue("@TimeSlot", updatedAppointment.TimeSlot);
                command.Parameters.AddWithValue("@DoctorName", updatedAppointment.DoctorName);
                command.Parameters.AddWithValue("@Age", updatedAppointment.Age);
                command.Parameters.AddWithValue("@Gender", updatedAppointment.Gender);

                if (command.ExecuteNonQuery() == 0)
                    throw new KeyNotFoundException("Appointment not found.");
            }
        }

        // Remove an appointment from the database
        public void Remove(int appointmentId)
        {
            using (var connection = _dataContext.GetConnection())
            {
                var command = new SQLiteCommand("DELETE FROM Appointments WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", appointmentId);

                if (command.ExecuteNonQuery() == 0)
                    throw new KeyNotFoundException("Appointment not found.");
            }
        }

        // Find an appointment by ID
        public Appointment FindById(int appointmentId)
        {
            using (var connection = _dataContext.GetConnection())
            {
                var command = new SQLiteCommand("SELECT * FROM Appointments WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", appointmentId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Appointment
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PatientName = reader["PatientName"].ToString(),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Gender = reader["Gender"].ToString(),
                        };
                    }
                }
            }

            return null;
        }

        // Find appointments by patient name
        public List<Appointment> FindByPatientName(string patientName)
        {
            var appointments = new List<Appointment>();

            using (var connection = _dataContext.GetConnection())
            {
                var command = new SQLiteCommand("SELECT * FROM Appointments WHERE PatientName LIKE @PatientName", connection);
                command.Parameters.AddWithValue("@PatientName", "%" + patientName + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new Appointment
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PatientName = reader["PatientName"].ToString(),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Gender = reader["Gender"].ToString(),
                        });
                    }
                }
            }

            return appointments;
        }
    }
}
