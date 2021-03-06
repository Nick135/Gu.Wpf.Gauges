namespace Gu.Wpf.Gauges.Tests.Internals
{
    using System.Windows;
    using NUnit.Framework;

    public class InterpolationTests
    {
        [TestCase(0, 0, 10, false, 0)]
        [TestCase(0, 0, 10, true, 10)]
        [TestCase(0.5, 0, 10, false, 5)]
        [TestCase(0.5, 0, 10, true, 5)]
        [TestCase(1, 0, 10, false, 10)]
        [TestCase(1, 0, 10, true, 0)]
        public void Interpolate(double value, double min, double max, bool isDirectionReversed, double expected)
        {
            var interpolation = new Interpolation(value);
            Assert.AreEqual(expected, interpolation.Interpolate(min, max, isDirectionReversed));
        }

        [TestCase(0, 10, null, false, 10)]
        [TestCase(0, 10, null, true, 0)]
        [TestCase(0.5, 10, null, false, 5)]
        [TestCase(0.5, 10, null, true, 5)]
        [TestCase(1, 10, null, false, 0)]
        [TestCase(1, 10, null, true, 10)]
        [TestCase(0, 10, "0 1 0 2", false, 8)]
        [TestCase(0, 10, "0 1 0 2", true, 1)]
        [TestCase(0.5, 10, "0 1 0 2", false, 4.5)]
        [TestCase(0.5, 10, "0 1 0 2", true, 4.5)]
        [TestCase(1, 10, "0 1 0 2", false, 1)]
        [TestCase(1, 10, "0 1 0 2", true, 8)]
        public void InterpolateVertical(double value, double height, string padding, bool isDirectionReversed, double expected)
        {
            var interpolation = new Interpolation(value);
            var thickness = padding.AsThickness();
            Assert.AreEqual(expected, interpolation.InterpolateVertical(new Size(0, height), thickness, isDirectionReversed));
        }

        [TestCase(0, "0,0 1 -90 90", false, "-1, 0")]
        [TestCase(0.5, "0,0 1 -90 90", false, "0, -1")]
        [TestCase(1, "0,0 1 -90 90", false, "1, 0")]
        [TestCase(0, "0,0 1 -90 90", true, "1, 0")]
        [TestCase(0.5, "0,0 1 -90 90", true, "0, -1")]
        [TestCase(1, "0,0 1 -90 90", true, "-1, 0")]
        public void InterpolateArc(double value, string arcString, bool isDirectionReversed, string expected)
        {
            var interpolation = new Interpolation(value);
            var arc = ArcInfo.Parse(arcString);
            Assert.AreEqual(expected, interpolation.Interpolate(arc, isDirectionReversed).ToString("F0"));
        }
    }
}