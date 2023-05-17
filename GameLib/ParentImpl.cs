using SFML.Graphics;
using System.Diagnostics;

namespace GameLib
{
    public struct ParentImpl
    {
        public Entity? Parent { get; private set; }
        public void _NotifyParentAdded(Entity parent)
        {
            Debug.Assert(Parent == null, "Double add parent.");
            this.Parent = parent;
        }
        public void _NotifyParentRemoved()
        {
            Debug.Assert(Parent != null, "No parent to remove.");
            this.Parent = null;
        }
        public void Detach(Entity entity)
        {
            if (Parent == null)
                return;

            Group? group = Parent as Group;
            if (group == null)
                return;

            group.Remove(entity);
        }

        public Transform CalcGlobalTransform(Transform localTransform) { 
            if(Parent == null)
            {
                Debug.Assert(false, "CalcGlobalTransform() : need parent");
                return Transform.Identity;
            }

            return Parent.GlobalTransform * localTransform;
        }
    }
}
