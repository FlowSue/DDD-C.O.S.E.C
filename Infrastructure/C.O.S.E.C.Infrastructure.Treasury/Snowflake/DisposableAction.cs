//系统包
using System;

namespace C.O.S.E.C.Infrastructure.Treasury.Snowflake
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected virtual void Dispose(bool flag)
        {
            if (flag)
            {
                _action();
                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
