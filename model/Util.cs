using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
namespace LKCamelot.model
{
    public static class Util
    {
        // System.Random은 스레드 안전하지 않다. 락 없이 여러 스레드가 쓰면 내부 상태가 깨져
        // 재시작 전까지 계속 0만 돌려준다. 모든 호출은 Next()를 거친다.
        private static readonly Random m_Random = new Random();

        private static int Next(int maxValue)
        {
            lock (m_Random)
                return m_Random.Next(maxValue);
        }

        public static int ToInt32(string value)
        {
            int i;

            if (value.StartsWith("0x"))
                int.TryParse(value.Substring(2), NumberStyles.HexNumber, null, out i);
            else
                int.TryParse(value, out i);

            return i;
        }

        public static int Dice(int numDice, int numSides, int bonus)
        {
            int total = 0;
            for (int i = 0; i < numDice; ++i)
                total += Random(numSides) + 1;
            total += bonus;
            return total;
        }

        public static bool RandomBool()
        {
            return (Next(2) == 0);
        }

        public static int RandomMinMax(int min, int max)
        {
            if (min > max)
            {
                int copy = min;
                min = max;
                max = copy;
            }
            else if (min == max)
            {
                return min;
            }

            return min + Next((max - min) + 1);
        }

        public static int Random(int from, int count)
        {
            if (count == 0)
            {
                return from;
            }
            else if (count > 0)
            {
                return from + Next(count);
            }
            else
            {
                return from - Next(-count);
            }
        }

        public static int Random(int count)
        {
            if (count == 0)
                return 0;
            return Next(count);
        }

        public static ushort checkUShort(uint value)
        {
            ushort ret = (ushort)value;
            if (value > ushort.MaxValue)
            {
                //ret = (ushort)(value / 1000);
                ret = ushort.MaxValue;
            }
            return ret;
        }

        public static bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;

            return input.ToCharArray().Any(c => c > MaxAnsiCode);
        }
    }
}
