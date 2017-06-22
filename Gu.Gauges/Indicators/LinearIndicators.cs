﻿namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    using Gu.Gauges.Helpers;

    [ContentProperty("Items")]
    public class LinearIndicators : FrameworkElement
    {
        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
nameof(Gauge),
            typeof(LinearGauge),
            typeof(LinearIndicators),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

#pragma warning disable SA1202 // Elements must be ordered by access
        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access
#pragma warning disable SA1401 // Fields must be private
        internal readonly LinearPanel Panel;
#pragma warning restore SA1401 // Fields must be private

        static LinearIndicators()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearIndicators), new FrameworkPropertyMetadata(typeof(LinearIndicators)));
        }

        public LinearIndicators()
        {
            this.Panel = new LinearPanel();
            this.AddVisualChild(this.Panel);
            this.AddLogicalChild(this.Panel);
        }

        public UIElementCollection Items => this.Panel.Children;

        public LinearGauge Gauge
        {
            get => (LinearGauge)this.GetValue(GaugeProperty);
            internal set => this.SetValue(GaugePropertyKey, value);
        }

        protected override int VisualChildrenCount => 1;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.Minimum, this.Panel, LinearPanel.MinimumProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.Maximum, this.Panel, LinearPanel.MaximumProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.IsDirectionReversed, this.Panel, LinearPanel.IsDirectionReversedProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.Placement, this.Panel, LinearPanel.PlacementProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.ReservedSpace, this.Panel, LinearPanel.ReservedSpaceProperty);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            var gauge = this.VisualAncestors()
                .OfType<LinearGauge>()
                .FirstOrDefault();
            this.OnGaugeChanged(gauge);
            base.OnVisualParentChanged(oldParent);
            base.OnVisualParentChanged(oldParent);
        }

        protected override Visual GetVisualChild(int index)
        {
            return index == 0 ? this.Panel : null;
        }

        /// <summary>
        ///     Default control measurement is to measure only the first visual child.
        ///     This child would have been created by the inflation of the
        ///     visual tree from the control's style.
        ///
        ///     Derived controls may want to override this behavior.
        /// </summary>
        /// <param name="constraint">The measurement constraints.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            this.Panel.Measure(constraint);
            return this.Panel.DesiredSize;
        }

        /// <summary>
        ///     Default control arrangement is to only arrange
        ///     the first visual child. No transforms will be applied.
        /// </summary>
        /// <param name="arrangeBounds">The computed size.</param>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.Panel.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        private void OnGaugeChanged(LinearGauge newGauge)
        {
            BindingOperations.ClearAllBindings(this.Panel);
            this.Gauge = newGauge;
        }
    }
}
