using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Repositories;

public interface ICustomerRepositoryDb
{
    Customer? GetByName(string name);
}