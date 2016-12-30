using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RxUICommand.Test
{
	public class WhenAnyObservableTest
	{
		public class TestViewModel : ReactiveObject
		{
			public TestViewModel()
			{
				var obs = this.WhenAnyObservable(x => x.IncrementCount)
					.Scan(0, (acc, _) => acc + 1)
					.StartWith(0);


				countBefore = obs
					.ToProperty(this, x => x.CountBefore);

				IncrementCount = ReactiveCommand.Create(() => Unit.Default);

				countAfter = obs
					.ToProperty(this, x => x.CountAfter);
			}

			public ReactiveCommand<Unit, Unit> IncrementCount { get; protected set; }

			ObservableAsPropertyHelper<int> countBefore;
			public int CountBefore => countBefore.Value;

			ObservableAsPropertyHelper<int> countAfter;
			public int CountAfter => countAfter.Value;		
		}

		[Fact]
		public async void CreateObservableUsingWhenAnyObservableBeforeReactiveCommandIsCreated()
		{
			var vm = new TestViewModel();

			Assert.Equal(0, vm.CountAfter);
			Assert.Equal(0, vm.CountBefore);

			await vm.IncrementCount.Execute();

			Assert.Equal(1, vm.CountAfter);
			Assert.Equal(1, vm.CountBefore);
		}
	}
}
