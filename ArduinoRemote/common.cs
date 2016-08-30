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
    public class common
    {
        public const Uint32 MESSAGE_SIZE = 10;

        public const Uint8 REPEAT = 0;
        public const Uint8 EXCHANGE_KEY = 1;
        public const Uint8 MESSAGE = 2;
        public const Uint8 WAIT = 3;

        public const Uint32 HOLD_TIME_SHORT = 300;
        public const Uint32 HOLD_TIME_LONG = 10000;
        public const Uint32 WAIT_INTERVAL = 1000;

        // The commands.  Keep these private.
        public const Uint64 CHECK_STATUS = 0x9053870b774f8abcuL;
        public const Uint64 PRESS_SHORT = 0x8cd38d4705b6e7d3uL;
        public const Uint64 PRESS_LONG = 0x8478c2b6d2e8dc4cuL;
        public const Uint64 STATUS_ON = 0xba0cd692655ba3d2uL;
        public const Uint64 STATUS_OFF = 0x8df6c823ce1e3610uL;
        public const Uint64 PRESS_RESTART = 0x3278c2bad2e8cd4cuL;

        // The modulus (an unsigned 64 bit prime).
        public const Uint64 p = 0xca8532ff1b7e4881uL;
        // The generator (an unsigned 64 bit integer less than p).
        public const Uint64 g = 0x89e94f1b04822985uL;

    }
}
