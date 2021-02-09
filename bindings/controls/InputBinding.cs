using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace TJ.Bindings.Controls {

	abstract class InputBinding<ValueType> {
		protected InputAction input;
		protected Action<ValueType> action;

		public InputBinding (InputAction _input, Action<ValueType> _action) {
			// TODO can check value type of input matches?
			input = _input;
			action = _action;
		}
	}

	abstract class InputBinding {
		protected InputAction input;
		protected Action action;
		public InputBinding(InputAction _input, Action _action) {
			// TODO can check value type of input matches?
			input = _input;
			action = _action;
		}
	}

	class UpdatingInputBinding<ValueType>
		: InputBinding<ValueType>, IUpdating
		where ValueType : struct
	{

		public UpdatingInputBinding(InputAction i, Action<ValueType> a) : base(i, a) {}
		public void Update() {
			action?.Invoke(input.ReadValue<ValueType>());
		}
	}

	// is this a bool or a non-value action?
	class ButtonBinding : InputBinding, IBinding {

		public ButtonBinding(InputAction i, Action a) : base(i, a) {}

		public void Bind() {
			input.performed += TranslateToAction;
		}
		public void Unbind() {
			input.performed -= TranslateToAction;
		}

		void TranslateToAction(InputAction.CallbackContext cc) {
			if (cc.ReadValueAsButton()) {
				action?.Invoke();
			}
		}
	}

	class ButtonCanceledBinding : InputBinding, IBinding {

		public ButtonCanceledBinding(InputAction i, Action a) : base(i, a) {}

		public void Bind() {
			input.canceled += TranslateToAction;
		}
		public void Unbind() {
			input.canceled -= TranslateToAction;
		}

		void TranslateToAction(InputAction.CallbackContext cc) {
			action?.Invoke();
		}
	}

	class PerformedBinding<ValueType>
		: InputBinding<ValueType>, IBinding
		where ValueType : struct
	{
		public PerformedBinding(InputAction i, Action<ValueType> a) : base(i, a) {}

		public void Bind() {
			input.performed += TranslateToAction;
		}
		public void Unbind() {
			input.performed -= TranslateToAction;
		}

		void TranslateToAction(InputAction.CallbackContext cc) =>
			action?.Invoke(cc.ReadValue<ValueType>());
	}

	class CanceledBinding<ValueType>
		: InputBinding<ValueType>, IBinding
		where ValueType : struct
	{
		public CanceledBinding(InputAction i, Action<ValueType> a) : base(i, a) {}

		public void Bind() {
			input.canceled += TranslateToAction;
		}
		public void Unbind() {
			input.canceled -= TranslateToAction;
		}

		void TranslateToAction(InputAction.CallbackContext cc) =>
			action?.Invoke(cc.ReadValue<ValueType>());
	}

	class ActionMapBinding : IBinding {
		protected InputActionMap map;

		public ActionMapBinding(InputActionMap m) {
			map = m;
		}

		public void Bind() => map.Enable();
		public void Unbind() => map.Disable();
	}

}
