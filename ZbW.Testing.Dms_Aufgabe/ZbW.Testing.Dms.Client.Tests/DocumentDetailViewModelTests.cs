using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;
using ZbW.Testing.Dms.Client.ViewModels;
using ZbW.Testing.Dms.Client.Views;

namespace ZbW.Testing.Dms.Client.Tests
{
    [TestFixture()]
    class DocumentDetailViewModelTests
    {
        private const string benutzer = "Fer";
        private const string FOLDER_PATH = "FOLDER_PATH";
        private const string DEST_PATH = "DEST_PATH";
        private const string SOURCE_PATH = "SOURCE_PATH";

        [Test]

        public void OnCmdSpeichernWithoutKeywordAndTypThrowsException()
        {
            //arrange
            var actionMock = A.Fake<Action>();
            
            var sut = new DocumentDetailViewModel(benutzer, actionMock);

            sut.Bezeichnung = null;
            sut.SelectedTypItem = null;
            sut.ValutaDatum = null;


            //act

            void TestDelegate() => sut.CmdSpeichern.Execute();

            //assert

            Assert.That(TestDelegate, Throws.Exception.TypeOf<Exception>());

        }

    }
}
