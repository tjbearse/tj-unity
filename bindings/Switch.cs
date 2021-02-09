using System;
using System.Collections.Generic;
using System.Linq;

namespace TJ.Bindings {

	public interface ISwitch<ValueType> {
		void Switch(ValueType v);
	}

	class SwitchCollection<T> : ISwitch<T> {
		List<ISwitch<T>> switches;
		public SwitchCollection(IEnumerable<ISwitch<T>> _switches) {
			switches = _switches.ToList();
		}
		public SwitchCollection() {
			switches = new List<ISwitch<T>>();
		}

		public void Add(ISwitch<T> s) => switches.Add(s);

		public void Switch(T t) {
			foreach (var s in switches) {
				s.Switch(t);
			}
		}
	}
}
