using NUnit.Framework;
using SurveyPortal.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Unit
{
    [TestFixture]
    public class CopyTests
    {
        public class Source
        {
            private string Prop0;
            public string Prop1;
            public string Prop2;
            public int Prop3 { get; set; }
        }

        public class Target
        {
            public static string Prop1 { get; set; }
            public string Prop2 { get; }
            public bool Prop3 { get; set; }
        }

        
        [Test]
        public void ShouldThrowIfSourceIsMissing()
        {
            var target = new Target();

            Assert.Throws<ArgumentNullException>(() => PropertyCopy.Copy<Source, Target>(null, target));
        }

        [Test]
        public void ShouldThrowIfTargetIsStatic()
        {
            var source = new Source
            {
                Prop1 = "val",
            };

            var target = new Target();

            Assert.Throws<ArgumentException>(() => PropertyCopy.Copy(source, target));
        }

        [Test]
        public void ShouldThrowIfTargetHasNoSetter()
        {
            var source = new Source
            {
                Prop2 = "val",
            };

            var target = new Target();

            Assert.Throws<ArgumentException>(() => PropertyCopy.Copy(source, target));
        }

        [Test]
        public void ShouldThrowIfTargetHasTypeMismatch()
        {
            var source = new Source
            {
                Prop3 = 5,
            };

            var target = new Target();

            Assert.Throws<ArgumentException>(() => PropertyCopy.Copy(source, target));
        }
    }
}
