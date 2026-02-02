using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FleetManagementSystem.Api.DTOs;
using FleetManagementSystem.Api.Services;

namespace FleetManagementSystem.Api.Controllers;

[ApiController]
[Route("booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("{bookingId}")]
    public ActionResult<BookingResponse> GetBooking(long bookingId)
    {
        try
        {
            return Ok(_bookingService.GetBooking(bookingId));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("user/{email}")]
    public ActionResult<List<BookingResponse>> GetBookingsByUser(string email)
    {
        return Ok(_bookingService.GetBookingsByEmail(email));
    }

    [HttpGet("all")]
    public ActionResult<List<BookingResponse>> GetAllBookings()
    {
        return Ok(_bookingService.GetAllBookings());
    }

    [HttpPost("create")]
    public ActionResult<BookingResponse> CreateBooking([FromBody] BookingRequest request)
    {
        try
        {
            return Ok(_bookingService.CreateBooking(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("handover/{bookingId}")]
    public ActionResult<BookingResponse> HandoverCar(long bookingId)
    {
        // Delegating to ProcessHandover wrapper logic if needed, but ProcessHandover(HandoverRequest) is main 
        // Java code: handoverCar(Long) -> calls processHandover(new HandoverRequest(id))
        var req = new HandoverRequest { BookingId = bookingId };
        try
        {
            return Ok(_bookingService.ProcessHandover(req));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("process-handover")]
    public ActionResult<BookingResponse> ProcessHandover([FromBody] HandoverRequest request)
    {
        try
        {
            return Ok(_bookingService.ProcessHandover(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("return")]
    public ActionResult<BookingResponse> ReturnCar([FromBody] ReturnRequest request)
    {
        try
        {
            return Ok(_bookingService.ReturnCar(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("cancel/{bookingId}")]
    public ActionResult<BookingResponse> CancelBooking(long bookingId)
    {
        try
        {
            return Ok(_bookingService.CancelBooking(bookingId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("modify/{bookingId}")]
    public ActionResult<BookingResponse> ModifyBooking(long bookingId, [FromBody] BookingRequest request)
    {
        try
        {
             return Ok(_bookingService.ModifyBooking(bookingId, request));
        }
        catch (Exception ex)
        {
             return BadRequest(ex.Message);
        }
    }

    [HttpPost("storeDates")]
    public ActionResult<string> StoreBookingDates([FromQuery] string start_date, [FromQuery] string end_date, [FromQuery] int cust_id)
    {
        // Stub as in Java
        // initialDateService.storeTempDateandTime(...)
        return Ok("success");
    }
}
