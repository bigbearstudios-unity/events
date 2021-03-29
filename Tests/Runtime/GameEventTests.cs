using NUnit.Framework;

using BBUnity;

public class GameEventTests {

    [Test]
    public void Event_ShouldAllowConstructionWithACaller() {
        GameEvent e = new GameEvent(5);
        Assert.AreEqual(5, e.Caller);
    }

    [Test]
    public void Event_ShouldAllowConstructionWithACallerAndData() {
        GameEvent e = new GameEvent(5, 10);
        Assert.AreEqual(10, e.Data);
    }

    [Test]
    public void HasCaller_ShouldReturnFalseWithNoCaller() {
        Assert.IsFalse(new GameEvent().HasCaller);
    }

    [Test]
    public void HasCaller_ShouldReturnTrueWithACaller() {
        GameEvent e = new GameEvent(10);
        Assert.IsTrue(e.HasCaller);
    }

    [Test]
    public void HasData_ShouldReturnFalseWithNoData() {
        Assert.IsFalse(new GameEvent().HasData);
    }

    [Test]
    public void HasData_ShouldReturnTrueWithData() {
        Assert.IsTrue(new GameEvent(caller: null, data: "test").HasData);
    }

    [Test]
    public void Name_ShouldReturnTheTypeName_WhenConstructedWithNoName() {
        Assert.AreEqual("Event", new GameEvent().Name);
    }

    [Test]
    public void Name_ShouldReturnTheConstructedName() {
        Assert.AreEqual("Test", new GameEvent("Test").Name);
    }
}