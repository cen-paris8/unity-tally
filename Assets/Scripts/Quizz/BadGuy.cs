using System;

public class BadGuy : IComparable<BadGuy>
{
    public string name;
    public int power;

    public BadGuy(string NewName, int NewPower)
    {
        name = NewName;
        power = NewPower;
    }

    public int CompareTo(BadGuy other)
    {
        if (other == null)
        {
            return 1;
        }
        return power - other.power;
    }
   
}
