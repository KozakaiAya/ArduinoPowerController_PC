/*
   A C-program for MT19937-64 (2004/9/29 version).
   Coded by Takuji Nishimura and Makoto Matsumoto.

   This is a 64-bit version of Mersenne Twister pseudorandom number
   generator.

   Rewrite in C# by QSS for the Zhejiang University's Arduino Lesson
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoRemote
{
    public class mt64
    {
        const int NN = 312;
        const int MM = 156;
        const ulong MATRIX_A = 0xB5026F5AA96619E9UL;
        const ulong UM = 0xFFFFFFFF80000000UL; /* Most significant 33 bits */
        const ulong LM = 0x7FFFFFFFUL; /* Least significant 31 bits */


        /* The array for the state vector */
        static ulong[] mt = new ulong[NN];
        /* mti==NN+1 means mt[NN] is not initialized */
        static int mti = NN + 1;

        public static void init_genrand64(ulong seed)
        {
            mt[0] = seed;
            for (mti = 1; mti < NN; mti++)
                mt[mti] = (6364136223846793005UL * (mt[mti - 1] ^ (mt[mti - 1] >> 62)) + (ulong)mti);
        }

        /* initialize by an array with array-length */
        /* init_key is the array for initializing keys */
        /* key_length is its length */
        public static void init_by_array64(ulong[] init_key, ulong key_length)
        {
            ulong i, j, k;
            init_genrand64(19650218UL);
            i = 1;
            j = 0;
            k = (NN > key_length ? NN : key_length);
            for (; k != 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 62)) * 3935559000370003845UL)) + init_key[j] + j; /* non linear */
                i++;
                j++;
                if (i >= NN)
                {
                    mt[0] = mt[NN - 1];
                    i = 1;
                }
                if (j >= key_length)
                    j = 0;
            }
            for (k = NN - 1; k != 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 62)) * 2862933555777941757UL)) - i; /* non linear */
                i++;
                if (i >= NN)
                {
                    mt[0] = mt[NN - 1];
                    i = 1;
                }
            }
            mt[0] = 1UL << 63; /* MSB is 1; assuring non-zero initial array */
        }

        /* generates a random number on [0, 2^64-1]-interval */
        public static ulong genrand64_int64()
        {
            int i;
            ulong x;
            ulong[] mag01 = { 0UL, MATRIX_A };

            if (mti >= NN)
            { /* generate NN words at one time */

                /* if init_genrand64() has not been called, */
                /* a default initial seed is used     */
                if (mti == NN + 1)
                    init_genrand64(5489UL);

                for (i = 0; i < NN - MM; i++)
                {
                    x = (mt[i] & UM) | (mt[i + 1] & LM);
                    mt[i] = mt[i + MM] ^ (x >> 1) ^ mag01[(int)(x & 1UL)];
                }
                for (; i < NN - 1; i++)
                {
                    x = (mt[i] & UM) | (mt[i + 1] & LM);
                    mt[i] = mt[i + (MM - NN)] ^ (x >> 1) ^ mag01[(int)(x & 1UL)];
                }
                x = (mt[NN - 1] & UM) | (mt[0] & LM);
                mt[NN - 1] = mt[MM - 1] ^ (x >> 1) ^ mag01[(int)(x & 1UL)];

                mti = 0;
            }

            x = mt[mti++];

            x ^= (x >> 29) & 0x5555555555555555UL;
            x ^= (x << 17) & 0x71D67FFFEDA60000UL;
            x ^= (x << 37) & 0xFFF7EEE000000000UL;
            x ^= (x >> 43);

            return x;
        }


        /* generates a random number on [0, 2^63-1]-interval */
        public static long genrand64_int63()
        {
            return (long)(genrand64_int64() >> 1);
        }

        /* generates a random number on [0,1]-real-interval */
        public static double genrand64_real1()
        {
            return (genrand64_int64() >> 11) * (1.0 / 9007199254740991.0);
        }

        /* generates a random number on [0,1)-real-interval */
        public static double genrand64_real2()
        {
            return (genrand64_int64() >> 11) * (1.0 / 9007199254740992.0);
        }

        /* generates a random number on (0,1)-real-interval */
        public static double genrand64_real3()
        {
            return ((genrand64_int64() >> 12) + 0.5) * (1.0 / 4503599627370496.0);
        }
    }
}
