using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Search")]
    public partial class SearchViewController : MvxViewController<SearchViewModel>
    {
        public SearchViewController (IntPtr handle) : base (handle)
        {
        }
    }
}