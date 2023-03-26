using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static Argis.CalenderSystem.Runtime.DateTimeExtensions;

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
        private static readonly DateTime nextYear = when.AddYears(1);
        private static readonly DateTime nextMonth = when.AddMonths(1);
        private static readonly DateTime nextWeek = when.AddDays(7);

        //[Test]
        //public void DateTime_ToIso8601Format()
        //{
        //    var clock = new FixedDateClock(when);
        //    Debug.Log(clock.Now);

        //    Assert.AreEqual(when, clock);
        //}

        [TestCaseSource(nameof(GetDateTimeInputs))]
        public void DateTime_ToIso8601Format(DateTime dateTime, string expected)
        {
            // Actual
            var actual = dateTime.ToIso8601Format();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> GetDateTimeInputs()
        {
            yield return new TestCaseData(new DateTime(2023, 1, 1), "");
            yield return new TestCaseData(-1, -2, -3);
            yield return new TestCaseData(0, 0, 0);
        }
    }
}