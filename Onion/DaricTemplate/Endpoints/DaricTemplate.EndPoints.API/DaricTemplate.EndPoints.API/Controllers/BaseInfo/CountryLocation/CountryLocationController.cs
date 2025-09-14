using Daric.EndPoints.Web.Controllers; 

using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.CreateCountryLocation;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.DeleteCountryLocation;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Commands.UpdateCountryLocation;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations.Queries.DTOs;
using DaricTemplate.Core.Contracts.Common.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DaricTemplate.EndPoints.API.Controllers.BaseInfo.CountryLocation;

[Route("api/[controller]")]
[ApiController]

public class CountryLocationController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCountryLocation(CreateCountryLocationCommand createCountryLocation)
        => await Create<CreateCountryLocationCommand, long>(createCountryLocation);

    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteCountryLocation(DeleteCountryLocationCommand deleteCountryLocation)
        => await Delete<DeleteCountryLocationCommand, long>(deleteCountryLocation);

    [HttpPut("[action]")]
    public async Task<IActionResult> EditCountryLocation(UpdateCountryLocationCommand updateCountryLocation)
        => await Edit<UpdateCountryLocationCommand, UpdateCountryLocationCommand>(updateCountryLocation);

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCountryLocationById([FromQuery] GetCountryLocationByIdQuery query)
        => await Query<GetCountryLocationByIdQuery, CountryLocationDto?>(query);

    

}
