using System;

using static BBUnity.Events.EventSystem;

namespace BBUnity.Events {
    public static class GlobalEventSystem {

        private static EventSystem __globalEventSystem;

        static GlobalEventSystem() {
            __globalEventSystem = new EventSystem();
        }

        public static void ListenFor(string eventName, EventDelegate onEvent) {
            __globalEventSystem.ListenFor(eventName, onEvent);
        }

        public static void ListenFor<T>(EventDelegate onEvent) where T : Event {
            __globalEventSystem.ListenFor<T>(onEvent);
        }

        public static void ListenFor(Type eventType, EventDelegate onEvent) {
            __globalEventSystem.ListenFor(eventType, onEvent);
        }

        public static void Add(string eventName, EventDelegate onEvent) {
            __globalEventSystem.Add(eventName, onEvent);
        }

        public static void Add<T>(EventDelegate onEvent) where T : Event {
            __globalEventSystem.Add<T>(onEvent);
        }

        public static void Add(Type eventType, EventDelegate onEvent) {
            __globalEventSystem.Add(eventType, onEvent);
        }

        /*
         * Boardcasting / Calling of Events
         */

        public static void Broadcast(string eventName) {
            __globalEventSystem.Broadcast(eventName);
        }

        public static void Broadcast(string eventName, object caller) {
            __globalEventSystem.Broadcast(eventName, caller);
        }

        public static void Broadcast(string eventName, object caller, object data) {
            __globalEventSystem.Broadcast(eventName, caller, data);
        }

        public static void Broadcast(Event e) {
            __globalEventSystem.Broadcast(e);
        }

        public static void Send(string eventName) {
            __globalEventSystem.Send(eventName);
        }

        public static void Send(string eventName, object caller) {
             __globalEventSystem.Send(eventName, caller);
        }

        public static void Send(string eventName, object caller, object data) {
            __globalEventSystem.Send(eventName, caller, data);
        }

        public static void Send(Event e) {
           __globalEventSystem.Send(e);
        }

        /*
         * Removal of a single listener
         */

        public static void Remove(object listener) {
            __globalEventSystem.Remove(listener);
        }

        public static void Remove(string eventName, object listener) {
            __globalEventSystem.Remove(eventName, listener);
        }
        
        public static void Remove<T>(object listener) where T : Event {
           __globalEventSystem.Remove<T>(listener);
        }

        public static void Remove(Type eventType, object listener) {
            __globalEventSystem.Remove(eventType, listener);
        }

        /*
         * Removing of listeners of a given Event Type
         */

        public static void Remove(string eventName) {
            __globalEventSystem.Remove(eventName);
        }

        public static void Remove<T>() where T : Event {
            __globalEventSystem.Remove<T>();
        }

        public static void Remove(Type eventType) {
            __globalEventSystem.Remove(eventType);
        }

        public static void Remove(string eventName, bool preserveLookups) {
            __globalEventSystem.Remove(eventName, preserveLookups);
        }

        public static void Remove<T>(bool preserveLookups) where T : Event {
            __globalEventSystem.Remove<T>(preserveLookups);
        }

        public static void Remove(Type eventType, bool preserveLookups) {
           __globalEventSystem.Remove(eventType, preserveLookups);
        }

        /*
         * Removing of all Listeners
         */

        public static void Clear() {
            __globalEventSystem.Clear();
        }

        public static void Clear(bool preserveLookups) {
            __globalEventSystem.Clear(preserveLookups);
        }
    }
}

