namespace Ground.Extensions.Events.Abstractions
{
    /// <summary>
    /// Defines methods for retrieving and updating outbox event items used in event publishing workflows.
    /// </summary>
    public interface IOutBoxEventItemRepository
    {
        /// <summary>
        /// Retrieves a list of outbox event items that are ready to be published, up to the specified maximum count.
        /// </summary>
        /// <param name="maxCount">The maximum number of outbox event items to retrieve. The default value is 100.</param>
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublish(int maxCount = 100);
        /// <summary>
        /// Marks the specified outbox event items as read.
        /// </summary>
        /// <param name="outBoxEventItems">A list of <see cref="OutBoxEventItem"/> instances to be marked as read. Cannot be null or contain null
        /// elements.</param>
        void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
    }
}
