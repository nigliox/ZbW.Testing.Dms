using System.Collections.ObjectModel;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Repositories
{
    public interface IMetaDataRepository
    {
        void StoreMetaDataItem(MetadataItem data);
        ObservableCollection<MetadataItem> SearchMetaDataItemsAndAddToList();
    }
}