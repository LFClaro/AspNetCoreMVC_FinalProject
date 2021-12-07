using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Areas.Admin.Models
{
    public class Validate
    {
        private const string EmailKey = "validEmail";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
