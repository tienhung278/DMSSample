using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Repositories;

public interface IPartInvoiceRepositoryDb
{
    void Add(PartInvoice invoice);
}