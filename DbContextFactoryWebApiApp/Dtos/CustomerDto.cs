using System;

namespace DbContextFactoryWebApiApp.Dtos
{
    public class CustomerDto
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTime RegistrationDate
        {
            get; set;
        }

        public string Website
        {
            get; set;
        }
    }
}
