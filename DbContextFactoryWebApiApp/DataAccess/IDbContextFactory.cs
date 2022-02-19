namespace DbContextFactoryWebApiApp.DataAccess
{
    public interface IDbContextFactory
    {
        //string TenantName
        //{
        //    get; set;
        //}

        int? ServiceId
        {
            get; set;
        }

        CRMContext Create();
    }
}
