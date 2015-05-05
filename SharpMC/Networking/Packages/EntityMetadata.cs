﻿// Distrubuted under the MIT license
// ===================================================
// SharpMC uses the permissive MIT license.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the “Software”), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// ©Copyright Kenny van Vulpen - 2015
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMC.Enums;
using SharpMC.Utils;

namespace SharpMC.Networking.Packages
{
	public class EntityMetadata : Package<EntityMetadata>
	{
		public int EntityId = 0;
		public ObjectType type;
		public object data;

		public EntityMetadata(ClientWrapper client) : base(client)
		{
			SendId = 0x1c;
		}

		public EntityMetadata(ClientWrapper client, MSGBuffer buffer) : base(client, buffer)
		{
			SendId = 0x1c;
		}

		public override void Write()
		{
			if (Buffer != null)
			{
				Buffer.WriteVarInt(SendId);
				Buffer.WriteVarInt(EntityId);
				if (type == ObjectType.ItemStack)
				{
					var item = (ItemStack) data;
					Buffer.WriteByte((5 << 5 | 10 & 0x1F) & 0xFF);
					Buffer.WriteShort((short) (item.ItemId != 0 ? item.ItemId : 1));
					Buffer.WriteByte(1);
					Buffer.WriteShort(item.MetaData);
					Buffer.WriteByte(0); //nbt shit starting
					Buffer.WriteByte(127);
				}
				Buffer.FlushData();
			}
		}
	}
}