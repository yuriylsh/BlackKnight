using System;
using System.Collections.Generic;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => swallowType switch
        {
            SwallowType.African => new AfricanSwallow(),
            SwallowType.European => new EuropeanSwallow(),
            _ => throw new ArgumentException($"Unknown Swallow type {swallowType}", nameof(swallowType))
        };
    }

    public abstract class Swallow
    {
        public abstract SwallowType Type { get; }
        protected abstract double BaseVelocity { get; }
        public SwallowLoad Load { get; private set; }

        private static readonly IReadOnlyDictionary<SwallowLoad, double> VelocityReduction = new Dictionary<SwallowLoad, double>
        {
            [SwallowLoad.None] = 0d,
            [SwallowLoad.Coconut] = 4d
        };

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public double GetAirspeedVelocity() => VelocityReduction.TryGetValue(Load, out var reduction)
                ? BaseVelocity - reduction
                : throw new InvalidOperationException();
    }

    internal class AfricanSwallow : Swallow
    {
        public override SwallowType Type => SwallowType.African;
        protected override double BaseVelocity => 22d;
    }

    internal class EuropeanSwallow : Swallow
    {
        public override SwallowType Type => SwallowType.European;
        protected override double BaseVelocity => 20d;
    }
}