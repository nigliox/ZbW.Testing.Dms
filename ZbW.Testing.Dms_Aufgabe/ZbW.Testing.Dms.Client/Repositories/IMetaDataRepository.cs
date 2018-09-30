using System.Collections.ObjectModel;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Repositories
{
    public interface IMetaDataRepository
    {
        string FolderPath { get; set; }
        void StoreMetaDataItem(MetadataItem data);
        ObservableCollection<MetadataItem> SearchMetaDataItemsAndAddToList();
    }
}