using System;

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
        public SwallowLoad Load { get; protected set; }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public double GetAirspeedVelocity()
            => Load switch
            {
                SwallowLoad.None => BaseVelocity,
                SwallowLoad.Coconut => BaseVelocity - 4,
                _ => throw new ArgumentException()
            };
    }

    internal class AfricanSwallow : Swallow
    {
        public override SwallowType Type => SwallowType.African;
        protected override double BaseVelocity => 22;
    }

    internal class EuropeanSwallow : Swallow
    {
        public override SwallowType Type => SwallowType.European;
        protected override double BaseVelocity => 20;
    }
}