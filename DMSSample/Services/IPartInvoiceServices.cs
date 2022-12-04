using Pinewood.DMSSample.Business.Models.Responses;

namespace Pinewood.DMSSample.Business.Services;

public interface IPartInvoiceServices
{
    Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity,
        string customerName);
}