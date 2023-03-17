using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Argis.CalanderSystem.Tests
{
    public interface ISystemClock
    {
        DateTime Now { get; }
    }

    public class SystemClock : ISystemClock
    {
        public DateTime Now
            => DateTime.Now;
    }

    public class FixedDateClock : ISystemClock
    {
        private readonly DateTime _when;

        public FixedDateClock(DateTime when)
        {
            _when = when;
        }

        public DateTime Now
            => _when;
    }

    public class CalenderUnitTests
    {
        private static readonly DateTime when = new DateTime(2021, 01, 01);
        private static readonly DateTime lastYear = when.AddYears(-1);
        private static readonly DateTime lastMonth = when.AddMonths(-1);

        [Test]
        public void DateTime_ToIso8601Format()
        {
            var clock = new FixedDateClock(when);
            Debug.Log(clock.Now);

            Assert.AreEqual(when, clock);
        }
    }
}