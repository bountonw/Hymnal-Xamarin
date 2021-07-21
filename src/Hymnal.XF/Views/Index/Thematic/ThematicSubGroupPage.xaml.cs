using Hymnal.XF.ViewModels;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThematicSubGroupPage : BaseContentPage<ThematicSubGroupViewModel>
    {
        public ThematicSubGroupPage()
        {
            InitializeComponent();
        }
    }
}