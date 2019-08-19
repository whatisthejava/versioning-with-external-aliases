extern alias v1Schemas;
using Newtonsoft.Json;

namespace Schemas
{
    public class ProcessSchema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Owner = "Default Owner added to V2";
        public string Version = "v2";

        public ProcessSchema()
        {
        }

        public ProcessSchema(string id, string title, string owner)
        {
            Id = id;
            Title = title;
            Owner = owner;
        }

        public ProcessSchema(string potentialSchema)
        {
            var schema = JsonConvert.DeserializeObject<ProcessSchema>(potentialSchema);
            if (schema.Version == this.Version)
            {
                this.Id = schema.Id;
                this.Title = schema.Title;
            }   
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);
                this.Id = previousSchema.Id;
                this.Title = previousSchema.Title;
            }
            this.Owner = schema.Owner;
            this.Version = schema.Version;
        }
                    
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Using external aliases i can convert this object into the previous verison 
        /// </summary>
        /// <returns></returns>
        public v1Schemas.Schemas.ProcessSchema ToPreviousVersion(string schema)
        {
            return new v1Schemas.Schemas.ProcessSchema(schema);
        }
    }
}
