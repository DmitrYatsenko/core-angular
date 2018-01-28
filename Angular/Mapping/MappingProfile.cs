using System.Collections.Generic;
using System.Linq;
using Angular.Controllers.Resources;
using Angular.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Angular.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, VehicleResource>().
                ForMember(vr=>vr.Make, opt=>opt.MapFrom(v=>v.Model.Make)).ForMember(vr => vr.Contact, opt=>opt.MapFrom(v=> 
                new ContactResource{Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone} )).
                ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(vf=>vf.FeatureId)));
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.Id, op => op.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))

                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))

                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))

                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    var removedFetures = new List<VehicleFeature>();
                    foreach (var f in v.Features)
                    {
                        if (!vr.Features.Contains(f.FeatureId))
                            removedFetures.Add(f);
                      // var removedFeatures = v.Features.Where(fs => !vr.Features.Contains(fs.FeatureId));
                        foreach (var rf in removedFetures)
                            v.Features.Remove(rf);
                        foreach (var id in vr.Features)
                        
                            if(v.Features.Any(fi=>fi.FeatureId == id))
                                v.Features.Add(new VehicleFeature{ FeatureId = id});
                       // var AddedFeatures = v.Features.Select(fs => !vr.Features.Contains(fs.FeatureId));
                       // foreach (var id in vr.AddedFeatures)
                        //v.Features.Add(new VehicleFeature{ FeatureId = id});
                    }
                }); //MapFrom(vr => vr.Features.Select(id =>new VehicleFeature{ FeatureId = id})));
        }
    }
}