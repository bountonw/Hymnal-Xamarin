using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    public partial class SettingsViewController : MvxViewController<SettingsViewModel>
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }
    }
}