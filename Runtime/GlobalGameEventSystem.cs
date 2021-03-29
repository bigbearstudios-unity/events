using System;

using static BBUnity.GameEventSystem;

namespace BBUnity {
    public static class GlobalGameEventSystem {

        private static GameEventSystem _globalGameEventSystem;

        static GlobalGameEventSystem() {
            _globalGameEventSystem = new GameEventSystem();
        }

        public static void ListenFor(string eventName, EventDelegate onEvent) {
            _globalGameEventSystem.ListenFor(eventName, onEvent);
        }

        public static void ListenFor<T>(EventDelegate onEvent) where T : GameEvent {
            _globalGameEventSystem.ListenFor<T>(onEvent);
        }

        public static void ListenFor(Type eventType, EventDelegate onEvent) {
            _globalGameEventSystem.ListenFor(eventType, onEvent);
        }

        public static void Add(string eventName, EventDelegate onEvent) {
            _globalGameEventSystem.Add(eventName, onEvent);
        }

        public static void Add<T>(EventDelegate onEvent) where T : GameEvent {
            _globalGameEventSystem.Add<T>(onEvent);
        }

        public static void Add(Type eventType, EventDelegate onEvent) {
            _globalGameEventSystem.Add(eventType, onEvent);
        }

        /*
         * Boardcasting / Calling of Events
         */

        public static void Broadcast(string eventName) {
            _globalGameEventSystem.Broadcast(eventName);
        }

        public static void Broadcast(string eventName, object caller) {
            _globalGameEventSystem.Broadcast(eventName, caller);
        }

        public static void Broadcast(string eventName, object caller, object data) {
            _globalGameEventSystem.Broadcast(eventName, caller, data);
        }

        public static void Broadcast(GameEvent e) {
            _globalGameEventSystem.Broadcast(e);
        }

        public static void Send(string eventName) {
            _globalGameEventSystem.Send(eventName);
        }

        public static void Send(string eventName, object caller) {
             _globalGameEventSystem.Send(eventName, caller);
        }

        public static void Send(string eventName, object caller, object data) {
            _globalGameEventSystem.Send(eventName, caller, data);
        }

        public static void Send(GameEvent e) {
           _globalGameEventSystem.Send(e);
        }

        /*
         * Removal of a single listener
         */

        public static void Remove(object listener) {
            _globalGameEventSystem.Remove(listener);
        }

        public static void Remove(string eventName, object listener) {
            _globalGameEventSystem.Remove(eventName, listener);
        }
        
        public static void Remove<T>(object listener) where T : GameEvent {
           _globalGameEventSystem.Remove<T>(listener);
        }

        public static void Remove(Type eventType, object listener) {
            _globalGameEventSystem.Remove(eventType, listener);
        }

        /*
         * Removing of listeners of a given Event Type
         */

        public static void Remove(string eventName) {
            _globalGameEventSystem.Remove(eventName);
        }

        public static void Remove<T>() where T : GameEvent {
            _globalGameEventSystem.Remove<T>();
        }

        public static void Remove(Type eventType) {
            _globalGameEventSystem.Remove(eventType);
        }

        public static void Remove(string eventName, bool preserveLookups) {
            _globalGameEventSystem.Remove(eventName, preserveLookups);
        }

        public static void Remove<T>(bool preserveLookups) where T : GameEvent {
            _globalGameEventSystem.Remove<T>(preserveLookups);
        }

        public static void Remove(Type eventType, bool preserveLookups) {
           _globalGameEventSystem.Remove(eventType, preserveLookups);
        }

        /*
         * Removing of all Listeners
         */

        public static void Clear() {
            _globalGameEventSystem.Clear();
        }

        public static void Clear(bool preserveLookups) {
            _globalGameEventSystem.Clear(preserveLookups);
        }
    }
}

