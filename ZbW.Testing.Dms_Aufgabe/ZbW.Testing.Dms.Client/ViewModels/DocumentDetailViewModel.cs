using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Win32;
    using Prism.Commands;
    using Prism.Mvvm;
    using ZbW.Testing.Dms.Client.Repositories;

    internal class DocumentDetailViewModel : BindableBase
    {
        private readonly Action _navigateBack;

        private string _benutzer;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        private string _filePath;

        private string _newFilePath;

        private bool _isRemoveFileEnabled;

        private string _selectedTypItem;

        private string _stichwoerter;

        private List<string> _typItems;

        private DateTime? _valutaDatum;

        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            TypItems = ComboBoxItems.Typ;

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
            this.MetaDataRepository = new MetaDataRepository(ConfigurationManager.AppSettings["RepositoryDir"]);
        }

        [ExcludeFromCodeCoverage]
        public MetaDataRepository MetaDataRepository { get; set; }

        [ExcludeFromCodeCoverage]
        public string Stichwoerter
        {
            get { return _stichwoerter; }

            set { SetProperty(ref _stichwoerter, value); }
        }

        [ExcludeFromCodeCoverage]
        public string Bezeichnung
        {
            get { return _bezeichnung; }

            set { SetProperty(ref _bezeichnung, value); }
        }

        [ExcludeFromCodeCoverage]
        public List<string> TypItems
        {
            get { return _typItems; }

            set { SetProperty(ref _typItems, value); }
        }
        [ExcludeFromCodeCoverage]
        public string SelectedTypItem
        {
            get { return _selectedTypItem; }

            set { SetProperty(ref _selectedTypItem, value); }
        }

        [ExcludeFromCodeCoverage]
        public DateTime Erfassungsdatum
        {
            get { return _erfassungsdatum; }

            set { SetProperty(ref _erfassungsdatum, value); }
        }

        [ExcludeFromCodeCoverage]
        public string Benutzer
        {
            get { return _benutzer; }

            set { SetProperty(ref _benutzer, value); }
        }

        public DelegateCommand CmdDurchsuchen { get; }

        public DelegateCommand CmdSpeichern { get; }

        [ExcludeFromCodeCoverage]
        public DateTime? ValutaDatum
        {
            get { return _valutaDatum; }

            set { SetProperty(ref _valutaDatum, value); }
        }

        [ExcludeFromCodeCoverage]
        public bool IsRemoveFileEnabled
        {
            get { return _isRemoveFileEnabled; }

            set { SetProperty(ref _isRemoveFileEnabled, value); }
        }

        [ExcludeFromCodeCoverage]
        private void OnCmdDurchsuchen()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                _filePath = openFileDialog.FileName;
            }
        }

        private void OnCmdSpeichern()
        {
            if (_filePath == "" || !_valutaDatum.HasValue || string.IsNullOrEmpty(_bezeichnung) || _selectedTypItem == null)
            {
                var msg = new MessageBoxTestable();

                msg.ShowMessage("Es müssen alle Pflichtfelder ausgefüllt werden!");
                return;
            }


            

            //Generate XML an Safe XML And File with the Same GUID
            var mDataItem = new MetadataItem
            {
                Bezeichnung = _bezeichnung,
                Benutzer = _benutzer,
                Typ = _selectedTypItem.ToString(),
                ValutaDatum = _valutaDatum.Value,
                Guid = Guid.NewGuid(),
                FilePath = _filePath,
                NewFilePath = _newFilePath
               
            };

            this.MetaDataRepository.StoreMetaDataItem(mDataItem);


            if (IsRemoveFileEnabled == true)
            {
                File.Delete(_filePath);

                const string message = "Das Dokument wird gelöscht! Sind Sie Sicher?? ";
                const string caption = "Form Clsing";

                MessageBox.Show(message,caption,MessageBoxButton.YesNo);
            }

            _navigateBack();
        }
    }
}