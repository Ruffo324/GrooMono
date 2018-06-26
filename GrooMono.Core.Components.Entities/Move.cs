namespace GrooMono.Core.Components.Entities
{
    public class Move
    {
        private readonly Entity _entity;

        public Move(Entity entity)
        {
            _entity = entity;
        }

        public void Right()
        {
            _entity.Movement.X = _entity.MovementSpeed;
        }

        public void Left()
        {
            _entity.Movement.X = _entity.MovementSpeed * -1;
        }

        public void Up()
        {
            _entity.Movement.Y = _entity.MovementSpeed * -1;
        }

        public void StopX()
        {
            _entity.Movement.X = 0;
        }

        public void StopY()
        {
            _entity.Movement.Y = 0;
        }
    }
}