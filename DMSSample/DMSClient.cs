using Microsoft.Extensions.DependencyInjection;
using Pinewood.DMSSample.Business.Controllers;
using Pinewood.DMSSample.Business.Models.Responses;
using Pinewood.DMSSample.Business.Repositories;
using Pinewood.DMSSample.Business.Repositories.Concretes;
using Pinewood.DMSSample.Business.Services;
using Pinewood.DMSSample.Business.Services.Concretes;

namespace Pinewood.DMSSample.Business;

public class DmsClient
{
    private readonly PartInvoiceController _controller;

    public DmsClient()
    {
        var servicesCollection = new ServiceCollection();
        var builder = RegisterServices(servicesCollection);

        _controller = new PartInvoiceController(builder.GetRequiredService<IPartInvoiceServices>());
    }

    private static ServiceProvider RegisterServices(IServiceCollection servicesCollection)
    {
        servicesCollection.AddScoped<IPartInvoiceServices, PartInvoiceServices>();
        servicesCollection.AddScoped<ICustomerRepositoryDb, CustomerRepositoryDb>();
        servicesCollection.AddScoped<IPartInvoiceRepositoryDb, PartInvoiceRepositoryDb>();
        servicesCollection.AddScoped<IPartAvailabilityClient, PartAvailabilityClient>();

        return servicesCollection.BuildServiceProvider();
    }

    public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode,
        int quantity, string customerName)
    {
        return await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
    }
}