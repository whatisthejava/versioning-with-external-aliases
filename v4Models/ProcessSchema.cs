extern alias v3Schemas;
extern alias v2Schemas;
extern alias v1Schemas;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Schemas
{
    public class ProcessSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public string Purpose { get; set; }
        public List<Actor> Actors { get; set; }
        public string Version = "v4";

        public ProcessSchema()
        {
            Actors = new List<Actor>();
        }

        public ProcessSchema(string id, string title, string owner, string purpose)
        {
            Id = id;
            Title = title;
            Owner = owner;
            Purpose = purpose;
            Actors = new List<Actor>();
        }

        public ProcessSchema(string potentialSchema)
        {
            var schema = JsonConvert.DeserializeObject<ProcessSchema>(potentialSchema);
            if (schema.Version == this.Version)
            {
                this.Id = schema.Id;
                this.Title = schema.Title;
                this.Owner = schema.Owner;
                this.Purpose = schema.Purpose;
            }
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);
                this.Id = previousSchema.Id;
                this.Title = previousSchema.Title;
                this.Owner = previousSchema.Owner;
                this.Purpose = previousSchema.Purpose;
            }

            // Lists and versions need to be done afterwards
            this.Actors = schema.Actors != null ? schema.Actors : new List<Actor>();
            this.Version = schema.Version;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        public v3Schemas.Schemas.ProcessSchema ToPreviousVersion(string schema)
        {
            return new v3Schemas.Schemas.ProcessSchema(schema);
        }
    }
}
