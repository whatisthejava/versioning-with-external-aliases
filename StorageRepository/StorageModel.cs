using System;

namespace v1StorageModel
{
    public class StorageModel
    {
        public string Id { get; set; }
        public string Contents { get; set; }
        public int VersionOfContents { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var model = (StorageModel)obj;
                return (this.Id == model.Id) && (this.Contents == model.Contents) && (this.VersionOfContents == model.VersionOfContents);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
