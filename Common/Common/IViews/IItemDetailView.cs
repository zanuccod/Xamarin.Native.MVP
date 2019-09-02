using System;
using Common.Entities;

namespace Common.IViews
{
    public interface IItemDetailView
    {
        void PopulateViewValues(Student s);
        Student GetStudentViewValues();
        void CloseActivity();
        void HideSaveBtn();
        string GetDataFromParent();
    }
}
