extern alias v4Schemas;
using Newtonsoft.Json;

namespace Schemas
{
    public class Actor
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public string Version = "v5";

        public Actor()
        {
        }

        public Actor(string name, string age)
        {
            Name = name;
            Age = age;
        }

        public Actor(string potentialSchema)
        {
            var schema = JsonConvert.DeserializeObject<Actor>(potentialSchema);
            if (schema.Version == this.Version)
            {
                this.Name = schema.Name;
                this.Age = schema.Age;
            }
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);
                this.Name = previousSchema.Name;
                this.Age = previousSchema.Age;
            }
            this.Version = schema.Version;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


        public v4Schemas.Schemas.Actor ToPreviousVersion(string schema)
        {
            return new v4Schemas.Schemas.Actor(schema);
        }
    }
}