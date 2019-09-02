using System;
using System.Threading.Tasks;
using Common.Helper;
using Common.Helpers;
using Common.IViews;
using Common.Models;

namespace Common.Presenters
{
    public class ItemsDetailViewPresenter<T> : BaseViewPresenter, IDisposable
    {
        private readonly IDataStore<T> modelDataStore;
        private readonly IItemDetailView view;

        public Command AddItemCommand { get; private set; }

        public ItemsDetailViewPresenter(IItemDetailView view, IDataStore<T> model = null)
        {
            this.view = view;
            modelDataStore = model ?? ServiceLocator.Instance.Get<IDataStore<T>>();

            Init();
        }

        #region Public Methods

        public void Dispose()
        {
            modelDataStore.Dispose();
        }

        public void OnResume()
        {

        }

        #endregion

        #region Private Methods

        private void Init()
        {
            AddItemCommand = new Command<T>(async (T item) => await AddItem(item));
        }

        private async Task AddItem(T item)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
               await modelDataStore.AddItemAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
