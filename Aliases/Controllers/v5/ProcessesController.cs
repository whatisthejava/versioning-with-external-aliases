extern alias v5Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StorageRepository;
using System.Collections.Generic;
using System.Linq;
using v5Schemas::Schemas;

namespace Aliases.Controllers.v5
{
    [ApiVersion("5.0", Deprecated = true)]
    [Route("api/v5/processes")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        private IDefaultStorageRepository _repo;

        public ProcessesController(IDefaultStorageRepository repo)
        {
            _repo = repo;
        }

        [MapToApiVersion("5.0")]
        [HttpPost]
        public ActionResult Write([FromBody] ProcessSchema schema)
        {
            _repo.Write(new v1StorageModel.StorageModel() { Contents = schema.ToString(), Id = schema.Id, VersionOfContents = 5 });
            return Ok();
        }


        [MapToApiVersion("5.0")]
        [HttpGet]
        public ActionResult<List<ProcessSchema>> Get()
        {
            return _repo.GetAll().Select(x => new ProcessSchema(x.Contents)).ToList<ProcessSchema>();
        }

        [HttpGet]
        [MapToApiVersion("5.0")]
        [Route("{id}")]
        public ActionResult<ProcessSchema> GetById(string id)
        {
            return new ProcessSchema(_repo.Get(id).Contents);
        }
    }
}
