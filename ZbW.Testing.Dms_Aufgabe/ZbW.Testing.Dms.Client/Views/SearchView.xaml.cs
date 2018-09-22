using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace ZbW.Testing.Dms.Client.Views
{
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
            DataContext = new SearchViewModel();
            
        }

    }
}