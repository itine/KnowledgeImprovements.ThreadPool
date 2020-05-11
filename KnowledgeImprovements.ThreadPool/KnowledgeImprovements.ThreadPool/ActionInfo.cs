using System;

namespace KnowledgeImprovements.ThreadPool
{
    public class ActionInfo
    {
        public Guid Guid { get; set; }
        public Action Action { get; set; }
    }
}
