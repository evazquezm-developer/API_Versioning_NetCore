using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api_StringList_Versioning.V2.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("2.0")]
public class StringListController : ControllerBase
{
    // For testing headers.
    [HttpGet]
    public IEnumerable<string> Get() 
    {
        return Data.Summaries.Where(x => x.StartsWith("S"));
    }
}

