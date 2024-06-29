namespace Ground.Core.Contracts.Data.Commands
{
    /// <summary>
    /// If we want to work with mutliple aggregateRoots.
    /// https://martinfowler.com/eaaCatalog/unitOfWork.html
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// use this method to start transactions manually.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// use this method to commit when the transaction manually created.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// When an error occured.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// To commit transaction which is automaticlly created by the system.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// To commit transaction which is automaticlly created by the system.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
