using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.Tests
{
    [TestFixture]

    class MetaDataServiceTests
    {
        [Test]

        public void SearchItemsByKeywordOrTypWithNullShouldReturnEmptyList()
        {
            //arrange

            var repoMock = A.Fake<IMetaDataRepository>();

            var sut = new MetaDataService(repoMock);

            //act
            var result = sut.SearchItemsByKeywordOrTyp(null);

            //assert
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(ObservableCollection<MetadataItem>),result);
            Assert.That(result.Count==0);


        }

        [Test]
        public void SearchItemsByKeywordOrTypWithKeywordShouldReturnTwoItems()
        {
            //arrange

            var testList = new ObservableCollection<MetadataItem>
            {
                new MetadataItem()
                {
                    Bezeichnung = "hallo",
                    Typ = "Vertrag"


                },
                new MetadataItem()
                {
                    Bezeichnung = "Samy",
                    Typ = "Quittungen"
                },

                new MetadataItem()
                {
                    Bezeichnung = "Hallo Welt",
                    Typ = "Vertrag"
                }
            };
            
            
            var repoMock = A.Fake<IMetaDataRepository>();

            A.CallTo(() => repoMock.SearchMetaDataItemsAndAddToList()).Returns(testList);

            var sut = new MetaDataService(repoMock);

            //act

            var result = sut.SearchItemsByKeywordOrTyp("Hallo");

            //assert

            Assert.That(result.Count==2);
            Assert.AreEqual(testList[0], result[0]);
            Assert.AreEqual(testList[2],result[1]);




        }

        [Test]
        public void SearchItemsByKeywordAndTypNoInputShouldReturnEnEmptyList()
        {
            //arrange

            var repoMock = A.Fake<IMetaDataRepository>();

            var sut = new MetaDataService(repoMock);

            //act
            var result = sut.SearchItemsByKeywordAndTyp(null,null);

            //assert
            Assert.NotNull(result);
            Assert.IsInstanceOf(typeof(ObservableCollection<MetadataItem>), result);
            Assert.That(result.Count == 0);

        }

        [Test]

        public void SearchItemsByKeywordAndTypBothInputShouldReturnTwoObjects()
        {
            //arrange

            var testList = new ObservableCollection<MetadataItem>
            {
                new MetadataItem()
                {
                    Bezeichnung = "hallo",
                    Typ = "Vertrag"


                },
                new MetadataItem()
                {
                    Bezeichnung = "Samy",
                    Typ = "Quittungen"
                },

                new MetadataItem()
                {
                    Bezeichnung = "Hallo Welt",
                    Typ = "Vertrag"
                }
            };


            var repoMock = A.Fake<IMetaDataRepository>();

            A.CallTo(() => repoMock.SearchMetaDataItemsAndAddToList()).Returns(testList);

            var sut = new MetaDataService(repoMock);

            //act

            var result = sut.SearchItemsByKeywordAndTyp("Hallo","Vertrag");

            //assert

            Assert.That(result.Count == 2);
            Assert.That(result[0] == testList[0]);
            Assert.That(result[1] == testList[2]);


        }
    }
}
