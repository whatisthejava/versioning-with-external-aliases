using Newtonsoft.Json;

namespace Schemas
{
    public class ActorName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Version = "v6";

        public ActorName()
        {
        }

        public ActorName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ActorName(string potentialContents)
        {
            var actor = JsonConvert.DeserializeObject<ActorName>(potentialContents);
            this.FirstName = actor.FirstName;
            this.LastName = actor.LastName;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
