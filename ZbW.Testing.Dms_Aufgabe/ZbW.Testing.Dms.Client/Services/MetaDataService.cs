using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;

namespace ZbW.Testing.Dms.Client.Services
{
    class MetaDataService
    {

        public MetaDataRepository MetaDataRepository { get; set; }


        public MetaDataService(MetaDataRepository data)
        {
            MetaDataRepository = data;

        }


        public List<MetadataItem> SearchItemsByKeywordOrTyp(string value)
        {
            var allItems = new List<MetadataItem>();
            allItems = this.MetaDataRepository.SearchMetaDataItemsAndAddToList();
            var foundItems = new List<MetadataItem>();

            foreach (var item in allItems)
            {
                if (item.Bezeichnung.Contains(value) || item.Typ.Contains(value))
                {
                    foundItems.Add(item);
                }

            }

            return foundItems;

        }

        public List<MetadataItem> SearchItemsByKeywordAndTyp(string keyword, string typ)
        {
            var allItems = new List<MetadataItem>();
            allItems = this.MetaDataRepository.SearchMetaDataItemsAndAddToList();
            var foundItems= new List<MetadataItem>();
            

            foreach (var item in allItems)
            {
                if (item.Bezeichnung.Contains(keyword) && item.Typ.Contains(typ))
                {
                    foundItems.Add(item);
                }


            }

            return foundItems;

        }

        
    }
}
