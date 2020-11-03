using static BBUnity.Events.LWEventSystem;

namespace BBUnity.Events {
    public class GlobalLWEventSystem {

        private static LWEventSystem __globalLWEventSystem;

        static GlobalLWEventSystem() {
            __globalLWEventSystem = new LWEventSystem();
        }


        public void ListenFor(string eventName, LWEventDelegate onEvent) {
            __globalLWEventSystem.ListenFor(eventName, onEvent);
        }

        public void Add(string eventName, LWEventDelegate onEvent) {
           __globalLWEventSystem.Add(eventName, onEvent);
        }

        /*
         * Boardcasting / Calling of Events
         */

        public void Broadcast(string eventName) {
            __globalLWEventSystem.Broadcast(eventName);
        }

        public void Broadcast(string eventName, object caller) {
            __globalLWEventSystem.Broadcast(eventName, caller);
        }

        public void Broadcast(string eventName, object caller, object data) {
            __globalLWEventSystem.Broadcast(eventName, caller, data);
        }

        public void Send(string eventName) {
            __globalLWEventSystem.Send(eventName);
        }

        public void Send(string eventName, object caller) {
             __globalLWEventSystem.Send(eventName, caller);
        }

        public void Send(string eventName, object caller, object data) {
            __globalLWEventSystem.Send(eventName, caller, data);
        }

        /*
         * Removal of a single listener
         */

        public void Remove(object listener) {
            __globalLWEventSystem.Remove(listener);
        }

        public void Remove(string eventName, object listener) {
           __globalLWEventSystem.Remove(eventName, listener);
        }

        /*
         * Removing of listeners of a given Event Type
         */

        public void Remove(string eventName) {
           __globalLWEventSystem.Remove(eventName);
        }

        public void Remove(string eventName, bool preserveLookups) {
            __globalLWEventSystem.Remove(eventName, preserveLookups);
        }

        /*
         * Removing of all Listeners
         */

        public void Clear() {
            __globalLWEventSystem.Clear();
        }

        public void Clear(bool preserveLookups) {
            __globalLWEventSystem.Clear(preserveLookups);
        }
    }
}