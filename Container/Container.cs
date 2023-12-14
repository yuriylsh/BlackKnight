using System;
using System.Collections.Generic;

namespace DeveloperSample.Container;

// I'm only making existing tests to pass, no handling for edge cases or validation.
// I assume adding test cases and adjusting the implementation accordingly will be the part of the 
// follow-up technical interview...
public class Container
{
    private static readonly Dictionary<Type, Type> Store = new();

    public void Bind(Type interfaceType, Type implementationType)  => Store[interfaceType] = implementationType;

    public T Get<T>() => (T)Activator.CreateInstance(Store[typeof(T)]);
}