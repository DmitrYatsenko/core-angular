using System.Collections.Generic;
using System.Collections.ObjectModel;
using Angular.Models;

namespace Angular.Controllers.Resources
{
    public class MakeResource: KeyValuePairResource
    {   
        /*public int Id { get; set; }
        public string Name { get; set; }*/
        public ICollection<KeyValuePairResource> Models { get; set; }
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
        
    }
}