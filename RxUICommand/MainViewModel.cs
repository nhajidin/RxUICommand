using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxUICommand
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel()
        {
            count = this.WhenAnyObservable(x => x.DoIt)
                .Scan(0, (acc, _) => acc + 1)
                .StartWith(0)
                .ToProperty(this, x => x.Count);

            DoIt = ReactiveCommand.Create(() => { });
        }

        public ReactiveCommand<Unit, Unit> DoIt { get; protected set; }

        ObservableAsPropertyHelper<int> count;
        public int Count
        {
            get { return count.Value; }
        }
    }
}
