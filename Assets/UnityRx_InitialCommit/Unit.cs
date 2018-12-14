using System;

namespace UnityRx_InitialCommit
{
    // from Rx Official
    /// <summary>
    /// null
    /// </summary>
    [Serializable]
    public struct Unit : IEquatable<Unit>//一个用来表示空的结构
    {
        static readonly Unit @default = new Unit(); //有趣的符号 但是ide自动提示貌似不认...

        public static Unit Default { get { return @default; } }

        public static bool operator ==(Unit first, Unit second)
        {
            return true;
        }

        public static bool operator !=(Unit first, Unit second)
        {
            return false;
        }

        public bool Equals(Unit other)
        {
            return true;
        }
        public override bool Equals(object obj)
        {
            return obj is Unit;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "()";
        }
    }
}