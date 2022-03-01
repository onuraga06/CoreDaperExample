using CoreDaperExample.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDaperExample.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private IDbConnection db;
        public StudentRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Student Add(Student entity)
        {
            var sql = "INSERT INTO Student(Name,SurName,Number,Adress) VALUES(@Name,@SurName,@Number,@Adress); SELECT CAST(SCOPE_IDENTITY() as int) ";
            var id = db.Query<int>(sql, entity).Single();
            entity.StudentID = id;
            return entity;
        }

        public Student Find(int id)
        {
            var sql = "SELECT * FROM Student WHERE StudentID=@id";
            return db.Query<Student>(sql, new { @id = id }).Single();
        }

        public List<Student> GetList(string name,string surname,string adress )
        {   
            var sql = "";
            if (name == null && surname==null && adress==null)
            {
                sql = "SELECT * FROM Student";
            }
            else if(name!=null)
            {
                sql = "SELECT * FROM Student WHERE Name LIKE CONCAT('%',@name,'%')";
                if (surname != null)
                {
                    sql = "SELECT * FROM Student WHERE SurName LIKE CONCAT('%',@surname,'%')";
                    if (adress != null)
                    {
                        sql = "SELECT * FROM Student WHERE Adress LIKE CONCAT('%',@adress,'%')";
                    }

                }
                

            }
           

           
            return db.Query<Student>(sql, new { @name = name, @surname = surname, @adress = adress}).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Student WHERE StudentID=@id";
            db.Execute(sql, new { id });
        }
        public Student Update(Student entity)
        {
            var sql = "UPDATE Student SET  Name=@Name,SurName=@SurName,Number=@Number,Adress=@Adress WHERE StudentID=@StudentID ";
            db.Execute(sql, entity);
            return entity;
        }
    }
}
