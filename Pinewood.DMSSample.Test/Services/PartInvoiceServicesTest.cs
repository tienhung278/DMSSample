using Moq;
using Pinewood.DMSSample.Business.Models;
using Pinewood.DMSSample.Business.Models.Responses;
using Pinewood.DMSSample.Business.Repositories;
using Pinewood.DMSSample.Business.Services;
using Pinewood.DMSSample.Business.Services.Concretes;

namespace Pinewood.DMSSample.Test.Services;

public class PartInvoiceServicesTest
{
    private readonly IPartInvoiceServices _partInvoiceServices;
    private readonly Mock<ICustomerRepositoryDb> _customerRepositoryDb;
    private readonly Mock<IPartAvailabilityClient> _partAvailabilityClient;
        
    public PartInvoiceServicesTest()
    {
        _customerRepositoryDb = new Mock<ICustomerRepositoryDb>();
        _partAvailabilityClient = new Mock<IPartAvailabilityClient>();
        var partInvoiceRepositoryDb = new Mock<IPartInvoiceRepositoryDb>();
        _partInvoiceServices =
            new PartInvoiceServices(_customerRepositoryDb.Object, partInvoiceRepositoryDb.Object,
                _partAvailabilityClient.Object);
    }

    [Fact]
    public void CreatePartInvoiceAsyncWithStockCodeIsEmpty()
    {
        //Arrange
        var stockCode = string.Empty;
        var quantity = 10;
        var customerName = "John Doe";
        
        //Act
        var result = _partInvoiceServices.CreatePartInvoiceAsync(stockCode, quantity, customerName)
            .Result;
        
        //Assert
        Assert.IsType<CreatePartInvoiceResult>(result);
        Assert.False(result.Success);
    }
    
    [Fact]
    public void CreatePartInvoiceAsyncWithQuantityLessThanOrEqualZero()
    {
        //Arrange
        var stockCode = "1234";
        var quantity = 0;
        var customerName = "John Doe";
        
        //Act
        var result = _partInvoiceServices.CreatePartInvoiceAsync(stockCode, quantity, customerName)
            .Result;
        
        //Assert
        Assert.IsType<CreatePartInvoiceResult>(result);
        Assert.False(result.Success);
    }
    
    [Fact]
    public void CreatePartInvoiceAsyncWithCustomerIdLessThanOrEqualZero()
    {
        //Arrange
        var stockCode = "1234";
        var quantity = 10;
        var customerName = "John Doe";
        var customer = new Customer(0, customerName, "address");
        _customerRepositoryDb.Setup(r => r.GetByName(customerName)).Returns(customer);
        
        //Act
        var result = _partInvoiceServices.CreatePartInvoiceAsync(stockCode, quantity, customerName)
            .Result;
        
        //Assert
        Assert.IsType<CreatePartInvoiceResult>(result);
        Assert.False(result.Success);
    }
    
    [Fact]
    public void CreatePartInvoiceAsyncWithAvailabilityLessThanOrEqualZero()
    {
        //Arrange
        var stockCode = "1234";
        var quantity = 10;
        var customerName = "John Doe";
        var customer = new Customer(1, customerName, "address");
        _customerRepositoryDb.Setup(r => r.GetByName(customerName)).Returns(customer);
        _partAvailabilityClient.Setup(c => c.GetAvailability(stockCode)).ReturnsAsync(0);
        
        //Act
        var result = _partInvoiceServices.CreatePartInvoiceAsync(stockCode, quantity, customerName)
            .Result;
        
        //Assert
        Assert.IsType<CreatePartInvoiceResult>(result);
        Assert.False(result.Success);
    }
    
    [Fact]
    public void CreatePartInvoiceAsyncWithValidData()
    {
        //Arrange
        var stockCode = "1234";
        var quantity = 10;
        var customerName = "John Doe";
        var customer = new Customer(1, customerName, "address");
        _customerRepositoryDb.Setup(r => r.GetByName(customerName)).Returns(customer);
        _partAvailabilityClient.Setup(c => c.GetAvailability(stockCode)).ReturnsAsync(1);
        
        //Act
        var result = _partInvoiceServices.CreatePartInvoiceAsync(stockCode, quantity, customerName)
            .Result;
        
        //Assert
        Assert.IsType<CreatePartInvoiceResult>(result);
        Assert.True(result.Success);
    }
}