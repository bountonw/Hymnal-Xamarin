using Realms;

namespace Plugin.StorageManager
{
    public static class BaseStorageManagerImplementationExtension
    {
        public static void Init(this IStorageManager storageManager, Realm instance)
        {
            var realmStorageManager = storageManager as BaseStorageManagerImplementation;
            realmStorageManager.RealmInstance = instance;
        }
    }
}
