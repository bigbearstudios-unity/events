using System;
using System.Collections.Generic;

namespace BBUnity.Events {

    /// <summary>
    /// A simple event system
    /// The events or event system should not be persisted into a file or database due to the
    /// nature of generation of the UUIDs
    /// </summary>
    public class EventSystem {
        public delegate void EventDelegate(Event e);

        private Dictionary<int, Dictionary<int, EventDelegate>> _events;

        public EventSystem() {
            _events = new Dictionary<int, Dictionary<int, EventDelegate>>();
        }

        /*
         * Registeration of Event Listeners
         */

        public void ListenFor(string eventName, EventDelegate onEvent) {
            AddInternalListener(Utilities.HashCodeForEventName(eventName), onEvent);
        }

        public void ListenFor<T>(EventDelegate onEvent) where T : Event {
            AddInternalListener(typeof(T), onEvent);
        }

        public void ListenFor(Type eventType, EventDelegate onEvent) {
            AddInternalListener(eventType, onEvent);
        }

        public void Add(string eventName, EventDelegate onEvent) {
            AddInternalListener(Utilities.HashCodeForEventName(eventName), onEvent);
        }

        public void Add<T>(EventDelegate onEvent) where T : Event {
            AddInternalListener(typeof(T), onEvent);
        }

        public void Add(Type eventType, EventDelegate onEvent) {
            AddInternalListener(eventType, onEvent);
        }

        private void AddInternalListener(Type eventType, EventDelegate onEvent) {
            AddInternalListener(Utilities.HashCodeForEventType(eventType), onEvent);
        }

        private void AddInternalListener(int eventHashCode, EventDelegate onEvent) {
            int objectHashCode = Utilities.HashCodeForEventDelegate(onEvent);

            if(_events.TryGetValue(eventHashCode, out Dictionary<int, EventDelegate> events)) {
                events[objectHashCode] = onEvent;
            } else {
                _events[eventHashCode] = new Dictionary<int, EventDelegate>() { { objectHashCode, onEvent } };
            }
        }

        /*
         * Boardcasting / Calling of Events
         */

        public void Broadcast(string eventName) {
            SendInternalEvent(new Event(eventName));
        }

        public void Broadcast(string eventName, object caller) {
            SendInternalEvent(new Event(eventName, caller));
        }

        public void Broadcast(string eventName, object caller, object data) {
            SendInternalEvent(new Event(eventName, caller, data));
        }

        public void Broadcast(Event e) {
            SendInternalEvent(e);
        }

        public void Send(string eventName) {
            SendInternalEvent(new Event(eventName));
        }

        public void Send(string eventName, object caller) {
            SendInternalEvent(new Event(eventName, caller));
        }

        public void Send(string eventName, object caller, object data) {
            SendInternalEvent(new Event(eventName, caller, data));
        }

        public void Send(Event e) {
           SendInternalEvent(e);
        }

        private void SendInternalEvent(Event e) {
            int eventHashCode = Utilities.HashCodeForEvent(e);
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, EventDelegate> value)) {
                foreach(EventDelegate del in value.Values) {
                    del(e);
                }
            }
        }

        /*
         * Removal of a single listener
         */

        public void Remove(object listener) {
            int objectHashCode = listener.GetHashCode();
            foreach(Dictionary<int, EventDelegate> events in _events.Values) {
                if(events.ContainsKey(objectHashCode)) {
                    events.Remove(objectHashCode);
                }
            }
        }

        public void Remove(string eventName, object listener) {
            RemoveListener(Utilities.HashCodeForEventName(eventName), listener);
        }
        
        public void Remove<T>(object listener) where T : Event {
            RemoveListener(Utilities.HashCodeForEventType(typeof(T)), listener);
        }

        public void Remove(Type eventType, object listener) {
            RemoveListener(Utilities.HashCodeForEventType(eventType), listener);
        }

        private void RemoveListener(int eventHashCode, object listener) {
            int objectHashCode = listener.GetHashCode();
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, EventDelegate> events)) {
                if(events.ContainsKey(objectHashCode)) {
                    events.Remove(objectHashCode);
                }
            }
        }

        /*
         * Removing of listeners of a given Event Type
         */

        public void Remove(string eventName) {
            RemoveListeners(Utilities.HashCodeForEventName(eventName), false);
        }

        public void Remove<T>() where T : Event {
            RemoveListeners(Utilities.HashCodeForEventType(typeof(T)), false);
        }

        public void Remove(Type eventType) {
            RemoveListeners(Utilities.HashCodeForEventType(eventType), false);
        }

        public void Remove(string eventName, bool preserveLookups) {
            RemoveListeners(Utilities.HashCodeForEventName(eventName), preserveLookups);
        }

        public void Remove<T>(bool preserveLookups) where T : Event {
            RemoveListeners(Utilities.HashCodeForEventType(typeof(T)), preserveLookups);
        }

        public void Remove(Type eventType, bool preserveLookups) {
            RemoveListeners(Utilities.HashCodeForEventType(eventType), preserveLookups);
        }

        private void RemoveListeners(int eventHashCode, bool preserveLookups) {
            if(preserveLookups) { //Remove the values from within the lookup, but not the lookup itself
                if(_events.TryGetValue(eventHashCode, out Dictionary<int, EventDelegate> events)) {
                    events.Clear();
                }   
            } else { //Removes all of the listeners lookups
                if(_events.ContainsKey(eventHashCode)) {
                    _events.Remove(eventHashCode);
                }
            }
        }

        /*
         * Removing of all Listeners
         */

        public void Clear() {
            _events.Clear();
        }

        public void Clear(bool preserveLookups) {
            if(preserveLookups) {
                foreach(Dictionary<int, EventDelegate> events in _events.Values) {
                    events.Clear();
                }
            } else {
                Clear();
            }
        }
    }
}