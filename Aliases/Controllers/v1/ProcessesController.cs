extern alias v1Schemas;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StorageRepository;
using System.Collections.Generic;
using System.Linq;
using v1Schemas::Schemas;

namespace Aliases.Controllers.v1
{
    [ApiVersion("1.0", Deprecated =true)]
    [Route("api/v1/processes")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        private IDefaultStorageRepository _repo;

        public ProcessesController(IDefaultStorageRepository repo) 
        {
            _repo = repo;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult Write([FromBody] ProcessSchema schema)
        {
            _repo.Write(new v1StorageModel.StorageModel() { Contents = schema.ToString(), Id = schema.Id, VersionOfContents = 1 });
            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<List<ProcessSchema>> Get()
        {
            
            return _repo.GetAll()
              .Select(x => JsonConvert.DeserializeObject<ProcessSchema>(x.Contents))
              .ToList();
            
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("{id}")]
        public ActionResult<ProcessSchema> GetById(string id)
        {
            return JsonConvert.DeserializeObject<ProcessSchema>(_repo.Get(id).Contents);
        }
    }
}
