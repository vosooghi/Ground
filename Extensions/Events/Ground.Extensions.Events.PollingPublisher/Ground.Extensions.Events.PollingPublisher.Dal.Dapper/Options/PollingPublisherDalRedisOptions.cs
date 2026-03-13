namespace Ground.Extensions.Events.PollingPublisher.Dal.Dapper.Options
{
    /// <summary>
    /// The configuration options for the Polling Publisher DAL
    /// </summary>
    public class PollingPublisherDalRedisOptions
    {
        /// <summary>
        /// The name of the application using the polling publisher.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// The connection string for the database.
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// The SQL command to select outbox event items for publishing.
        /// </summary>
        public string SelectCommand { get; set; } = "Select top (@Count) * from ground.OutBoxEventItems where IsProcessed = 0";
        /// <summary>
        /// The SQL command to update outbox event items as processed.
        /// </summary>
        public string UpdateCommand { get; set; } = "Update ground.OutBoxEventItems set IsProcessed = 1 where OutBoxEventItemId in @Ids";
    }
}
