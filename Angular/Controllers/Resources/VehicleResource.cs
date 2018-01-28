using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Angular.Models;

namespace Angular.Controllers.Resources
{
    public class ContactResource
    {   [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
    }

    public class VehicleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        public ICollection<int> Features { get; set; }

        public VehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}