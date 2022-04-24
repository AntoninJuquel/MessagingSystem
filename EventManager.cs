using System.Collections.Generic;

namespace MessagingSystem
{
    public class EventManager
    {
        public static EventManager Instance
        {
            get { return _instance ??= new EventManager(); }
        }

        private static EventManager _instance;

        public delegate void EventDelegate<in T>(T e) where T : Event;

        private delegate void EventDelegate(Event e);

        /// <summary>
        /// The actual delegate, there is one delegate per unique event. Each
        /// delegate has multiple invocation list items.
        /// </summary>
        private readonly Dictionary<System.Type, EventDelegate> _delegates = new Dictionary<System.Type, EventDelegate>();

        /// <summary>
        /// Lookups only, there is one delegate lookup per listener
        /// </summary>
        private readonly Dictionary<System.Delegate, EventDelegate> _delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        /// <summary>
        /// Add the delegate.
        /// </summary>
        public void AddListener<T>(EventDelegate<T> del) where T : Event
        {
            if (_delegateLookup.ContainsKey(del))
            {
                return;
            }

            // Create a new non-generic delegate which calls our generic one.  This
            // is the delegate we actually invoke.
            void InternalDelegate(Event e) => del((T) e);
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
        public void RemoveListener<T>(EventDelegate<T> del) where T : Event
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
        /// Raise the event to all the listeners
        /// </summary>
        public void Raise(Event e)
        {
            if (_delegates.TryGetValue(e.GetType(), out var del))
            {
                del.Invoke(e);
            }
        }
    }
}