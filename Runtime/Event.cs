using System.Collections.Generic;

namespace BBUnity.Events {

    public class Event {

        /// <summary>
        /// The name of the event, this can be provided for matching or will fall
        /// back onto the Type name
        /// </summary>
        protected string _name;

        /// <summary>
        /// The caller of the event
        /// </summary>
        protected object _caller = null;

        /// <summary>
        /// The data which is associated with the event
        /// </summary>
        protected object _data = null;

        public object Caller { get { return _caller; } }
        public bool HasCaller { get { return Caller != null; } }

        public object Data { get { return _data; } }
        public bool HasData { get { return _data != null; } }

        public string Name { get { return _name; } }

        private string NameForType { get { return GetType().Name; } }

        public Event() {
            Construct(NameForType);
        }

        public Event(object caller) {
            Construct(NameForType, caller);
        }

        public Event(object caller, object data) {
            Construct(NameForType, caller, data);
        }

        public Event(string name) {
            Construct(name);
        }

        public Event(string name, object caller) {
            Construct(name, caller);
        }

        public Event(string name, object caller, object data) {
            Construct(name, caller, data);
        }

        private void Construct(string name, object caller = null, object data = null) {
            _name = name;
            _caller = caller;
            _data = data;
        }
    }
}