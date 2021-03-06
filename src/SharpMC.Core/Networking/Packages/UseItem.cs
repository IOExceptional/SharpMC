﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMC.Core.Enums;
using SharpMC.Core.Utils;

namespace SharpMC.Core.Networking.Packages
{
	public class UseItem : Package<UseItem>
	{
		public UseItem(ClientWrapper client) : base(client)
		{
			ReadId = 0x08;
		}

		public UseItem(ClientWrapper client, DataBuffer buffer) : base(client, buffer)
		{
			ReadId = 0x08;
		}

		public override void Read()
		{
			if (Buffer != null)
			{
				var hand = (byte)Buffer.ReadByte();
				var binhand = Client.Player.Inventory.GetItemInHand(hand);
				if (binhand != null)
				{
					if (binhand.IsUsable)
					{
						binhand.UseItem(Client.Player.Level, Client.Player, new Vector3(-1), BlockFace.PositiveY);
						if (Client.Player.Gamemode != Gamemode.Creative)
						{
							Client.Player.Inventory.RemoveItem((short) binhand.Id, binhand.Metadata, 1);
						}
					}
				}
			}
		}
	}
}
