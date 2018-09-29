using System.Collections.ObjectModel;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public interface IMetaDataService
    {
        ObservableCollection<MetadataItem> SearchItemsByKeywordOrTyp(string value);
        ObservableCollection<MetadataItem> SearchItemsByKeywordAndTyp(string keyword, string typ);
    }
}