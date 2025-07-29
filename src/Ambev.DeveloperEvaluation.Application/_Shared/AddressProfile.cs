using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application._Shared;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressCommand, Address>()
            .ForMember(f => f.GeoLocation, o => o.MapFrom(m => m.Geo != null ? $"{m.Geo.Lat},{m.Geo.Long}" : string.Empty));

        CreateMap<Address, AddressResult>()
            .AfterMap((src, dest) =>
            {
                if(!string.IsNullOrEmpty(src.GeoLocation))
                {
                    var split = src.GeoLocation.Split(",");

                    double.TryParse(split[0], out var _lat);
                    double.TryParse(split[1], out var _long);

                    dest.Geo = new AddressResult.GeoLocationResult
                    {
                        Lat = _lat,
                        Long = _long
                    };
                }
            });
    }
}
