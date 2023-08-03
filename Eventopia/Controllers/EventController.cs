﻿using Eventopia.Core.Data;
using Eventopia.Core.DTO;
using Eventopia.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eventopia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService; 

    public EventController(IEventService eventService) 
    {
        _eventService = eventService;
    }

    [HttpPost]
    [Route("SearchEventsBetweenDates")]
    //[Authorize(Policy = "AdminOnly")]
    public ActionResult<List<Event>> SearchEventsBetweenDates([FromBody] SearchBetweenDatesDTO searchDTO)
    {
		List<Event> eventsInRange = ((IEventService)_eventService).GetEventsBetweenDates(searchDTO); 

        return Ok(eventsInRange);
    }


    [HttpPost]
    [Route("SearchEventsByName")]
    public IActionResult SearchEventsByName(
		[Required(ErrorMessage = "Name is required.")]
	    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
		string eventName)
    {
        return Ok(_eventService.SearchEventsByName(eventName));
    }

    [HttpPost]
    [Route("CreateNewEvent")]
    public IActionResult CreateNewEvent([FromBody] Event eventt)
    {
		_eventService.CreateNew(eventt);

        return Ok();
    }

    [HttpGet]
    [Route("GetEventByID/{id}")]
    public IActionResult GetEventByID(
		[Required(ErrorMessage = "EventId is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "EventId must be a positive number.")]
		int id)
    {
		return Ok(_eventService.GetById(id));
    }

    [HttpGet]
    [Route("GetAllEvents")]
    public List<Event> GetAllEvents()
    {
        return _eventService.GetAll();
    }

    [HttpPut]
    [Route("UpdateEvent")]
    public IActionResult UpdateEvent([FromBody] Event eventt)
    {
		_eventService.Update(eventt);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteEvent/{id}")]
    public IActionResult DeleteEvent(
		[Required(ErrorMessage = "EventId is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "EventId must be a positive number.")]
		int id)
    {
		_eventService.Delete(id);
        return Ok();
    }
}
