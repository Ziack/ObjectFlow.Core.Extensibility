using Newtonsoft.Json;
using ObjectFlow.Core.Extensibility.Exceptions;
using ObjectFlow.Core.Extensibility.Interfaces;
using Rainbow.ObjectFlow.Framework;
using Rainbow.ObjectFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFlow.Core.Extensibility.Framework
{
    public abstract class ActionBase<TInput, TOutput> : IAction<TInput, TOutput>, IDisposable
        where TInput : class
        where TOutput : class
    {
        #region Private Members

        private Workflow<TInput> _pipeline;

        private Context _context;

        private ISet<Exception> _rethrow;

        #endregion

        #region Events

        public event EventHandler<StepOperationCompleteEventArgs<TInput>> StepOperationCompleted;

        public event EventHandler<StepEventTrackedEventArgs<TInput>> StepOperationEvent;

        public event EventHandler<StepOperationCompleteEventArgs<TInput>> StepOperationInit;

        #endregion

        #region Abstract Methods
        protected abstract TOutput RealizarAccion(TInput input, Context context);

        protected abstract Context BuildContext(TInput message);

        protected abstract Workflow<TInput> BuildDefinition(TInput message);

        #endregion

        public TOutput Run(TInput entrada,  params StepOperation<TInput>[] extraOperations)
        {
            _rethrow = new HashSet<Exception>();

            try
            {
                _context = BuildContext(entrada);
                _pipeline = BuildDefinition(entrada);                
            }
            catch (Exception ex)
            {
                throw new InicializacionDeAccionException(string.Format("No se pudo inicializar la acción: '{0}'", ex.Message), ex);
            }

            _pipeline.Start(entrada);

            var result = RealizarAccion(entrada, _context);

            return result;
        }

        private void OnPipelineOperationInit(IOperation<TInput> operation, TInput context, Exception exception = null)
        {
            if (StepOperationInit != null)
            {
                var args = new StepOperationCompleteEventArgs<TInput>(message: context, handler: (StepOperation<TInput>)operation, context: _context, exception: exception);

                StepOperationInit(this, args);

                if (args.WithErrors && args.ErrorMustBeReThrow)
                    _rethrow.Add(exception);
            }
        }

        private void OnPipelineOperationExecuted(IOperation<TInput> operation, TInput context, Exception exception = null)
        {
            if (StepOperationCompleted != null)
            {
                var args = new StepOperationCompleteEventArgs<TInput>(message: context, handler: (StepOperation<TInput>)operation, context: _context, exception: exception);

                StepOperationCompleted(this, args);

                if (args.WithErrors && args.ErrorMustBeReThrow)
                    _rethrow.Add(exception);
            }
        }

        protected void OnPipelineEventTracked(IOperation<TInput> operation, EventType eventType, String message)
        {
            if (StepOperationEvent != null)
            {
                var args = new StepEventTrackedEventArgs<TInput>(eventType: eventType, message: message, handler: (StepOperation<TInput>)operation, context: _context);
                StepOperationEvent(this, args);                
            }
        }

        protected virtual IEnumerable<IOperation<TInput>> ObtenerHandlersParaElContexto(Context context)
        {
            return new IOperation<TInput>[] { };
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Clear();
            if (_rethrow != null)
                _rethrow.Clear();

            _context = null;
            _pipeline = null;
            _rethrow = null;
        }                        
    }
}
