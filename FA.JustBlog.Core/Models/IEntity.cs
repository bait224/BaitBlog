namespace FA.JustBlog.Core.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
