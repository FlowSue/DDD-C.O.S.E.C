//系统包
using System;

namespace C.O.S.E.C.Infrastructure.Treasury.Snowflake
{
    public sealed class IdWorker
    {
        /// <summary>The _last timestamp</summary>
        private long _lastTimestamp = -1;
        /// <summary>The _lock</summary>
        private readonly object _lock = new object();
        /// <summary>The epoch</summary>
        private const long Twepoch = 1288834974657L;
        /// <summary>The worker identifier bits</summary>
        private const int WorkerIdBits = 5;
        /// <summary>The datacenter identifier bits</summary>
        private const int DatacenterIdBits = 5;
        /// <summary>The sequence bits</summary>
        private const int SequenceBits = 12;
        /// <summary>The maximum worker identifier</summary>
        private const long MaxWorkerId = 31L;
        /// <summary>The maximum datacenter identifier</summary>
        private const long MaxDatacenterId = 31L;
        /// <summary>The worker identifier shift</summary>
        private const int WorkerIdShift = 12;
        /// <summary>The datacenter identifier shift</summary>
        private const int DatacenterIdShift = 17;
        /// <summary>The timestamp left shift</summary>
        public const int TimestampLeftShift = 22;
        /// <summary>The sequence mask</summary>
        private const long SequenceMask = 4095L;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLC.Treasury.Snowflake.IdWorker" /> class.
        /// </summary>
        /// <param name="workerId">The worker identifier.</param>
        /// <param name="datacenterId">The datacenter identifier.</param>
        /// <param name="sequence">The sequence.</param>
        /// <exception cref="!:System.ArgumentException">
        /// </exception>
        public IdWorker(long workerId, long datacenterId, long sequence = 0)
        {
            this.WorkerId = workerId;
            this.DatacenterId = datacenterId;
            this.Sequence = sequence;
            if (workerId > MaxWorkerId || workerId < 0L)
                throw new ArgumentException($"worker Id can't be greater than {MaxWorkerId} or less than 0");
            if (datacenterId > MaxDatacenterId || datacenterId < 0L)
                throw new ArgumentException($"datacenter Id can't be greater than {MaxDatacenterId} or less than 0");
        }

        /// <summary>Gets or sets the worker identifier.</summary>
        /// <value>The worker identifier.</value>
        private long WorkerId { get; set; }

        /// <summary>Gets or sets the datacenter identifier.</summary>
        /// <value>The datacenter identifier.</value>
        private long DatacenterId { get; set; }

        /// <summary>Gets the sequence.</summary>
        /// <value>The sequence.</value>
        private long Sequence { get; set; }

        /// <summary>Next the identifier.</summary>
        /// <returns>System.Int64.</returns>
        /// <exception cref="T:NLC.Treasury.Snowflake.InvalidSystemClock"></exception>
        public long NextId()
        {
            lock (this._lock)
            {
                var num = TimeGen();
                if (num < this._lastTimestamp)
                    throw new InvalidSystemClockException($"Clock moved backwards.  Refusing to generate id for {this._lastTimestamp - num} milliseconds");
                if (this._lastTimestamp == num)
                {
                    this.Sequence = this.Sequence + 1L & SequenceMask;
                    if (this.Sequence == 0L)
                        num = TilNextMillis(this._lastTimestamp);
                }
                else
                    this.Sequence = 0L;
                this._lastTimestamp = num;
                return num - Twepoch << TimestampLeftShift | this.DatacenterId << DatacenterIdShift | this.WorkerId << WorkerIdShift | this.Sequence;
            }
        }

        /// <summary>Tils the next millis.</summary>
        /// <param name="lastTimestamp">The last timestamp.</param>
        /// <returns>System.Int64.</returns>
        private static long TilNextMillis(long lastTimestamp)
        {
            var num = TimeGen();
            while (num <= lastTimestamp)
                num = TimeGen();
            return num;
        }

        /// <summary>Times the gen.</summary>
        /// <returns>System.Int64.</returns>
        private static long TimeGen()
        {
            return SnowflakeHelper.CurrentTimeMillis();
        }
    }
}
