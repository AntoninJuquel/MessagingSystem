namespace MessagingSystem
{
    /// <summary>
    ///  Interface for message handlers
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Subscribe to messages
        /// </summary>
        void SubscribeMessages();

        /// <summary>
        /// Unsubscribe from messages
        /// </summary>
        void UnsubscribeMessages();
    }
}