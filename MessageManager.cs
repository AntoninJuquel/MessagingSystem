using System.Collections.Generic;

namespace MessagingSystem
{
    public class MessageManager
    {
        public static MessageManager Instance
        {
            get { return _instance ??= new MessageManager(); }
        }

        private static MessageManager _instance;

        public delegate void MessageDelegate<in T>(T e) where T : Message;

        private delegate void MessageDelegate(Message e);

        /// <summary>
        /// The actual delegate, there is one delegate per unique event. Each
        /// delegate has multiple invocation list items.
        /// </summary>
        private readonly Dictionary<System.Type, MessageDelegate> _delegates = new Dictionary<System.Type, MessageDelegate>();

        /// <summary>
        /// Lookups only, there is one delegate lookup per listener
        /// </summary>
        private readonly Dictionary<System.Delegate, MessageDelegate> _delegateLookup = new Dictionary<System.Delegate, MessageDelegate>();

        /// <summary>
        /// Add the delegate.
        /// </summary>
        public void Subscribe<T>(MessageDelegate<T> del) where T : Message
        {
            if (_delegateLookup.ContainsKey(del))
            {
                return;
            }

            // Create a new non-generic delegate which calls our generic one.  This
            // is the delegate we actually invoke.
            void InternalDelegate(Message e) => del((T) e);
            _delegateLookup[del] = InternalDelegate;

            if (_delegates.TryGetValue(typeof(T), out var tempDel))
            {
                _delegates[typeof(T)] = tempDel + InternalDelegate;
            }
            else
            {
                _delegates[typeof(T)] = InternalDelegate;
            }
        }

        /// <summary>
        /// Remove the delegate. Can be called multiple times on same delegate.
        /// </summary>
        public void Unsubscribe<T>(MessageDelegate<T> del) where T : Message
        {
            if (!_delegateLookup.TryGetValue(del, out var internalDelegate)) return;
            if (_delegates.TryGetValue(typeof(T), out var tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    _delegates.Remove(typeof(T));
                }
                else
                {
                    _delegates[typeof(T)] = tempDel;
                }
            }

            _delegateLookup.Remove(del);
        }

        /// <summary>
        /// The count of delegate lookups. The delegate lookups will increase by
        /// one for each unique AddListener. Useful for debugging and not much else.
        /// </summary>
        public int DelegateLookupCount => _delegateLookup.Count;

        /// <summary>
        /// Send the message to all the listeners
        /// </summary>
        public void Send(Message e)
        {
            if (_delegates.TryGetValue(e.GetType(), out var del))
            {
                del.Invoke(e);
            }
        }
    }
}