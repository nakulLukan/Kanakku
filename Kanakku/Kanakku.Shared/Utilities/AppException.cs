using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanakku.Shared.Utilities
{
    public class AppException : Exception
    {
        public string ErrorMessage { get; }

        public AppException(string errorMessage) : base(errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
