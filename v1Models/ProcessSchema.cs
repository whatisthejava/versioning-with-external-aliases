using Newtonsoft.Json;
using System;

namespace Schemas
{
    public class ProcessSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Version = "v1";

        public ProcessSchema()
        {
        }

        public ProcessSchema(string potentialContents )
        {
            var schema = JsonConvert.DeserializeObject<ProcessSchema>(potentialContents);
            this.Id = schema.Id;
            this.Title = schema.Title;
            this.Version = schema.Version;
        }

        public ProcessSchema(string id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
