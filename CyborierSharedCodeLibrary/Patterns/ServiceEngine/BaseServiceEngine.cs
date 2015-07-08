using System;
using Cyborier.Shared.Patterns.ServiceEngine.Exceptions;

namespace Cyborier.Shared.Patterns.ServiceEngine
{
    /// <summary>
    /// Service Engine Interface.
    /// </summary>
    public abstract class BaseServiceEngine
    {
        public event EventHandler<StateChangedEventArgs> StateChanged;
        public event EventHandler<ProcessingFailedEventArgs> ProcessingFailed;

        protected virtual void OnProcessingFailed(ProcessingFailedEventArgs e)
        {
            EventHandler<ProcessingFailedEventArgs> handler = ProcessingFailed;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnStateChanged(EngineStates newState, Exception errorException = null, string message = null)
        {
            var oldState = CurrentState;

            CurrentState = newState;

            var e = new StateChangedEventArgs(oldState, newState, errorException, message);

            EventHandler<StateChangedEventArgs> handler = StateChanged;
            if (handler != null) handler(this, e);
        }

        public EngineStates CurrentState { get; set; }

        /// <summary>
        /// Start Service Engine.
        /// </summary>
        public void Start()
        {
            try
            {
                OnStateChanged(EngineStates.Starting);
                OnStart();
                OnStateChanged(EngineStates.Running);
            }
            catch (Exception ex)
            {
                OnStateChanged(EngineStates.Error);
                throw;
            }
        }

        protected abstract void OnStart();


        /// <summary>
        /// Stop Service Engine.
        /// </summary>
        public void Stop()
        {
            try
            {
                OnStateChanged(EngineStates.Stopping);
                OnStop();
                OnStateChanged(EngineStates.Stopped);
            }
            catch (Exception ex)
            {
                OnStateChanged(EngineStates.Error);
                throw;
            }
        }

        protected abstract void OnStop();

    }

    public class ProcessingFailedEventArgs : EventArgs
    {
        public ProcessingFailedException ProcessingFailedException { get; set; }
        public string Message { get; set; }

        public ProcessingFailedEventArgs(ProcessingFailedException ex, string message = null)
        {
            ProcessingFailedException = ex;
            message = message;
        }
    }

    public class StateChangedEventArgs : EventArgs
    {
        public EngineStates OldState { get; set; }
        public EngineStates NewState { get; set; }

        public String Message { get; set; }
        public Exception ErrorException { get; set; }

        public StateChangedEventArgs(EngineStates oldState, EngineStates newState, Exception errorException = null, string message = null)
            : base()
        {
            OldState = oldState;
            NewState = newState;
            ErrorException = errorException;
            Message = message;
        }
    }

    public enum EngineStates
    {
        /// <summary>
        /// Engine Is in Error State.
        /// </summary>
        Error,
        /// <summary>
        /// Engine is Starting.
        /// </summary>
        Starting,
        /// <summary>
        /// Engine is Started and Running.
        /// </summary>
        Running,
        /// <summary>
        /// Engine is Sopping
        /// </summary>
        Stopping,
        /// <summary>
        /// Engine is Stopped.
        /// </summary>
        Stopped
    }
}
