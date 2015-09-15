using System.Threading.Tasks;

namespace Migrap.AspNet.Http {
    internal static class TaskDone {
        private readonly static Task<int> DoneConstant;

        public static Task Done { get { return TaskDone.DoneConstant; } }

        static TaskDone() {
            TaskDone.DoneConstant = Task.FromResult<int>(1);
        }
    }
}