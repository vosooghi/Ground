namespace Ground.Core.Contracts.Data.Commands
{
    /// <summary>
    /// If we want to work with mutliple aggregateRoots.
    /// https://martinfowler.com/eaaCatalog/unitOfWork.html
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// در صورت نیاز به کنترل تراکنش‌ها از این متد جهت شروع تراکنش استفاده می‌شود.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// در صورت کنترل دستی تراکنش از این متد جهت پایان موفقیت آمیز تراکنش استفاده می‌شود.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// در صورت بروز خطا در فرایند‌ها از این متد جهت بازگشت تغییرات استفاده می‌شود.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// برای تایید تراکنشی که اتوماتیک توسط سیستم ایجاد شده است از این متد استفاده می‌شود.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// برای تایید تراکنشی که اتوماتیک توسط سیستم ایجاد شده است از این متد استفاده می‌شود.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
