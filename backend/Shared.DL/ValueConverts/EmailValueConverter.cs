using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Core.ValueObjects;

namespace Shared.DL.ValueConverts
{
    public class EmailValueConverter : ValueConverter<Email, string>
    {
        public EmailValueConverter()
            : base(
                email => email.Value,
                emailString => CreateEmailFrom(emailString))
        {

        }

        private static Email CreateEmailFrom(string value)
        {
            var result = Email.CreateFrom(value);

            if(!result.IsSuccess)
            {
                throw new ArgumentException(result.ComposedErrorMessage);
            }

            return result.Model;
        }
    }
}
