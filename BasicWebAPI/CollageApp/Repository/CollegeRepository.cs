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
        public static List<UserModel> Users { get; set; } = new List<UserModel>
            {
                new UserModel
                {
                    UserId = 1,
                    UserName = "Ram",
                    Company = "Mindfire Solutions"
                },
                new UserModel
                {
                    UserId = 1,
                    UserName = "chand",
                    Company = "Mindfire Solutions"
                },
                new UserModel
                {
                    UserId = 1,
                    UserName = "Abc",
                    Company = "Abc Solutions"
                }
        };
    }
}
