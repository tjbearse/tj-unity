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

	class UpdatingBindingCollection : IUpdatingBinding {
		BindingCollection bindings;
		UpdatingCollection updatings;
		public UpdatingBindingCollection(IEnumerable<IUpdatingBinding> b) {
			bindings = new BindingCollection(b);
			updatings = new UpdatingCollection(b);
		}

		public UpdatingBindingCollection() {
			bindings = new BindingCollection();
			updatings = new UpdatingCollection();
		}

		public void Add(IUpdatingBinding b) {
			bindings.Add(b);
			updatings.Add(b);
		}
		public void Add(IBinding b) => bindings.Add(b);
		public void Add(IUpdating b) => updatings.Add(b);

		public void Bind() => bindings.Bind();
		public void Unbind() => bindings.Unbind();
		public void Update() => updatings.Update();
	}
}
