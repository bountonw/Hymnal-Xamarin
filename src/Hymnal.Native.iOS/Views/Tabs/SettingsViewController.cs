using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Settings", TabIconName = "tab settings")]
    public partial class SettingsViewController : BaseViewController<SettingsViewModel>
    {
        public SettingsViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
