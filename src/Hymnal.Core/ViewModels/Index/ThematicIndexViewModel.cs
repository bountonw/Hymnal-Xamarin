using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class ThematicIndexViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        public MvxObservableCollection<Thematic> Thematics { get; set; } = new MvxObservableCollection<Thematic>();

        public Thematic SelectedThematic
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedThematicExecuteAsync(value).ConfigureAwait(true);
                RaisePropertyChanged(nameof(SelectedThematic));
            }
        }

        private HymnalLanguage loadedLanguage;

        public ThematicIndexViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
        }

        ~ThematicIndexViewModel()
        {
            preferencesService.HymnalLanguageConfiguratedChanged -= PreferencesService_HymnalLanguageConfiguratedChangedAsync;
        }

        public override async Task Initialize()
        {
            preferencesService.HymnalLanguageConfiguratedChanged += PreferencesService_HymnalLanguageConfiguratedChangedAsync;

            HymnalLanguage language = preferencesService.ConfiguratedHymnalLanguage;
            await CheckAsync(language);

            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(ThematicIndexViewModel) },
                { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private async void PreferencesService_HymnalLanguageConfiguratedChangedAsync(object sender, HymnalLanguage e)
        {
            await CheckAsync(e);
        }


        private async Task CheckAsync(HymnalLanguage newLanguage)
        {
            if (loadedLanguage == null)
            {
                loadedLanguage = newLanguage;
            }
            else
            {
                // If the Language changed
                if (!newLanguage.Equals(loadedLanguage))
                {
                    loadedLanguage = newLanguage;
                    Thematics.Clear();
                }
            }

            if (loadedLanguage.SupportThematicList && Thematics.Count == 0)
            {
                Thematics.AddRange(await hymnsService.GetThematicListAsync(loadedLanguage));
            }
        }

        private async Task SelectedThematicExecuteAsync(Thematic thematic)
        {
            await navigationService.Navigate<ThematicSubGroupViewModel, Thematic>(thematic);
        }
    }
}
