using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Icon = "TabIndex")]
    [MvxCarouselPagePresentation(CarouselPosition.Root, NoHistory = true)]
    public partial class IndexPage : MvxCarouselPage<IndexViewModel>
    {
        public IndexPage()
        {
            InitializeComponent();
        }
    }
}
