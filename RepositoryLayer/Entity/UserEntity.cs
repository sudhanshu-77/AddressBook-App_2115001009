﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string? FullName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        public virtual ICollection<AddressBookEntity> AddressBookEntries { get; set; } = new List<AddressBookEntity>();

    }
}
