using System;
using System.Collections.Generic;
using System.Globalization;
using Hymnal.Core.Models;

namespace Hymnal.Core
{
    public static class Constants
    {
        public static CultureInfo CurrentCultureInfo { get; set; }

        /// <summary>
        /// Configuration items
        /// </summary>
        public static List<HymnalLanguage> HymnsLanguages = new List<HymnalLanguage>
        {
            // ENGLISH
            // Default language version - default english version
            new HymnalLanguage
            {
                Id = "en-newVersion",
                Name = "New Version 1985",
                Detail = "English",
                Year = 1985,
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "new-hymnal-en.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-en.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1985%20version/instrumental/###.mp3",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_en_###"
            },
            new HymnalLanguage
            {
                Id = "en-oldVersion",
                Name = "Old Version 1941",
                Detail = "English",
                Year = 1941,
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "old-hymnal-en.json",
                ThematicHymnsFileName = "old-hymnal-thematic-list-en.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1941%20version/instrumental/###.mp3"
            },

            // SPANISH
            // default spanish version
            new HymnalLanguage
            {
                Id = "es-newVersion",
                Name = "Nueva Versión 2009",
                Detail = "Español",
                Year = 2009,
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "new-hymnal-es.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-es.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/instrumental/###.mp3",
                SungMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/sung/###.mp3",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_es_###"
            },
            new HymnalLanguage
            {
                Id = "es-oldVersion",
                Name = "Antigua Versión 1962",
                Detail = "Español",
                Year = 1962,
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "old-hymnal-es.json",
                ThematicHymnsFileName = "old-hymnal-thematic-list-es.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/1962%20version/instrumental/###.mp3"
            },

            // PORTUGUESE
            // default portuguese version
            new HymnalLanguage
            {
                Id = "pt-newVersion",
                Name = "Nova versão 1996",
                Detail = "Português",
                Year = 1996,
                TwoLetterISOLanguageName = "pt",
                HymnsFileName = "new-hymnal-pt.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-pt.json",
                SungMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/portuguese/1996%20version/sung/###.mp3"
            },

            // RUSSIAN
            // default russian version
            new HymnalLanguage
            {
                Id = "ru-newVersion",
                Name = "Гимны Надежды 1997",
                Detail = "Русский",
                Year = 1997,
                TwoLetterISOLanguageName = "ru",
                HymnsFileName = "new-hymnal-ru.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-ru.json",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_ru_###",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/russian/1997%20version/instrumental/###.mp3"
            }
        };

        public struct WebLinks
        {
            /// <summary>
            /// Developer promotion Webiste
            /// </summary>
            public const string DeveloperWebSite = @"https://storage.googleapis.com/hymn-music/about/index.html";

            /// <summary>
            /// GitHub developing website
            /// </summary>
            public const string GitHubDevelopingLink = @"https://github.com/isax5/Hymnal-Xamarin";

            /// <summary>
            /// AppStore download Link
            /// </summary>
            public const string AppDownloadLinkIOS = @"https://apps.apple.com/cl/app/adventist-hymnal/id1153114394";

            /// <summary>
            /// PlayStore Download Link
            /// </summary>
            public const string AppDownloadLinkAndroid = @"https://play.google.com/store/apps/details?id=net.ddns.HimnarioAdventistaSPA";
        }

        /// <summary>
        /// App link scheme
        /// </summary>
        public struct AppLink
        {
            public const string Scheme = "adv";
            public const string Host = "hymnal";
            public const string UriBase = "adv://hymnal/";
        }

        /// <summary>
        /// Event key & scheme for AppCenter
        /// Time, Time Zone, iOS Version, App Version, App Build, Account Id (not using yet), AppNamespace, Device model, Country code, etc.
        /// https://docs.microsoft.com/en-us/appcenter/analytics/export#azure-blob-storage
        /// </summary>
        public struct TrackEv
        {
            /// <summary>
            /// App started
            /// Lenguage <see cref="CultureInfo"/> / Version of the <see cref="HymnalLanguage"/> configurated / Dark or Light Theme
            /// </summary>
            public const string AppOpened = "App Opened";
            public struct AppOpenedScheme
            {
                /// <summary>
                /// Use <see cref="CultureInfo.Name"/>
                /// </summary>
                public const string CultureInfo = "Culture Information";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";

                /// <summary>
                /// Use <see cref="Theme"/> <see cref="IAppInformationService"/>
                /// </summary>
                public const string ThemeConfigurated = "Theme Configurated";
            }

            /// <summary>
            /// Tabs, Search, History, Hymnal Opend from (Index [Numeric, alphabetic, thematic], number, favorites, history)
            /// </summary>
            public const string Navigation = "Navigation";
            public struct NavigationReferenceScheme
            {
                /// <summary>
                /// Use <see cref="nameof(Page)"/>
                /// </summary>
                public const string PageName = "Page Name";

                /// <summary>
                /// Use <see cref="CultureInfo.Name"/>
                /// </summary>
                public const string CultureInfo = "Culture Information";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";
            }

            /// <summary>
            /// Hymn reference for event tracking
            /// </summary>
            public struct HymnReferenceScheme
            {
                public const string Number = "Number";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";

                /// <summary>
                /// Use <see cref="CultureInfo.Name"/>
                /// </summary>
                public const string CultureInfo = "Culture Information";

                /// <summary>
                /// Use <see cref="DateTime.Now.ToLocalTime()"/>
                /// </summary>
                public const string Time = "Time";

                /// <summary>
                /// Use <see cref="InstrumentalMusic"/> and <see cref="SungMusic"/>
                /// </summary>
                public const string TypeOfMusicPlaying = "Type of Music Playing";

                /// <summary>
                /// Instrumental for: <see cref="TypeOfMusicPlaying"/>
                /// </summary>
                public const string InstrumentalMusic = "Instrumental";

                /// <summary>
                /// Sung for: <see cref="TypeOfMusicPlaying"/>
                /// </summary>
                public const string SungMusic = "Sung";
            }

            /// <summary>
            /// Hymn opened
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnOpened = "Hymn Opened";

            /// <summary>
            /// Hymn Music Sheet Opened
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnMusicSheetOpened = "Hymn Music Sheet Opened";

            /// <summary>
            /// Hymn added to favorites
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnAddedToFavorites = "Hymn Added To Favorites";

            /// <summary>
            /// Hymn remove favorites
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnRemoveFromFavorites = "Hymn Remove From Favorites";

            /// <summary>
            /// Music played
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnMusicPlayed = "Music Played";

            /// <summary>
            /// Hymn shared
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnShared = "Hymn Shared";

            /// <summary>
            /// Hymn founded through <see cref="ViewModels.SearchViewModel"/>
            /// </summary>
            public const string HymnFounded = "Hymn Founded";

            /// <summary>
            /// Hymn founded through <see cref="ViewModels.SearchViewModel"/>
            /// </summary>
            public struct HymnFoundedScheme
            {
                /// <summary>
                /// Query in the search field
                /// </summary>
                public const string Query = "Query";

                /// <summary>
                /// Number of the hymn founded
                /// </summary>
                public const string HymnFounded = "Hymn Founded";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";

            }
        }

        public const int MAXIMUM_RECORDS = 100;

        public const int DEFAULT_HYMNALS_FONT_SIZE = 18;
        public const int MINIMUM_HYMNALS_FONT_SIZE = 12;
        public const int MAXIMUM_HYMNALS_FONT_SIZE = 55;
    }
}
