namespace ExamProject.Extensions
{
    public static class LoggerExtensions
    {
        public static void TaskCreated(this ILogger logger, string name, int id)
        {
            logger.LogInformation("Task {Name} ({Id}) created", name, id);
        }

        public static void TaskError(this ILogger logger, string action, Exception ex)
        {
            logger.LogError(ex, "Error on {Action}", action);
        }
    }

}
