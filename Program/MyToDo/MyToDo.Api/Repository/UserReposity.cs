using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;

namespace MyToDo.Api.Repository
{
    public class UserReposity : Repository<User>, IRepository<User>
    {
        public UserReposity(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
