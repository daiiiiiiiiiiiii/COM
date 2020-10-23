using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    void Start()
    {
        
    }
    class BaseStatus
    {
        private int _hp { get; }    // 体力
        private int _atk { get; }   // 攻撃力
        private int _def { get; }   // 防御力
        public BaseStatus(int hp,int atk,int def)
        {
            _hp = hp;
            _atk = atk;
            _def = def;
        }
    }
    class FighterStatus:BaseStatus
    {
        FighterStatus(int hp,int atk,int def):base(hp,atk,def)
        {
            // 処理なし
        }
    }
}
