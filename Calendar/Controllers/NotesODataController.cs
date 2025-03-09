using Calendar.Abstraction.Interfaces;
using Calendar.Data;
using Calendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Calendar.Controllers;

[Route("odata/notes")] 
[ApiController]
public class NotesODataController : ODataController
{
    private readonly IReadRepository<Note> _readRepository;

    public NotesODataController(IReadRepository<Note> readRepository) => _readRepository = readRepository;


    [HttpGet]
    [EnableQuery] 
    public async Task<IQueryable<Note>> Get()
    {
        return _readRepository.AsQueryable();
    }


    


}







