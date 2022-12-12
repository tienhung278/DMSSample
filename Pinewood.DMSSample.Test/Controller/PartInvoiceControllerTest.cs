using Moq;
using Pinewood.DMSSample.Business.Controllers;
using Pinewood.DMSSample.Business.Models.Responses;
using Pinewood.DMSSample.Business.Repositories;
using Pinewood.DMSSample.Business.Services;

namespace Pinewood.DMSSample.Test.Controller;

public class PartInvoiceControllerTest
{
    private readonly Mock<IPartInvoiceServices> _partInvoiceServices;
    private readonly PartInvoiceController _partInvoiceController;
    
    public PartInvoiceControllerTest()
    {
        _partInvoiceServices = new Mock<IPartInvoiceServices>();
        _partInvoiceController = new PartInvoiceController(_partInvoiceServices.Object);
    }

    [Fact]
    public void CreatePartInvoiceAsync()
    {
        //Arrange
        var stockCode = "1234";
        var quantity = 0;
        var customerName = "John Doe";
        var partInvoice = new CreatePartInvoiceResult(true);
        _partInvoiceServices.Setup(s => s.CreatePartInvoiceAsync(stockCode, quantity, customerName))
            .ReturnsAsync(partInvoice);
        
        //Act
        var result = 
            _partInvoiceController.CreatePartInvoiceAsync(stockCode, quantity, customerName).Result;

        //Assert
        Assert.IsType<CreatePartInvoiceResult>(result);
        Assert.True(result.Success);
    }
}