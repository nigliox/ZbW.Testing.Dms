using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;

namespace ZbW.Testing.Dms.Client.Tests
{
    [TestFixture()]
    class MetaDataRepositoryTests
    {
        private string SOURCE_PATH = "C:\\Users\\Fernando Maniglio\\source\\repos\\ZbW.TestingDMS\\ZbW.Testing.Dms\\ZbW.Testing.Dms_Aufgabe\\ZbW.Testing.Dms.Client.Tests\\SempleFile\\Test.pdf";
        private string DEST_PATH = "C:\\Users\\Fernando Maniglio\\source\\repos\\ZbW.TestingDMS\\ZbW.Testing.Dms\\ZbW.Testing.Dms_Aufgabe\\ZbW.Testing.Dms.Client.Tests\\LokalTestRepository";

        private string FOLDER_PATH = "C:\\Users\\Fernando Maniglio\\source\\repos\\ZbW.TestingDMS\\ZbW.Testing.Dms\\ZbW.Testing.Dms_Aufgabe\\ZbW.Testing.Dms.Client.Tests\\LokalTestRepository";

        private DateTime valuta;

        [Test]
        public void StoreMetaDataItemChangeFilePathChangePositiv()
        {
            this.CleanUpTestFolder();
            //arrange 
            var metaDataMock = new MetadataItem();

            var sut = new MetaDataRepository(FOLDER_PATH);
            this.valuta = new DateTime(2018,09,20);

            metaDataMock.FilePath = SOURCE_PATH;
            metaDataMock.ValutaDatum = this.valuta;
            metaDataMock.Guid = Guid.NewGuid();
            


            //act
            sut.StoreMetaDataItem(metaDataMock);
            

            //assert
            Assert.That(sut.FolderPath!=null);
            Assert.That(metaDataMock.FilePath!=metaDataMock.NewFilePath);
            this.CleanUpTestFolder();
        }

        [Test]

        public void SearchMetaDataItemsAndAddToListAddedListItemsReturnList()
        {
            this.CleanUpTestFolder();
            //arrange 
            var metaDataMock = new MetadataItem();
            

            var sut = new MetaDataRepository(FOLDER_PATH);
            this.valuta = new DateTime(2018, 09, 20);
           

            metaDataMock.FilePath = SOURCE_PATH;
            metaDataMock.ValutaDatum = this.valuta;
            metaDataMock.Guid = Guid.NewGuid();
            metaDataMock.Benutzer = "Fer";
            metaDataMock.Bezeichnung = "Hallo";
            metaDataMock.Typ = "Verträge";



            //act
            sut.StoreMetaDataItem(metaDataMock);
            ObservableCollection<MetadataItem> liste = sut.SearchMetaDataItemsAndAddToList();


            //assert

            Assert.That(liste.Count == 1);
            this.CleanUpTestFolder();

        }

        private void CleanUpTestFolder()
        {
            foreach (var testDirectories in Directory.EnumerateDirectories(FOLDER_PATH))
            {
                foreach (var file in Directory.EnumerateFiles(testDirectories))
                {
                    File.Delete(file);
                }
            }
        }

    }
}
