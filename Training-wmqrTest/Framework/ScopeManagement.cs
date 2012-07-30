using Castle.ActiveRecord;

namespace Training_wmqrTest.Framework
{
    public static class ScopeManagement
    {
        public static SessionScope Scope;
        public static TransactionScope TransactionScope;

        public static void DisposeScope()
        {
            if (TransactionScope != null)
            {
                TransactionScope.Dispose();
                TransactionScope = null;
            }
            if (Scope != null)
            {
                Scope.Dispose();
                Scope = null;
            }
        }

        public static void CreateScope()
        {
            Scope = new SessionScope();
            TransactionScope = new TransactionScope(OnDispose.Commit);
        }

        public static void ResetScope()
        {
            DisposeScope();
            CreateScope();
        }
    }
}