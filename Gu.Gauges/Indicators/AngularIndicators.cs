﻿namespace Gu.Gauges
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class AngularIndicators : Indicators<AngularGauge>
    {
        private static readonly DependencyPropertyKey ItemsPropertyKey = DependencyProperty.RegisterReadOnly(
            "Items",
            typeof(ObservableCollection<AngularIndicator>),
            typeof(AngularIndicators),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ItemsProperty = ItemsPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
            "Gauge",
            typeof(AngularGauge),
            typeof(AngularIndicators),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;

        static AngularIndicators()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularIndicators), new FrameworkPropertyMetadata(typeof(AngularIndicators)));
        }

        public AngularIndicators()
        {
            this.Items = new ObservableCollection<AngularIndicator>();
        }

        public ObservableCollection<AngularIndicator> Items
        {
            get { return (ObservableCollection<AngularIndicator>)this.GetValue(ItemsProperty); }
            protected set { this.SetValue(ItemsPropertyKey, value); }
        }

        public AngularGauge Gauge
        {
            get { return (AngularGauge)this.GetValue(GaugeProperty); }
            protected set { this.SetValue(GaugePropertyKey, value); }
        }

        protected override void OnGaugeChanged(AngularGauge newValue)
        {
            this.Gauge = (AngularGauge)newValue;
        }
    }
}