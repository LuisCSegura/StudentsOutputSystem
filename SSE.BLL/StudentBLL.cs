using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.DAL;
using SSE.BOL;

namespace SSE.BLL
{
    public class StudentBLL
    {
         public StudentDAL dal = new StudentDAL();
        public void Create(Student s)
        {
            try
            {
                Validate(s);
                dal.Create(s);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<Student> Read()
        {
            try
            {
                return dal.Read();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public void Update(Student s)
        {
            try
            {
                Validate(s);
                dal.Update(s);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Delete(long id)
        {
            try
            {
                dal.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<Student> ReadByFilter(string filter)
        {
            try
            {
                return dal.ReadByFilter(filter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        private void Validate(Student s)
        {
            if (String.IsNullOrEmpty(s.Code) ||
                String.IsNullOrEmpty(s.Name) ||
                String.IsNullOrEmpty(s.LastName) ||
                String.IsNullOrEmpty(s.IdNumber) ||
                String.IsNullOrEmpty(s.Section) ||
                String.IsNullOrEmpty(s.Genre.ToString()))
            {
                throw new Exception("Debe completar toda la información que se le solicita");
            }
            if (s.PersonalPhoto == null || s.SectionPhoto == null)
            {
                throw new Exception("Debe seleccionar imagenes validas para el perfil del estudiante");
            }
        }

    }
}
