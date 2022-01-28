# BBUnity Events

A simple event system for Unity.

There are two event systems avalible through this library, the first is the EventSystem which is a fully featured and extendable event system, the second is the LWEventSystem which does no allocations of an Event object and instead just forwards the event name, caller and data directly through to the event delegate.

- [EventSystem](Documentation/EventSystem.md)
- [LightWeightEventSystem](Documentation/LWEventSystem.md)

## Namespaces

``` csharp

using BBUnity.Events;

```
  
### Basic Usage

``` csharp

//Define a new event
class MyEvent : Event {}
class MyEventHandler : MonoBehaviour {
  private void Start() {
    GameEventSystem.Global.ListenFor<MyEvent>(this.HandleEvent);
  }

  //This will be called when .Broadcast or .Send is called
  private HandleEvent() {

  }
}

class MyEventTrigger : MonoBehaviour {
  private void DoSomething() {
    GameEventSystem.Global.Broadcast(new MyEvent());
  }
}

```
