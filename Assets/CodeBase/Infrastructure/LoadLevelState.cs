namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            sceneLoader.Load(sceneName);
        }

        public void Exit()
        {
        }
    }
}