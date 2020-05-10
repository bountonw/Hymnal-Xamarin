using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Helpers;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class HymnViewModel : MvxViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IDataStorageService dataStorageService;
        private readonly IPreferencesService preferencesService;
        private readonly IMediaService mediaService;
        private readonly IConnectivityService connectivityService;
        private readonly IDialogService dialogService;
        private readonly IShareService shareService;

        public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
        public int HymnFontSize => preferencesService.HymnalsFontSize;

        private Hymn hymn;
        public Hymn Hymn
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private HymnalLanguage language;
        public HymnalLanguage Language
        {
            get => language;
            set => SetProperty(ref language, value);
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value);
        }

        private bool isPlaying;
        public bool IsPlaying
        {
            get => isPlaying;
            set => SetProperty(ref isPlaying, value);
        }

        private HymnIdParameter hymnId;
        public HymnIdParameter HymnParameter
        {
            get => hymnId;
            set => SetProperty(ref hymnId, value);
        }


        public HymnViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IDataStorageService dataStorageService,
            IPreferencesService preferencesService,
            IMediaService mediaService,
            IConnectivityService connectivityService,
            IDialogService dialogService,
            IShareService shareService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.dataStorageService = dataStorageService;
            this.preferencesService = preferencesService;
            this.mediaService = mediaService;
            this.connectivityService = connectivityService;
            this.dialogService = dialogService;
            this.shareService = shareService;
            this.mediaService.Playing += MediaService_Playing;
            this.mediaService.Stopped += MediaService_Stopped;
            this.mediaService.EndReached += MediaService_EndReached;
        }

        ~HymnViewModel()
        {
            mediaService.Playing -= MediaService_Playing;
            mediaService.Stopped -= MediaService_Stopped;
            mediaService.EndReached -= MediaService_EndReached;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnParameter = parameter;
            Language = HymnParameter.HymnalLanguage.Configuration();
        }

        public override async Task Initialize()
        {
            Hymn = await hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage);

            IsPlaying = mediaService.IsPlaying;

            // Is Favorite
            IsFavorite = dataStorageService.GetItems<FavoriteHymn>().Exists(h => h.Number == Hymn.Number && h.HymnalLanguage.Equals(Language));

            // History
            if (HymnParameter.SaveInHistory)
            {
                List<HistoryHymn> history = dataStorageService.GetItems<HistoryHymn>();

                // was this hymn in the history?
                history.RemoveAll(h => h.Number == Hymn.Number && h.HymnalLanguage.Equals(Language));

                // add new in History
                history.Add(Hymn.ToHistoryHymn(HymnParameter.HymnalLanguage));

                history = history.OrderByDescending(h => h.SavedAt).ToList();

                // remove over history
                if (history.Count > Constants.MAXIMUM_RECORDS)
                    history = history.GetRange(0, Constants.MAXIMUM_RECORDS);

                // save history
                dataStorageService.ReplaceItems(history);
            }

            await base.Initialize();
        }

        #region Events
        private void MediaService_Playing(object sender, EventArgs e)
        {
            IsPlaying = true;
        }

        private void MediaService_Stopped(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        private void MediaService_EndReached(object sender, EventArgs e)
        {
            IsPlaying = false;
        }
        #endregion

        #region Commands
        public MvxCommand OpenSheetCommand => new MvxCommand(OpenSheet);
        private void OpenSheet()
        {
            navigationService.Navigate<MusicSheetViewModel, HymnIdParameter>(HymnParameter);
        }

        public MvxCommand FavoriteCommand => new MvxCommand(FavoriteExecute);
        private void FavoriteExecute()
        {
            List<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>();

            if (IsFavorite)
                favorites.RemoveAll(h => h.Number == HymnParameter.Number && h.HymnalLanguage.Equals(Language));
            else
                favorites.Add(Hymn.ToFavoriteHymn(Language));

            dataStorageService.ReplaceItems(favorites);

            IsFavorite = !IsFavorite;
        }

        public MvxCommand ShareCommand => new MvxCommand(ShareExecute);
        private void ShareExecute()
        {
            shareService.Share(
                title: hymn.Title,
                text: $"{hymn.Title}\n\n{hymn.Content}\n\n{Constants.WebLinks.DeveloperWebSite}");
        }

        public MvxCommand PlayCommand => new MvxCommand(PlayExecute);
        private void PlayExecute()
        {
            // Check internet connection
            if (!connectivityService.InternetAccess)
            {
                dialogService.Alert(Languages.WeHadAProblem, Languages.NoInternetConnection, Languages.Ok);
                return;
            }

            // Stop/Start playing
            if (IsPlaying)
            {
                mediaService.Stop();
                // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not put play
                IsPlaying = false;
            }
            else
            {
                if (Language.SupportInstrumentalMusic)
                    mediaService.Play(Language.GetInstrumentURL(Hymn.Number));
                else
                    mediaService.Play(language.GetSungURL(Hymn.Number));

                // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not to put play from the first moment
                IsPlaying = true;
            }
        }

        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
        #endregion


    }
}
