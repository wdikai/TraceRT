namespace TraceRT.DataAccess.Providers
{
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Streams;

    public class StorageProvider
    {
        private static readonly Lazy<StorageProvider> instance = new Lazy<StorageProvider>(() => new StorageProvider(), true);

        private StorageProvider() 
        {
        }

        public static StorageProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public async Task SaveAsync<TObject>(TObject saveObject, string path) 
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var writer = new DataWriter(stream))
                {
                    var json = JsonConvert.SerializeObject(saveObject);
                    writer.WriteString(json);
                    await writer.StoreAsync();
                }
            }
        }

        public async Task<TObject> LoadAsync<TObject>(string path)
        {
            var json = string.Empty;
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.CreateFileAsync(path, CreationCollisionOption.OpenIfExists);
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var reader = new DataReader(inputStream))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        json = reader.ReadString((uint)stream.Size);
                    }
                }
            }

            TObject result = JsonConvert.DeserializeObject<TObject>(json);
            return result;
        }
    }
}
