using System;
namespace Newton.Factory
{
    public interface IMachine
    {
        Rate ActualSpeed { get; set; }
        string Name { get; set; }
        Rate PotentialSpeed { get; set; }
    }
}
