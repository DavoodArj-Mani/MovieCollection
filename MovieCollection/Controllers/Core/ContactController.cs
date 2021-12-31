using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MovieCollection.Model.Core;
using MovieCollection.Services.App.AuthenticationServices;
using MovieCollection.Services.Core.CollectionServices;
using MovieCollection.Services.Core.ContactServices;

namespace MovieCollection.Controllers.Core
{
    [Authorize]
    [Authorize(Roles = "User")]
    [Route("api/{controller}")]
    [ApiController]

    public class ContactController : Controller
    {
        public IContactService _contactService;
        public IAuthenticationService _authenticationService;

        public ContactController(IContactService contactService, IAuthenticationService authenticationService)
        {
            _contactService = contactService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("QueryContactByUserName/{userName}")]
        public IActionResult QueryContactByUserName([FromRoute] string userName)
        {
            var userId = _authenticationService.UserInfo(Request).UserId;
            var result = _contactService.QueryContactByUserName(userName, userId);
            if (result == null)
                return NotFound("There is no data with this information");
            else
                return Ok(result);
        }

        [HttpPost]
        [Route("CreateContact")]
        public IActionResult CreateContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var userId = _authenticationService.UserInfo(Request).UserId;
            

            var contactExist = _contactService.QueryContactByUserName(contact.UserName, userId);
            if (contactExist != null)
            {
                return BadRequest("Contact exist");
            }
            else
            {
                contact.UserId = userId;
                var result = _contactService.CreateContact(contact);
                return Ok(result);
            }
        }

        [HttpPut]
        [Route("UpdateContact/{contactId}")]
        public IActionResult UpdateContact(Guid contactId, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var userId = _authenticationService.UserInfo(Request).UserId;
            if(contact.UserId == userId)
            {
                var contactExist = _contactService.QueryContactById(contactId);
                if (contactExist == null)
                {
                    return NotFound();
                }
                else
                {
                    var result = _contactService.UpdateContact(contact);
                    return Ok(result);
                }
            }
            else
            {
                return Unauthorized();
            }
           

        }
    }
}
