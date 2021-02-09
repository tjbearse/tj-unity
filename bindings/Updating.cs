using System;
using System.Collections.Generic;
using System.Linq;

namespace TJ.Bindings {

	public interface IUpdating {
		void Update();
	}

	public interface IUpdatingBinding : IUpdating, IBinding {}

	class UpdatingCollection : IUpdating {
		List<IUpdating> bindings;
		public UpdatingCollection(IEnumerable<IUpdating> _bindings) {
			bindings = _bindings.ToList();
		}
		public UpdatingCollection() {
			bindings = new List<IUpdating>();
		}

		public void Add(IUpdating u) => bindings.Add(u);

		// IDEA: better exception handling in foreaches?
		public void Update() {
			foreach(var b in bindings) {
				b.Update();
			}
		}
	}
}
