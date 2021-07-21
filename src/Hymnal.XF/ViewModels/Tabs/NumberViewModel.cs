using System.Collections.Generic;
using System.Linq;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Hymnal.XF.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Hymnal.XF.ViewModels
{
    public class NumberViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        private string hymnNumber;
        public string HymnNumber
        {
            get => hymnNumber;
            set => SetProperty(ref hymnNumber, value);
        }

        public DelegateCommand<string> OpenHymnCommand { get; internal set; }

        public NumberViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;

            OpenHymnCommand = new DelegateCommand<string>(OpenHymnAsync).ObservesCanExecute(() => NotBusy);
#if DEBUG
            // A long hymn
            HymnNumber = $"{1}";
#endif
        }

        //public override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(NumberViewModel) },
        //        { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
        //    });
        //}

        private async void OpenHymnAsync(string text)
        {
            Busy = true;
            var num = text ?? HymnNumber;

            HymnalLanguage language = preferencesService.ConfiguratedHymnalLanguage;
            IEnumerable<Hymn> hymns = await hymnsService.GetHymnListAsync(language);

            if (int.TryParse(num, out var number))
            {
                if (number < 0 || number > hymns.Count())
                    return;

                await NavigationService.NavigateAsync(
                    $"{nameof(NavigationPage)}/{nameof(HymnPage)}",
                    new HymnIdParameter
                    {
                        Number = number,
                        HymnalLanguage = language
                    },
                    true, true);
            }
            Busy = false;
        }
    }
}