using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Repositories
{
    class MetaDataRepository
    {
        public string FolderPath { get; set; }

        public MetaDataRepository(string folderPath)
        {
            this.FolderPath = folderPath;
            Directory.CreateDirectory(folderPath);
            
        }

        public void StoreMetaDataItem(MetadataItem data)
        {
            var folderName = data.ValutaDatum.Year;
            Directory.CreateDirectory(this.FolderPath + "\\" + folderName);

            File.Copy(data.FilePath,
                FolderPath + "\\" + data.ValutaDatum.Year + "\\" + data.Guid + "_Content" + Path.GetExtension(data.FilePath));

            data.NewFilePath = FolderPath + "\\" + data.ValutaDatum.Year + "\\" + data.Guid + "_Content" + Path.GetExtension(data.FilePath);


            using (var metaFile = File.Create(FolderPath + "\\" + folderName + "\\" + data.Guid + "_Metadata.xml"))
            {
                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(data.GetType());
                writer.Serialize(metaFile, data);

            }


           
        }

        

        public List<MetadataItem> SearchMetaDataItemsAndAddToList()
        {
            var directories = Directory.GetDirectories(this.FolderPath);
            var foundItems = new List<MetadataItem>();
            foreach (var directory in directories)
            {

                var files = Directory.EnumerateFiles(directory, "*.xml");
                foreach (var file in files)
                {
                    using (var fileStream = File.Open(file, FileMode.Open, FileAccess.Read))
                    {
                        System.Xml.Serialization.XmlSerializer writer =
                            new System.Xml.Serialization.XmlSerializer(typeof(MetadataItem));
                       var item =  writer.Deserialize(fileStream) as MetadataItem;

                       foundItems.Add(item);
                       
                    }
                }
            }

            return foundItems;
        }

    }
}
