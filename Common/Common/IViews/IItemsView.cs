using System;
using Common.Entities;

namespace Common.IViews
{
    public interface IItemsView
    {
        void GoToDetailView(params string[] parameters);
    }
}
