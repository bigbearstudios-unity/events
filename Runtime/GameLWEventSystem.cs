using System;
using System.Collections.Generic;

namespace BBUnity {

    /// <summary>
    /// A lightweight version of the EventSystem. This doesn't allocate any extra data
    /// via the Event wrapper and just forwards the caller / data along. 
    /// </summary>
    public class GameLWEventSystem {
        public delegate void GameLWEventDelegate(string eventName, object caller, object data);

        /// <summary>
        /// Reference to the global (shared) event system. 
        /// </summary>
        static private GameLWEventSystem __globalLWEventSystem = null;

        private Dictionary<int, Dictionary<int, GameLWEventDelegate>> _events;

        public GameLWEventSystem() {
            _events = new Dictionary<int, Dictionary<int, GameLWEventDelegate>>();
        }

        static GameLWEventSystem() {
            __globalLWEventSystem = new GameLWEventSystem();
        }

        /*
         * Registeration of Event Listeners
         */

        public void ListenFor(string eventName, GameLWEventDelegate onEvent) {
            AddInternalListener(Internal.Events.Utilities.HashCodeForEventName(eventName), onEvent);
        }

        public void Add(string eventName, GameLWEventDelegate onEvent) {
            AddInternalListener(Internal.Events.Utilities.HashCodeForEventName(eventName), onEvent);
        }

        private void AddInternalListener(int eventHashCode, GameLWEventDelegate onEvent) {
            int objectHashCode = Internal.Events.Utilities.HashCodeForGameLWEventDelegate(onEvent);

            if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameLWEventDelegate> events)) {
                events[objectHashCode] = onEvent;
            } else {
                _events[eventHashCode] = new Dictionary<int, GameLWEventDelegate>() { { objectHashCode, onEvent } };
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
            int eventHashCode = Internal.Events.Utilities.HashCodeForEventName(eventName);
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameLWEventDelegate> value)) {
                foreach(GameLWEventDelegate del in value.Values) {
                    del(eventName, caller, data);
                }
            }
        }

        /*
         * Removal of a single listener
         */

        public void Remove(object listener) {
            int objectHashCode = listener.GetHashCode();
            foreach(Dictionary<int, GameLWEventDelegate> events in _events.Values) {
                if(events.ContainsKey(objectHashCode)) {
                    events.Remove(objectHashCode);
                }
            }
        }

        public void Remove(string eventName, object listener) {
            RemoveListener(Internal.Events.Utilities.HashCodeForEventName(eventName), listener);
        }

        private void RemoveListener(int eventHashCode, object listener) {
            int objectHashCode = listener.GetHashCode();
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameLWEventDelegate> events)) {
                if(events.ContainsKey(objectHashCode)) {
                    events.Remove(objectHashCode);
                }
            }
        }

        /*
         * Removing of listeners of a given Event Type
         */

        public void Remove(string eventName) {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventName(eventName), false);
        }

        public void Remove(string eventName, bool preserveLookups) {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventName(eventName), preserveLookups);
        }

        private void RemoveListeners(int eventHashCode, bool preserveLookups) {
            if(preserveLookups) { //Remove the values from within the lookup, but not the lookup itself
                if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameLWEventDelegate> events)) {
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
                foreach(Dictionary<int, GameLWEventDelegate> events in _events.Values) {
                    events.Clear();
                }
            } else {
                Clear();
            }
        }

        static public GameLWEventSystem Global {
            get { return __globalLWEventSystem; }
        }
    }
}
