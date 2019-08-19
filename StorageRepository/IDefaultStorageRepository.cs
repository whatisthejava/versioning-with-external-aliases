using System.Collections.Generic;
using v1StorageModel;

namespace StorageRepository
{
    public interface IDefaultStorageRepository
    {
        StorageModel Get(string id);
        List<StorageModel> GetAll();
        void Write(StorageModel model);
    }
}