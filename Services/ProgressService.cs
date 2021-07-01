using System.Collections.Generic;
using System.Linq;
using work_platform_backend.Models;

namespace work_platform_backend.Services 
{
    public class ProgressService 
    {
        public int CalculateTaskProgress(List<CheckPoint> checkpoints)
        {
            int checkpointProgressSum = 0 ;
            checkpointProgressSum += checkpoints.Select(c => c.Percentage).SingleOrDefault();

            return checkpointProgressSum / checkpoints.Count;
        }


        public int CalculateCheckpointProgress(CheckPoint checkpoint)
        {
            if(checkpoint.SubTasks.Count == 0)
            {
                // return checkpoint.SubTasks.Select(st => st.);
            }
            return 0;
        }
    }
}