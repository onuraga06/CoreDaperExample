using CoreDaperExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDaperExample.Repository
{
  public  interface IStudentRepository
    {
        Student Find(int id);
        List<Student> GetList(string name,string surname,string adress );
        Student Add(Student entity);
        Student Update(Student entity);
        void Remove(int id);
        
    }
}
