using System;

namespace ConsoleRenderer
{
    public struct Pixel : IEquatable<Pixel>
    {
        public char Character;
        public TerminalColor Foreground;
        public TerminalColor Background;

        public Pixel(char character, TerminalColor foreground, TerminalColor background)
        {
            Character = character;
            Foreground = foreground;
            Background = background;
        }

        public static bool operator ==(Pixel p1, Pixel p2)
        {
            return p1.Character == p2.Character &&
                   p1.Foreground == p2.Foreground &&
                   p1.Background == p2.Background;
        }

        public static bool operator !=(Pixel p1, Pixel p2)
        {
            return p1.Character != p2.Character ||
                   p1.Foreground != p2.Foreground ||
                   p1.Background != p2.Background;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Pixel pixel)
                return pixel == this;

            return false;
        }

        public bool Equals(Pixel other)
        {
            return Character == other.Character &&
                   Foreground.Equals(other.Foreground) &&
                   Background.Equals(other.Background);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Character, Foreground, Background);
        }
    }
}

public readonly struct TerminalColor : IEquatable<TerminalColor>
{
    public readonly ConsoleColor StandardColor;
    public readonly byte R, G, B;
    public readonly bool IsRgb;

    public TerminalColor(ConsoleColor color)
    {
        StandardColor = color;
        IsRgb = false;
        R = G = B = 0;
    }

    public TerminalColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        IsRgb = true;
        StandardColor = ConsoleColor.Black;
    }

    public bool Equals(TerminalColor other)
    {
        if (IsRgb != other.IsRgb) return false;
        return IsRgb
            ? (R == other.R && G == other.G && B == other.B)
            : StandardColor == other.StandardColor;
    }

    public override bool Equals(object? obj)
    {
        return obj is TerminalColor other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)StandardColor, R, G, B, IsRgb);
    }

    public static bool operator ==(TerminalColor left, TerminalColor right) => left.Equals(right);
    public static bool operator !=(TerminalColor left, TerminalColor right) => !(left == right);
}