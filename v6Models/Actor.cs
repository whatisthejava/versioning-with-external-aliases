extern alias v5Schemas;
using Newtonsoft.Json;

namespace Schemas
{
    public class Actor
    {
        public ActorName ActorName { get; set; }
        public string Role { get; set;  }

        public string Version = "v6";

        public Actor()
        {
        }

        public Actor(ActorName name, string role)
        {
            ActorName = name;
            Role = role;
        }

        public Actor(string potentialSchema)
        {
            var schema = JsonConvert.DeserializeObject<Actor>(potentialSchema);
            if (schema.Version == this.Version)
            {
                this.ActorName = schema.ActorName;
                this.Role = schema.Role;
            }
            else
            {
                var previousSchema = ToPreviousVersion(potentialSchema);
                this.ActorName = new ActorName() { FirstName = previousSchema.Name } ;
                this.Role = "Default Role Added to v6";
            }
            this.Version = schema.Version;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


        public v5Schemas.Schemas.Actor ToPreviousVersion(string schema)
        {
            return new v5Schemas.Schemas.Actor(schema);
        }
    }
}