using System;
using System.Threading.Tasks;
using Common.Entities;
using Common.Helper;
using Common.Helpers;
using Common.IViews;
using Common.Models;
using Newtonsoft.Json;

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

        }

        public void OnResume()
        {
            var itemFromParent = view.GetDataFromParent();

            if (itemFromParent != null)
                InitializeViewWithItem(itemFromParent);
        }

        public void AddNewItem()
        {
            var item = view.GetStudentViewValues();

            AddItemCommand.Execute(item);
            view.CloseActivity();
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

        private void InitializeViewWithItem(string itemStr = null)
        {
            var item = JsonConvert.DeserializeObject<Student>(itemStr);
            view.PopulateViewValues(item);
            view.HideSaveBtn();
        }

        #endregion
    }
}
