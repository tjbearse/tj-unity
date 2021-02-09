using System;
using System.Collections.Generic;
using System.Linq;

// IDEA: can this binding pattern be used more generally to simplify component enable / disable?
// e.g. on start up or enable as a mix in rather than base class
namespace TJ.Bindings {
	
	public interface IBinding {
		void Unbind();
		void Bind();
	}

	class BindingCollection : IBinding {
		List<IBinding> bindings;
		public BindingCollection(IEnumerable<IBinding> _bindings) {
			bindings = _bindings.ToList();
		}
		public BindingCollection() {
			bindings = new List<IBinding>();
		}

		public void Add(IBinding u) => bindings.Add(u);

		// IDEA: better exception handling in foreaches?
		public void Bind() {
			foreach(var b in bindings) {
				b.Bind();
			}
		}

		public void Unbind() {
			for(int i = bindings.Count() - 1; i >= 0; i--) {
				bindings[i].Unbind();
			}
		}
	}

}
