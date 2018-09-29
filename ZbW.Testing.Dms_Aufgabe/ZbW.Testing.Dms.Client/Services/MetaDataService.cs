using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;

namespace ZbW.Testing.Dms.Client.Services
{
    public class MetaDataService : IMetaDataService
    {

        public IMetaDataRepository MetaDataRepository { get; set; }

     
        public MetaDataService(IMetaDataRepository data)
        {
            MetaDataRepository = data;

        }


        public ObservableCollection<MetadataItem> SearchItemsByKeywordOrTyp(string value)
        {
            var allItems = this.MetaDataRepository.SearchMetaDataItemsAndAddToList();
            
            var foundItems = new ObservableCollection<MetadataItem>();

            foreach (var item in allItems)
            {
                if (item.Bezeichnung.ToLower().Contains(value.ToLower()) || item.Typ.Contains(value))
                {
                    foundItems.Add(item);
                }

            }

            return foundItems;

        }

        public ObservableCollection<MetadataItem> SearchItemsByKeywordAndTyp(string keyword, string typ)
        {
            var allItems = this.MetaDataRepository.SearchMetaDataItemsAndAddToList();

            var foundItems= new ObservableCollection<MetadataItem>();
            

            foreach (var item in allItems)
            {
                if (item.Bezeichnung.ToLower().Contains(keyword) && item.Typ.Contains(typ))
                {
                    foundItems.Add(item);
                }


            }

            return foundItems;

        }

        
    }
}
