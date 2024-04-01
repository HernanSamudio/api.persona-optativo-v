using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Repository.Data
{
    public class PersonaRepository : IPersona
    {
        private readonly IDbConnection conexionDB;

        public PersonaRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            conexionDB = new DbConection(connectionString).CreateConnection();
        }

        public bool add(PersonaModel persona)
        {
            try
            {
                var sql = "INSERT INTO DatosPersonas(nombre, apellido, cedula) VALUES (@Nombre, @Apellido, @Cedula)";
                var affectedRows = conexionDB.Execute(sql, persona);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool remove(PersonaModel persona)
        {
            try
            {
                var sql = "DELETE FROM DatosPersonas WHERE id = @Id";
                var affectedRows = conexionDB.Execute(sql, new { Id = persona.Id });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(PersonaModel persona)
        {
            try
            {
                var sql = "UPDATE DatosPersonas SET nombre = @Nombre, apellido = @Apellido, cedula = @Cedula WHERE id = @Id";
                var affectedRows = conexionDB.Execute(sql, persona);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonaModel get(int id)
        {
            try
            {
                var sql = "SELECT * FROM DatosPersonas WHERE id = @Id";
                return conexionDB.QueryFirstOrDefault<PersonaModel>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PersonaModel> list()
        {
            try
            {
                var sql = "SELECT * FROM DatosPersonas";
                return conexionDB.Query<PersonaModel>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}