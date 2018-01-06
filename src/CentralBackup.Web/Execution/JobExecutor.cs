using CentralBackup.Core.Interfaces.Repositories;

namespace CentralBackup.Web.Execution
{
    public class JobExecutor
    {
        private readonly IJobRepository _jobRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly CommandFactory _commandFactory;

        public JobExecutor(
            IJobRepository jobRepository,
            IConfigurationRepository configurationRepository,
            CommandFactory commandFactory
        )
        {
            _jobRepository = jobRepository;
            _configurationRepository = configurationRepository;
            _commandFactory = commandFactory;
        }

        public void Execute(int jobId)
        {
            var job = _jobRepository.Load(jobId);

            foreach (var step in job.Steps)
            {
                var configuration = _configurationRepository.GetByStep(step);
                var command = _commandFactory.Create(step.CommandType);

                command.Execute(configuration);
            }
        }
    }
}