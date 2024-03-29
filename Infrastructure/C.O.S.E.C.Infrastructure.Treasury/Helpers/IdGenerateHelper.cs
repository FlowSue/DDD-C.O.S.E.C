﻿//本地项目包

using C.O.S.E.C.Infrastructure.Treasury.Snowflake;

namespace C.O.S.E.C.Infrastructure.Treasury.Helpers
{
    public static class IdGenerateHelper
    {
        private static readonly IdWorker Worker = new IdWorker(1L, 1L, 0L);

        public static long NewId => IdGenerateHelper.Worker.NextId();
    }
}
