using System;
using NUnit.Framework;

using BBUnity;

namespace TestSupport {
    public class TestGameEvent : GameEvent {}

    public class EventAssert {
        private bool _called = false;

        public void OnEvent(GameEvent e) {
            _called = true;
        }

        public void OnEvent(string eventName, object caller, object data) {
            _called = true;
        }

        public static void AssertCalled(Action<EventAssert> func) {
            EventAssert caller = new EventAssert();
            func(caller);
            Assert.IsTrue(caller._called);
        }

        public static void AssertNotCalled(Action<EventAssert> func) {
            EventAssert caller = new EventAssert();
            func(caller);
            Assert.IsFalse(caller._called);
        }

        public static void AssertFirstCalled(Action<EventAssert, EventAssert> func) {
            EventAssert caller = new EventAssert();
            EventAssert caller2 = new EventAssert();
            func(caller, caller2);
            Assert.IsTrue(caller._called);
            Assert.IsFalse(caller2._called);
        }
    }
}


