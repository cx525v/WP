using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
namespace CIS.WebApi.UnitTests.Common
{
    public class MockStringLocalizer<T> : IStringLocalizer<T>
    {
        #region IStringLocalizer Implementation

        private LocalizedString[] range = new LocalizedString[5];
        public LocalizedString this[int indexrange]
        {
            get
            {
                return range[indexrange];
            }
            set
            {
                range[indexrange] = value;
            }
        }


        //public LocalizedString this[string name] =>  new LocalizedString("Test Key", "Test Localized String");

        public LocalizedString this[string name]
        {
            get
            {
                if (range != null && range.Length > 0)
                {
                    var list = range.ToList().Where(m => true == string.Equals(m?.Name, name));
                    if (null != list && list.Any())
                    {
                        return list.First();
                    }
                        
                }
               return new LocalizedString("Test Key", "Test Localized String");
            }
        }


        public LocalizedString this[string name, params object[] arguments] => new LocalizedString("Test Key", "Test Localized String");

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return new List<LocalizedString>();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new MockStringLocalizer<string>();
        }

        #endregion
    }
}
