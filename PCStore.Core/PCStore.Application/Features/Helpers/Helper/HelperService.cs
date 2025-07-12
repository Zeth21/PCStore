using PCStore.Application.Features.Helpers.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.Helpers
{
    public class HelperService : IHelperService
    {
        public bool AttributeValueIsValid(string value, string dataType)
        {
            return dataType.ToLower() switch
            {
                "int" => int.TryParse(value, out _),
                "float" => float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _),
                "bool" => bool.TryParse(value, out _),
                "string" => true,
                _ => false
            };
        }
    }
}
