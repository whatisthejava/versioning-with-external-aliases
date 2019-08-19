extern alias v3Schemas;
extern alias v2Schemas;
extern alias v1Schemas;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using StorageRepository;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using v3Schemas::Schemas;

namespace Aliases.Controllers.latest
{
    [Route("api/processes")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        private int CurrentVersionOfApiToMapTo;
        private IDefaultStorageRepository _repo;

        public ProcessesController(IDefaultStorageRepository repo, IOptions<ApiVersioningOptions> options)
        {
            _repo = repo;
            CurrentVersionOfApiToMapTo = options.Value.DefaultApiVersion.MajorVersion.Value;
        }

        [HttpGet]
        public ActionResult<List<ProcessSchema>> LatestGet()
        {
            if (Request.ContainsApiVersion())
            {
                return Redirect($"/api/v{Request.GetApiVersion()}/processes");
            }
            return Redirect($"/api/v{CurrentVersionOfApiToMapTo}/processes");

        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProcessSchema> LatestGetById(string id)
        {
            if (Request.ContainsApiVersion())
            {
                return Redirect($"/api/v{Request.GetApiVersion()}/processes/{id}");
            }
            return Redirect($"/api/v{CurrentVersionOfApiToMapTo}/processes/{id}");
        }


        /// <summary>
        /// The reason this accepts a jsonResult is if in future we change the schema significantly we need to be able to still accept this version of the schema 
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LatestWrite([FromBody] JObject jsonResult)
        {
            int version = CurrentVersionOfApiToMapTo;
            int.TryParse(Request.GetApiVersion(), out version);

            jsonResult["version"] = $"v{version}";
            var processSchema = new ProcessSchema(JsonConvert.SerializeObject(jsonResult));

            _repo.Write(new v1StorageModel.StorageModel() { Id = processSchema.Id, VersionOfContents = version, Contents = JsonConvert.SerializeObject(jsonResult) });
            return Ok();
        }
    }
}
