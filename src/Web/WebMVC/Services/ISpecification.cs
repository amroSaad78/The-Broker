namespace WebMVC.Services
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T file);
    }
}
