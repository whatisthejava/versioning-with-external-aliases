using System;
using System.Collections.Generic;
using System.IO;
using v1StorageModel;

namespace StorageRepository
{
    public class DefaultStorageRepository : IDefaultStorageRepository
    {
        private string _path;

        public DefaultStorageRepository(string path)
        {
            _path = path;
        }
        

        private const string FileName = "Alias-Versioning-Demo-Model";
        public void Write(StorageModel model)
        {

            var writePath = $"{_path}{FileName}-{model.Id}.temp";
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            File.WriteAllText($"{writePath}", str);
        }

        public List<StorageModel> GetAll()
        {
            var list = new List<StorageModel>();

            foreach (string file in Directory.EnumerateFiles(_path, "*.temp"))
            {
                string contents = File.ReadAllText(file);
                list.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<StorageModel>(contents));
            }
            return list;
        }

        public StorageModel Get(string id)
        {
            var readPath = $"{_path}{FileName}-{id}.temp";
            var contents = File.ReadAllText(readPath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<StorageModel>(contents);
        }
    }
}
