using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Models;

namespace Common.ViewModels
{
    public class ItemsViewModel<T> : BaseViewModel, IDisposable
    {
        private readonly IDataStore<T> modelDataStore;

        public ObservableCollection<T> Items { get; private set; }
        public Command LoadItemsCommand { get; private set; }
        public Command AddItemCommand { get; private set; }
        public Command DeleteAllCommand { get; private set; }

        public ItemsViewModel(IDataStore<T> model)
        {
            modelDataStore = model;
            Init();
        }

        #region Public Methods

        public void Dispose()
        {
            modelDataStore.Dispose();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Items = new ObservableCollection<T>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            AddItemCommand = new Command<T>(async (T item) => await AddItem(item));
            DeleteAllCommand = new Command<T>(async (T item) => await DeleteAllItems());
        }

        private async Task LoadItems()
        {
            Items.Clear();
            (await modelDataStore.GetItemsAsync()).ForEach(x => Items.Add(x));
        }

        private async Task AddItem(T item)
        {
            await modelDataStore.AddItemAsync(item);
            Items.Add(item);
        }

        private async Task DeleteAllItems()
        {
            await modelDataStore.DeleteAllAsync();
            Items.Clear();
        }

        #endregion
    }
}
