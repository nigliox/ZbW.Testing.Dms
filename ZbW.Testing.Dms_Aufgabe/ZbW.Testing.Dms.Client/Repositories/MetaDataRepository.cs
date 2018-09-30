using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Repositories
{
    public class MetaDataRepository:IMetaDataRepository
    {
        public MetaDataRepository(string folderPath)
        {
            FolderPath = folderPath;
            Directory.CreateDirectory(folderPath);
        }

        public string FolderPath { get; set; }

        public void StoreMetaDataItem(MetadataItem data)
        {
            var folderName = data.ValutaDatum.Year;
            Directory.CreateDirectory(FolderPath + "\\" + folderName);

            File.Copy(data.FilePath,
                FolderPath + "\\" + data.ValutaDatum.Year + "\\" + data.Guid + "_Content" +
                Path.GetExtension(data.FilePath));

            data.NewFilePath = FolderPath + "\\" + data.ValutaDatum.Year + "\\" + data.Guid + "_Content" +
                               Path.GetExtension(data.FilePath);


            using (var metaFile = File.Create(FolderPath + "\\" + folderName + "\\" + data.Guid + "_Metadata.xml"))
            {
                var writer =
                    new XmlSerializer(data.GetType());
                writer.Serialize(metaFile, data);
            }
        }


        public ObservableCollection<MetadataItem> SearchMetaDataItemsAndAddToList()
        {
            var directories = Directory.GetDirectories(FolderPath);
            var foundItems = new ObservableCollection<MetadataItem>();
            foreach (var directory in directories)
            {
                var files = Directory.EnumerateFiles(directory, "*.xml");
                foreach (var file in files)
                    using (var fileStream = File.Open(file, FileMode.Open, FileAccess.Read))
                    {
                        var writer =
                            new XmlSerializer(typeof(MetadataItem));
                        var item = writer.Deserialize(fileStream) as MetadataItem;

                        foundItems.Add(item);
                    }
            }

            return foundItems;
        }
    }
}