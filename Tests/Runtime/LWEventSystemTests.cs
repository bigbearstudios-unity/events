using NUnit.Framework;
using TestSupport;

using System;
using BBUnity.Events;

public class LWEventSystemTests {

    [Test]
    public void LWEventSystem_ShouldAllowRegisteringOfEventName() {
        CreateLWEventSystem((LWEventSystem e) => {
            EventAssert.AssertCalled((EventAssert helper) => {
                e.ListenFor("TestEvent", helper.OnEvent);
                e.Broadcast("TestEvent");
            });
        });
    }

    [Test]
    public void Clear_ShouldRemoveAllListeners() {
        CreateLWEventSystem((LWEventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor("Event", helper.OnEvent);
                e.Clear();

                e.Broadcast("Event");
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveAllListeners_ForAGivenEvent() {
        CreateLWEventSystem((LWEventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor("Event", helper.OnEvent);
                e.ListenFor("TestEvent", helper2.OnEvent);

                e.Remove("TestEvent");

                e.Broadcast("Event");
                e.Broadcast("TestEvent");
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveListenerForObject() {
        CreateLWEventSystem((LWEventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor("Event", helper.OnEvent);
                e.Remove(helper);

                e.Broadcast("Event");
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveListenerForEvent() {
        CreateLWEventSystem((LWEventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor("Event",helper.OnEvent);
                e.Remove("Event",helper);

                e.Broadcast("Event");
            });
        });
    }

    [Test]
    public void Remove_WhenRegisteredWithMutlipleEvents_ShouldOnlyClearMatchingEvents() {
        CreateLWEventSystem((LWEventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor("Event", helper.OnEvent);
                e.ListenFor("TestEvent", helper2.OnEvent);

                e.Remove("TestEvent", helper2);

                e.Broadcast("Event");
                e.Broadcast("TestEvent");
            });
        });
    } 

    private void CreateLWEventSystem(Action<LWEventSystem> func) {
        LWEventSystem eventSystem = new LWEventSystem();
        func(eventSystem);
    }
}
