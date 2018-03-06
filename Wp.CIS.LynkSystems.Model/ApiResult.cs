using System.Collections.Generic;

namespace Wp.CIS.LynkSystems.Model
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            ErrorMessages = new List<string>();

            WarningMessages = new List<string>();
        }

        public ICollection<string> ErrorMessages { get; private set; }

        public ICollection<string> WarningMessages { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return ErrorMessages.Count == 0 && WarningMessages.Count == 0;
            }
        }

        public T Result { get; set; }

        public void AddErrorMessage(string format, params object[] args)
        {
            ErrorMessages.Add(string.Format(format, args));
        }

        public void AddErrorMessage(string message)
        {
            ErrorMessages.Add(message);
        }

        public void CloneMessages<R>(ApiResult<R> source)
        {
            foreach (var item in source.ErrorMessages)
            {
                AddErrorMessage(item);
            }

            foreach (var warningitem in source.WarningMessages)
            {
                WarningMessages.Add(warningitem);
            }
        }

        public void AddWarningMessage(string format, params object[] args)
        {
            WarningMessages.Add(string.Format(format, args));
        }

        public void AddWarningMessage(string message)
        {
            WarningMessages.Add(message);
        }
    }
}