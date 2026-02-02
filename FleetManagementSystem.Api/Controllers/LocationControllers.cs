using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FleetManagementSystem.Api.Models;
using FleetManagementSystem.Api.Services;

namespace FleetManagementSystem.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class LocationController : ControllerBase
{
    private readonly IStateService _stateService;
    private readonly ICityService _cityService;
    private readonly IHubService _hubService;
    private readonly IAirportService _airportService;

    public LocationController(IStateService stateService, ICityService cityService, IHubService hubService, IAirportService airportService)
    {
        _stateService = stateService;
        _cityService = cityService;
        _hubService = hubService;
        _airportService = airportService;
    }

    // State Endpoints
    [HttpGet("states")]
    public ActionResult<List<StateMaster>> GetAllStates()
    {
        return Ok(_stateService.GetAllStates());
    }

    // City Endpoints
    [HttpGet("cities")]
    public ActionResult<List<CityMaster>> GetAllCities()
    {
        return Ok(_cityService.GetAllCities());
    }

    [HttpGet("cities/{stateId}")]
    public ActionResult<List<CityMaster>> GetCitiesByStateId(int stateId)
    {
        return Ok(_cityService.GetCitiesByStateId(stateId));
    }

    // Hub Endpoints
    [HttpGet("hubs")]
    public ActionResult<List<HubMaster>> GetAllHubs()
    {
        return Ok(_hubService.GetAllHubs());
    }

    [HttpGet("hubs/{cityId}")] // Verify route
    public ActionResult<List<HubMaster>> GetHubsByCityId(int cityId)
    {
        return Ok(_hubService.GetHubsByCityId(cityId));
    }
    
    // Check if separate HubController is needed or combined logic is fine.
    // Java usually has separate controllers.
    // I am combining here for brevity but will split if needed.
    // Actually, let's stick to separate controllers to match "Port Controllers" strictly.
    // I will refactor below to separate classes.
}

[ApiController]
[Route("api/v1")]
public class StateController : ControllerBase
{
    private readonly IStateService _stateService;
    public StateController(IStateService stateService) => _stateService = stateService;

    [HttpGet("states")]
    public ActionResult<List<StateMaster>> GetAllStates() => Ok(_stateService.GetAllStates());
}

[ApiController]
[Route("api/v1")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;
    public CityController(ICityService cityService) => _cityService = cityService;

    [HttpGet("cities")]
    public ActionResult<List<CityMaster>> GetAllCities() => Ok(_cityService.GetAllCities());

    [HttpGet("cities/state/{stateId}")]
    public ActionResult<List<CityMaster>> GetCitiesByStateId(int stateId) => Ok(_cityService.GetCitiesByStateId(stateId));
}

[ApiController]
[Route("api/v1")]
public class HubController : ControllerBase
{
    private readonly IHubService _hubService;
    public HubController(IHubService hubService) => _hubService = hubService;

    [HttpGet("hubs")]
    public ActionResult<List<HubMaster>> GetAllHubs() => Ok(_hubService.GetAllHubs());
    
    [HttpGet("hubs/city/{cityId}")]
    public ActionResult<List<HubMaster>> GetHubsByCityId(int cityId) => Ok(_hubService.GetHubsByCityId(cityId));
}

[ApiController]
[Route("api/v1")]
public class AirportController : ControllerBase
{
    private readonly IAirportService _airportService;
    public AirportController(IAirportService airportService) => _airportService = airportService;

    [HttpGet("airports")]
    public ActionResult<List<AirportMaster>> GetAllAirports() => Ok(_airportService.GetAllAirports());

    [HttpGet("airports/{stateId}")]
    public ActionResult<List<AirportMaster>> GetAirportsByStateId(int stateId) => Ok(_airportService.GetAirportsByStateId(stateId));
}
