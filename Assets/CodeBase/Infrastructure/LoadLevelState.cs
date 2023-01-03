namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            sceneLoader.Load("GameScene");
        }

        public void Exit()
        {
        }
    }
}