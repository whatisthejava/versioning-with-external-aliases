extern alias v4Schemas;
extern alias v3Schemas;
extern alias v2Schemas;
extern alias v1Schemas;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Schemas
{
    public class ProcessSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public string Purpose { get; set; }
        public List<Actor> ActorCollection { get; set; }
        public string Version = "v5";

        public ProcessSchema()
        {
            ActorCollection = new List<Actor>();
        }

        public ProcessSchema(string id, string title, string owner, string purpose)
        {
            Id = id;
            Title = title;
            Owner = owner;
            Purpose = purpose;
            ActorCollection = new List<Actor>();
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
                this.ActorCollection = schema.ActorCollection;
            }
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);             
                this.Id = previousSchema.Id;
                this.Title = previousSchema.Title;
                this.Owner = previousSchema.Owner;
                this.Purpose = previousSchema.Purpose;
                this.ActorCollection = previousSchema.Actors.Select(x => new Actor(x.ToString())).ToList<Actor>();
            }
            this.ActorCollection = this.ActorCollection != null ? this.ActorCollection : schema.ActorCollection;

            this.Version = schema.Version;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public v4Schemas.Schemas.ProcessSchema ToPreviousVersion(string schema)
        {
            return new v4Schemas.Schemas.ProcessSchema(schema);
        }
    }
}
