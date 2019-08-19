extern alias v4Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StorageRepository;
using System.Collections.Generic;
using System.Linq;
using v4Schemas::Schemas;

namespace Aliases.Controllers.v4
{
    [ApiVersion("4.0", Deprecated = true)]
    [Route("api/v4/processes")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {

        private IDefaultStorageRepository _repo;

        public ProcessesController(IDefaultStorageRepository repo)
        {
            _repo = repo;
        }

        [MapToApiVersion("4.0")]
        [HttpPost]
        public ActionResult Write([FromBody] ProcessSchema schema)
        {
            _repo.Write(new v1StorageModel.StorageModel() { Contents = schema.ToString(), Id = schema.Id, VersionOfContents = 4 });
            return Ok();
        }


        [MapToApiVersion("4.0")]
        [HttpGet]
        public ActionResult<List<ProcessSchema>> Get()
        {
            return _repo.GetAll().Select(x => new ProcessSchema(x.Contents)).ToList<ProcessSchema>();
        }

        [HttpGet]
        [MapToApiVersion("4.0")]
        [Route("{id}")]
        public ActionResult<ProcessSchema> GetById(string id)
        {
            return new ProcessSchema(_repo.Get(id).Contents);
        }
    }
}
