using System;
using System.Collections.Generic;

namespace BBUnity.Events {

    /// <summary>
    /// A lightweight version of the EventSystem. This doesn't allocate any extra data
    /// via the Event wrapper and just forwards the caller / data along. 
    /// </summary>
    public class LWEventSystem {
        public delegate void LWEventDelegate(string eventName, object caller, object data);

        private Dictionary<int, Dictionary<int, LWEventDelegate>> _events;

        public LWEventSystem() {
            _events = new Dictionary<int, Dictionary<int, LWEventDelegate>>();
        }

        /*
         * Registeration of Event Listeners
         */

        public void ListenFor(string eventName, LWEventDelegate onEvent) {
            AddInternalListener(Utilities.HashCodeForEventName(eventName), onEvent);
        }

        public void Add(string eventName, LWEventDelegate onEvent) {
            AddInternalListener(Utilities.HashCodeForEventName(eventName), onEvent);
        }

        private void AddInternalListener(int eventHashCode, LWEventDelegate onEvent) {
            int objectHashCode = Utilities.HashCodeForLWEventDelegate(onEvent);

            if(_events.TryGetValue(eventHashCode, out Dictionary<int, LWEventDelegate> events)) {
                events[objectHashCode] = onEvent;
            } else {
                _events[eventHashCode] = new Dictionary<int, LWEventDelegate>() { { objectHashCode, onEvent } };
            }
        }

        /*
         * Boardcasting / Calling of Events
         */

        public void Broadcast(string eventName) {
            SendInternalEvent(eventName);
        }

        public void Broadcast(string eventName, object caller) {
            SendInternalEvent(eventName, caller);
        }

        public void Broadcast(string eventName, object caller, object data) {
            SendInternalEvent(eventName, caller, data);
        }

        public void Send(string eventName) {
            SendInternalEvent(eventName);
        }

        public void Send(string eventName, object caller) {
             SendInternalEvent(eventName, caller);
        }

        public void Send(string eventName, object caller, object data) {
            SendInternalEvent(eventName, caller, data);
        }

        private void SendInternalEvent(string eventName, object caller = null, object data = null) {
            int eventHashCode = Utilities.HashCodeForEventName(eventName);
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, LWEventDelegate> value)) {
                foreach(LWEventDelegate del in value.Values) {
                    del(eventName, caller, data);
                }
            }
        }

        /*
         * Removal of a single listener
         */

        public void Remove(object listener) {
            int objectHashCode = listener.GetHashCode();
            foreach(Dictionary<int, LWEventDelegate> events in _events.Values) {
                if(events.ContainsKey(objectHashCode)) {
                    events.Remove(objectHashCode);
                }
            }
        }

        public void Remove(string eventName, object listener) {
            RemoveListener(Utilities.HashCodeForEventName(eventName), listener);
        }

        private void RemoveListener(int eventHashCode, object listener) {
            int objectHashCode = listener.GetHashCode();
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, LWEventDelegate> events)) {
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

        public void Remove(string eventName, bool preserveLookups) {
            RemoveListeners(Utilities.HashCodeForEventName(eventName), preserveLookups);
        }

        private void RemoveListeners(int eventHashCode, bool preserveLookups) {
            if(preserveLookups) { //Remove the values from within the lookup, but not the lookup itself
                if(_events.TryGetValue(eventHashCode, out Dictionary<int, LWEventDelegate> events)) {
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
                foreach(Dictionary<int, LWEventDelegate> events in _events.Values) {
                    events.Clear();
                }
            } else {
                Clear();
            }
        }
    }
}
