using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FakeItEasy.Configuration;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Prism.Commands;
using ZbW.Testing.Dms.Client.Annotations;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.Tests
{
    [TestFixture]
    class SearchViewModelTests
    {
        [Test]

        public void OnCmdSuchenWithoutKeywordAndTypThrowsMessageBox()
        {
            //arrange
            var serviceMock = A.Fake<IMetaDataService>();
            var msgStub = A.Fake<MessageBoxTestable>();
            A.CallTo(() => msgStub.ShowMessage(A<string>.Ignored)).Throws<Exception>();

            var sut =  new SearchViewModel(serviceMock);
            sut.Suchbegriff = null;
            sut.SelectedTypItem = null;

            //act

            void TestDelegate()=> sut.CmdSuchen.Execute();

            //assert

            Assert.That(TestDelegate,Throws.Exception.TypeOf<Exception>());

        }

        [Test]

        public void OnCmdSuchenWithoutKeywordShouldReturnEmptyList()
        {
            var suchbegriff = "Hallo";
            //arrange
            var mockService = A.Fake<IMetaDataService>();
            A.CallTo(() => mockService.SearchItemsByKeywordOrTyp(suchbegriff))
                .Returns(new ObservableCollection<MetadataItem>());

            var sut = new SearchViewModel(mockService);
            sut.Suchbegriff = suchbegriff;

            // act

            sut.CmdSuchen.Execute();

            // test


            A.CallTo(() => mockService.SearchItemsByKeywordOrTyp(suchbegriff)).MustHaveHappenedOnceExactly();
        }

        [Test]

        public void OnCmdSuchenKeywordAndTypShouldCallServiceSearchByKeywordAndTyp()
        {

            //arrange

            var mockService = A.Fake<IMetaDataService>();
            var sut = new SearchViewModel(mockService);
            var typ = "Verträge";
            var suchbegriff = "Hallo";
            sut.Suchbegriff = suchbegriff;
            sut.SelectedTypItem = typ;

            A.CallTo(() => mockService.SearchItemsByKeywordAndTyp(suchbegriff,typ))
                .Returns(new ObservableCollection<MetadataItem>());

          

            // act

            sut.CmdSuchen.Execute();

            // assert


            A.CallTo(() => mockService.SearchItemsByKeywordAndTyp(suchbegriff,typ)).MustHaveHappenedOnceExactly();
        }

        [Test]

        public void OnCmdSuchenTypShouldCallServiceSearchByKeywordOrTyp()
        {

            //arrange

            var mockService = A.Fake<IMetaDataService>();
            var sut = new SearchViewModel(mockService);
            var typ = "Verträge";
            
           
            sut.SelectedTypItem = typ;

            A.CallTo(() => mockService.SearchItemsByKeywordOrTyp(typ))
                .Returns(new ObservableCollection<MetadataItem>());



            // act

            sut.CmdSuchen.Execute();

            // assert


            A.CallTo(() => mockService.SearchItemsByKeywordOrTyp(typ)).MustHaveHappenedOnceExactly();
        }

        [Test]

        public void OnCmdOeffnenFileNotFoundNoPathThrowsException()
        {
            var mockService = A.Fake<IMetaDataService>();
            var sut = new SearchViewModel(mockService);
            var stubMetaDataItem = A.Fake<MetadataItem>();

            stubMetaDataItem.NewFilePath = "test";

            sut.SelectedMetadataItem = stubMetaDataItem;

            

            //act 

            void TestDelegate()=> sut.CmdOeffnen.Execute();

            //assert

            Assert.That(TestDelegate, Throws.Exception.TypeOf<System.ComponentModel.Win32Exception>());
        }

        [Test]

        public void OnCmdResetClearDataReturnEmptyList()
        {
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
            var serviceStub = A.Fake<IMetaDataService>();

            A.CallTo(() => repoMock.SearchMetaDataItemsAndAddToList()).Returns(testList);

            var sut = new SearchViewModel(serviceStub);

            sut.FilteredMetadataItems = testList;

            //act

            sut.CmdReset.Execute();

            //assert

            Assert.That(testList.Count==0);
        }




    }
}
