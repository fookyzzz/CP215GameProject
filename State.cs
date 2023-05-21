namespace GameProject
{
    public class State
    {
        public GameState state { get; set; }

        public State(GameState state)
        {
            this.state = state;
        }
    }
}
