﻿using L2dotNET.Network;

namespace L2dotNET.GameService.Network.Serverpackets
{
    class ExBuySellListClose : GameserverPacket
    {
        public override void Write()
        {
            WriteByte(0xFE);
            WriteShort(0xB7);
            WriteInt(1);
            WriteShort(0);
            WriteShort(0);
            WriteByte(1);
        }
    }
}