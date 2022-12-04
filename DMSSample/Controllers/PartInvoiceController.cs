using Pinewood.DMSSample.Business.Models.Responses;
using Pinewood.DMSSample.Business.Services;

namespace Pinewood.DMSSample.Business.Controllers;

public class PartInvoiceController
{
    private readonly IPartInvoiceServices _partInvoiceServices;

    public PartInvoiceController(IPartInvoiceServices partInvoiceServices)
    {
        _partInvoiceServices = partInvoiceServices;
    }

    public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode,
        int quantity, string customerName)
    {
        return await _partInvoiceServices.CreatePartInvoiceAsync(stockCode, quantity, customerName);
    }
}