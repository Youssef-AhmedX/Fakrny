namespace Fakrny.Infrastructure.Implementations;
public class BaseService<T> : IBaseService<T> where T : class
{
    protected readonly IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<T> GetAll()
    {
        return _unitOfWork.Repository<T>().GetAll(withNoTracking: true);
    }

    public T? GetById(int id)
    {
        return _unitOfWork.Repository<T>().GetById(id);
    }

    public void Add(T model)
    {
        _unitOfWork.Repository<T>().Add(model);
        _unitOfWork.Complete();
    }

    public void Update(T model)
    {
        _unitOfWork.Repository<T>().Update(model);
        _unitOfWork.Complete();
    }
}
