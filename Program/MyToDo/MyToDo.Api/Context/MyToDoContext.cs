using Microsoft.EntityFrameworkCore;

namespace MyToDo.Api.Context
{
    public class MyToDoContext : DbContext
    {
        public MyToDoContext()
        {

        }
        public MyToDoContext(DbContextOptions<MyToDoContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDoSet { get; set; }
        public DbSet<Memory> MemorySet { get; set; }
        public DbSet<User> UserSet { get; set; }

    }
}
