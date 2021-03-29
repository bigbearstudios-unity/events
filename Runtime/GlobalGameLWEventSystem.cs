using static BBUnity.GameLWEventSystem;

namespace BBUnity {
    public class GlobalGameLWEventSystem {

        private static GameLWEventSystem _globalGameLwEventSystem;

        static GlobalGameLWEventSystem() {
            _globalGameLwEventSystem = new GameLWEventSystem();
        }


        public void ListenFor(string eventName, LWEventDelegate onEvent) {
            _globalGameLwEventSystem.ListenFor(eventName, onEvent);
        }

        public void Add(string eventName, LWEventDelegate onEvent) {
           _globalGameLwEventSystem.Add(eventName, onEvent);
        }

        /*
         * Boardcasting / Calling of Events
         */

        public void Broadcast(string eventName) {
            _globalGameLwEventSystem.Broadcast(eventName);
        }

        public void Broadcast(string eventName, object caller) {
            _globalGameLwEventSystem.Broadcast(eventName, caller);
        }

        public void Broadcast(string eventName, object caller, object data) {
            _globalGameLwEventSystem.Broadcast(eventName, caller, data);
        }

        public void Send(string eventName) {
            _globalGameLwEventSystem.Send(eventName);
        }

        public void Send(string eventName, object caller) {
             _globalGameLwEventSystem.Send(eventName, caller);
        }

        public void Send(string eventName, object caller, object data) {
            _globalGameLwEventSystem.Send(eventName, caller, data);
        }

        /*
         * Removal of a single listener
         */

        public void Remove(object listener) {
            _globalGameLwEventSystem.Remove(listener);
        }

        public void Remove(string eventName, object listener) {
           _globalGameLwEventSystem.Remove(eventName, listener);
        }

        /*
         * Removing of listeners of a given Event Type
         */

        public void Remove(string eventName) {
           _globalGameLwEventSystem.Remove(eventName);
        }

        public void Remove(string eventName, bool preserveLookups) {
            _globalGameLwEventSystem.Remove(eventName, preserveLookups);
        }

        /*
         * Removing of all Listeners
         */

        public void Clear() {
            _globalGameLwEventSystem.Clear();
        }

        public void Clear(bool preserveLookups) {
            _globalGameLwEventSystem.Clear(preserveLookups);
        }
    }
}