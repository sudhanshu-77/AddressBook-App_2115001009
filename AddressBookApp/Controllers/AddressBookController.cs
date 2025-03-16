using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressBookController : ControllerBase
    {
        private static List<AddressModel> _contacts = new List<AddressModel>();

        // GET: api/addressbook
        [HttpGet]
        public ActionResult<ResponseModel<IEnumerable<AddressModel>>> GetAllContacts()
        {
            return Ok(new ResponseModel<IEnumerable<AddressModel>>
            {
                Success = true,
                Message = "Contacts retrieved successfully.",
                Data = _contacts
            });
        }

        // GET: api/addressbook/{id}
        [HttpGet("{id}")]
        public ActionResult<ResponseModel<AddressModel>> GetContactById(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound(new ResponseModel<AddressModel>
                {
                    Success = false,
                    Message = "Contact not found."
                });
            }
            return Ok(new ResponseModel<AddressModel>
            {
                Success = true,
                Message = "Contact retrieved successfully.",
                Data = contact
            });
        }

        // POST: api/addressbook
        [HttpPost]
        public ActionResult<ResponseModel<AddressModel>> AddContact([FromBody] AddressModel contact)
        {
            if (contact == null)
            {
                return BadRequest(new ResponseModel<AddressModel>
                {
                    Success = false,
                    Message = "Invalid contact data."
                });
            }

            contact.Id = _contacts.Count + 1; // Simulating auto-increment ID
            _contacts.Add(contact);

            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id },
                new ResponseModel<AddressModel>
                {
                    Success = true,
                    Message = "Contact added successfully.",
                    Data = contact
                });
        }

        // PUT: api/addressbook/{id}
        [HttpPut("{id}")]
        public ActionResult<ResponseModel<AddressModel>> UpdateContact(int id, [FromBody] AddressModel updatedContact)
        {
            if (updatedContact == null || id != updatedContact.Id)
            {
                return BadRequest(new ResponseModel<AddressModel>
                {
                    Success = false,
                    Message = "Invalid request data."
                });
            }

            var existingContact = _contacts.FirstOrDefault(c => c.Id == id);
            if (existingContact == null)
            {
                return NotFound(new ResponseModel<AddressModel>
                {
                    Success = false,
                    Message = "Contact not found."
                });
            }

            existingContact.Name = updatedContact.Name;
            existingContact.Email = updatedContact.Email;
            existingContact.PhoneNumber = updatedContact.PhoneNumber;
            existingContact.Address = updatedContact.Address;

            return Ok(new ResponseModel<AddressModel>
            {
                Success = true,
                Message = "Contact updated successfully.",
                Data = existingContact
            });
        }

        // DELETE: api/addressbook/{id}
        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<string>> DeleteContact(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Contact not found."
                });
            }

            _contacts.Remove(contact);
            return Ok(new ResponseModel<string>
            {
                Success = true,
                Message = "Contact deleted successfully.",
                Data = $"Contact with ID {id} deleted."
            });
        }
    }
}