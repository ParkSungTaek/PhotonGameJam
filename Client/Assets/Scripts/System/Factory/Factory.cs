using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class BuffFactory
    {
        public static BuffBase InstanceBuff(BuffData buffName)
        { 
            switch(buffName._BuffName)
            {
                case BuffName.AddFixedValue: return new AddFixedValue(buffName);
                case BuffName.AddPerValue: return new AddPerValue(buffName);

            }
            return null;
        }


    }

}
