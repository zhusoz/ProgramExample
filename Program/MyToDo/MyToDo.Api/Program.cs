using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api.Context;
using MyToDo.Api.Extension;
using MyToDo.Api.Repository;
using MyToDo.Api.Service;

namespace MyToDo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MyToDoContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("MyToDoConnection"));
            }).AddUnitOfWork<MyToDoContext>();
            builder.Services.AddCustomRepository<ToDo, ToDoRepository>();
            builder.Services.AddCustomRepository<Memory, MemoryReposity>();
            builder.Services.AddCustomRepository<User, UserReposity>();
            builder.Services.AddTransient<IToDoService, ToDoService>();
            builder.Services.AddTransient<IMemoryService, MemoryService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<AutoMapperProfile>();
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCatchException();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.MapSwagger();
            
            app.Run();
        }
    }
}