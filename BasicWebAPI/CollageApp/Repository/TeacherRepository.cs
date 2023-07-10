
using CollageApp.Model;
using System.Xml.Linq;

namespace CollageApp.Repository
{
    public static class TeacherRepository
    {
        public static  List<Teacher> Teachers { get; set; } = new List<Teacher> { new Teacher
        {
             Id = 1,
              Degnation = "assist",
               Department = "English",
                Name = "Kamal passa",
        }
        ,
           new Teacher   {
             Id = 2,
              Degnation = "Prof",
               Department = "English",
                Name = "jamal passa",
        }
           ,
           new Teacher   {
             Id = 3,
              Degnation = "Pertime",
               Department = "English",
                Name = "abc passa",
        }
        };



    }
}
