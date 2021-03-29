using NUnit.Framework;
using TestSupport;

using System;
using BBUnity;

public class EventSystemTests {

    [Test]
    public void EventSystem_ShouldAllowRegisteringOfEventName() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertCalled((EventAssert helper) => {
                e.ListenFor("TestEvent", helper.OnEvent);
                e.Broadcast("TestEvent");
            });
        });
    }

    [Test]
    public void EventSystem_WhenRegisteredWithAnEvent_ShouldRecieveCallback() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertCalled((EventAssert helper) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.Broadcast(new GameEvent());
            });
        });
    }

    [Test]
    public void EventSystem_WhenRegisteredWithMultipleEvents_ShouldOnlyCallMatchingEvents() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.ListenFor<TestGameEvent>(helper2.OnEvent);

                e.Broadcast(new GameEvent());
            });
        });
    }

    [Test]
    public void Clear_ShouldRemoveAllListeners() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.Clear();

                e.Broadcast(new GameEvent());
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveAllListeners_ForAGivenEvent() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.ListenFor<TestGameEvent>(helper2.OnEvent);

                e.Remove<TestGameEvent>();

                e.Broadcast(new GameEvent());
                e.Broadcast(new TestGameEvent());
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveListenerForObject() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.Remove(helper);

                e.Broadcast(new GameEvent());
            });
        });
    }

    [Test]
    public void Remove_ShouldRemoveListenerForEvent() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertNotCalled((EventAssert helper) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.Remove<GameEvent>(helper);

                e.Broadcast(new GameEvent());
            });
        });
    }

    [Test]
    public void Remove_WhenRegisteredWithMutlipleEvents_ShouldOnlyClearMatchingEvents() {
        CreateEventSystem((GameEventSystem e) => {
            EventAssert.AssertFirstCalled((EventAssert helper, EventAssert helper2) => {
                e.ListenFor<GameEvent>(helper.OnEvent);
                e.ListenFor<TestGameEvent>(helper2.OnEvent);

                e.Remove<TestGameEvent>(helper2);

                e.Broadcast(new GameEvent());
                e.Broadcast(new TestGameEvent());
            });
        });
    }

    private void CreateEventSystem(Action<GameEventSystem> func) {
        GameEventSystem gameEventSystem = new GameEventSystem();
        func(gameEventSystem);
    }
}
