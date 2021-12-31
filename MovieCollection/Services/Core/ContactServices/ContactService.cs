using System;
using System.Linq;
using MovieCollection.Model;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.Core.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _db;

        public ContactService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Contact QueryContactById(Guid contactId)
        {
            if (_db.Contacts.Any(a => a.ContactId == contactId))
            {
                Contact contact = _db.Contacts.Where(a => a.ContactId == contactId).Single();
                return contact;
            }
            return null;
        }

        public Contact QueryContactByUserName(string userName, Guid userId)
        {
            if (_db.Users.Any(a => a.UserName == userName))
            {
                User user = _db.Users.Where(a => a.UserName == userName).Single();
                if(user.UserId == userId && _db.Contacts.Any(a => a.UserId == user.UserId))
                {
                    Contact contact = _db.Contacts.Where(a => a.UserId == user.UserId).Single();
                    contact.UserName = user.UserName;
                    return contact;
                }
            }
            return null;
        }

        public Contact CreateContact(Contact contact)
        {
            _db.Contacts.Add(contact);
            _db.SaveChanges();
            return contact;
        }

        public Contact UpdateContact(Contact contact)
        {
            var _contact = _db.Contacts.Where(a => a.ContactId == contact.ContactId).Single();
            if (_contact != null)
            {
                if (contact.FirstName != null)
                    _contact.FirstName = contact.FirstName;
                if (contact.LastName != null)
                    _contact.LastName = contact.LastName;

                _contact.Gender = contact.Gender;
                _contact.BirthDate = contact.BirthDate;


                _db.Contacts.Update(_contact);
                _db.SaveChanges();

                User user = _db.Users.Where(a => a.UserId == contact.UserId).Single();
                _contact.UserName = user.UserName;

                return _contact;
            }
            else
            {
                return null;
            }
        }
    }
}
