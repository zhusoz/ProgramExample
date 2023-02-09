namespace MyToDo.Api.Context
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }

    }
}
