using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uint8 = System.Byte;
using Uint16 = System.UInt16;
using Uint32 = System.UInt32;
using Uint64 = System.UInt64;

namespace ArduinoRemote
{
    public class calc
    {
        public static Uint64 addm(Uint64 a, Uint64 b, Uint64 c)
        {
            Uint64 al = a & 0xFFFFFFFFuL;
            Uint64 ah = a >> 32;
            Uint64 bl = b & 0xFFFFFFFFuL;
            Uint64 bh = b >> 32;
            Uint64 lowSum = al + bl;
            Uint64 lowCarry = lowSum >> 32;
            lowSum = lowSum & 0xFFFFFFFFuL;
            Uint64 highSum = lowCarry + ah + bh;
            Uint64 highCarry = highSum >> 32;
            highSum = highSum & 0xFFFFFFFFuL;
            Uint64 sum = lowSum + (highSum << 32);
            if (highCarry != 0)
            {
                Uint64 lowBit = sum & 1uL;
                sum = ((sum >> 1) | 0x8000000000000000uL) % c;
                Uint64 c2 = c >> 1;
                if (sum > c2) sum = ((sum - c2 - 1) << 1) + (c & 1);
                else sum <<= 1;
                sum += lowBit;
            }
            return sum;
        }

        public static Uint64 shiftlm(Uint64 a, Uint64 c)
        {
            a = a % c;
            if ((a & 0x8000000000000000uL) != 0)
            {
                Uint64 c2 = c >> 1;
                if (a > c2) return ((a - c2 - 1) << 1) + (c & 1);
                else return a << 1;
            }
            else
            {
                return a << 1;
            }
        }

        public static Uint64 mulm(Uint64 a, Uint64 b, Uint64 c)
        {
            Uint64 p = 0;
            Uint64 n = 1;
            while (b != 0)
            {
                if ((b & 1) != 0) p = addm(p, a, c);
                a = shiftlm(a, c);
                b >>= 1;
            }
            return p;
        }

        public Uint64 log2(Uint64 a)
        {
            Uint64 r = 0;
            if ((a & 0x2uL) != 0)
            {
                a >>= 1;
                r |= 1;
            }
            if ((a & 0xCuL) != 0)
            {
                a >>= 2;
                r |= 2;
            }
            if ((a & 0xF0uL) != 0)
            {
                a >>= 4;
                r |= 4;
            }
            if ((a & 0xFF00uL) != 0)
            {
                a >>= 8;
                r |= 8;
            }
            if ((a & 0xFFFF0000uL) != 0)
            {
                a >>= 16;
                r |= 16;
            }
            if ((a & 0xFFFFFFFF00000000uL) != 0)
            {
                a >>= 32;
                r |= 32;
            }
            return r;
        }

        public static Uint64 powm(Uint64 b, Uint64 a, Uint64 m)
        {
            Uint64 result = 1;
            while (a!=0)
            {
                if ((a & 1)!=0) result = mulm(result, b, m);
                b = mulm(b, b, m);
                a >>= 1;
            }
            return result;
        }
    }
}
