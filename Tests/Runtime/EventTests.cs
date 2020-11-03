using NUnit.Framework;

using BBUnity.Events;

public class EventTests {

    [Test]
    public void Event_ShouldAllowConstructionWithACaller() {
        Event e = new Event(5);
        Assert.AreEqual(5, e.Caller);
    }

    [Test]
    public void Event_ShouldAllowConstructionWithACallerAndData() {
        Event e = new Event(5, 10);
        Assert.AreEqual(10, e.Data);
    }

    [Test]
    public void HasCaller_ShouldReturnFalseWithNoCaller() {
        Assert.IsFalse(new Event().HasCaller);
    }

    [Test]
    public void HasCaller_ShouldReturnTrueWithACaller() {
        Event e = new Event(10);
        Assert.IsTrue(e.HasCaller);
    }

    [Test]
    public void HasData_ShouldReturnFalseWithNoData() {
        Assert.IsFalse(new Event().HasData);
    }

    [Test]
    public void HasData_ShouldReturnTrueWithData() {
        Assert.IsTrue(new Event(caller: null, data: "test").HasData);
    }

    [Test]
    public void Name_ShouldReturnTheTypeName_WhenConstructedWithNoName() {
        Assert.AreEqual("Event", new Event().Name);
    }

    [Test]
    public void Name_ShouldReturnTheConstructedName() {
        Assert.AreEqual("Test", new Event("Test").Name);
    }
}