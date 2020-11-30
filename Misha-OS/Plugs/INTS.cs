using IL2CPU.API.Attribs;
using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Text;
using static Cosmos.Core.INTs;

namespace MishaOS.Plugs
{
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class INTs
    {
        public static void HandleException(uint aEIP, string aDescription, string aName, ref IRQContext ctx, uint lastKnownAddressValue = 0)
        {
            const string xHex = "0123456789ABCDEF";

            string ctxinterrupt = "";
            ctxinterrupt = ctxinterrupt + xHex[(int)((ctx.Interrupt >> 4) & 0xF)];
            ctxinterrupt = ctxinterrupt + xHex[(int)(ctx.Interrupt & 0xF)];

            string lastsknowaddress = "";

            if (lastKnownAddressValue != 0)
            {
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 28) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 24) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 20) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 16) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 12) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 8) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)((lastKnownAddressValue >> 4) & 0xF)];
                lastsknowaddress = lastsknowaddress + xHex[(int)(lastKnownAddressValue & 0xF)];
            }
            BlueScreen.Panic(BlueScreen.GetProperMessage(aName + " " + aDescription + " " + lastsknowaddress + " " + ctxinterrupt));
        }
    }
}
