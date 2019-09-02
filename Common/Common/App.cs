using System;
using Common.Entities;
using Common.Helper;
using Common.Models;

namespace Common
{
    public class App
    {
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Student>, StudentSqLiteDataStore>();
        }
    }
}
