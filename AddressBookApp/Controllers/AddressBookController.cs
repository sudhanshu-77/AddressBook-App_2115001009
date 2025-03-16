using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressBookController : ControllerBase
    {
        private static List<AddressModel> _contacts = new List<AddressModel>();
        private readonly IMapper _mapper;

        public AddressBookController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // ? Get All Contacts (Returns DTO)
        [HttpGet]
        public ActionResult<ResponseModel<IEnumerable<AddressBookDTO>>> GetAllContacts()
        {
            var dtoContacts = _mapper.Map<IEnumerable<AddressBookDTO>>(_contacts);
            return Ok(new ResponseModel<IEnumerable<AddressBookDTO>>
            {
                Success = true,
                Message = "Contacts retrieved successfully.",
                Data = dtoContacts
            });
        }

        // ? Get Contact by ID (Returns DTO)
        [HttpGet("{id}")]
        public ActionResult<ResponseModel<AddressBookDTO>> GetContactById(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound(new ResponseModel<AddressBookDTO>
                {
                    Success = false,
                    Message = "Contact not found."
                });
            }

            var dtoContact = _mapper.Map<AddressBookDTO>(contact);
            return Ok(new ResponseModel<AddressBookDTO>
            {
                Success = true,
                Message = "Contact retrieved successfully.",
                Data = dtoContact
            });
        }

        // ? Add Contact (Accepts DTO)
        [HttpPost]
        public ActionResult<ResponseModel<AddressBookDTO>> AddContact([FromBody] AddressBookDTO contactDto)
        {
            var contact = _mapper.Map<AddressModel>(contactDto);
            contact.Id = _contacts.Count + 1; // Simulating auto-increment ID
            _contacts.Add(contact);

            var createdDto = _mapper.Map<AddressBookDTO>(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = createdDto.Id },
                new ResponseModel<AddressBookDTO>
                {
                    Success = true,
                    Message = "Contact added successfully.",
                    Data = createdDto
                });
        }

        // ? Update Contact (Accepts DTO)
        [HttpPut("{id}")]
        public ActionResult<ResponseModel<AddressBookDTO>> UpdateContact(int id, [FromBody] AddressBookDTO contactDto)
        {
            if (id != contactDto.Id)
            {
                return BadRequest(new ResponseModel<AddressBookDTO>
                {
                    Success = false,
                    Message = "Invalid request data."
                });
            }

            var existingContact = _contacts.FirstOrDefault(c => c.Id == id);
            if (existingContact == null)
            {
                return NotFound(new ResponseModel<AddressBookDTO>
                {
                    Success = false,
                    Message = "Contact not found."
                });
            }

            // Update fields
            _mapper.Map(contactDto, existingContact);

            var updatedDto = _mapper.Map<AddressBookDTO>(existingContact);
            return Ok(new ResponseModel<AddressBookDTO>
            {
                Success = true,
                Message = "Contact updated successfully.",
                Data = updatedDto
            });
        }

        // ? Delete Contact
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