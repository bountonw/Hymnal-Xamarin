using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class FavoritesViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IDataStorageService dataStorageService;

        public MvxObservableCollection<FavoriteHymn> Hymns { get; set; } = new MvxObservableCollection<FavoriteHymn>();

        public Hymn SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedHymnExecute(value);
                RaisePropertyChanged(nameof(SelectedHymn));
            }
        }


        public FavoritesViewModel(IMvxNavigationService navigationService, IDataStorageService dataStorageService)
        {
            this.navigationService = navigationService;
            this.dataStorageService = dataStorageService;
        }

        public override Task Initialize()
        {
            Hymns.AddRange(dataStorageService.GetItems<FavoriteHymn>().OrderByDescending(h => h.SavedAt));
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            // Update list
            IOrderedEnumerable<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>().OrderByDescending(h => h.SavedAt);

            if (Hymns.Count() == 0)
            {
                Hymns.AddRange(favorites);
                return;
            }

            // Add new Hymns
            foreach (FavoriteHymn item in favorites.Where(h1 => Hymns.Where(h2 => h2.Number == h1.Number).Count() == 0))
            {
                var position = Hymns.Where(h => h.SavedAt > item.SavedAt).Count();
                Hymns.Insert(position, item);
            }

            // Remove no favorites hymns
            var removeList = new List<FavoriteHymn>();
            foreach (FavoriteHymn item in Hymns.Where(h1 => favorites.Where(h2 => h2.Number == h1.Number).Count() == 0))
            {
                removeList.Add(item);
            }
            foreach (FavoriteHymn item in removeList)
                Hymns.Remove(item);
        }

        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = hymn.Number });
        }
    }
}
