namespace MessagingSystem
{
    /// <summary>
    ///  Interface for event handlers
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Subscribe to events
        /// </summary>
        void SubscribeEvents();

        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        void UnsubscribeEvents();
    }
}