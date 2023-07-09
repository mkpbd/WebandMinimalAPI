using CollageApp.Model;

namespace CollageApp.Repository
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>
             {

                 new Student { Id = 1, Address="abc", Email ="mostoakamal@gmail.com", StudentName="kamal"},
                 new Student { Id = 2, Address="jamal", Email ="jamal@gmail.com", StudentName="jamal"},
             };
    }
}
