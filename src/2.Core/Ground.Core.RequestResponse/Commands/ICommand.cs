using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Core.RequestResponse.Commands
{
    /// <summary>
    /// a marker to classes that have commands' inputs.
    /// Regarding CQRS pattern, a command shouldn't have return value.
    /// </summary>
    public interface ICommand { }
    /// <summary>
    /// a marker for classes that have commands' inputs.
    /// somewhere we need a return value in CQRS! although we can handle it by raising an event, it takes time.
    /// </summary>
    /// <typeparam name="TData">indicates return type</typeparam>
    public interface ICommand<TData> { }

}
