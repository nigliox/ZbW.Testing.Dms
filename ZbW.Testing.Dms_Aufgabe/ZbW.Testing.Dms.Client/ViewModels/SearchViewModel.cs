using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Repositories;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    internal class SearchViewModel : BindableBase
    {
        private List<MetadataItem> _filteredMetadataItems;

        private MetadataItem _selectedMetadataItem;

        private string _selectedTypItem;

        private string _suchbegriff;

        private List<string> _typItems;

        public SearchViewModel()
        {
            TypItems = ComboBoxItems.Typ;

            CmdSuchen = new DelegateCommand(OnCmdSuchen);
            CmdReset = new DelegateCommand(OnCmdReset);
            CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
            MetaDataRepository = new MetaDataRepository(ConfigurationManager.AppSettings["RepositoryDir"]);
            MetaDataService = new MetaDataService(MetaDataRepository);
        }

        public MetaDataService MetaDataService { get; set; }

        public MetaDataRepository MetaDataRepository { get; set; }

        public DelegateCommand CmdOeffnen { get; }

        public DelegateCommand CmdSuchen { get; }

        public DelegateCommand CmdReset { get; }

        [ExcludeFromCodeCoverage]
        public string Suchbegriff
        {
            get => _suchbegriff;

            set => SetProperty(ref _suchbegriff, value);
        }

        [ExcludeFromCodeCoverage]
        public List<string> TypItems
        {
            get => _typItems;

            set => SetProperty(ref _typItems, value);
        }

        [ExcludeFromCodeCoverage]
        public string SelectedTypItem
        {
            get => _selectedTypItem;

            set => SetProperty(ref _selectedTypItem, value);
        }
        [ExcludeFromCodeCoverage]
        public List<MetadataItem> FilteredMetadataItems
        {
            get => _filteredMetadataItems;

            set => SetProperty(ref _filteredMetadataItems, value);
        }
        [ExcludeFromCodeCoverage]
        public MetadataItem SelectedMetadataItem
        {
            get => _selectedMetadataItem;

            set
            {
                if (SetProperty(ref _selectedMetadataItem, value)) CmdOeffnen.RaiseCanExecuteChanged();
            }
        }

        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }

        private void OnCmdOeffnen()
        {
            var newPath = SelectedMetadataItem.NewFilePath;


            try
            {
                Process.Start(newPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Das File wurde nicht gefunden");
                throw;
            }
        }

        private void OnCmdSuchen()
        {
            FilteredMetadataItems = MetaDataRepository.SearchMetaDataItemsAndAddToList();

            if (_suchbegriff != null || _selectedTypItem != null)
            {
                if (_suchbegriff != null && _selectedTypItem == null)
                    FilteredMetadataItems = MetaDataService.SearchItemsByKeywordOrTyp(_suchbegriff.ToUpper());
                else if (_selectedTypItem != null && _suchbegriff == null)
                    FilteredMetadataItems = MetaDataService.SearchItemsByKeywordOrTyp(_selectedTypItem);
                else if (_selectedTypItem != null && _selectedTypItem != null)
                    FilteredMetadataItems =
                        MetaDataService.SearchItemsByKeywordAndTyp(_suchbegriff.ToUpper(), _selectedTypItem);
            }
        }

        private void OnCmdReset()
        {
            
            FilteredMetadataItems.Clear();
  
        }
    }
}