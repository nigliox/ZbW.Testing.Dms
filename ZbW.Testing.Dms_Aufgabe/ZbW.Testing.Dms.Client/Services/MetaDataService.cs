using System.Collections.ObjectModel;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;

namespace ZbW.Testing.Dms.Client.Services
{
    public class MetaDataService : IMetaDataService
    {
        public MetaDataService(IMetaDataRepository data)
        {
            MetaDataRepository = data;
        }

        public IMetaDataRepository MetaDataRepository { get; set; }


        public ObservableCollection<MetadataItem> SearchItemsByKeywordOrTyp(string value)
        {
            var allItems = MetaDataRepository.SearchMetaDataItemsAndAddToList();

            var foundItems = new ObservableCollection<MetadataItem>();

            foreach (var item in allItems)
                if (item.Bezeichnung.ToLower().Contains(value.ToLower()) || item.Typ.Contains(value))
                    foundItems.Add(item);

            return foundItems;
        }

        public ObservableCollection<MetadataItem> SearchItemsByKeywordAndTyp(string keyword, string typ)
        {
            var allItems = MetaDataRepository.SearchMetaDataItemsAndAddToList();

            var foundItems = new ObservableCollection<MetadataItem>();


            foreach (var item in allItems)
                if (item.Bezeichnung.ToLower().Contains(keyword.ToLower()) && item.Typ.Contains(typ))
                    foundItems.Add(item);

            return foundItems;
        }
    }
}