using System;
using System.Collections.Generic;

namespace BBUnity {

    /// <summary>
    /// A simple event system
    /// The events or event system should not be persisted into a file or database due to the
    /// nature of generation of the UUIDs.
    /// </summary>
    public class GameEventSystem {
        /// <summary>
        /// GameEventDelegate, sets out the call signiture for recieving an Event
        /// </summary>
        public delegate void GameEventDelegate(GameEvent e);

        /// <summary>
        /// Reference to the global (shared) event system. 
        /// </summary>
        static private GameEventSystem __globalEventSystem = null;

        /// <summary>
        /// Internal Event Dictionary.
        /// </summary>
        private Dictionary<int, Dictionary<int, GameEventDelegate>> _events;

        public GameEventSystem() {
            _events = new Dictionary<int, Dictionary<int, GameEventDelegate>>();
        }

        static GameEventSystem() {
            __globalEventSystem = new GameEventSystem();
        }

        /*
         * Registration of Event Listeners
         */

        public void ListenFor(string eventName, GameEventDelegate onEvent) {
            AddInternalListener(Internal.Events.Utilities.HashCodeForEventName(eventName), onEvent);
        }

        public void ListenFor<T>(GameEventDelegate onEvent) where T : GameEvent {
            AddInternalListener(typeof(T), onEvent);
        }

        public void ListenFor(Type eventType, GameEventDelegate onEvent) {
            AddInternalListener(eventType, onEvent);
        }

        public void Add(string eventName, GameEventDelegate onEvent) {
            AddInternalListener(Internal.Events.Utilities.HashCodeForEventName(eventName), onEvent);
        }

        public void Add<T>(GameEventDelegate onEvent) where T : GameEvent {
            AddInternalListener(typeof(T), onEvent);
        }

        public void Add(Type eventType, GameEventDelegate onEvent) {
            AddInternalListener(eventType, onEvent);
        }

        private void AddInternalListener(Type eventType, GameEventDelegate onEvent) {
            AddInternalListener(Internal.Events.Utilities.HashCodeForEventType(eventType), onEvent);
        }

        private void AddInternalListener(int eventHashCode, GameEventDelegate onEvent) {
            int objectHashCode = Internal.Events.Utilities.HashCodeForGameEventDelegate(onEvent);

            if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameEventDelegate> events)) {
                events[objectHashCode] = onEvent;
            } else {
                _events[eventHashCode] = new Dictionary<int, GameEventDelegate>() { { objectHashCode, onEvent } };
            }
        }

        /*
         * Boardcasting / Calling of Events
         */

        public void Broadcast(string eventName) {
            SendInternalEvent(new GameEvent(eventName));
        }

        public void Broadcast(string eventName, object caller) {
            SendInternalEvent(new GameEvent(eventName, caller));
        }

        public void Broadcast(string eventName, object caller, object data) {
            SendInternalEvent(new GameEvent(eventName, caller, data));
        }

        public void Broadcast(GameEvent e) {
            SendInternalEvent(e);
        }

        public void Send(string eventName) {
            SendInternalEvent(new GameEvent(eventName));
        }

        public void Send(string eventName, object caller) {
            SendInternalEvent(new GameEvent(eventName, caller));
        }

        public void Send(string eventName, object caller, object data) {
            SendInternalEvent(new GameEvent(eventName, caller, data));
        }

        public void Send(GameEvent e) {
           SendInternalEvent(e);
        }

        private void SendInternalEvent(GameEvent e) {
            int eventHashCode = Internal.Events.Utilities.HashCodeForEvent(e);
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameEventDelegate> value)) {
                foreach(GameEventDelegate del in value.Values) {
                    del(e);
                }
            }
        }

        /*
         * Removal of a single listener
         */

        public void Remove(object listener) {
            int objectHashCode = listener.GetHashCode();
            foreach(Dictionary<int, GameEventDelegate> events in _events.Values) {
                if(events.ContainsKey(objectHashCode)) {
                    events.Remove(objectHashCode);
                }
            }
        }

        public void Remove(string eventName, object listener) {
            RemoveListener(Internal.Events.Utilities.HashCodeForEventName(eventName), listener);
        }
        
        public void Remove<T>(object listener) where T : GameEvent {
            RemoveListener(Internal.Events.Utilities.HashCodeForEventType(typeof(T)), listener);
        }

        public void Remove(Type eventType, object listener) {
            RemoveListener(Internal.Events.Utilities.HashCodeForEventType(eventType), listener);
        }

        private void RemoveListener(int eventHashCode, object listener) {
            int objectHashCode = listener.GetHashCode();
            if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameEventDelegate> events)) {
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

        public void Remove<T>() where T : GameEvent {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventType(typeof(T)), false);
        }

        public void Remove(Type eventType) {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventType(eventType), false);
        }

        public void Remove(string eventName, bool preserveLookups) {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventName(eventName), preserveLookups);
        }

        public void Remove<T>(bool preserveLookups) where T : GameEvent {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventType(typeof(T)), preserveLookups);
        }

        public void Remove(Type eventType, bool preserveLookups) {
            RemoveListeners(Internal.Events.Utilities.HashCodeForEventType(eventType), preserveLookups);
        }

        private void RemoveListeners(int eventHashCode, bool preserveLookups) {
            if(preserveLookups) { //Remove the values from within the lookup, but not the lookup itself
                if(_events.TryGetValue(eventHashCode, out Dictionary<int, GameEventDelegate> events)) {
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
                foreach(Dictionary<int, GameEventDelegate> events in _events.Values) {
                    events.Clear();
                }
            } else {
                Clear();
            }
        }

        /*
         * Static Methods
         */

        static public GameEventSystem Global {
            get { return __globalEventSystem; }
        }
    }
}