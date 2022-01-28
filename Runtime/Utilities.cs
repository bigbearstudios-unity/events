using System;

using static BBUnity.GameEventSystem;
using static BBUnity.GameLWEventSystem;

namespace BBUnity.Internal.Events {
    public class Utilities {
        public static int HashCodeForEventName(string eventName) {
            return eventName.GetHashCode();
        }

        public static int HashCodeForEvent(GameEvent e) {
            return e.Name.GetHashCode();
        }

        public static int HashCodeForEventType(Type eventType) {
            return eventType.Name.GetHashCode();
        }

        public static int HashCodeForGameEventDelegate(GameEventDelegate onEvent) {
            return onEvent.Target.GetHashCode();
        }

        public static int HashCodeForGameLWEventDelegate(GameLWEventDelegate onEvent) {
            return onEvent.Target.GetHashCode();
        }
    }
}