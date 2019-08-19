extern alias v2Schemas;
extern alias v1Schemas;


using Newtonsoft.Json;

namespace Schemas
{
    public class ProcessSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public string Purpose = "Default Purpose added to V3";
        public string Version = "v3";

        public ProcessSchema()
        {
        }

        public ProcessSchema(string id, string title, string owner, string purpose)
        {
            Id = id;
            Title = title;
            Owner = owner;
            Purpose = purpose;
        }

        public ProcessSchema(string potentialSchema)
        {
            var schema = JsonConvert.DeserializeObject<ProcessSchema>(potentialSchema);
            if (schema.Version == this.Version)
            {
                this.Id = schema.Id;
                this.Title = schema.Title;
                this.Owner = schema.Owner;
            }
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);
                this.Id = previousSchema.Id;
                this.Title = previousSchema.Title;
                this.Owner = previousSchema.Owner;
            }

            this.Purpose = schema.Purpose;
            this.Version = schema.Version;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public v2Schemas.Schemas.ProcessSchema ToPreviousVersion(string schema)
        {
            return new v2Schemas.Schemas.ProcessSchema(schema);
        }
    }
}
