using ObjectFlow.Core.Extensibility.Framework;

namespace ObjectFlow.Core.Extensibility.Interfaces
{
    public interface IAction<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        TOutput Run(TInput input, params StepOperation<TInput>[] extraOperations);
    }
}
