using Pinewood.DMSSample.Business.Models;
using Pinewood.DMSSample.Business.Models.Responses;
using Pinewood.DMSSample.Business.Repositories;

namespace Pinewood.DMSSample.Business.Services.Concretes;

public class PartInvoiceServices : IPartInvoiceServices
{
    private readonly ICustomerRepositoryDb _customerRepositoryDb;
    private readonly IPartInvoiceRepositoryDb _partInvoiceRepositoryDb;
    private readonly IPartAvailabilityClient _partAvailabilityClient;

    public PartInvoiceServices(ICustomerRepositoryDb customerRepositoryDb,
        IPartInvoiceRepositoryDb partInvoiceRepositoryDb, IPartAvailabilityClient partAvailabilityClient)
    {
        _customerRepositoryDb = customerRepositoryDb;
        _partInvoiceRepositoryDb = partInvoiceRepositoryDb;
        _partAvailabilityClient = partAvailabilityClient;
    }

    public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode,
        int quantity, string customerName)
    {
        if (string.IsNullOrEmpty(stockCode)) return new CreatePartInvoiceResult(false);

        if (quantity <= 0) return new CreatePartInvoiceResult(false);

        var customer = _customerRepositoryDb.GetByName(customerName);
        var customerId = customer?.Id ?? 0;
        if (customerId <= 0) return new CreatePartInvoiceResult(false);

        var availability = await _partAvailabilityClient.GetAvailability(stockCode);
        if (availability <= 0) return new CreatePartInvoiceResult(false);

        var partInvoice = new PartInvoice(
            stockCode,
            quantity,
            customerId
        );

        _partInvoiceRepositoryDb.Add(partInvoice);

        return new CreatePartInvoiceResult(true);
    }
}