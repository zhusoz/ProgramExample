using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;

namespace MyToDo.Api.Repository
{
    public class MemoryReposity : Repository<Memory>, IRepository<Memory>
    {
        public MemoryReposity(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
