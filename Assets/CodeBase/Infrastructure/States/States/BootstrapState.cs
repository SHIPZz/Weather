using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.ServersProcessing;

namespace CodeBase.Infrastructure.States.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IServerApiService _serverApiService;

        public BootstrapState(IStateMachine stateMachine, 
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