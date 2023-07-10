using CollageApp.Model;

namespace CollageApp.Repository
{
    public  class TodoItemRepository
    {
        public static async  Task<List<TodoItem>> TodoItem()
        {
            return new List<TodoItem>()
        {
            new TodoItem()
            {
                 Id = 1,
                  IsComplete = true,
                   Name = "Test",
            } ,
            new TodoItem()
            {
                 Id = 2,
                  IsComplete = false,
                   Name = "Test",
            }
            ,new TodoItem()
            {
                 Id = 3,
                  IsComplete = true,
                   Name = "Test",
            }
        };
        }
    }
}
