using NUnit.Framework;
using TestSupport;

using System;
using BBUnity.Events;

public class EventSystemTests {

    [Test]
    public void EventSystem_ShouldAllowRegisteringOfEventName() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertCalled((EventAssert helper) => {
                e.ListenFor("TestEvent", helper.OnEvent);
                e.Broadcast("TestEvent");
            });
        });
    }

    [Test]
    public void EventSystem_WhenRegisteredWithAnEvent_ShouldRecieveCallback() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertCalled((EventAssert helper) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.Broadcast(new Event());
            });
        });
    }

    [Test]
    public void EventSystem_WhenRegisteredWithMultipleEvents_ShouldOnlyCallMatchingEvents() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.ListenFor<TestEvent>(helper2.OnEvent);

                e.Broadcast(new Event());
            });
        });
    }

    [Test]
    public void Clear_ShouldRemoveAllListeners() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.Clear();

                e.Broadcast(new Event());
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveAllListeners_ForAGivenEvent() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.ListenFor<TestEvent>(helper2.OnEvent);

                e.Remove<TestEvent>();

                e.Broadcast(new Event());
                e.Broadcast(new TestEvent());
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveListenerForObject() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.Remove(helper);

                e.Broadcast(new Event());
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveListenerForEvent() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.Remove<Event>(helper);

                e.Broadcast(new Event());
            });
        });
    }

    [Test]
    public void Remove_WhenRegisteredWithMutlipleEvents_ShouldOnlyClearMatchingEvents() {
        CreateEventSystem((EventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor<Event>(helper.OnEvent);
                e.ListenFor<TestEvent>(helper2.OnEvent);

                e.Remove<TestEvent>(helper2);

                e.Broadcast(new Event());
                e.Broadcast(new TestEvent());
            });
        });
    }

    private void CreateEventSystem(Action<EventSystem> func) {
        EventSystem eventSystem = new EventSystem();
        func(eventSystem);
    }
}
