using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.BOL;
using Npgsql;
using System.Drawing;
using System.IO;
namespace SSE.DAL
{
    public class StudentDAL
    {
        public void Create(Student s)
        {
            Connection connection = new Connection();

            String sqlCode = "INSERT INTO students(code,name,last_name,id_number,genre,section,personal_photo,section_photo,p_lunch,p_free_lessons,p_free_afternoon) " +
                "VALUES (@code,@name,@last_name,@id_number,@genre,@section,@personal_photo,@section_photo,@p_lunch,@p_free_lessons,@p_free_afternoon);";


            try
            {
                connection.Connect();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, connection.con);
                command.Parameters.Add(new NpgsqlParameter("code", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("last_name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("id_number", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("genre", NpgsqlTypes.NpgsqlDbType.Char));
                command.Parameters.Add(new NpgsqlParameter("section", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("personal_photo", NpgsqlTypes.NpgsqlDbType.Bytea));
                command.Parameters.Add(new NpgsqlParameter("section_photo", NpgsqlTypes.NpgsqlDbType.Bytea));
                command.Parameters.Add(new NpgsqlParameter("p_lunch", NpgsqlTypes.NpgsqlDbType.Boolean));
                command.Parameters.Add(new NpgsqlParameter("p_free_lessons", NpgsqlTypes.NpgsqlDbType.Boolean));
                command.Parameters.Add(new NpgsqlParameter("p_free_afternoon", NpgsqlTypes.NpgsqlDbType.Boolean));
                command.Parameters[0].Value = s.Code;
                command.Parameters[1].Value = s.Name;
                command.Parameters[2].Value = s.LastName;
                command.Parameters[3].Value = s.IdNumber;
                command.Parameters[4].Value = s.Genre;
                command.Parameters[5].Value = s.Section;
                command.Parameters[6].Value = ImageToByteArray(s.PersonalPhoto);
                command.Parameters[7].Value = ImageToByteArray(s.SectionPhoto);
                command.Parameters[8].Value = s.PLunch;
                command.Parameters[9].Value = s.PFreeLessons;
                command.Parameters[10].Value = s.PFreeAfternoon;
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Disconnect();
            }
        }
        public List<Student> Read()
        {
            List<Student> students = new List<Student>();
            Connection connection = new Connection();

            String sqlCode = "SELECT id,code,name,last_name,id_number,genre,section,personal_photo,section_photo,p_lunch, p_free_lessons, p_free_afternoon  FROM students ORDER BY id";
            try
            {
                connection.Connect();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, connection.con);
                NpgsqlDataReader rs = command.ExecuteReader();
                while (rs.Read())
                {
                    students.Add(LoadNewStudent(rs));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Disconnect();
            }
            return students;

        }
        public void Update(Student s)
        {
            Connection connection = new Connection();

            String sqlCode = "UPDATE students SET code=@code,name=@name,last_name=@last_name,id_number=@id_number,genre=@genre," +
                "section=@section,personal_photo=@personal_photo,section_photo=@section_photo,p_lunch=@p_lunch," +
                "p_free_lessons=@p_free_lessons,p_free_afternoon=@p_free_afternoon" +
                " WHERE id = @id; ";

            try
            {
                connection.Connect();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, connection.con);
                command.Parameters.Add(new NpgsqlParameter("code", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("last_name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("id_number", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("genre", NpgsqlTypes.NpgsqlDbType.Char));
                command.Parameters.Add(new NpgsqlParameter("section", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("personal_photo", NpgsqlTypes.NpgsqlDbType.Bytea));
                command.Parameters.Add(new NpgsqlParameter("section_photo", NpgsqlTypes.NpgsqlDbType.Bytea));
                command.Parameters.Add(new NpgsqlParameter("p_lunch", NpgsqlTypes.NpgsqlDbType.Boolean));
                command.Parameters.Add(new NpgsqlParameter("p_free_lessons", NpgsqlTypes.NpgsqlDbType.Boolean));
                command.Parameters.Add(new NpgsqlParameter("p_free_afternoon", NpgsqlTypes.NpgsqlDbType.Boolean));
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = s.Code;
                command.Parameters[1].Value = s.Name;
                command.Parameters[2].Value = s.LastName;
                command.Parameters[3].Value = s.IdNumber;
                command.Parameters[4].Value = s.Genre;
                command.Parameters[5].Value = s.Section;
                command.Parameters[6].Value = ImageToByteArray(s.PersonalPhoto);
                command.Parameters[7].Value = ImageToByteArray(s.SectionPhoto);
                command.Parameters[8].Value = s.PLunch;
                command.Parameters[9].Value = s.PFreeLessons;
                command.Parameters[10].Value = s.PFreeAfternoon;
                command.Parameters[11].Value = s.Id;
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Disconnect();
            }

        }
        public void Delete(long id)
        {

            Connection connection = new Connection();

            String sqlCode = "DELETE FROM students" +
                " WHERE id = @id; ";
            sqlCode = string.Format(sqlCode, id);

            try
            {
                connection.Connect();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, connection.con);
                command.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException sqle)
            {
                throw new Exception("Failed to delete the record");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Disconnect();
            }
        }
        

        public List<Student> ReadByFilter(string filter)
        {
            filter = "%" + filter + "%";
            List<Student> students = new List<Student>();
            Connection connection = new Connection();
            String sqlCode = "SELECT id,code,name,last_name,id_number,genre,section,personal_photo,section_photo,p_lunch, p_free_lessons, p_free_afternoon FROM students" +
                " WHERE code ILIKE @fil OR name ILIKE @fil OR last_name ILIKE @fil OR id_number ILIKE @fil OR section ILIKE @fil " +
                " ORDER BY id; ";
            try
            {
                connection.Connect();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, connection.con);
                command.Parameters.Add(new NpgsqlParameter("fil", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters[0].Value = filter;
                NpgsqlDataReader rs = command.ExecuteReader();
                while (rs.Read())
                {
                    students.Add(LoadNewStudent(rs));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Disconnect();
            }
            return students;

        }

        private Student LoadNewStudent(NpgsqlDataReader rs)
        {
                Student s = new Student();
                s.Id = Convert.ToInt32(rs[0]);
                s.Code = (string)rs[1];
                s.Name = (string)rs[2];
                s.LastName = (string)rs[3];
                s.IdNumber = (string)rs[4];
                s.Genre = Convert.ToChar(rs[5]);
                s.Section = (string)rs[6];
                s.PersonalPhoto = ByteArrayToImage((byte[])rs[7]);
                s.SectionPhoto = ByteArrayToImage((byte[])rs[8]);
                s.PLunch = (bool)rs[9];
                s.PFreeLessons = (bool)rs[10];
                s.PFreeAfternoon = (bool)rs[11];
                return s;

        }
        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}
