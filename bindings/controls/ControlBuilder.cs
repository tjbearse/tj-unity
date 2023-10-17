using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace TJ.Bindings.Controls {
	
	public interface IBuilder {
		IInputAssetSyntax Asset(InputActionAsset asset);
	}

	public interface IInputAssetSyntax {
		IMapSyntax Map(string name);
	}

	public interface IMapSyntax {
		IActionSyntax Action(string name);
	}

	public interface IActionSyntax {
		IBuildable ExecuteOnPerformed(Action action);
		IBuildable ExecuteOnPerformed<ValueType>(Action<ValueType> action)
			where ValueType : struct;

		IBuildable ExecuteOnCanceled(Action action);
		IBuildable ExecuteOnCanceled<ValueType>(Action<ValueType> action)
			where ValueType : struct;

		IBuildable ExecuteOnUpdate<ValueType>(Action<ValueType> action)
			where ValueType : struct;
	}

	public interface IBuildable
		: IInputAssetSyntax, IMapSyntax, IActionSyntax
	{
		IUpdatingBinding Build();
	}

	public class ControlBuilder
		: IBuildable, IBuilder, IInputAssetSyntax, IMapSyntax, IActionSyntax
	{
		InputActionAsset currAsset;
		InputActionMap currMap;
		InputAction currAction;
		UpdatingBindingCollection bindings;

		ControlBuilder() {
			bindings = new UpdatingBindingCollection();
		}

		public static IBuilder Get() {
			return new ControlBuilder();
		}

		public IInputAssetSyntax Asset(InputActionAsset asset) {
			currAsset = asset;
			return this;
		}

		public IMapSyntax Map(string name) {
			currMap = currAsset.FindActionMap(name, true);
			bindings.Add(new ActionMapBinding(currMap));
			return this;
		}

		public IActionSyntax Action(string name) {
			currAction = currMap.FindAction(name, true);
			return this;
		}

		public IBuildable ExecuteOnPerformed<ValueType>(Action<ValueType> action)
			where ValueType : struct
		{
			var b = new PerformedBinding<ValueType>(currAction, action);
			bindings.Add(b);
			return this;
		}

		public IBuildable ExecuteOnPerformed(Action action) {
			var b = new ButtonBinding(currAction, action);
			bindings.Add(b);
			return this;
		}

		public IBuildable ExecuteOnCanceled<ValueType>(Action<ValueType> action)
			where ValueType : struct
		{
			var b = new CanceledBinding<ValueType>(currAction, action);
			bindings.Add(b);
			return this;
		}

		public IBuildable ExecuteOnCanceled(Action action) {
			var b = new ButtonCanceledBinding(currAction, action);
			bindings.Add(b);
			return this;
		}

		public IBuildable ExecuteOnUpdate<ValueType>(Action<ValueType> action)
			where ValueType : struct
		{
			var b = new UpdatingInputBinding<ValueType>(currAction, action);
			bindings.Add(b);
			return this;
		}

		public IUpdatingBinding Build() {
			var b = bindings;
			bindings = null;
			return b;
		}
	}
}
