using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class SearchViewModel : BindableBase
    {
        private ObservableCollection<MetadataItem> _filteredMetadataItems;

        private MetadataItem _selectedMetadataItem;

        private string _selectedTypItem;

        private string _suchbegriff;

        private List<string> _typItems;

     

        public SearchViewModel(IMetaDataService service)
        {
            TypItems = ComboBoxItems.Typ;

            CmdSuchen = new DelegateCommand(OnCmdSuchen);
            CmdReset = new DelegateCommand(OnCmdReset);
            CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
           
            MetaDataService = service;
            
        }

        public IMetaDataService MetaDataService { get; set; }

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
        public ObservableCollection<MetadataItem> FilteredMetadataItems
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

        [ExcludeFromCodeCoverage]
        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }

        private void OnCmdOeffnen()
        {
            var process = new ProcessTestable();
            var path = SelectedMetadataItem.NewFilePath;

            try
            {
                process.Open(path);
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Das File wurde nicht gefunden");
                throw e;
                
            }
        }

        private void OnCmdSuchen()
        { 
           
            if (_suchbegriff == null && _selectedTypItem == null)
            {
                
                
                var msg = new MessageBoxTestable();
                msg.ShowMessage("Es muss mind. ein Suchbegriff oder ein Typ angewählt werden");
                return;
            }


            if (_suchbegriff != null && _selectedTypItem == null)
            {
                FilteredMetadataItems = MetaDataService.SearchItemsByKeywordOrTyp(_suchbegriff);
                return;
            }

            if (_selectedTypItem != null && _suchbegriff == null)
            {
                FilteredMetadataItems = MetaDataService.SearchItemsByKeywordOrTyp(_selectedTypItem);
                return;
            }

            FilteredMetadataItems = MetaDataService.SearchItemsByKeywordAndTyp(_suchbegriff, _selectedTypItem);
        }

        private void OnCmdReset()
        {
            
            FilteredMetadataItems.Clear();

  
        }
    }
}