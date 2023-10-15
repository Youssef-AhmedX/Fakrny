namespace Fakrny.Application.Interfaces;
public interface IBaseService<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Add(T model);
    void Update(T model);
}
