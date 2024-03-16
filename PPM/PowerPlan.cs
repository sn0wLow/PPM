using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM
{
    public class PowerPlan
    {
        public readonly Guid guid;
        private readonly string name;
        private bool isActive;

        public PowerPlan(Guid guid, string name, bool isActive)
        {
            this.guid = guid;
            this.name = name;
            this.isActive = isActive;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object? obj)
        {
            return (obj as PowerPlan)?.guid == guid;
        }

        public override int GetHashCode()
        {
            return this.guid.GetHashCode();
        }

        public Guid GUID => guid;
        public string Name => name;
        public bool IsActive { get { return isActive; } set { isActive = value; } }
    }
}
