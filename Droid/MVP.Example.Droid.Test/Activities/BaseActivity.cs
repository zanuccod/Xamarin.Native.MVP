using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;

namespace MVP.Example.Droid.Test.Activities
{
    public abstract class BaseActivity
    {

        protected BaseActivity(IApp app, string pageTitle)
        {
            App = app;
            Title = pageTitle;
        }

        #endregion

        #region Properties

        public string Title { get; }
        protected IApp App { get; }

        #endregion

        #region Methods

        public virtual Task WaitForPageToLoad() => Task.FromResult(App.WaitForElement(Title));

        #endregion
    }
}
