namespace InterviewTask.Services;

public interface ISeedProvider
{
    long[] GetSeeds(string fileName, string seedHeader);
}