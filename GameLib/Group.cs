using SFML.Graphics;
using SFML.Window;
using System.Collections;
using System.Collections.Generic;

namespace GameLib
{
    public class Group : BlankTransformableEntity, IEnumerable<Entity>
    {
        private List<Entity> entities = new List<Entity>();
        public override Transform GlobalTransform { 
            get { // เฉพาะ Group ที่อนุญาต กรณีไม่มี parent ก็ยังมี GlobalTransform ได้
                Transform parentTransform;
                if (this.Parent == null)
                    parentTransform = Transform.Identity;
                else
                    parentTransform = this.Parent.GlobalTransform;

                return parentTransform * this.Transform; 
            } 
        }

        public int Count { get { return entities.Count; } }
        public Entity this[int i]
        {
            get { return entities[i]; }
            set { entities[i] = value; }
        }
        public void Add(Entity entity)
        {
            entities.Add(entity);
            entity._NotifyParentAdded(this);
        }
        public void Remove(Entity entity)
        {
            entities.Remove(entity);
            entity._NotifyParentRemoved();
        }
        public void Clear()
        {
            for(int i=entities.Count-1; i>=0; i--)
                Remove(entities[i]);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= this.Transform;
            for (int i = 0; i < entities.Count; i++)
                entities[i].Draw(target, states);
        }
        public override void FrameUpdate(float deltaTime)
        {
            for (int i = 0; i < entities.Count; i++)
                entities[i].FrameUpdate(deltaTime);
        }

        public override void PhysicsUpdate(float fixTime)
        {
            for (int i = 0; i < entities.Count; i++)
                entities[i].PhysicsUpdate(fixTime);
        }

        public override void KeyPressed(KeyEventArgs e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].KeyPressed(e);
        }

        public override void KeyReleased(KeyEventArgs e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].KeyReleased(e);
        }

        public override void MouseButtonPressed(MouseButtonArguments e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].MouseButtonPressed(e);
        }

        public override void MouseButtonReleased(MouseButtonArguments e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].MouseButtonReleased(e);
        }

        public override void MouseMoved(MouseMoveArguments e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].MouseMoved(e);
        }

        public override void MouseWheelScrolled(MouseWheelScrollArguments e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].MouseWheelScrolled(e);
        }

        public override void TextEntered(TextEventArgs e)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].TextEntered(e);
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }
    }
}
