using System;
using SQLite;

namespace Presenter.Droid.Entities
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public long SerialNumber { get; set; }

        public string Name { get; set; }

        public string BornDate { get; set; }

        public string Country { get; set; }

        public bool Equals(Student item)
        {
            var result = true;
            result &= (SerialNumber == item.SerialNumber);
            result &= (Name.Equals(item.Name));
            result &= (BornDate.Equals(item.BornDate));
            result &= (Country.Equals(item.Country));
            return result;
        }
    }
}
