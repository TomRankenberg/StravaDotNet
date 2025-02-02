using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Data.Models.Strava;
using System.Data.Entity;
using Data.Context;

[ApiController]
[Route("api/[controller]")]
public class DetailedActivitiesController : ControllerBase
{
    private readonly DatabaseContext _context;

    public DetailedActivitiesController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<DetailedActivity>> Get()
    {
        return _context.Activities.ToList();
    }
}
