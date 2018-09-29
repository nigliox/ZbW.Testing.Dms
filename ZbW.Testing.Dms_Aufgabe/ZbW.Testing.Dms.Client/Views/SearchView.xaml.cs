using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using ZbW.Testing.Dms.Client.Annotations;
using ZbW.Testing.Dms.Client.Repositories;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.Views
{
    using System.Configuration;
    using System.Windows.Controls;
    using ZbW.Testing.Dms.Client.ViewModels;

    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        [ExcludeFromCodeCoverage]
        public SearchView()
        {
            InitializeComponent();
            var repo = new MetaDataRepository(ConfigurationManager.AppSettings["RepositoryDir"]);
            var service = new MetaDataService(repo);
            DataContext = new SearchViewModel(service);
        }
    }
}