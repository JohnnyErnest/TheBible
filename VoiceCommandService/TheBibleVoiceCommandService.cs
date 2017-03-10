using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace VoiceCommandService
{
    /// <summary>
    /// This command service is the entry point of background processing
    /// voice commands that integrate into Cortana. You'll define it in
    /// TheBible project under the app manifest.
    /// </summary>
    public sealed class TheBibleVoiceCommandService : IBackgroundTask
    {
        /// <summary>
        /// Cortana Related:
        /// </summary>
        VoiceCommandServiceConnection voiceServiceConnection;

        /// <summary>
        /// Cortana Related:
        /// </summary>
        BackgroundTaskDeferral backgroundDeferral;

        /// <summary>
        /// Implementation:
        /// The IBackgroundTask interface's Run method that executes when the service begins.
        /// </summary>
        /// <param name="taskInstance"></param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            throw new NotImplementedException();
        }
    }
}
