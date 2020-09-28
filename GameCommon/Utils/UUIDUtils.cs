namespace GameCommon.Utils
{
    public class UUIDUtils
    {
        public static string GetUUID()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
