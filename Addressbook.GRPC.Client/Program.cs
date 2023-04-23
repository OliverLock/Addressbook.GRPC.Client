using System.Threading.Tasks;
using Grpc.Net.Client;
using Addressbook.GRPC.Client;


// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7282");


var newContact = new AddContactRequest {
    FirstName = "John",
    LastName = "Doe",
    PhoneNumber = "0123456789",
    PostCode = "AB12 3CD"
};


var client = new Contacts.ContactsClient(channel);
var reply = await client.AddContactAsync(newContact);
Console.WriteLine($"Added Contact {reply.FirstName} {reply.LastName} with id of {reply.Id}");



var resonseGetContacts = await client.GetContactsAsync(new Empty());

if (resonseGetContacts.Contacts != null)
{
    foreach (var contact in resonseGetContacts.Contacts)
    {
        Console.WriteLine($"Found Contact {contact.FirstName} {contact.LastName} with id: {contact.Id}");
    }
}
else
{
    Console.WriteLine("No Contacts Found");
}



Console.WriteLine("Press any key to exit...");
Console.ReadKey();