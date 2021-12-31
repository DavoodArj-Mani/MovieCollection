using System;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.Core.ContactServices
{
    public interface IContactService
    {
        Contact QueryContactById(Guid contactId);

        Contact QueryContactByUserName(string userName, Guid userId);

        Contact CreateContact(Contact contact);

        Contact UpdateContact(Contact contact);

    }
}
