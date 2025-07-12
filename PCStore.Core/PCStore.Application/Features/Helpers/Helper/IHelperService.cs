using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers.Helper
{
    public interface IHelperService
    {
        bool AttributeValueIsValid(string value, string dataType);
    }
}
