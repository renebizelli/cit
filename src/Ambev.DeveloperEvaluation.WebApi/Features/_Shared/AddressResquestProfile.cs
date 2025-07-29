using Ambev.DeveloperEvaluation.Application._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features._Shared;

public class AddressResquestProfile : Profile
{
    public AddressResquestProfile()
    {
        CreateMap<AddressRequest, AddressCommand>();
        CreateMap<AddressRequest.GeoLocation, AddressCommand.GeoLocation>();

        CreateMap<AddressCommand, AddressResult>();
        CreateMap<AddressCommand.GeoLocation, AddressResult.GeoLocationResult>();

        CreateMap<AddressResult, AddressResponse>();
        CreateMap<AddressResult.GeoLocationResult, AddressResponse.GeoLocation>();
    }
}
