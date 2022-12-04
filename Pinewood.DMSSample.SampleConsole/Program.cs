// See https://aka.ms/new-console-template for more information

using Pinewood.DMSSample.Business;

var dmsClient = new DmsClient();

await dmsClient.CreatePartInvoiceAsync("1234", 10, "John Doe");