using Newtonsoft.Json;

namespace Schemas
{
    public class Actor
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public string Version = "v4";

        public Actor()
        {
        }

        public Actor(string name, string age)
        {
            Name = name;
            Age = age;
        }

        public Actor(string potentialContents)
        {
            var actor = JsonConvert.DeserializeObject<Actor>(potentialContents);
            this.Name = actor.Name;
            this.Age = actor.Age;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
