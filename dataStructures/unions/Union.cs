using System;

// inspired by f# and adapted from this SO question
// https://stackoverflow.com/questions/3151702/discriminated-union-in-c-sharp
namespace TJ.Unions {
	public abstract class Union2<A, B> {

		public abstract T Match<T>(Func<A, T> f, Func<B, T> g);
		public abstract void Match(Action<A> f, Action<B> g);

		// private ctor ensures no external classes can inherit
		private Union2() { } 

		public sealed class Case1 : Union2<A, B> {
			public readonly A Value;
			public Case1(A value) : base() { this.Value = value; }

			public override T Match<T>(Func<A, T> f, Func<B, T> g) =>
				f(Value);
			public override void Match(Action<A> f, Action<B> g) =>
				f(Value);
		}

		public sealed class Case2 : Union2<A, B> {
			public readonly B Value;
			public Case2(B value) { this.Value = value; }

			public override T Match<T>(Func<A, T> f, Func<B, T> g) =>
				g(Value);
			public override void Match(Action<A> f, Action<B> g) =>
				g(Value);
		}
	}

	public abstract class Union3<A, B, C> {

		public abstract T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h);
		public abstract void Match(Action<A> f, Action<B> g, Action<C> h);

		// private ctor ensures no external classes can inherit
		private Union3() { } 

		public sealed class Case1 : Union3<A, B, C> {
			public readonly A Value;
			public Case1(A value) : base() { this.Value = value; }

			public override T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h) =>
				f(Value);
			public override void Match(Action<A> f, Action<B> g, Action<C> h) =>
				f(Value);
		}

		public sealed class Case2 : Union3<A, B, C> {
			public readonly B Value;
			public Case2(B value) { this.Value = value; }

			public override T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h) =>
				g(Value);
			public override void Match(Action<A> f, Action<B> g, Action<C> h) =>
				g(Value);
		}

		public sealed class Case3 : Union3<A, B, C> {
			public readonly C Value;
			public Case3(C value) { this.Value = value; }

			public override T Match<T>(Func<A, T> f, Func<B, T> g, Func<C, T> h) =>
				h(Value);
			public override void Match(Action<A> f, Action<B> g, Action<C> h) =>
				h(Value);
		}
	}
}
