using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Common
{
    public static class QueueHelper
    {
        private readonly static ConcurrentQueue<long> articelAcountQueue = new ConcurrentQueue<long>();

        public static void QueueEnque(long articelId)
        {
            articelAcountQueue.Enqueue(articelId);
        }
        public static bool QueueDequeue(out long articelId)
        {
            return articelAcountQueue.TryDequeue(out articelId);
        }
    }
}
