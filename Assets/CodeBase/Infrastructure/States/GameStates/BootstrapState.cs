using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.ServersProcessing;

namespace CodeBase.Infrastructure.States.GameStates
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IServerApiService _serverApiService;

        public BootstrapState(IGameStateMachine stateMachine, 
            IServerApiService serverApiService)
        {
            _serverApiService = serverApiService;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _serverApiService.Init();
            
            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        public void Exit()
        {
            
        }
    }
}