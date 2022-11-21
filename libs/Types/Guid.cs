using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
namespace Karen.Types
{
    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
    public class Guid :IComparer<Guid>
    {
        static int next_guid = 1;
        public int Id { get; private set; }
        public Guid()
        {
            Id = next_guid;
            next_guid++;
        }
        public static Guid New()
        {
            return new();
        }
        public override bool Equals(object obj)
        {
            var q = (Guid)obj;
            return q.Id.Equals(Id);
        }
        public int Compare(Guid x, Guid y)
        {
            return x.Id - y.Id;
        }
        public override string ToString()
        {
            return Id.ToString();
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
