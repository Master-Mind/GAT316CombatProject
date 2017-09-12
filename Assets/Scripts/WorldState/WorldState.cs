using UnityEngine;
using System.Collections;
using System.Linq;

public static class WorldState
{
    private static GameObject Player = null;
    private static int _capacity = 12;
    private static int [] _idsNextToPlayer = new int[8];
    static WorldState()
    {
        
    }

    public static void SetPlayer(GameObject player)
    {
        Player = player;
    }

    public static int QueryAttackPosition(RoomData roomDat)
    {
        int lastIndex = 0;
        for(int i = 0; i < _idsNextToPlayer.Length; ++i)
        {
            if (_idsNextToPlayer[i] == roomDat.Id)
            {
                return i;
            }
            else if(_idsNextToPlayer[i] == 0)
            {
                lastIndex = i;
            }
        }

        if (roomDat.weight < _capacity)
        {
            _capacity -= roomDat.weight;
            _idsNextToPlayer[lastIndex] = roomDat.Id;

            return lastIndex;
        }

        return -1;
    }
}
