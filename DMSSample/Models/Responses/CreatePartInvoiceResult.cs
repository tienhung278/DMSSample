namespace Pinewood.DMSSample.Business.Models.Responses;

public class CreatePartInvoiceResult
{
    public CreatePartInvoiceResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; }
}