using System.Collections.Generic;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace Hymnal.Core.ViewModels
{
    public class RootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMvxLog log;
        private readonly IPreferencesService preferencesService;

        public RootViewModel(
            IMvxNavigationService navigationService,
            IMvxLog log,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.log = log;
            this.preferencesService = preferencesService;
        }

        private bool loaded = false;
        public override async void ViewAppearing()
        {
            base.ViewAppearing();

            if (loaded)
                return;

            loaded = true;

#if __IOS__ || __ANDROID__
            await navigationService.Navigate<NumberViewModel>();
            await navigationService.Navigate<IndexViewModel>();
            await navigationService.Navigate<FavoritesViewModel>();
            await navigationService.Navigate<SettingsViewModel>();
#elif __TVOS__
            // Native project, RootViewController
#else
            await navigationService.Navigate<SimpleViewModel>();
#endif
        }

        // LifeCycle implemented in RootViewModel
        #region LifeCycle
#if __IOS__ || __ANDROID__
        public override void Start()
        {
            log.Debug("App Started");

            Analytics.TrackEvent(Constants.TrackEvents.AppOpened, new Dictionary<string, string>
            {
                { Constants.TrackEvents.AppOpenedScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.AppOpenedScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id },
                { Constants.TrackEvents.AppOpenedScheme.ThemeConfigurated, AppInfo.RequestedTheme.ToString() }
            });

            base.Start();
        }
#endif
#endregion
    }
}
