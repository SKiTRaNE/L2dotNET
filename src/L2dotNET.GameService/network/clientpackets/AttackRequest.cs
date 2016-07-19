﻿using L2dotNET.GameService.Model.Player;
using L2dotNET.GameService.Network.Serverpackets;
using L2dotNET.GameService.World;
using L2dotNET.Network;

namespace L2dotNET.GameService.Network.Clientpackets
{
    class AttackRequest : PacketBase
    {
        private readonly GameClient _client;
        private readonly int _objectId;
        private readonly int _originX;
        private readonly int _originY;
        private readonly int _originZ;
        private readonly int _attackId;

        public AttackRequest(Packet packet, GameClient client)
        {
            _client = client;
            _objectId = packet.ReadInt();
            _originX = packet.ReadInt();
            _originY = packet.ReadInt();
            _originZ = packet.ReadInt();
            _attackId = packet.ReadByte(); // 0 for simple click   1 for shift-click
        }

        public override void RunImpl()
        {
            L2Player player = _client.CurrentPlayer;

            if (player.PBlockAct == 1)
            {
                player.SendActionFailed();
                return;
            }

            if (_objectId == player.ObjId)
            {
                player.SendSystemMessage(SystemMessage.SystemMessageId.CannotUseOnYourself);
                player.SendActionFailed();
                return;
            }

            L2Object obj = player.KnownObjects[_objectId];

            if (obj == null)
            {
                player.SendActionFailed();
                return;
            }

            //if (obj is L2Npc)
            //{
            //    if (((L2Npc)obj).Template._can_be_attacked == 0)
            //    {
            //        player.sendSystemMessage(144);//That is the incorrect target.
            //        player.sendActionFailed();
            //        return;
            //    }
            //}

            obj.OnForcedAttack(player);
        }
    }
}