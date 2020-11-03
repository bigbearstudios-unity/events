using System;

using static BBUnity.Events.EventSystem;
using static BBUnity.Events.LWEventSystem;

namespace BBUnity.Events {
    public class Utilities {
        public static int HashCodeForEventName(string eventName) {
            return eventName.GetHashCode();
        }

        public static int HashCodeForEvent(Event e) {
            return e.Name.GetHashCode();
        }

        public static int HashCodeForEventType(Type eventType) {
            return eventType.Name.GetHashCode();
        }

        public static int HashCodeForEventDelegate(EventDelegate onEvent) {
            return onEvent.Target.GetHashCode();
        }

        public static int HashCodeForLWEventDelegate(LWEventDelegate onEvent) {
            return onEvent.Target.GetHashCode();
        }
    }
}